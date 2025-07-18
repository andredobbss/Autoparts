UPDATE Products
SET Stock = StockMovements.Stock
FROM (
    SELECT 
        ProductId,
        SUM(Quantity) AS Stock
    FROM (
        SELECT ProductId, Quantity FROM PurchaseProducts
        UNION ALL
        SELECT ProductId, Quantity FROM ReturnProducts
        UNION ALL
        SELECT ProductId, -Quantity AS Quantity FROM SaleProducts
    ) AS Movements
    GROUP BY ProductId
) AS StockMovements
WHERE Products.ProductId = StockMovements.ProductId AND StockMovements.ProductId IN (SELECT ProductId FROM ReturnProducts WHERE loss = true);
