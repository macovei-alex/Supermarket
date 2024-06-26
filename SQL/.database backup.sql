USE [master]
GO
/****** Object:  Database [Supermarket]    Script Date: 28/05/2024 10:50:21 ******/
CREATE DATABASE [Supermarket]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Supermarket', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Supermarket.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Supermarket_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Supermarket_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Supermarket] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Supermarket].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Supermarket] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Supermarket] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Supermarket] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Supermarket] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Supermarket] SET ARITHABORT OFF 
GO
ALTER DATABASE [Supermarket] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Supermarket] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Supermarket] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Supermarket] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Supermarket] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Supermarket] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Supermarket] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Supermarket] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Supermarket] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Supermarket] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Supermarket] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Supermarket] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Supermarket] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Supermarket] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Supermarket] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Supermarket] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Supermarket] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Supermarket] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Supermarket] SET  MULTI_USER 
GO
ALTER DATABASE [Supermarket] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Supermarket] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Supermarket] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Supermarket] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Supermarket] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Supermarket] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Supermarket] SET QUERY_STORE = ON
GO
ALTER DATABASE [Supermarket] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Supermarket]
GO
/****** Object:  User [mac]    Script Date: 28/05/2024 10:50:21 ******/
CREATE USER [mac] FOR LOGIN [mac] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 28/05/2024 10:50:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](64) NOT NULL,
	[is_active] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 28/05/2024 10:50:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](64) NOT NULL,
	[is_active] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MarketUser]    Script Date: 28/05/2024 10:50:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MarketUser](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](64) NOT NULL,
	[password_hash] [char](64) NOT NULL,
	[id_type] [int] NOT NULL,
	[is_active] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producer]    Script Date: 28/05/2024 10:50:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_country] [int] NOT NULL,
	[name] [nvarchar](64) NOT NULL,
	[is_active] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 28/05/2024 10:50:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](64) NOT NULL,
	[barcode] [varchar](128) NOT NULL,
	[id_category] [int] NOT NULL,
	[id_producer] [int] NOT NULL,
	[is_active] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Receipt]    Script Date: 28/05/2024 10:50:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Receipt](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[issue_date] [date] NOT NULL,
	[id_cashier] [int] NOT NULL,
	[total_price] [decimal](12, 2) NOT NULL,
	[is_active] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReceiptItem]    Script Date: 28/05/2024 10:50:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReceiptItem](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_receipt] [int] NOT NULL,
	[id_product] [int] NOT NULL,
	[quantity] [int] NOT NULL,
	[total_price] [decimal](12, 2) NOT NULL,
	[is_active] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stock]    Script Date: 28/05/2024 10:50:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stock](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_product] [int] NOT NULL,
	[quantity] [int] NOT NULL,
	[initial_quantity] [int] NOT NULL,
	[unit] [nvarchar](64) NOT NULL,
	[supply_date] [date] NOT NULL,
	[expiration_date] [date] NOT NULL,
	[purchase_price] [decimal](12, 2) NOT NULL,
	[selling_price] [decimal](12, 2) NOT NULL,
	[is_active] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserType]    Script Date: 28/05/2024 10:50:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserType](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](64) NOT NULL,
	[is_active] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([id], [name], [is_active]) VALUES (1, N'food', 1)
INSERT [dbo].[Category] ([id], [name], [is_active]) VALUES (2, N'drink', 1)
INSERT [dbo].[Category] ([id], [name], [is_active]) VALUES (3, N'clothes', 1)
INSERT [dbo].[Category] ([id], [name], [is_active]) VALUES (4, N'test', 1)
INSERT [dbo].[Category] ([id], [name], [is_active]) VALUES (5, N'toys', 1)
INSERT [dbo].[Category] ([id], [name], [is_active]) VALUES (6, N'snorkleing', 0)
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Country] ON 

