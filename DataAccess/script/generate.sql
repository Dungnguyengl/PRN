CREATE TABLE [MEMBER]
(
    MemberId INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    Email NVARCHAR(100),
    CompanyName NVARCHAR(40),
    City NVARCHAR(15),
    Country NVARCHAR(15),
    Password NVARCHAR(30)
)

CREATE TABLE [ORDER](
    OrderId INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    MemberId INT NOT NULL FOREIGN KEY REFERENCES [MEMBER](MemberId),
    OrderDate DATETIME,
    RequiredDate DATETIME,
    ShippedDate DATETIME,
    Freight MONEY
)

CREATE TABLE [PRODUCT](
    ProductId INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    CategoryId INT NOT NULL,
    ProductName NVARCHAR(40),
    Weight NVARCHAR(20),
    UnitPrice MONEY,
    UnitInStock INT
)

CREATE TABLE [ORDER_DETAIL](
    OrderId INT NOT NULL FOREIGN KEY REFERENCES [ORDER](OrderId),
    ProductId INT NOT NULL FOREIGN KEY REFERENCES [PRODUCT](ProductId),
    UnitPrice MONEY,
    Quantity INT,
    Discount FLOAT

    CONSTRAINT PK_ORDER_DETAIL PRIMARY KEY (OrderId, ProductId)
)
GO

CREATE OR ALTER VIEW [GET_ORDER] AS
SELECT o.OrderId, m.Email, m.MemberId, o.OrderDate, o.RequiredDate, o.ShippedDate, o.Freight
FROM [ORDER] AS o
LEFT JOIN [MEMBER] AS m ON m.Membe  rId = o.MemberId
GO

CREATE OR ALTER VIEW [GET_ORDER_DETAIL] AS
SELECT od.OrderId, p.ProductId, p.ProductName, od.UnitPrice, od.Quantity, od.Discount
FROM ORDER_DETAIL AS od
LEFT JOIN [PRODUCT] AS p ON p.ProductId = od.ProductId
GO
