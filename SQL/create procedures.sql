USE Supermarket;
GO

CREATE PROCEDURE GetAllUsers
AS
BEGIN
	SELECT id, name, password_hash, id_type
	FROM MarketUser;
END;
GO

CREATE PROCEDURE GetActiveUsers
AS
BEGIN
	SELECT id, name, password_hash, id_type
	FROM MarketUser
	WHERE is_active = 1;
END;
GO

CREATE PROCEDURE FindUser
	@Name NVARCHAR(64),
	@PasswordHash CHAR(64),
	@UserType INT
AS
BEGIN
	SELECT MarketUser.id, MarketUser.name, UserType.name
	FROM MarketUser
	JOIN UserType ON UserType.id = MarketUser.id_type
	WHERE MarketUser.is_active = 1
		AND MarketUser.name = @Name
		AND MarketUser.password_hash = @PasswordHash
		AND MarketUser.id_type = @UserType
	ORDER BY MarketUser.id;
END;
GO

CREATE PROCEDURE GetUserID
	@Name NVARCHAR(64)
AS
BEGIN
	SELECT MarketUser.id
	FROM MarketUser
	WHERE is_active = 1
		AND name = @Name;
END;
GO

CREATE PROCEDURE CreateUser
	@Name NVARCHAR(64),
	@PasswordHash CHAR(64),
	@UserType INT
AS
BEGIN
	IF NOT EXISTS (SELECT 1 FROM MarketUser WHERE is_active = 1 AND name = @Name)
	BEGIN
		
		INSERT INTO MarketUser (name, password_hash, id_type)
		VALUES (@Name, @PasswordHash, @UserType);
		
		SELECT 'Success';
		
	END
	
	ELSE BEGIN SELECT 'There already is a user with this username'; END
END;
GO

CREATE PROCEDURE DeleteUser
	@ID INT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM MarketUser WHERE is_active = 1 AND id = @id)
	BEGIN
	
		UPDATE MarketUser
		SET is_active = 0
		WHERE is_active = 1
			AND id = @ID;
		
		SELECT 'Success';
		
	END
	
	ELSE BEGIN SELECT 'There is no user with this ID'; END
END;
GO

CREATE PROCEDURE DeleteUserByName
	@Name NVARCHAR(64)
AS
BEGIN
	IF EXISTS (SELECT 1 FROM MarketUser WHERE is_active = 1 AND name = @Name)
	BEGIN
	
		UPDATE MarketUser
		SET is_active = 0
		WHERE is_active = 1
			AND name = @Name;
			
		SELECT 'Success';
		
	END
	
	ELSE BEGIN SELECT 'There is no user with this username'; END
END;
GO

CREATE PROCEDURE EditUser
	@ID INT,
	@Name NVARCHAR(64),
	@OldPasswordHash CHAR(64),
	@NewPasswordHash CHAR(64),
	@UserType INT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM MarketUser WHERE is_active = 1 AND password_hash = @OldPasswordHash AND id = @ID)
	BEGIN
	
		BEGIN TRANSACTION;
			
		UPDATE MarketUser
		SET name = @Name, password_hash = @NewPasswordHash, id_type = @UserType
		WHERE is_active = 1
			AND id = @ID
			AND password_hash = @OldPasswordHash;
			
		IF (SELECT COUNT(id) FROM MarketUser WHERE is_active = 1 AND name = @Name) > 1
		BEGIN
		
			ROLLBACK TRANSACTION;
			SELECT 'There already is a category with this name';
			
		END
		
		ELSE
		BEGIN
		
			COMMIT TRANSACTION;
			SELECT 'Success';
			
		END
		
	END
	
	ELSE BEGIN SELECT 'There is no user with these credentials'; END
END;
GO

CREATE PROCEDURE GetAllUserTypes
AS
BEGIN
	SELECT id, name
	FROM UserType
	WHERE is_active = 1;
END;
GO

CREATE PROCEDURE GetAllCountries
AS
BEGIN
	SELECT id, name
	FROM Country
	WHERE is_active = 1;
END;
GO

CREATE PROCEDURE CreateCountry
	@Name NVARCHAR(64)
AS
BEGIN 
	IF NOT EXISTS (SELECT 1 FROM Country WHERE is_active = 1 AND name = @Name)
	BEGIN
	
		INSERT INTO Country (name)
		VALUES (@Name);
		
		SELECT 'Success';
		
	END
	
	ELSE BEGIN SELECT 'There already is a country with this name'; END
END;
GO

CREATE PROCEDURE DeleteCountry
	@ID INT
