USE Supermarket;
GO

------------------------------------------------------------------------------------------------

CREATE TRIGGER trigger_CheckCountryExists
ON dbo.Country
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    IF (
        SELECT COUNT(dbo.Country.id)
        FROM dbo.Country
        WHERE dbo.Country.name IN (SELECT name FROM inserted)
    ) > 1
    BEGIN
		DELETE FROM dbo.Country
		WHERE dbo.Country.id IN (SELECT id FROM inserted);
		
        UPDATE dbo.Country
		SET is_active = 1
		FROM dbo.Country
		JOIN inserted ON inserted.name = dbo.Country.name;
    END
END;
GO

------------------------------------------------------------------------------------------------

CREATE TRIGGER trigger_CheckCategoryExists
ON dbo.Category
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    IF (
        SELECT COUNT(dbo.Category.id)
        FROM dbo.Category
        WHERE dbo.Category.name IN (SELECT name FROM inserted)
    ) > 1
    BEGIN
		DELETE FROM dbo.Category
		WHERE dbo.Category.id IN (SELECT id FROM inserted);
		
        UPDATE dbo.Category
		SET is_active = 1
		FROM dbo.Category
		JOIN inserted ON inserted.name = dbo.Category.name;
    END
END;
GO

------------------------------------------------------------------------------------------------

CREATE TRIGGER trigger_CheckUserTypeExists
ON dbo.UserType
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    IF (
        SELECT COUNT(dbo.UserType.id)
        FROM dbo.UserType
        WHERE dbo.UserType.name IN (SELECT name FROM inserted)
    ) > 1
    BEGIN
		DELETE FROM dbo.UserType
		WHERE dbo.UserType.id IN (SELECT id FROM inserted);
		
        UPDATE dbo.UserType
		SET is_active = 1
		FROM dbo.UserType
		JOIN inserted ON inserted.name = dbo.UserType.name;
    END
END;
GO

------------------------------------------------------------------------------------------------

CREATE TRIGGER trigger_CheckProducerExists
ON dbo.Producer
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    IF (
        SELECT COUNT(dbo.Producer.id)
		FROM dbo.Producer
		WHERE EXISTS (
			SELECT 1
			FROM inserted
			WHERE dbo.Producer.name = inserted.name
				AND dbo.Producer.id_country = inserted.id_country
		)
    ) > 1
    BEGIN
		DELETE p 
		FROM dbo.Producer p
		JOIN inserted ON inserted.name = p.name
			AND inserted.id_country = p.id_country
		WHERE p.id IN (SELECT id FROM inserted);
		
        UPDATE dbo.Producer
		SET is_active = 1
		FROM dbo.Producer
		JOIN inserted ON inserted.name = dbo.Producer.name
			AND inserted.id_country = dbo.Producer.id_country;
    END
END;
GO

------------------------------------------------------------------------------------------------

CREATE TRIGGER trigger_CheckMarketUserExists
ON dbo.MarketUser
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    IF (
        SELECT COUNT(dbo.MarketUser.id)
        FROM dbo.MarketUser
		WHERE EXISTS (
			SELECT 1
			FROM inserted
			WHERE dbo.MarketUser.name = inserted.name
				AND dbo.MarketUser.password_hash = inserted.password_hash
				AND dbo.MarketUser.id_type = inserted.id_type
		)
    ) > 1
    BEGIN
		DELETE market_user
		FROM dbo.MarketUser market_user
		JOIN inserted ON inserted.name = market_user.name
			AND inserted.password_hash = market_user.password_hash
		WHERE market_user.id IN (SELECT id FROM inserted);
		
        UPDATE dbo.MarketUser
		SET is_active = 1
		FROM dbo.MarketUser
		JOIN inserted ON inserted.name = dbo.MarketUser.name
			AND inserted.password_hash = dbo.MarketUser.password_hash
			AND inserted.id_type = dbo.MarketUser.id_type;
    END
END;
GO

------------------------------------------------------------------------------------------------

CREATE TRIGGER trigger_CheckProductExists
ON dbo.Product
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    IF (
        SELECT COUNT(dbo.Product.id)
        FROM dbo.Product
		WHERE EXISTS (
			SELECT 1
			FROM inserted
			WHERE dbo.Product.name = inserted.name
				AND dbo.Product.barcode = inserted.barcode
				AND dbo.Product.id_category = inserted.id_category
				AND dbo.Product.id_producer = inserted.id_producer
		)
    ) > 1
    BEGIN
		DELETE product
		FROM dbo.Product product
		JOIN inserted ON inserted.name = product.name
			AND inserted.barcode = product.barcode
			AND inserted.id_category = product.id_category
			AND inserted.id_producer = product.id_producer
		WHERE product.id IN (SELECT id FROM inserted);
		
        UPDATE dbo.Product
		SET is_active = 1
		FROM dbo.Product
		JOIN inserted ON inserted.name = dbo.Product.name
			AND inserted.barcode = dbo.Product.barcode
			AND inserted.id_category = dbo.Product.id_category
			AND inserted.id_producer = dbo.Product.id_producer;
    END
END;
GO

------------------------------------------------------------------------------------------------

CREATE TRIGGER trigger_CheckStockUpdate
ON dbo.Stock
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
	
	IF EXISTS (SELECT 1 FROM inserted WHERE quantity <= 0)
    BEGIN
        UPDATE stock
        SET is_active = 0
        FROM dbo.Stock stock
        JOIN inserted ON inserted.id = stock.id
		WHERE inserted.quantity <= 0;
    END;
END;
GO