INSERT [dbo].[Country] ([id], [name], [is_active]) VALUES (1, N'United States', 1)
INSERT [dbo].[Country] ([id], [name], [is_active]) VALUES (2, N'Romania', 1)
INSERT [dbo].[Country] ([id], [name], [is_active]) VALUES (3, N'China', 1)
INSERT [dbo].[Country] ([id], [name], [is_active]) VALUES (4, N'Germany', 1)
INSERT [dbo].[Country] ([id], [name], [is_active]) VALUES (5, N'France', 1)
INSERT [dbo].[Country] ([id], [name], [is_active]) VALUES (6, N'India', 1)
INSERT [dbo].[Country] ([id], [name], [is_active]) VALUES (7, N'test', 0)
INSERT [dbo].[Country] ([id], [name], [is_active]) VALUES (8, N'test', 1)
INSERT [dbo].[Country] ([id], [name], [is_active]) VALUES (9, N'Tazmania', 1)
INSERT [dbo].[Country] ([id], [name], [is_active]) VALUES (10, N'UK', 1)
SET IDENTITY_INSERT [dbo].[Country] OFF
GO
SET IDENTITY_INSERT [dbo].[MarketUser] ON 

INSERT [dbo].[MarketUser] ([id], [name], [password_hash], [id_type], [is_active]) VALUES (1, N'admin', N'8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', 1, 1)
INSERT [dbo].[MarketUser] ([id], [name], [password_hash], [id_type], [is_active]) VALUES (2, N'cashier', N'6a79b51fec89db977e62d3b5aee3ea8b9de93cabf2446aae7d6f517db6d16178', 2, 1)
INSERT [dbo].[MarketUser] ([id], [name], [password_hash], [id_type], [is_active]) VALUES (5, N'alex', N'48dc130365c61f352551ccc655aa555bed9c53e330df2dcf97d7c1f08b69acfa', 2, 0)
INSERT [dbo].[MarketUser] ([id], [name], [password_hash], [id_type], [is_active]) VALUES (6, N'alexandru2', N'97b90b61227678d748bc4587b2a7fa6122403774ba96c53108aec2f97513a7d4', 2, 0)
INSERT [dbo].[MarketUser] ([id], [name], [password_hash], [id_type], [is_active]) VALUES (7, N'david', N'e7af66506f7c7cc1ec73962f3b6f109bcf9fc8bae13fe917b15f047492a37213', 1, 0)
INSERT [dbo].[MarketUser] ([id], [name], [password_hash], [id_type], [is_active]) VALUES (8, N'andrei', N'2762b510321a3b67d109fdbfb0c59b2361e89ba070a23a9fa615fa8b1bc622c4', 1, 0)
INSERT [dbo].[MarketUser] ([id], [name], [password_hash], [id_type], [is_active]) VALUES (9, N'andrei10', N'35412371ccb007e6d4dfa20302b17b6a18f6abed28cb9a3e5d62ea9fceb6d2b1', 2, 0)
INSERT [dbo].[MarketUser] ([id], [name], [password_hash], [id_type], [is_active]) VALUES (10, N'test2', N'6016bcc377c93692f2fe19fbad47eee6fb8f4cc98c56e935db5edb69806d84f6', 2, 0)
INSERT [dbo].[MarketUser] ([id], [name], [password_hash], [id_type], [is_active]) VALUES (11, N'Alex', N'77262e1725996f425bbb0c488db40ffbd04081e99359a8b56956925b83e52c23', 1, 0)
INSERT [dbo].[MarketUser] ([id], [name], [password_hash], [id_type], [is_active]) VALUES (12, N'test4', N'60303ae22b998861bce3b28f33eec1be758a213c86c93c076dbe9f558c11c752', 1, 0)
INSERT [dbo].[MarketUser] ([id], [name], [password_hash], [id_type], [is_active]) VALUES (13, N'test5', N'348a629f5ceed032c3e8706ec47d9bfafb00fb4250b018dd965435ca50cb836e', 1, 0)
INSERT [dbo].[MarketUser] ([id], [name], [password_hash], [id_type], [is_active]) VALUES (14, N'test6', N'60303ae22b998861bce3b28f33eec1be758a213c86c93c076dbe9f558c11c752', 2, 0)
INSERT [dbo].[MarketUser] ([id], [name], [password_hash], [id_type], [is_active]) VALUES (15, N'test7', N'60303ae22b998861bce3b28f33eec1be758a213c86c93c076dbe9f558c11c752', 2, 0)
INSERT [dbo].[MarketUser] ([id], [name], [password_hash], [id_type], [is_active]) VALUES (16, N'lala', N'724316b9b7191e98390e87844a80dfccde1529dc5b8bcd30b55237abfe9c3d1b', 1, 0)
INSERT [dbo].[MarketUser] ([id], [name], [password_hash], [id_type], [is_active]) VALUES (17, N'Alex', N'348a629f5ceed032c3e8706ec47d9bfafb00fb4250b018dd965435ca50cb836e', 2, 0)
INSERT [dbo].[MarketUser] ([id], [name], [password_hash], [id_type], [is_active]) VALUES (18, N'david', N'07d046d5fac12b3f82daf5035b9aae86db5adc8275ebfbf05ec83005a4a8ba3e', 1, 0)
INSERT [dbo].[MarketUser] ([id], [name], [password_hash], [id_type], [is_active]) VALUES (19, N'test', N'9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08', 2, 0)
INSERT [dbo].[MarketUser] ([id], [name], [password_hash], [id_type], [is_active]) VALUES (20, N'test2', N'a80b568a237f50391d2f1f97beaf99564e33d2e1c8a2e5cac21ceda701570312', 2, 0)
INSERT [dbo].[MarketUser] ([id], [name], [password_hash], [id_type], [is_active]) VALUES (21, N'david', N'07d046d5fac12b3f82daf5035b9aae86db5adc8275ebfbf05ec83005a4a8ba3e', 2, 0)
INSERT [dbo].[MarketUser] ([id], [name], [password_hash], [id_type], [is_active]) VALUES (22, N'cashier2', N'cc8c368fde9cc291c8ea587790342f929d70fa06f612cd51b6141ea5bca46bdb', 2, 0)
INSERT [dbo].[MarketUser] ([id], [name], [password_hash], [id_type], [is_active]) VALUES (23, N'cashier2', N'cc8c368fde9cc291c8ea587790342f929d70fa06f612cd51b6141ea5bca46bdb', 2, 0)
INSERT [dbo].[MarketUser] ([id], [name], [password_hash], [id_type], [is_active]) VALUES (24, N'test', N'9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08', 2, 0)
INSERT [dbo].[MarketUser] ([id], [name], [password_hash], [id_type], [is_active]) VALUES (25, N'alex1', N'd6fd8c626f7cc90d27c68721b4cb4abd63f9cabb55dc24b6dddaed7b22153858', 1, 1)
INSERT [dbo].[MarketUser] ([id], [name], [password_hash], [id_type], [is_active]) VALUES (26, N'david', N'07d046d5fac12b3f82daf5035b9aae86db5adc8275ebfbf05ec83005a4a8ba3e', 2, 0)
SET IDENTITY_INSERT [dbo].[MarketUser] OFF
GO
SET IDENTITY_INSERT [dbo].[Producer] ON 

