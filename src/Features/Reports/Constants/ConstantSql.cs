namespace Autoparts.Api.Features.Reports.Constants;

public static class ConstantSql
{

    public const string SalesReportSqlWithParameters = @"SELECT 
                                                          	p.[Name],
                                                          	p.[SellingPrice],
                                                          	p.[AcquisitionCost],
                                                          	(p.[SellingPrice] - P.[AcquisitionCost]) AS Margin,
                                                          	p.[Stock],
                                                          	s.[InvoiceNumber],
                                                          	S.[PaymentMethod],
                                                          	sp.[Quantity],
                                                          	sp.[TotalItem],
                                                          	s.[TotalSale],
                                                          	s.[CreatedAt]
                                                          FROM
                                                          	[dbo].[Products] AS p
                                                          		LEFT JOIN SaleProducts AS sp ON p.ProductId = sp.ProductId
                                                          		LEFT JOIN Sales as s on sp.SaleId = s.SaleId
                                                          WHERE
                                                          	s.[CreatedAt] BETWEEN @startDate AND @endDate
                                                          ORDER BY 
                                                          	s.[CreatedAt], 
                                                          	s.[InvoiceNumber]";

    public const string SalesReportSql = @"SELECT 
                                           	p.[Name],
                                           	p.[SellingPrice],
                                           	p.[AcquisitionCost],
                                           	(p.[SellingPrice] - P.[AcquisitionCost]) AS Margin,
                                           	p.[Stock],
                                           	s.[InvoiceNumber],
                                           	S.[PaymentMethod],
                                           	sp.[Quantity],
                                           	sp.[TotalItem],
                                           	s.[TotalSale],
                                           	s.[CreatedAt]
                                           FROM
                                           	[dbo].[Products] AS p
                                           		LEFT JOIN SaleProducts AS sp ON p.ProductId = sp.ProductId
                                           		LEFT JOIN Sales as s on sp.SaleId = s.SaleId;";
}                                                         
