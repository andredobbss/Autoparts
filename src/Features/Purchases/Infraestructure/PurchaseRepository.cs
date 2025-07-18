using Autoparts.Api.Features.Purchases.Domain;
using Autoparts.Api.Features.Purchases.Dto;
using Autoparts.Api.Infraestructure.Persistence;
using BenchmarkDotNet.Attributes;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Z.PagedList;

namespace Autoparts.Api.Features.Purchases.Infraestructure;

public class PurchaseRepository : IPurchaseRepository, IDisposable
{
    private readonly AutopartsDbContext _context;

    private readonly IValidator<Purchase> _validator;

    public PurchaseRepository(AutopartsDbContext context, IValidator<Purchase> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<IPagedList<Purchase>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        return await _context.Purchases.AsNoTracking().Include(p => p.PurchaseProducts).ThenInclude(p => p.Product).ToPagedListAsync(pageNumber, pageSize, cancellationToken);
    }

    [Benchmark]
    public async Task<IEnumerable<PurchaseProductCategoryManufaturerDto>> GetPurchaseProductCategoryManufaturerAsync(CancellationToken cancellationToken)
    {

        var result = await _context
                           .Database
                           .SqlQuery<PurchaseProductCategoryManufaturerDto>($@"SELECT
                                                                                	PU.[InvoiceNumber],
                                                                                    PP.[Quantity],
                                                                                	PU.[TotalPurchase],
                                                                                	PU.[PaymentMethod],
                                                                                	PR.[Name],
                                                                                	PR.[TechnicalDescription],
                                                                                	PR.[SKU],
                                                                                	PR.[Compatibility],
                                                                                	PR.[SellingPrice],
                                                                                	PR.[Stock],
                                                                                	PR.[StockStatus],
                                                                                	CA.[Description] AS CategoryDescription,
                                                                                	MA.[Description] AS ManufacturerDescription,
                                                                                    SU.[CompanyName]
                                                                                FROM [dbo].[Purchases] AS PU
                                                                                	INNER JOIN [dbo].[PurchaseProducts] AS PP
                                                                                ON PU.PurchaseId = PP.PurchaseId
                                                                                	INNER JOIN [dbo].[Products] AS PR
                                                                                ON PP.ProductId = PR.ProductId
                                                                                	INNER JOIN [dbo].[Categories] AS CA
                                                                                ON PR.CategoryId = CA.CategoryId
                                                                                	INNER JOIN [dbo].[Manufacturers] AS MA
                                                                                ON PR.ManufacturerId = MA.ManufacturerId
                                                                                    INNER JOIN [dbo].[Suppliers] AS SU
                                                                                ON PU.SupplierId = SU.SupplierId")
                           .ToListAsync(cancellationToken);


       

        var query = await _context.Purchases
    .Join(_context.PurchaseProducts, pu => pu.PurchaseId, pp => pp.PurchaseId, (pu, pp) => new { pu, pp })
    .Join(_context.Products, temp => temp.pp.ProductId, pr => pr.ProductId, (temp, pr) => new { temp.pu, temp.pp, pr })
    .Join(_context.Categories, temp => temp.pr.CategoryId, ca => ca.CategoryId, (temp, ca) => new { temp.pu, temp.pp, temp.pr, ca })
    .Join(_context.Manufacturers, temp => temp.pr.ManufacturerId, ma => ma.ManufacturerId, (temp, ma) => new { temp.pu, temp.pp, temp.pr, temp.ca, ma })
    .Join(_context.Suppliers, temp => temp.pu.SupplierId, su => su.SupplierId, (temp, su) => new { temp.pu, temp.pp, temp.pr, temp.ca, temp.ma, su })
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

        //return result;
        return query;
    }

    public async Task<Purchase?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Purchases.AsNoTracking().Include(p => p.PurchaseProducts).ThenInclude(p => p.Product).SingleOrDefaultAsync(p => p.PurchaseId == id, cancellationToken);
    }

    public async Task<ValidationResult> AddAsync(Purchase purchase, CancellationToken cancellationToken)
    {
        var result = _validator.Validate(purchase);

        if (result.IsValid is false)
            return result;

        await _context.Purchases.AddAsync(purchase, cancellationToken);
        return result;
    }

    public async Task<ValidationResult> UpdateAsync(Purchase purchase, CancellationToken cancellationToken)
    {
        var result = _validator.Validate(purchase);

        if (result.IsValid is false)
            return result;
      
        _context.Purchases.Update(purchase).State = EntityState.Modified;
        return result;
    }

    public async Task<bool> DeleteAsync(Purchase purchase, CancellationToken cancellationToken)
    {
        var result = _context.Purchases.Remove(purchase).State = EntityState.Deleted;

        if (result is not EntityState.Deleted)
            return false;

        return true;
    }

    public async Task<bool> Commit(CancellationToken cancellationToken)
    {
        var commitResult = await _context.SaveChangesAsync(cancellationToken);

        if (commitResult <= 0)
            return false;

        return true;
    }

    public void Dispose()
    {
        _context.Dispose();
    }


}