INSERT [dbo].[Producer] ([id], [id_country], [name], [is_active]) VALUES (1, 1, N'Producer1.SRL', 1)
INSERT [dbo].[Producer] ([id], [id_country], [name], [is_active]) VALUES (2, 2, N'Producer2.SRL', 1)
INSERT [dbo].[Producer] ([id], [id_country], [name], [is_active]) VALUES (3, 3, N'Producer3.SRL', 1)
INSERT [dbo].[Producer] ([id], [id_country], [name], [is_active]) VALUES (4, 4, N'Producer4.SRL', 1)
INSERT [dbo].[Producer] ([id], [id_country], [name], [is_active]) VALUES (5, 8, N'test', 1)
INSERT [dbo].[Producer] ([id], [id_country], [name], [is_active]) VALUES (6, 9, N'Tazmanian Producer', 1)
INSERT [dbo].[Producer] ([id], [id_country], [name], [is_active]) VALUES (7, 10, N'UK Producer', 1)
SET IDENTITY_INSERT [dbo].[Producer] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([id], [name], [barcode], [id_category], [id_producer], [is_active]) VALUES (6, N'product 1', N'123', 1, 1, 1)
INSERT [dbo].[Product] ([id], [name], [barcode], [id_category], [id_producer], [is_active]) VALUES (7, N'product 2', N'23456', 2, 1, 1)
INSERT [dbo].[Product] ([id], [name], [barcode], [id_category], [id_producer], [is_active]) VALUES (8, N'product 3', N'34567', 1, 3, 1)
INSERT [dbo].[Product] ([id], [name], [barcode], [id_category], [id_producer], [is_active]) VALUES (9, N'product 4', N'45678', 2, 4, 0)
INSERT [dbo].[Product] ([id], [name], [barcode], [id_category], [id_producer], [is_active]) VALUES (10, N'product 5', N'56789', 3, 4, 0)
INSERT [dbo].[Product] ([id], [name], [barcode], [id_category], [id_producer], [is_active]) VALUES (11, N'test product', N'1234', 1, 1, 0)
INSERT [dbo].[Product] ([id], [name], [barcode], [id_category], [id_producer], [is_active]) VALUES (12, N'test', N'1234', 1, 1, 0)
INSERT [dbo].[Product] ([id], [name], [barcode], [id_category], [id_producer], [is_active]) VALUES (13, N'test', N'test', 1, 1, 0)
INSERT [dbo].[Product] ([id], [name], [barcode], [id_category], [id_producer], [is_active]) VALUES (14, N'test2', N'1234', 1, 1, 0)
INSERT [dbo].[Product] ([id], [name], [barcode], [id_category], [id_producer], [is_active]) VALUES (15, N'product 4', N'123456', 2, 3, 1)
INSERT [dbo].[Product] ([id], [name], [barcode], [id_category], [id_producer], [is_active]) VALUES (16, N'test', N'test', 4, 5, 0)
INSERT [dbo].[Product] ([id], [name], [barcode], [id_category], [id_producer], [is_active]) VALUES (17, N'water', N'12', 2, 1, 1)
INSERT [dbo].[Product] ([id], [name], [barcode], [id_category], [id_producer], [is_active]) VALUES (18, N'tea', N'123456789', 2, 7, 1)
INSERT [dbo].[Product] ([id], [name], [barcode], [id_category], [id_producer], [is_active]) VALUES (19, N'test', N'1234', 1, 1, 0)
INSERT [dbo].[Product] ([id], [name], [barcode], [id_category], [id_producer], [is_active]) VALUES (20, N'bread', N'3653225', 1, 7, 1)
INSERT [dbo].[Product] ([id], [name], [barcode], [id_category], [id_producer], [is_active]) VALUES (21, N'newProduct', N'121', 1, 1, 0)
INSERT [dbo].[Product] ([id], [name], [barcode], [id_category], [id_producer], [is_active]) VALUES (22, N'paine', N'123421', 1, 1, 0)
INSERT [dbo].[Product] ([id], [name], [barcode], [id_category], [id_producer], [is_active]) VALUES (23, N'paine', N'124398', 1, 2, 0)
INSERT [dbo].[Product] ([id], [name], [barcode], [id_category], [id_producer], [is_active]) VALUES (24, N'paine', N'2193479', 1, 7, 0)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[Receipt] ON 