AS
BEGIN 
	IF EXISTS (SELECT 1 FROM Country WHERE is_active = 1 AND id = @ID)
	BEGIN
	
		IF NOT EXISTS (SELECT 1 FROM Producer WHERE id_country = @ID)
		BEGIN
		
			UPDATE Country
			SET is_active = 0
			WHERE is_active = 1
				AND id = @ID;
		
			SELECT 'Success';
			
		END
		
		ELSE BEGIN SELECT 'There is at least 1 active producer from this country'; END
		
	END
	
	ELSE BEGIN SELECT 'There is no country with this ID'; END
END;
GO

CREATE PROCEDURE EditCountry
	@ID INT,
	@Name NVARCHAR(64)
AS
BEGIN
	IF NOT EXISTS (SELECT 1 FROM Country WHERE is_active = 1 AND name = @Name)
	BEGIN
	
		BEGIN TRANSACTION;
			
		UPDATE Country
		SET name = @Name
		WHERE is_active = 1
			AND id = @ID;
			
		IF (SELECT COUNT(id) FROM Country WHERE is_active = 1 AND name = @Name) > 1
		BEGIN
		
			ROLLBACK TRANSACTION;
			SELECT 'There already is a country with this name';
			
		END
	
		ELSE
		BEGIN
		
			COMMIT TRANSACTION;
			SELECT 'Success';
			
		END
	END
	
	ELSE BEGIN SELECT 'There already is a country with this name'; END
END;
GO

CREATE PROCEDURE GetAllCategories
AS
BEGIN
	SELECT id, name
	FROM Category
	WHERE is_active = 1;
END;
GO

CREATE PROCEDURE CreateCategory
	@Name NVARCHAR(64)
AS
BEGIN
	IF NOT EXISTS (SELECT 1 FROM Category WHERE is_active = 1 AND name = @Name)
	BEGIN
	
		INSERT INTO Category (name)
		VALUES (@Name);
		
		SELECT 'Success';
		
	END
	
	ELSE BEGIN SELECT 'There already is a category with this name'; END
END;
GO

CREATE PROCEDURE DeleteCategory
	@ID INT
AS
BEGIN 
	IF EXISTS (SELECT 1 FROM Category WHERE is_active = 1 AND id = @ID)
	BEGIN
	
		IF NOT EXISTS (SELECT 1 FROM Product WHERE id_category = @ID)
		BEGIN
		
			UPDATE Category
			SET is_active = 0
			WHERE is_active = 1
				AND id = @ID;
		
			SELECT 'Success';
			
		END
		
		ELSE BEGIN SELECT 'There is at least 1 active product in this category'; END
		
	END
	
	ELSE BEGIN SELECT 'There is no category with this ID'; END
END;
GO

CREATE PROCEDURE EditCategory
	@ID INT,
	@Name NVARCHAR(64)
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Category WHERE is_active = 1 AND id = @ID)
	BEGIN
	
		BEGIN TRANSACTION;
		
		UPDATE Category
		SET name = @Name
		WHERE is_active = 1
			AND id = @ID;
			
		IF (SELECT COUNT(id) FROM Category WHERE is_active = 1 AND name = @Name) > 1
		BEGIN
		
			ROLLBACK TRANSACTION;
			SELECT 'There already is a category with this name';
			
		END
		
		ELSE
		BEGIN
		
			SELECT 'Success';
			COMMIT TRANSACTION;
			
		END
		
	END
	
	ELSE BEGIN SELECT 'There is no category with this ID'; END
END;
GO

CREATE PROCEDURE GetAllProducers
AS
BEGIN
	SELECT id, id_country, name
	FROM Producer;
END;
GO

CREATE PROCEDURE GetActiveProducers
AS
BEGIN
	SELECT id, id_country, name
	FROM Producer
	WHERE is_active = 1;
END;
GO

CREATE PROCEDURE CreateProducer
	@Name NVARCHAR(64),
	@CountryID INT
AS
BEGIN
	IF NOT EXISTS (SELECT 1 FROM Producer WHERE is_active = 1 AND name = @Name)
	BEGIN
	
		INSERT INTO Producer (name, id_country)
		VALUES (@Name, @CountryID);
		
		SELECT 'Success';
		
	END
		
	ELSE BEGIN SELECT 'There already is a producer with this name'; END
END;
GO

CREATE PROCEDURE EditProducer
	@ID INT,
	@Name NVARCHAR(64),
	@CountryID INT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Producer WHERE is_active = 1 AND id = @ID)
	BEGIN
	
		BEGIN TRANSACTION;
		
		UPDATE Producer 
		SET name = @Name, id_country = @CountryID
		WHERE is_active = 1
			AND id = @ID;
			
		IF (SELECT COUNT(id) FROM Producer WHERE is_active = 1 AND name = @Name) > 1
		BEGIN
		
			ROLLBACK TRANSACTION;
			SELECT 'There already is a producer with this name';	
			
		END
		
		ELSE
		BEGIN
		
			SELECT 'Success';
			COMMIT TRANSACTION;
			
		END		
		
	END
	
	ELSE BEGIN SELECT 'There is no producer with this ID'; END
