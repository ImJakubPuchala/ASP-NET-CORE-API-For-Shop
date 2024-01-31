CREATE TABLE Products
(
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100),
    EANCode NVARCHAR(30)
);

CREATE TABLE Prices
(
    PriceID INT IDENTITY(1,1) PRIMARY KEY,
    ProductID INT NULL,
    Price DECIMAL(10, 2),
    StartDate DATE NULL,
    EndDate DATE NULL,
    CONSTRAINT FK_Prices_Products FOREIGN KEY (ProductID) REFERENCES Products (ProductID)
);

CREATE TABLE Sales (
    SaleId INT IDENTITY PRIMARY KEY,
    ProductId INT NOT NULL,
    QuantitySold INT NOT NULL,
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);

CREATE TABLE Reviews (
    ReviewId INT IDENTITY PRIMARY KEY,
    ProductId INT NOT NULL,
    Rating FLOAT NOT NULL,
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);

CREATE TABLE Warehouse (
    WarehouseId INT IDENTITY PRIMARY KEY,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    WarehouseNumber NVARCHAR(50) NOT NULL,
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);