INSERT [dbo].[Receipt] ([id], [issue_date], [id_cashier], [total_price], [is_active]) VALUES (22, CAST(N'2024-05-28' AS Date), 1, CAST(57.46 AS Decimal(12, 2)), 1)
INSERT [dbo].[Receipt] ([id], [issue_date], [id_cashier], [total_price], [is_active]) VALUES (23, CAST(N'2024-05-28' AS Date), 1, CAST(11.84 AS Decimal(12, 2)), 1)
INSERT [dbo].[Receipt] ([id], [issue_date], [id_cashier], [total_price], [is_active]) VALUES (24, CAST(N'2024-05-28' AS Date), 25, CAST(35.70 AS Decimal(12, 2)), 1)
INSERT [dbo].[Receipt] ([id], [issue_date], [id_cashier], [total_price], [is_active]) VALUES (25, CAST(N'2024-05-28' AS Date), 1, CAST(34.56 AS Decimal(12, 2)), 1)
INSERT [dbo].[Receipt] ([id], [issue_date], [id_cashier], [total_price], [is_active]) VALUES (26, CAST(N'2024-05-28' AS Date), 1, CAST(14.90 AS Decimal(12, 2)), 1)
INSERT [dbo].[Receipt] ([id], [issue_date], [id_cashier], [total_price], [is_active]) VALUES (27, CAST(N'2024-05-28' AS Date), 1, CAST(14.90 AS Decimal(12, 2)), 1)
SET IDENTITY_INSERT [dbo].[Receipt] OFF
GO
SET IDENTITY_INSERT [dbo].[ReceiptItem] ON 

