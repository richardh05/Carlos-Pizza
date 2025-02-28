-- Copied from Ian Vincent
create table CheckoutCustomers(
                                  Email varChar(20) Primary Key NOT NULL,
                                  Name varchar(50),
                                  BasketID int

);

create table Baskets(
                        BasketID int Primary Key NOT NULL,
);

create table BasketItems(
                            StockID int NOT NULL,
                            BasketID int NOT NULL,
                            Quantity int
);

ALTER TABLE BasketItems
    ADD CONSTRAINT PK_StockBasket PRIMARY KEY (StockID,BasketID);

-- ALTER TABLE BasketItems
--     ADD FOREIGN KEY (StockID) REFERENCES Stationaries(ID);

ALTER TABLE BasketItems
    ADD FOREIGN KEY (BasketID) REFERENCES Baskets(BasketID);

