UPDATE Products
SET Stock = StockMovements.Stock
FROM (
    SELECT 
        ProductId,
        SUM(Quantity) AS Stock
    FROM (
        SELECT ProductId, Quantity FROM PurchaseProducts
        UNION ALL
        SELECT ProductId, Quantity FROM ReturnProducts WHERE Loss = 0
        UNION ALL
        SELECT ProductId, -Quantity AS Quantity FROM SaleProducts
    ) AS Movements
    GROUP BY ProductId
) AS StockMovements
WHERE Products.ProductId = StockMovements.ProductId