END;
GO

CREATE PROCEDURE DeleteProducer
	@ID INT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Producer WHERE is_active = 1 AND id = @ID)
	BEGIN
	
		IF NOT EXISTS (SELECT 1 FROM Product WHERE is_active = 1 AND id_producer = @ID)
		BEGIN
		
			UPDATE Producer
			SET is_active = 0
			WHERE is_active = 1
				AND id = @ID;
				
			SELECT 'Success';
			
		END
		
		ELSE BEGIN SELECT 'There is an active stock provided by this producer'; END
		
	END
	
	ELSE BEGIN SELECT 'There is no active producer with this ID'; END
END;
GO

CREATE PROCEDURE GetAllProducts
AS
BEGIN
	SELECT id, name, barcode, id_category, id_producer
	FROM Product;
END;
GO

CREATE PROCEDURE GetActiveProducts
AS
BEGIN
	SELECT id, name, barcode, id_category, id_producer
	FROM Product
	WHERE is_active = 1;
END;
GO

CREATE PROCEDURE CreateProduct
	@Name NVARCHAR(64),
	@Barcode VARCHAR(128),
	@CategoryID INT,
	@ProducerID INT
AS
BEGIN
	IF NOT EXISTS (SELECT 1 FROM Product WHERE is_active = 1 AND (name = @Name OR barcode = @Barcode))
	BEGIN
	
		INSERT INTO Product (name, barcode, id_category, id_producer)
		VALUES (@Name, @Barcode, @CategoryID, @ProducerID);
		
		SELECT 'Success';
		
	END
	
	ELSE BEGIN SELECT 'There already is a product with this name or this barcode'; END
END;
GO

CREATE PROCEDURE EditProduct
	@ID INT,
	@Name NVARCHAR(64),
	@Barcode VARCHAR(128),
	@CategoryID INT,
	@ProducerID INT
AS
BEGIN
	BEGIN TRANSACTION;

	UPDATE Product
	SET name = @Name, barcode = @Barcode, id_category = @CategoryID, id_producer = @ProducerID
	WHERE is_active = 1
		AND id = @ID;
		
	IF (SELECT COUNT(id) FROM Product WHERE is_active = 1 AND name = @Name) > 1
	BEGIN
	
		ROLLBACK TRANSACTION;
		SELECT 'There already is a product with this name';
		
	END
	
	IF (SELECT COUNT(id) FROM Product WHERE is_active = 1 AND barcode = @Barcode) > 1
	BEGIN
	
		ROLLBACK TRANSACTION;
		SELECT 'There already is a product with this barcode';
		
	END
	
	ELSE
	BEGIN
	
		SELECT 'Success';
		COMMIT TRANSACTION;
		
	END
END;
GO

CREATE PROCEDURE DeleteProduct
	@ID INT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Product WHERE is_active = 1 AND id = @ID)
	BEGIN
		
		UPDATE Product
		SET Product.is_active = 0
		WHERE Product.id = @ID;
		
		SELECT 'Success';
		
	END
	
	ELSE BEGIN SELECT 'There currently is an active stock with this product'; END
END;
GO

CREATE PROCEDURE GetAllStocks
AS
BEGIN
	SELECT id, id_product, quantity, initial_quantity, unit, supply_date, expiration_date, purchase_price, selling_price
	FROM Stock
	WHERE is_active = 1;
END;
GO

CREATE PROCEDURE AddNewStock
	@ProductID INT,
	@Quantity INT,
	@Unit NVARCHAR(64),
	@SupplyDate DATE,
	@ExpirationDate DATE,
	@PurchasePrice DECIMAL(12, 2),
	@SellingPrice DECIMAL(12, 2)
AS
BEGIN
    INSERT INTO Stock (id_product, quantity, initial_quantity, unit,  supply_date, expiration_date,  purchase_price,selling_price)
	VALUES (@ProductID, @Quantity, @Quantity, @Unit, @SupplyDate, @ExpirationDate, @PurchasePrice, @SellingPrice);
		
	SELECT 'Success';
END;
GO

CREATE PROCEDURE AddNewStockToday
	@ProductID INT,
	@Quantity INT,
	@Unit NVARCHAR(64),
	@ExpirationDate DATE,
	@PurchasePrice DECIMAL(12, 2),
	@SellingPrice DECIMAL(12, 2)
AS
BEGIN
    EXEC AddNewStock
		@ProductID,
		@Quantity,
		@Unit,
		GETDATE,
		@ExpirationDate,
		@PurchasePrice,
		@SellingPrice;
