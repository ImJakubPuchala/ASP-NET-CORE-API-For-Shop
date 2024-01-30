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
