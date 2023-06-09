USE [master]
GO
/****** Object:  Database [bookstore]    Script Date: 24-Apr-23 2:51:46 AM ******/
CREATE DATABASE [bookstore]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'bookstore', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\bookstore.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'bookstore_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\bookstore_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [bookstore] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [bookstore].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [bookstore] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [bookstore] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [bookstore] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [bookstore] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [bookstore] SET ARITHABORT OFF 
GO
ALTER DATABASE [bookstore] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [bookstore] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [bookstore] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [bookstore] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [bookstore] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [bookstore] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [bookstore] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [bookstore] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [bookstore] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [bookstore] SET  ENABLE_BROKER 
GO
ALTER DATABASE [bookstore] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [bookstore] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [bookstore] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [bookstore] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [bookstore] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [bookstore] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [bookstore] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [bookstore] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [bookstore] SET  MULTI_USER 
GO
ALTER DATABASE [bookstore] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [bookstore] SET DB_CHAINING OFF 
GO
ALTER DATABASE [bookstore] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [bookstore] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [bookstore] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [bookstore] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [bookstore] SET QUERY_STORE = OFF
GO
USE [bookstore]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 24-Apr-23 2:51:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Books]    Script Date: 24-Apr-23 2:51:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Author] [nvarchar](max) NOT NULL,
	[Image] [nvarchar](max) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Inventory] [int] NOT NULL,
	[Genre] [nvarchar](max) NOT NULL,
	[AverageRating] [real] NOT NULL,
	[RatingCount] [int] NOT NULL,
	[TotalRating] [int] NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModificationDate] [datetime2](7) NULL,
	[ModificationBy] [uniqueidentifier] NULL,
	[DeletionDate] [datetime2](7) NULL,
	[DeleteBy] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Carts]    Script Date: 24-Apr-23 2:51:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carts](
	[UserId] [uniqueidentifier] NOT NULL,
	[BookId] [uniqueidentifier] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModificationDate] [datetime2](7) NULL,
	[ModificationBy] [uniqueidentifier] NULL,
	[DeletionDate] [datetime2](7) NULL,
	[DeleteBy] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Carts] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 24-Apr-23 2:51:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[BookId] [uniqueidentifier] NOT NULL,
	[OrderId] [uniqueidentifier] NOT NULL,
	[Quantity] [int] NOT NULL,
	[ReviewSubmitted] [bit] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModificationDate] [datetime2](7) NULL,
	[ModificationBy] [uniqueidentifier] NULL,
	[DeletionDate] [datetime2](7) NULL,
	[DeleteBy] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC,
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 24-Apr-23 2:51:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[OrderDate] [datetime2](7) NOT NULL,
	[ShippingAddress] [nvarchar](max) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
	[TotalPrice] [decimal](18, 2) NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModificationDate] [datetime2](7) NULL,
	[ModificationBy] [uniqueidentifier] NULL,
	[DeletionDate] [datetime2](7) NULL,
	[DeleteBy] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reviews]    Script Date: 24-Apr-23 2:51:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reviews](
	[Id] [uniqueidentifier] NOT NULL,
	[BookId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Rating] [int] NOT NULL,
	[Comments] [nvarchar](max) NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModificationDate] [datetime2](7) NULL,
	[ModificationBy] [uniqueidentifier] NULL,
	[DeletionDate] [datetime2](7) NULL,
	[DeleteBy] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Reviews] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 24-Apr-23 2:51:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NULL,
	[CreditBalance] [decimal](18, 2) NOT NULL,
	[Role] [int] NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModificationDate] [datetime2](7) NULL,
	[ModificationBy] [uniqueidentifier] NULL,
	[DeletionDate] [datetime2](7) NULL,
	[DeleteBy] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WishLists]    Script Date: 24-Apr-23 2:51:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WishLists](
	[BookId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModificationDate] [datetime2](7) NULL,
	[ModificationBy] [uniqueidentifier] NULL,
	[DeletionDate] [datetime2](7) NULL,
	[DeleteBy] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_WishLists] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230423194141_CreateDatabase', N'7.0.2')
