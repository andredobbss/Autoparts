namespace Autoparts.Api.Shared.Products.Dto;
public sealed record ProductDto(Guid ProductId,
                                string Name,
                                string TechnicalDescription,
                                string SKU,
                                string Compatibility,
                                decimal AcquisitionCost,
                                decimal SellingPrice);
