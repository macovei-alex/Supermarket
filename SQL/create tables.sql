USE Supermarket;

CREATE TABLE Country (
	id INT IDENTITY(1, 1) PRIMARY KEY,
	name NVARCHAR(64) NOT NULL DEFAULT 'undefined',
	is_active BIT NOT NULL DEFAULT 1
);

CREATE TABLE Category (
	id INT IDENTITY(1, 1) PRIMARY KEY,
	name NVARCHAR(64) NOT NULL DEFAULT 'undefined',
	is_active BIT NOT NULL DEFAULT 1
);

CREATE TABLE UserType (
	id INT IDENTITY(1, 1) PRIMARY KEY,
	name NVARCHAR(64) NOT NULL DEFAULT 'undefined',
	is_active BIT NOT NULL DEFAULT 1
);

CREATE TABLE Producer (
	id INT IDENTITY(1, 1) PRIMARY KEY,
	id_country INT NOT NULL,
	name NVARCHAR(64) NOT NULL DEFAULT 'undefined',
	is_active BIT NOT NULL DEFAULT 1,
	FOREIGN KEY (id_country) REFERENCES dbo.Country(id)
);

CREATE TABLE MarketUser (
	id INT IDENTITY(1, 1) PRIMARY KEY,
	name NVARCHAR(64) NOT NULL DEFAULT 'undefined',
	password_hash CHAR(64) NOT NULL DEFAULT 'undefined0000000000000000000000000000000000000000000000000000000',
	id_type INT NOT NULL,
	is_active BIT NOT NULL DEFAULT 1,
	FOREIGN KEY (id_type) REFERENCES dbo.UserType(id)
);

CREATE TABLE Product (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(64) NOT NULL DEFAULT 'undefined',
    barcode VARCHAR(128) NOT NULL DEFAULT 'undefined',
    id_category INT NOT NULL,
    id_producer INT NOT NULL,
	is_active BIT NOT NULL DEFAULT 1,
	FOREIGN KEY (id_category) REFERENCES dbo.Category(id),
	FOREIGN KEY (id_producer) REFERENCES dbo.Producer(id)
);

CREATE TABLE Stock (
	id INT IDENTITY(1, 1) PRIMARY KEY,
	id_product INT NOT NULL,
	quantity INT NOT NULL,
	initial_quantity INT NOT NULL,
	unit NVARCHAR(64) NOT NULL,
	supply_date DATE NOT NULL DEFAULT CONVERT(DATE, GETDATE()),
	expiration_date DATE NOT NULL DEFAULT CONVERT(DATE, GETDATE()),
	purchase_price DECIMAL(12, 2) NOT NULL,
	selling_price DECIMAL(12, 2) NOT NULL,
	is_active BIT NOT NULL DEFAULT 1,
	FOREIGN KEY (id_product) REFERENCES dbo.Product(id)
);

CREATE TABLE Receipt (
	id INT IDENTITY(1, 1) PRIMARY KEY,
	issue_date DATE NOT NULL DEFAULT CONVERT(DATE, GETDATE()),
	id_cashier INT NOT NULL,
	total_price DECIMAL(12, 2) NOT NULL,
	is_active BIT NOT NULL DEFAULT 1,
	FOREIGN KEY (id_cashier) REFERENCES dbo.MarketUser(id)
);

CREATE TABLE ReceiptItem (
	id INT IDENTITY(1, 1) PRIMARY KEY,
	id_receipt INT NOT NULL,
	id_product INT NOT NULL,
	quantity INT NOT NULL,
	total_price DECIMAL(12, 2) NOT NULL,
	is_active BIT NOT NULL DEFAULT 1,
	FOREIGN KEY (id_receipt) REFERENCES dbo.Receipt(id),
	FOREIGN KEY (id_product) REFERENCES dbo.Product(id)
);