INSERT [dbo].[ReceiptItem] ([id], [id_receipt], [id_product], [quantity], [total_price], [is_active]) VALUES (7, 22, 20, 2, CAST(3.54 AS Decimal(12, 2)), 1)
INSERT [dbo].[ReceiptItem] ([id], [id_receipt], [id_product], [quantity], [total_price], [is_active]) VALUES (8, 22, 17, 4, CAST(9.52 AS Decimal(12, 2)), 1)
INSERT [dbo].[ReceiptItem] ([id], [id_receipt], [id_product], [quantity], [total_price], [is_active]) VALUES (9, 22, 18, 15, CAST(44.40 AS Decimal(12, 2)), 1)
INSERT [dbo].[ReceiptItem] ([id], [id_receipt], [id_product], [quantity], [total_price], [is_active]) VALUES (10, 23, 20, 4, CAST(7.08 AS Decimal(12, 2)), 1)
INSERT [dbo].[ReceiptItem] ([id], [id_receipt], [id_product], [quantity], [total_price], [is_active]) VALUES (11, 23, 17, 2, CAST(4.76 AS Decimal(12, 2)), 1)
INSERT [dbo].[ReceiptItem] ([id], [id_receipt], [id_product], [quantity], [total_price], [is_active]) VALUES (12, 24, 17, 15, CAST(35.70 AS Decimal(12, 2)), 1)
INSERT [dbo].[ReceiptItem] ([id], [id_receipt], [id_product], [quantity], [total_price], [is_active]) VALUES (13, 25, 18, 10, CAST(29.80 AS Decimal(12, 2)), 1)
INSERT [dbo].[ReceiptItem] ([id], [id_receipt], [id_product], [quantity], [total_price], [is_active]) VALUES (14, 25, 17, 2, CAST(4.76 AS Decimal(12, 2)), 1)
INSERT [dbo].[ReceiptItem] ([id], [id_receipt], [id_product], [quantity], [total_price], [is_active]) VALUES (15, 26, 18, 5, CAST(14.90 AS Decimal(12, 2)), 1)
INSERT [dbo].[ReceiptItem] ([id], [id_receipt], [id_product], [quantity], [total_price], [is_active]) VALUES (16, 27, 18, 5, CAST(14.90 AS Decimal(12, 2)), 1)
SET IDENTITY_INSERT [dbo].[ReceiptItem] OFF
GO
SET IDENTITY_INSERT [dbo].[Stock] ON 

