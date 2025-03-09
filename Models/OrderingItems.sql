 -- Copied from Ian Vincent
create table OrderHistories(
                               OrderNo int NOT NULL Primary Key,
                               Email nvarchar(50) NOT NULL,
                               FOREIGN KEY (Email) REFERENCES CheckoutCustomers(Email)
);
create table OrderItems(
                           OrderNo int NOT NULL,
                           StockID int NOT NULL,
                           Quantity int NOT NULL,
                           CONSTRAINT PK_Items PRIMARY KEY (OrderNo,StockID),
                           FOREIGN KEY (OrderNo) REFERENCES OrderHistories(OrderNo),
                           FOREIGN KEY (StockID) REFERENCES MenuItem(Id)
);