using Autoparts.Api.Features.Purchases.Domain;
using Autoparts.Api.Features.Purchases.Dto;
using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Products.Stock;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Z.PagedList;

namespace Autoparts.Api.Features.Purchases.Infraestructure;

public class PurchaseRepository : IPurchaseRepository, IDisposable
{
    private readonly AutopartsDbContext _context;

    private readonly IStockCalculator _stockCalculator;

    private readonly IValidator<Purchase> _validator;

    public PurchaseRepository(AutopartsDbContext context, IValidator<Purchase> validator, IStockCalculator stockCalculator)
    {
        _context = context;
        _validator = validator;
        _stockCalculator = stockCalculator;
    }

    public async Task<IPagedList<Purchase>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        return await _context.Purchases!.AsNoTracking()
                                        .Include(p => p.PurchaseProducts)
                                        .ThenInclude(p => p.Product)
                                        .Include(p => p.Products)
                                        .ThenInclude(p => p.Manufacturer)
                                        .ToPagedListAsync(pageNumber, pageSize, cancellationToken);
    }

    public async Task<IEnumerable<PurchaseProductCategoryManufaturerDto>> GetPurchaseProductCategoryManufaturerAsync(CancellationToken cancellationToken)
    {
        var query = await _context.Purchases!
                   .Join(_context.PurchaseProducts!, pu => pu.PurchaseId, pp => pp.PurchaseId, (pu, pp) => new { pu, pp })
                   .Join(_context.Products!, temp => temp.pp.ProductId, pr => pr.ProductId, (temp, pr) => new { temp.pu, temp.pp, pr })
                   .Join(_context.Categories!, temp => temp.pr.CategoryId, ca => ca.CategoryId, (temp, ca) => new { temp.pu, temp.pp, temp.pr, ca })
                   .Join(_context.Manufacturers!, temp => temp.pr.ManufacturerId, ma => ma.ManufacturerId, (temp, ma) => new { temp.pu, temp.pp, temp.pr, temp.ca, ma })
                   .Join(_context.Suppliers!, temp => temp.pu.SupplierId, su => su.SupplierId, (temp, su) => new { temp.pu, temp.pp, temp.pr, temp.ca, temp.ma, su })
                   .Select(result => new PurchaseProductCategoryManufaturerDto(
                                     result.pu.InvoiceNumber,
                                     result.pp.Quantity,
                                     result.pu.TotalPurchase,
                                     result.pu.PaymentMethod,
                                     result.pr.Name,
                                     result.pr.TechnicalDescription,
                                     result.pr.SKU,
                                     result.pr.Compatibility,
                                     result.pr.SellingPrice,
                                     result.pr.Stock,
                                     result.pr.StockStatus,
                                     result.ca.Description,
                                     result.ma.Description,
                                     result.su.CompanyName
                                 )).ToListAsync(cancellationToken);
        return query;
    }

    public async Task<Purchase?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Purchases!.AsNoTracking().Include(p => p.PurchaseProducts).ThenInclude(p => p.Product).SingleOrDefaultAsync(p => p.PurchaseId == id, cancellationToken);
    }

    public async Task<ValidationResult> AddAsync(Purchase purchase, CancellationToken cancellationToken)
    {
        var result = _validator.Validate(purchase);
        if (!result.IsValid)
            return result;

        await _context.Purchases!.AddAsync(purchase, cancellationToken);
        return result;
    }

    public async Task<ValidationResult> UpdateAsync(Purchase purchase, CancellationToken cancellationToken)
    {
        var result = _validator.Validate(purchase);
        if (!result.IsValid)
            return result;

        _context.Purchases!.Update(purchase).State = EntityState.Modified;
        return result;
    }

    public async Task<bool> DeleteAsync(Purchase purchase, CancellationToken cancellationToken)
    {
        var result = _context.Purchases!.Update(purchase);
        if (result is null)
            return false;

        return true;
    }

    public async Task<bool> CommitAsync(CancellationToken cancellationToken)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var commitResult = await _context.SaveChangesAsync(cancellationToken);

            bool stockUpdated = await _stockCalculator.StockCalculateAsync(cancellationToken);

            if (commitResult > 0 && stockUpdated)
            {
                await transaction.CommitAsync(cancellationToken);
                return true;
            }

            await transaction.RollbackAsync(cancellationToken);
            return false;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            return false;
        }
    }

    public void Dispose()
    {
        _context.Dispose();
    }


}