GO
INSERT [dbo].[Books] ([Id], [Title], [Author], [Image], [Price], [Inventory], [Genre], [AverageRating], [RatingCount], [TotalRating], [CreationDate], [CreatedBy], [ModificationDate], [ModificationBy], [DeletionDate], [DeleteBy], [IsDeleted]) VALUES (N'00000001-0000-0000-0000-000000000000', N'System design interview', N'Alex Xu', N'Image1', CAST(500.00 AS Decimal(18, 2)), 10, N'Programming', 0, 0, 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Books] ([Id], [Title], [Author], [Image], [Price], [Inventory], [Genre], [AverageRating], [RatingCount], [TotalRating], [CreationDate], [CreatedBy], [ModificationDate], [ModificationBy], [DeletionDate], [DeleteBy], [IsDeleted]) VALUES (N'00000002-0000-0000-0000-000000000000', N'Code Writing ', N'David', N'Image2', CAST(500.00 AS Decimal(18, 2)), 100, N'Programming', 5, 5, 50, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Books] ([Id], [Title], [Author], [Image], [Price], [Inventory], [Genre], [AverageRating], [RatingCount], [TotalRating], [CreationDate], [CreatedBy], [ModificationDate], [ModificationBy], [DeletionDate], [DeleteBy], [IsDeleted]) VALUES (N'00000003-0000-0000-0000-000000000000', N'Science Applications', N'Mark', N'Image3', CAST(600.00 AS Decimal(18, 2)), 150, N'English', 0, 0, 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Books] ([Id], [Title], [Author], [Image], [Price], [Inventory], [Genre], [AverageRating], [RatingCount], [TotalRating], [CreationDate], [CreatedBy], [ModificationDate], [ModificationBy], [DeletionDate], [DeleteBy], [IsDeleted]) VALUES (N'00000004-0000-0000-0000-000000000000', N'Education of Computer Science', N'Bob', N'Image3', CAST(700.00 AS Decimal(18, 2)), 10, N'Programming', 0, 0, 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL, NULL, 0)
GO
INSERT [dbo].[OrderDetails] ([BookId], [OrderId], [Quantity], [ReviewSubmitted], [Id], [CreationDate], [CreatedBy], [ModificationDate], [ModificationBy], [DeletionDate], [DeleteBy], [IsDeleted]) VALUES (N'00000001-0000-0000-0000-000000000000', N'00000001-0000-0000-0000-000000000001', 2, 1, N'00000001-0000-0000-0000-000000000000', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[OrderDetails] ([BookId], [OrderId], [Quantity], [ReviewSubmitted], [Id], [CreationDate], [CreatedBy], [ModificationDate], [ModificationBy], [DeletionDate], [DeleteBy], [IsDeleted]) VALUES (N'00000002-0000-0000-0000-000000000000', N'00000002-0000-0000-0000-000000000002', 1, 0, N'00000002-0000-0000-0000-000000000000', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[OrderDetails] ([BookId], [OrderId], [Quantity], [ReviewSubmitted], [Id], [CreationDate], [CreatedBy], [ModificationDate], [ModificationBy], [DeletionDate], [DeleteBy], [IsDeleted]) VALUES (N'00000003-0000-0000-0000-000000000000', N'00000002-0000-0000-0000-000000000002', 1, 0, N'00000003-0000-0000-0000-000000000003', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL, NULL, 0)
GO
INSERT [dbo].[Orders] ([Id], [UserId], [OrderDate], [ShippingAddress], [Status], [TotalPrice], [CreationDate], [CreatedBy], [ModificationDate], [ModificationBy], [DeletionDate], [DeleteBy], [IsDeleted]) VALUES (N'00000001-0000-0000-0000-000000000001', N'00000001-1000-0000-0000-000000000000', CAST(N'2022-04-15T00:00:00.0000000' AS DateTime2), N'1234 Main St, New York, NY', N'Delivered', CAST(1500.00 AS Decimal(18, 2)), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Orders] ([Id], [UserId], [OrderDate], [ShippingAddress], [Status], [TotalPrice], [CreationDate], [CreatedBy], [ModificationDate], [ModificationBy], [DeletionDate], [DeleteBy], [IsDeleted]) VALUES (N'00000002-0000-0000-0000-000000000002', N'00000001-2000-0000-0000-000000000000', CAST(N'2022-04-16T00:00:00.0000000' AS DateTime2), N'5678 Main St, New York, NY', N'Shipped', CAST(500.00 AS Decimal(18, 2)), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Orders] ([Id], [UserId], [OrderDate], [ShippingAddress], [Status], [TotalPrice], [CreationDate], [CreatedBy], [ModificationDate], [ModificationBy], [DeletionDate], [DeleteBy], [IsDeleted]) VALUES (N'00000003-0000-0000-0000-000000000003', N'00000001-2000-0000-0000-000000000000', CAST(N'2022-04-17T00:00:00.0000000' AS DateTime2), N'9012 Main St, New York, NY', N'Processing', CAST(300.00 AS Decimal(18, 2)), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL, NULL, 0)
GO
INSERT [dbo].[Reviews] ([Id], [BookId], [UserId], [Rating], [Comments], [CreationDate], [CreatedBy], [ModificationDate], [ModificationBy], [DeletionDate], [DeleteBy], [IsDeleted]) VALUES (N'00000001-1000-0000-0000-000000000000', N'00000001-0000-0000-0000-000000000000', N'00000001-1000-0000-0000-000000000000', 4, N'Great book!', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Reviews] ([Id], [BookId], [UserId], [Rating], [Comments], [CreationDate], [CreatedBy], [ModificationDate], [ModificationBy], [DeletionDate], [DeleteBy], [IsDeleted]) VALUES (N'00000002-1000-0000-0000-000000000000', N'00000002-0000-0000-0000-000000000000', N'00000001-1000-0000-0000-000000000000', 3, N'It''s ok', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Reviews] ([Id], [BookId], [UserId], [Rating], [Comments], [CreationDate], [CreatedBy], [ModificationDate], [ModificationBy], [DeletionDate], [DeleteBy], [IsDeleted]) VALUES (N'00000003-1000-0000-0000-000000000000', N'00000003-0000-0000-0000-000000000000', N'00000001-2000-0000-0000-000000000000', 5, N'Love it!', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Reviews] ([Id], [BookId], [UserId], [Rating], [Comments], [CreationDate], [CreatedBy], [ModificationDate], [ModificationBy], [DeletionDate], [DeleteBy], [IsDeleted]) VALUES (N'00000004-1000-0000-0000-000000000000', N'00000004-0000-0000-0000-000000000000', N'00000001-2000-0000-0000-000000000000', 4, N'Very informative', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL, NULL, 0)
GO
INSERT [dbo].[Users] ([Id], [Name], [Email], [Password], [Address], [CreditBalance], [Role], [CreationDate], [CreatedBy], [ModificationDate], [ModificationBy], [DeletionDate], [DeleteBy], [IsDeleted]) VALUES (N'00000001-1000-0000-0000-000000000000', N'Võ Thương Trường Nhơn', N'vothuongtruongnhon2002@gmail.com', N'Password', N'69 Bùi Thị Xuân', CAST(10000.00 AS Decimal(18, 2)), 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Users] ([Id], [Name], [Email], [Password], [Address], [CreditBalance], [Role], [CreationDate], [CreatedBy], [ModificationDate], [ModificationBy], [DeletionDate], [DeleteBy], [IsDeleted]) VALUES (N'00000001-2000-0000-0000-000000000000', N'Võ Nhơn 2:40 còn code', N'fptvttnhon2018@gmail.com', N'Password', N'69 Bùi Thị Xuân', CAST(500.00 AS Decimal(18, 2)), 1, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL, NULL, 0)
GO
/****** Object:  Index [IX_Carts_BookId]    Script Date: 24-Apr-23 2:51:46 AM ******/
CREATE NONCLUSTERED INDEX [IX_Carts_BookId] ON [dbo].[Carts]
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderDetails_BookId]    Script Date: 24-Apr-23 2:51:46 AM ******/
CREATE NONCLUSTERED INDEX [IX_OrderDetails_BookId] ON [dbo].[OrderDetails]
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Orders_UserId]    Script Date: 24-Apr-23 2:51:46 AM ******/
CREATE NONCLUSTERED INDEX [IX_Orders_UserId] ON [dbo].[Orders]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Reviews_BookId]    Script Date: 24-Apr-23 2:51:46 AM ******/
CREATE NONCLUSTERED INDEX [IX_Reviews_BookId] ON [dbo].[Reviews]
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Reviews_UserId]    Script Date: 24-Apr-23 2:51:46 AM ******/
CREATE NONCLUSTERED INDEX [IX_Reviews_UserId] ON [dbo].[Reviews]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_WishLists_BookId]    Script Date: 24-Apr-23 2:51:46 AM ******/
CREATE NONCLUSTERED INDEX [IX_WishLists_BookId] ON [dbo].[WishLists]
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Carts]  WITH CHECK ADD  CONSTRAINT [FK_Carts_Books_BookId] FOREIGN KEY([BookId])
REFERENCES [dbo].[Books] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Carts] CHECK CONSTRAINT [FK_Carts_Books_BookId]
GO
ALTER TABLE [dbo].[Carts]  WITH CHECK ADD  CONSTRAINT [FK_Carts_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Carts] CHECK CONSTRAINT [FK_Carts_Users_UserId]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Books_BookId] FOREIGN KEY([BookId])
REFERENCES [dbo].[Books] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Books_BookId]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Orders_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Orders_OrderId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Users_UserId]
GO
ALTER TABLE [dbo].[Reviews]  WITH CHECK ADD  CONSTRAINT [FK_Reviews_Books_BookId] FOREIGN KEY([BookId])
REFERENCES [dbo].[Books] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Reviews] CHECK CONSTRAINT [FK_Reviews_Books_BookId]
GO
ALTER TABLE [dbo].[Reviews]  WITH CHECK ADD  CONSTRAINT [FK_Reviews_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Reviews] CHECK CONSTRAINT [FK_Reviews_Users_UserId]
GO
ALTER TABLE [dbo].[WishLists]  WITH CHECK ADD  CONSTRAINT [FK_WishLists_Books_BookId] FOREIGN KEY([BookId])
REFERENCES [dbo].[Books] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WishLists] CHECK CONSTRAINT [FK_WishLists_Books_BookId]
GO
ALTER TABLE [dbo].[WishLists]  WITH CHECK ADD  CONSTRAINT [FK_WishLists_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WishLists] CHECK CONSTRAINT [FK_WishLists_Users_UserId]
GO
USE [master]
GO
ALTER DATABASE [bookstore] SET  READ_WRITE 
GO