INSERT [dbo].[Stock] ([id], [id_product], [quantity], [initial_quantity], [unit], [supply_date], [expiration_date], [purchase_price], [selling_price], [is_active]) VALUES (34, 18, 105, 120, N'packet', CAST(N'2024-05-28' AS Date), CAST(N'2024-06-09' AS Date), CAST(2.49 AS Decimal(12, 2)), CAST(2.96 AS Decimal(12, 2)), 0)
INSERT [dbo].[Stock] ([id], [id_product], [quantity], [initial_quantity], [unit], [supply_date], [expiration_date], [purchase_price], [selling_price], [is_active]) VALUES (35, 17, 177, 200, N'bottle', CAST(N'2024-05-28' AS Date), CAST(N'2024-06-07' AS Date), CAST(2.00 AS Decimal(12, 2)), CAST(2.38 AS Decimal(12, 2)), 1)
INSERT [dbo].[Stock] ([id], [id_product], [quantity], [initial_quantity], [unit], [supply_date], [expiration_date], [purchase_price], [selling_price], [is_active]) VALUES (36, 20, 244, 250, N'loaf', CAST(N'2024-05-28' AS Date), CAST(N'2024-06-07' AS Date), CAST(1.49 AS Decimal(12, 2)), CAST(1.77 AS Decimal(12, 2)), 1)
INSERT [dbo].[Stock] ([id], [id_product], [quantity], [initial_quantity], [unit], [supply_date], [expiration_date], [purchase_price], [selling_price], [is_active]) VALUES (37, 18, 0, 20, N'packet', CAST(N'2024-05-28' AS Date), CAST(N'2024-12-01' AS Date), CAST(2.50 AS Decimal(12, 2)), CAST(2.98 AS Decimal(12, 2)), 0)
INSERT [dbo].[Stock] ([id], [id_product], [quantity], [initial_quantity], [unit], [supply_date], [expiration_date], [purchase_price], [selling_price], [is_active]) VALUES (38, 20, 20, 20, N'loaf', CAST(N'2024-05-28' AS Date), CAST(N'2024-06-04' AS Date), CAST(3.40 AS Decimal(12, 2)), CAST(4.05 AS Decimal(12, 2)), 1)
INSERT [dbo].[Stock] ([id], [id_product], [quantity], [initial_quantity], [unit], [supply_date], [expiration_date], [purchase_price], [selling_price], [is_active]) VALUES (39, 20, 20, 20, N'loaf', CAST(N'2024-05-28' AS Date), CAST(N'2024-06-04' AS Date), CAST(1.89 AS Decimal(12, 2)), CAST(2.25 AS Decimal(12, 2)), 1)
SET IDENTITY_INSERT [dbo].[Stock] OFF
GO
SET IDENTITY_INSERT [dbo].[UserType] ON 

