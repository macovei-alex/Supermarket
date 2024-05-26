USE Supermarket;

INSERT INTO dbo.UserType (name) VALUES ('administrator');
INSERT INTO dbo.UserType (name) VALUES ('cashier');

INSERT INTO dbo.Country (name) VALUES ('United States');
INSERT INTO dbo.Country (name) VALUES ('Romania');
INSERT INTO dbo.Country (name) VALUES ('China');
INSERT INTO dbo.Country (name) VALUES ('Germany');
INSERT INTO dbo.Country (name) VALUES ('France');
INSERT INTO dbo.Country (name) VALUES ('India');

INSERT INTO Category (name) VALUES ('food');
INSERT INTO Category (name) VALUES ('drink');
INSERT INTO Category (name) VALUES ('clothes');

INSERT INTO Producer (name, id_country) VALUES ('Producer1.SRL', 1);
INSERT INTO Producer (name, id_country) VALUES ('Producer2.SRL', 2);
INSERT INTO Producer (name, id_country) VALUES ('Producer3.SRL', 3);
INSERT INTO Producer (name, id_country) VALUES ('Producer4.SRL', 4);

INSERT INTO Product (name, barcode, id_category, id_producer) VALUES ('product 1', '12345', 1, 1);
INSERT INTO Product (name, barcode, id_category, id_producer) VALUES ('product 2', '23456', 2, 1);
INSERT INTO Product (name, barcode, id_category, id_producer) VALUES ('product 3', '34567', 1, 3);
INSERT INTO Product (name, barcode, id_category, id_producer) VALUES ('product 4', '45678', 2, 4);
INSERT INTO Product (name, barcode, id_category, id_producer) VALUES ('product 5', '56789', 3, 4);

INSERT INTO Stock (id_product, quantity, initial_quantity, unit, supply_date, expiration_date, purchase_price,selling_price) VALUES (6, 20, 25, 'kg', GETDATE(), GETDATE(), 10.00, 14.99);
INSERT INTO Stock (id_product, quantity, initial_quantity, unit, supply_date, expiration_date, purchase_price,selling_price) VALUES (7, 100, 100, 'piece', GETDATE(), GETDATE(), 2.49, 5.00);
INSERT INTO Stock (id_product, quantity, initial_quantity, unit, supply_date, expiration_date, purchase_price,selling_price) VALUES (8, 6, 10, 'mg', GETDATE(), GETDATE(), 6.49, 7.99);

INSERT INTO Receipt (issue_date, id_cashier, total_price) VALUES (GETDATE(), 1, 72.32);
INSERT INTO Receipt (issue_date, id_cashier, total_price) VALUES ('2024-05-24', 1, 24.33);
INSERT INTO Receipt (issue_date, id_cashier, total_price) VALUES ('2024-05-22', 1, 99.67);
INSERT INTO Receipt (issue_date, id_cashier, total_price) VALUES ('2024-04-20', 1, 5.33);
INSERT INTO Receipt (issue_date, id_cashier, total_price) VALUES ('2024-05-23', 2, 57.62);
INSERT INTO Receipt (issue_date, id_cashier, total_price) VALUES ('2024-05-21', 2, 95.98);
INSERT INTO Receipt (issue_date, id_cashier, total_price) VALUES ('2024-05-21', 2, 17.18);
