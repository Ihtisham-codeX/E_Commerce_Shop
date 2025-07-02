CREATE DATABASE IF NOT EXISTS ECommerceStore;
USE ECommerceStore;
CREATE TABLE Users (
    UserID INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(50) NOT NULL UNIQUE,
    Email VARCHAR(50) NOT NULL UNIQUE,
    Password VARCHAR(100) NOT NULL,
    Role ENUM('Customer', 'Merchant', 'Admin') NOT NULL
);
Create Table Shop_Categories(
	ShopCategoryID INT auto_increment primary key,
    CategoryName VARCHAR(50) NOT NULL UNIQUE
);
Create Table Shops(
	ShopID INT auto_increment primary key,
    ShopName VARCHAR(50) NOT NULL UNIQUE,
    MerchantID INT NOT NULL,
    ShopType VARCHAR(50) NOT NULL ,
     FOREIGN KEY (MerchantID) REFERENCES Users(UserID)
        ON DELETE CASCADE ON UPDATE CASCADE
);
-- the following table is for the sub categories for the products that lye in each shop
Create Table Product_Categories(
	Product_Category_ID INT auto_increment primary key,
    CategoryName VARCHAR(50) NOT NULL UNIQUE,
    Shop_Category_ID INT NOT NULL,
    ShopID INT NOT NULL,
     FOREIGN KEY (Shop_Category_ID) REFERENCES Shop_Categories(ShopCategoryID)
        ON DELETE CASCADE ON UPDATE CASCADE,
	 FOREIGN KEY (ShopID) REFERENCES Shops(ShopID)
        ON DELETE CASCADE ON UPDATE CASCADE
);
CREATE TABLE Products (
    ProductID INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Price DECIMAL(10,2) NOT NULL CHECK (Price >= 0),
    Quantity INT NOT NULL CHECK (Quantity >= 0),
    ImagePath VARCHAR(255) NOT NULL,
    CategoryID INT NOT NULL,
    MerchantID INT NOT NULL,
    FOREIGN KEY (MerchantID) REFERENCES Users(UserID)
        ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY (CategoryID) REFERENCES Product_Categories(Product_Category_ID)
        ON DELETE CASCADE ON UPDATE CASCADE
);
CREATE TABLE Carts (
    CartID INT AUTO_INCREMENT PRIMARY KEY,
    CustomerID INT NOT NULL,
    FOREIGN KEY (CustomerID) REFERENCES Users(UserID)
        ON DELETE CASCADE ON UPDATE CASCADE
);
CREATE TABLE CartItems (
    CartItemID INT AUTO_INCREMENT PRIMARY KEY,
    CartID INT NOT NULL,
    ProductID INT NOT NULL,
    Quantity INT NOT NULL CHECK (Quantity > 0),
    FOREIGN KEY (CartID) REFERENCES Carts(CartID)
        ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
        ON DELETE CASCADE ON UPDATE CASCADE
);
CREATE TABLE Orders (
    OrderID INT AUTO_INCREMENT PRIMARY KEY,
    CustomerID INT NOT NULL,
    OrderDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (CustomerID) REFERENCES Users(UserID)
        ON DELETE CASCADE ON UPDATE CASCADE
);
CREATE TABLE OrderItems (
    OrderItemID INT AUTO_INCREMENT PRIMARY KEY,
    OrderID INT NOT NULL,
    ProductID INT NOT NULL,
    Quantity INT NOT NULL CHECK (Quantity > 0),
    Price DECIMAL(10,2) NOT NULL CHECK (Price >= 0),
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID)
        ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
        ON DELETE CASCADE ON UPDATE CASCADE
);




    