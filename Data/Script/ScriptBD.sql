USE StoreSample
GO

--------------------------------------------------------------------
--> Create Views for queries
--------------------------------------------------------------------

--------------------------------------------------------------------
--> Query 1
--------------------------------------------------------------------
IF EXISTS (SELECT * FROM sys.views WHERE name = 'ViewNextOrderPrediction' AND schema_id = SCHEMA_ID('Sales'))
BEGIN
    DROP VIEW Sales.ViewNextOrderPrediction;
END;
GO

CREATE VIEW Sales.ViewNextOrderPrediction
	WITH SCHEMABINDING
AS
WITH LastOrder AS (
    -- Get the date of the last order for each client
    SELECT 
        SO.custid,
        MAX(SO.OrderDate) AS LastOrderDate
    FROM 
        Sales.Orders SO
    GROUP BY 
        SO.custid 
),
OrderAverage AS (
    SELECT 
        SO.custid,
        DATEDIFF(DAY, LAG(SO.OrderDate) OVER (PARTITION BY SO.custid ORDER BY SO.OrderDate), SO.OrderDate) AS DaysBetweenOrders
    FROM Sales.Orders SO
)
SELECT 
	SC.custid AS CustId,
	SC.companyname AS CompanyName,
	LO.LastOrderDate,
	-- AVG(OA.DaysBetweenOrders) AS Average, Test
    DATEADD(DAY, AVG(OA.DaysBetweenOrders), LO.LastOrderDate) AS NextPredictedOrder
FROM 
    OrderAverage OA
	INNER JOIN Sales.Customers SC ON OA.custid = SC.custid
	INNER JOIN LastOrder LO ON LO.custid = OA.custid
WHERE 
    OA.DaysBetweenOrders IS NOT NULL
	-- AND SC.companyname = 'Customer AHPOP' test
GROUP BY 
	SC.custid, SC.companyname, LO.LastOrderDate;
GO

--------------------------------------------------------------------
--> Query 2 - Get Client Orders
--------------------------------------------------------------------
IF EXISTS (SELECT * FROM sys.views WHERE name = 'ViewGetClientOrders' AND schema_id = SCHEMA_ID('Sales'))
BEGIN
    DROP VIEW Sales.ViewGetClientOrders;
END;
GO

CREATE VIEW Sales.ViewGetClientOrders
	WITH SCHEMABINDING
AS
SELECT Orderid, Requireddate, Shippeddate, Shipname, Shipaddress, Shipcity FROM Sales.Orders;
GO

--------------------------------------------------------------------
--> Query 3 - Get employees
--------------------------------------------------------------------
IF EXISTS (SELECT * FROM sys.views WHERE name = 'ViewGetEmployees' AND schema_id = SCHEMA_ID('HR'))
BEGIN
    DROP VIEW HR.ViewGetEmployees;
END;
GO

CREATE VIEW HR.ViewGetEmployees
	WITH SCHEMABINDING
AS
SELECT Empid, CONCAT(firstname, ' ', lastname) AS FullName FROM HR.Employees;
GO

--------------------------------------------------------------------
--> Query 4 - Get Shippers
--------------------------------------------------------------------
IF EXISTS (SELECT * FROM sys.views WHERE name = 'ViewGetShippers' AND schema_id = SCHEMA_ID('Sales'))
BEGIN
    DROP VIEW Sales.ViewGetShippers;
END;
GO

CREATE VIEW Sales.ViewGetShippers
	WITH SCHEMABINDING
AS
SELECT Shipperid, Companyname FROM Sales.Shippers;
GO

--------------------------------------------------------------------
--> Query 5 - Get Products
--------------------------------------------------------------------
IF EXISTS (SELECT * FROM sys.views WHERE name = 'ViewGetProducts' AND schema_id = SCHEMA_ID('Production'))
BEGIN
    DROP VIEW Production.ViewGetProducts;
END;
GO

CREATE VIEW Production.ViewGetProducts
	WITH SCHEMABINDING
AS
SELECT Productid, Productname FROM Production.Products;
GO

--------------------------------------------------------------------
--> Query 6 - Add New Order
--------------------------------------------------------------------
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.sp_CreateOrderWithDetails') AND type = N'P')
BEGIN
    -- Eliminar el procedimiento almacenado si existe
    DROP PROCEDURE dbo.sp_CreateOrderWithDetails;
END;
GO

CREATE PROCEDURE sp_CreateOrderWithDetails
    @CustId INT,
    @EmpId INT,
    @ShipperId INT,
    @ShipName NVARCHAR(40),
    @ShipAddress NVARCHAR(60),
    @ShipCity NVARCHAR(15),
    @OrderDate DATETIME,
    @RequiredDate DATETIME,
    @Shippeddate DATETIME,
    @ShipCountry NVARCHAR(15),
    @Freight MONEY,
    @ProductId INT,
    @UnitPrice MONEY,
    @Qty SMALLINT,
    @Discount NUMERIC(4,3),
    @NewOrderID INT OUTPUT
AS
BEGIN    
    BEGIN TRY
        BEGIN TRANSACTION

        -- Insert new order
        INSERT INTO Sales.Orders (
            custid, empid, orderdate, requireddate, shippeddate, shipperid, Freight,
            shipname, shipaddress, shipcity, ShipCountry
        )
        VALUES (
            @CustId, @EmpId, @OrderDate, @RequiredDate, @Shippeddate, @ShipperID, @Freight,
            @ShipName, @ShipAddress, @ShipCity, @ShipCountry
        );

        SET @NewOrderID = (SELECT TOP 1 orderId FROM Sales.Orders ORDER BY orderId DESC);

        -- Insert order datails
        INSERT INTO Sales.OrderDetails (
            OrderID, ProductId, UnitPrice, Qty, Discount
        )
        VALUES (
            @NewOrderID, @ProductId, @UnitPrice, @Qty, @Discount
        );

        COMMIT TRANSACTION
        
        -- Return the Id of the new order
        SELECT @NewOrderID AS NewOrderID;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION; 

        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT @ErrorMessage = ERROR_MESSAGE(),
               @ErrorSeverity = ERROR_SEVERITY(),
               @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END