INSERT [dbo].[UserType] ([id], [name], [is_active]) VALUES (1, N'administrator', 1)
INSERT [dbo].[UserType] ([id], [name], [is_active]) VALUES (2, N'cashier', 1)
SET IDENTITY_INSERT [dbo].[UserType] OFF
GO
ALTER TABLE [dbo].[Category] ADD  DEFAULT ('undefined') FOR [name]
GO
ALTER TABLE [dbo].[Category] ADD  DEFAULT ((1)) FOR [is_active]
GO
ALTER TABLE [dbo].[Country] ADD  DEFAULT ('undefined') FOR [name]
GO
ALTER TABLE [dbo].[Country] ADD  DEFAULT ((1)) FOR [is_active]
GO
ALTER TABLE [dbo].[MarketUser] ADD  DEFAULT ('undefined') FOR [name]
GO
ALTER TABLE [dbo].[MarketUser] ADD  DEFAULT ('undefined0000000000000000000000000000000000000000000000000000000') FOR [password_hash]
GO
ALTER TABLE [dbo].[MarketUser] ADD  DEFAULT ((1)) FOR [is_active]
GO
ALTER TABLE [dbo].[Producer] ADD  DEFAULT ('undefined') FOR [name]
GO
ALTER TABLE [dbo].[Producer] ADD  DEFAULT ((1)) FOR [is_active]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT ('undefined') FOR [name]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT ('undefined') FOR [barcode]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT ((1)) FOR [is_active]
GO
ALTER TABLE [dbo].[Receipt] ADD  DEFAULT (CONVERT([date],getdate())) FOR [issue_date]
GO
ALTER TABLE [dbo].[Receipt] ADD  DEFAULT ((1)) FOR [is_active]
GO
ALTER TABLE [dbo].[ReceiptItem] ADD  DEFAULT ((1)) FOR [is_active]
GO
ALTER TABLE [dbo].[Stock] ADD  DEFAULT (CONVERT([date],getdate())) FOR [supply_date]
GO
ALTER TABLE [dbo].[Stock] ADD  DEFAULT (CONVERT([date],getdate())) FOR [expiration_date]
GO
ALTER TABLE [dbo].[Stock] ADD  DEFAULT ((1)) FOR [is_active]
GO
ALTER TABLE [dbo].[UserType] ADD  DEFAULT ('undefined') FOR [name]
GO
ALTER TABLE [dbo].[UserType] ADD  DEFAULT ((1)) FOR [is_active]
GO
ALTER TABLE [dbo].[MarketUser]  WITH CHECK ADD FOREIGN KEY([id_type])
REFERENCES [dbo].[UserType] ([id])
GO
ALTER TABLE [dbo].[Producer]  WITH CHECK ADD FOREIGN KEY([id_country])
REFERENCES [dbo].[Country] ([id])
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD FOREIGN KEY([id_category])
REFERENCES [dbo].[Category] ([id])
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD FOREIGN KEY([id_producer])
REFERENCES [dbo].[Producer] ([id])
GO
ALTER TABLE [dbo].[Receipt]  WITH CHECK ADD FOREIGN KEY([id_cashier])
REFERENCES [dbo].[MarketUser] ([id])
GO
ALTER TABLE [dbo].[ReceiptItem]  WITH CHECK ADD FOREIGN KEY([id_product])
REFERENCES [dbo].[Product] ([id])
GO
ALTER TABLE [dbo].[ReceiptItem]  WITH CHECK ADD FOREIGN KEY([id_receipt])
REFERENCES [dbo].[Receipt] ([id])
GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD FOREIGN KEY([id_product])
REFERENCES [dbo].[Product] ([id])
GO
/****** Object:  StoredProcedure [dbo].[AddNewStock]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddNewStock]
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
/****** Object:  StoredProcedure [dbo].[AddNewStockToday]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddNewStockToday]
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
/****** Object:  StoredProcedure [dbo].[CreateCategory]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CreateCategory]
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
/****** Object:  StoredProcedure [dbo].[CreateCountry]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CreateCountry]
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
/****** Object:  StoredProcedure [dbo].[CreateProducer]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CreateProducer]
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
/****** Object:  StoredProcedure [dbo].[CreateProduct]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CreateProduct]
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
/****** Object:  StoredProcedure [dbo].[CreateReceipt]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreateReceipt]
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
/****** Object:  StoredProcedure [dbo].[CreateReceiptItem]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreateReceiptItem]
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
GO
/****** Object:  StoredProcedure [dbo].[CreateUser]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CreateUser]
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
/****** Object:  StoredProcedure [dbo].[DeleteCategory]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteCategory]
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
/****** Object:  StoredProcedure [dbo].[DeleteCountry]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteCountry]
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
/****** Object:  StoredProcedure [dbo].[DeleteProducer]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteProducer]
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
	
	ELSE BEGIN SELECT 'There is no producer with this ID'; END
END;
GO
/****** Object:  StoredProcedure [dbo].[DeleteProduct]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteProduct]
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
/****** Object:  StoredProcedure [dbo].[DeleteStock]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteStock]
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
/****** Object:  StoredProcedure [dbo].[DeleteUser]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteUser]
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
/****** Object:  StoredProcedure [dbo].[DeleteUserByName]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteUserByName]
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
/****** Object:  StoredProcedure [dbo].[EditCategory]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EditCategory]
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
/****** Object:  StoredProcedure [dbo].[EditCountry]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EditCountry]
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
/****** Object:  StoredProcedure [dbo].[EditProducer]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EditProducer]
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
/****** Object:  StoredProcedure [dbo].[EditProduct]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EditProduct]
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
/****** Object:  StoredProcedure [dbo].[EditStock]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EditStock]
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
/****** Object:  StoredProcedure [dbo].[EditUser]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EditUser]
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
/****** Object:  StoredProcedure [dbo].[FindUser]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[FindUser]
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
/****** Object:  StoredProcedure [dbo].[GetActiveProducers]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetActiveProducers]
AS
BEGIN
	SELECT id, id_country, name
	FROM Producer
	WHERE is_active = 1;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetActiveProducts]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetActiveProducts]
AS
BEGIN
	SELECT id, name, barcode, id_category, id_producer
	FROM Product
	WHERE is_active = 1;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetActiveUsers]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetActiveUsers]
AS
BEGIN
	SELECT id, name, password_hash, id_type
	FROM MarketUser
	WHERE is_active = 1;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetAllCategories]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllCategories]
AS
BEGIN
	SELECT id, name
	FROM Category
	WHERE is_active = 1;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetAllCountries]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllCountries]
AS
BEGIN
	SELECT id, name
	FROM Country
	WHERE is_active = 1;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetAllProducers]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllProducers]
AS
BEGIN
	SELECT id, id_country, name
	FROM Producer;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetAllProducts]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllProducts]
AS
BEGIN
	SELECT id, name, barcode, id_category, id_producer
	FROM Product;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetAllReceipts]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllReceipts]
AS
BEGIN
	SELECT id, issue_date, id_cashier, total_price
	FROM Receipt
	WHERE is_active = 1
	ORDER BY issue_date DESC;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetAllStocks]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllStocks]
AS
BEGIN
	SELECT id, id_product, quantity, initial_quantity, unit, supply_date, expiration_date, purchase_price, selling_price
	FROM Stock
	WHERE is_active = 1;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetAllUsers]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllUsers]
AS
BEGIN
	SELECT id, name, password_hash, id_type
	FROM MarketUser;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetAllUserTypes]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllUserTypes]
AS
BEGIN
	SELECT id, name
	FROM UserType
	WHERE is_active = 1;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetLargestReceipt]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetLargestReceipt]
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
/****** Object:  StoredProcedure [dbo].[GetLastReceipt]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetLastReceipt]
AS
BEGIN
	SELECT TOP 1 id, issue_date, id_cashier, total_price
	FROM Receipt
	WHERE is_active = 1
	ORDER BY id DESC;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetProductsValueFiltered]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetProductsValueFiltered]
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
/****** Object:  StoredProcedure [dbo].[GetReceiptItems]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetReceiptItems]
	@ReceiptID INT
AS
BEGIN
	SELECT id, id_receipt, id_product, quantity, total_price
	FROM ReceiptItem
	WHERE is_active = 1
		AND id_receipt = @ReceiptID;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetReceiptsFiltered]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetReceiptsFiltered]
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
/****** Object:  StoredProcedure [dbo].[GetUserID]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetUserID]
	@Name NVARCHAR(64)
AS
BEGIN
	SELECT MarketUser.id
	FROM MarketUser
	WHERE is_active = 1
		AND name = @Name;
END;
GO
/****** Object:  StoredProcedure [dbo].[InvalidateEmptyStocks]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InvalidateEmptyStocks]
AS
BEGIN
	UPDATE stock
	SET is_active = 0
	WHERE quantity <= 0;
	
	SELECT 'Success';
END;
GO
/****** Object:  StoredProcedure [dbo].[InvalidateExpiredStocks]    Script Date: 28/05/2024 10:50:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InvalidateExpiredStocks]
AS
BEGIN
	UPDATE stock
	SET is_active = 0
	WHERE expiration_date < GETDATE();
	
	SELECT 'Success';
END;
GO
USE [master]
GO
ALTER DATABASE [Supermarket] SET  READ_WRITE 
GO