END;
GO

CREATE PROCEDURE DeleteStock
	@ID INT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Stock WHERE is_active = 1 AND id = @id)
	BEGIN
	
		UPDATE Stock
		SET is_active = 0
		WHERE id = @ID;
		
		SELECT 'Success';
		
	END
	
	ELSE BEGIN SELECT 'There is no stock with this ID'; END
END;
GO

CREATE PROCEDURE EditStock
	@ID INT,
	@ProductID INT,
	@Quantity INT,
	@InitialQuantity INT,
	@Unit NVARCHAR(64),
	@SupplyDate DATE,
	@ExpirationDate DATE,
	@PurchasePrice DECIMAL(12, 2),
	@SellingPrice DECIMAL(12, 2)
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Stock WHERE is_active = 1 AND id = @ID)
	BEGIN
	
		UPDATE Stock
		SET id_product = @ProductID, quantity = @Quantity, initial_quantity = @InitialQuantity, unit = @Unit, supply_date = @SupplyDate, expiration_date = @ExpirationDate, purchase_price = @PurchasePrice, selling_price = @SellingPrice
		WHERE is_active = 1 AND id = @ID;
		
		SELECT 'Success';
		
	END
	
	ELSE BEGIN SELECT 'There is no active stock with this ID'; END
END;
GO

CREATE PROCEDURE InvalidateExpiredStocks
AS
BEGIN
	UPDATE stock
	SET is_active = 0
	WHERE expiration_date < GETDATE();
	
	SELECT 'Success';
END;
GO

CREATE PROCEDURE InvalidateEmptyStocks
AS
BEGIN
	UPDATE stock
	SET is_active = 0
	WHERE quantity <= 0;
	
	SELECT 'Success';
END;
GO

CREATE PROCEDURE GetProductsValueFiltered
	@CategoryID INT,
	@ProducerID INT
AS
BEGIN
	SELECT SUM(Stock.selling_price * Stock.quantity)
	FROM Stock
	JOIN Product ON Product.id = Stock.id_product
	JOIN Producer ON Producer.id = product.id_producer
	JOIN Category ON Category.id = Product.id_category
	WHERE Stock.is_active = 1
		AND Product.is_active = 1
		AND Producer.is_active = 1
		AND Category.is_active = 1
		AND Category.id = @CategoryID
		AND Producer.id = @ProducerID;
END;
GO

CREATE PROCEDURE GetReceiptsFiltered
	@CashierID INT,
	@StartDate DATE,
	@EndDate DATE
AS
BEGIN
	SELECT id, issue_date, id_cashier, total_price
	FROM Receipt
	WHERE is_active = 1
		AND id_cashier = @CashierID
		AND @StartDate <= issue_date
		AND issue_date < @EndDate;
END;
GO

CREATE PROCEDURE GetLastReceipt
AS
BEGIN
	SELECT TOP 1 id, issue_date, id_cashier, total_price
	FROM Receipt
	WHERE is_active = 1
	ORDER BY id DESC;
END;
GO

CREATE PROCEDURE GetLargestReceipt
	@SelectedDate DATE
AS
BEGIN
	SELECT TOP 1 id, issue_date, id_cashier, total_price
	FROM Receipt
	WHERE is_active = 1
		AND issue_date = @SelectedDate
	ORDER BY total_price DESC;
END;
GO

CREATE PROCEDURE GetAllReceipts
AS
BEGIN
	SELECT id, issue_date, id_cashier, total_price
	FROM Receipt
	WHERE is_active = 1
	ORDER BY issue_date DESC;
END;
GO

CREATE PROCEDURE CreateReceipt
	@IssueDate DATE,
	@CashierID INT,
	@TotalPrice DECIMAL(12, 2)
AS
BEGIN
	INSERT INTO Receipt (issue_date, id_cashier, total_price)
	VALUES (@IssueDate, @CashierID, @TotalPrice);
	
	SELECT 'Success';
END;
GO

CREATE PROCEDURE GetReceiptItems
	@ReceiptID INT
AS
BEGIN
	SELECT id, id_receipt, id_product, quantity, total_price
	FROM ReceiptItem
	WHERE is_active = 1
		AND id_receipt = @ReceiptID;
END;
GO

CREATE PROCEDURE CreateReceiptItem
	@ReceiptID INT,
	@ProductID INT,
	@Quantity INT,
	@TotalPrice DECIMAL(12, 2)
AS
BEGIN
	INSERT INTO ReceiptItem (id_receipt, id_product, quantity, total_price)
	VALUES (@ReceiptID, @ProductID, @Quantity, @TotalPrice);
	
	SELECT 'Success';
END
