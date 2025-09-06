/****** Object:  Table [dbo].[Order]    Script Date: 12/2/2023 12:17:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[id] [int] NOT NULL,
	[supershopid] [int] NOT NULL,
	[customerid] [int] NOT NULL,
	[orderdate] [datetime] NOT NULL,
	[paymentstatus] [bit] NOT NULL,
	[comment] [nvarchar](250) NULL,
	[rating] [float] NULL,
	[review] [nvarchar](250) NULL,
	[deliverystatus] [varchar](20) NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItem]    Script Date: 12/2/2023 12:17:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItem](
	[id] [int] NOT NULL,
	[supershopid] [int] NOT NULL,
	[orderid] [int] NOT NULL,
	[productid] [int] NOT NULL,
	[quantity] [int] NOT NULL,
	[price] [float] NOT NULL,
	[totalprice] [float] NOT NULL,
 CONSTRAINT [PK_OrderItem] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderPayment]    Script Date: 12/2/2023 12:17:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderPayment](
	[id] [int] NOT NULL,
	[supershopid] [int] NOT NULL,
	[paymentmethodid] [int] NOT NULL,
	[orderid] [int] NOT NULL,
	[amount] [float] NOT NULL,
 CONSTRAINT [PK_OrderPayment] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentMethod]    Script Date: 12/2/2023 12:17:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentMethod](
	[id] [int] NOT NULL,
	[supershopid] [int] NOT NULL,
	[methodname] [varchar](30) NOT NULL,
 CONSTRAINT [PK_PaymentMethod] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 12/2/2023 12:17:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[id] [int] NOT NULL,
	[supershopid] [int] NOT NULL,
	[producttypeid] [int] NOT NULL,
	[productname] [varchar](50) NOT NULL,
	[quantity] [int] NOT NULL,
	[price] [float] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductPurchase]    Script Date: 12/2/2023 12:17:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductPurchase](
	[id] [int] NOT NULL,
	[supershopid] [int] NOT NULL,
	[supplierid] [int] NOT NULL,
	[productid] [int] NOT NULL,
	[quantity] [int] NOT NULL,
	[price] [float] NOT NULL,
	[totalprice] [float] NOT NULL,
 CONSTRAINT [PK_ProductPurchase] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductType]    Script Date: 12/2/2023 12:17:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductType](
	[id] [int] NOT NULL,
	[supershopid] [int] NOT NULL,
	[typename] [varchar](30) NOT NULL,
 CONSTRAINT [PK_ProductType] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReviewRating]    Script Date: 12/2/2023 12:17:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReviewRating](
	[id] [int] NOT NULL,
	[supershopid] [int] NOT NULL,
	[orderitemid] [int] NOT NULL,
	[rating] [float] NULL,
	[review] [varchar](250) NULL,
 CONSTRAINT [PK_ReviewRating] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supershop]    Script Date: 12/2/2023 12:17:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supershop](
	[id] [int] NOT NULL,
	[name] [varchar](50) NOT NULL,
	[address] [nvarchar](250) NOT NULL,
	[contact] [varchar](20) NULL,
	[email] [nvarchar](30) NULL,
 CONSTRAINT [PK_Supershop] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 12/2/2023 12:17:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier](
	[id] [int] NOT NULL,
	[supershopid] [int] NOT NULL,
	[suppliername] [varchar](30) NOT NULL,
	[phone] [nvarchar](20) NOT NULL,
	[email] [nvarchar](30) NULL,
 CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 12/2/2023 12:17:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[id] [int] NOT NULL,
	[supershopid] [int] NOT NULL,
	[usertypeid] [int] NOT NULL,
	[username] [varchar](20) NOT NULL,
	[fullname] [varchar](50) NOT NULL,
	[phone] [nvarchar](20) NOT NULL,
	[email] [nvarchar](30) NULL,
	[password] [nvarchar](30) NOT NULL,
	[address] [nvarchar](250) NULL,
	[district] [varchar](30) NULL,
	[city] [varchar](30) NULL,
	[postcode] [varchar](10) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserType]    Script Date: 12/2/2023 12:17:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserType](
	[id] [int] NOT NULL,
	[supershopid] [int] NOT NULL,
	[typename] [varchar](20) NOT NULL,
 CONSTRAINT [PK_UserType] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[PaymentMethod] ([id], [supershopid], [methodname]) VALUES (1, 1, N'Cash')
GO
INSERT [dbo].[PaymentMethod] ([id], [supershopid], [methodname]) VALUES (2, 1, N'Card')
GO
INSERT [dbo].[PaymentMethod] ([id], [supershopid], [methodname]) VALUES (3, 1, N'bKash')
GO
INSERT [dbo].[ProductType] ([id], [supershopid], [typename]) VALUES (1, 1, N'Kitchen')
GO
INSERT [dbo].[ProductType] ([id], [supershopid], [typename]) VALUES (2, 1, N'Confectionary')
GO
INSERT [dbo].[ProductType] ([id], [supershopid], [typename]) VALUES (3, 1, N'Kutlery')
GO
INSERT [dbo].[ProductType] ([id], [supershopid], [typename]) VALUES (4, 1, N'Cosmetics')
GO
INSERT [dbo].[ProductType] ([id], [supershopid], [typename]) VALUES (5, 1, N'Garments')
GO
INSERT [dbo].[Supershop] ([id], [name], [address], [contact], [email]) VALUES (1, N'University Project Shop', N'Kuratoli, Dhaka', N'0170000000000', N'4friends@aiub.com')
GO
INSERT [dbo].[UserType] ([id], [supershopid], [typename]) VALUES (1, 1, N'Admin')
GO
INSERT [dbo].[UserType] ([id], [supershopid], [typename]) VALUES (2, 1, N'Customer')
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Supershop] FOREIGN KEY([supershopid])
REFERENCES [dbo].[Supershop] ([id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Supershop]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_User] FOREIGN KEY([customerid])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_User]
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_OrderItem_Order] FOREIGN KEY([orderid])
REFERENCES [dbo].[Order] ([id])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_Order]
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_OrderItem_Product] FOREIGN KEY([productid])
REFERENCES [dbo].[Product] ([id])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_Product]
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_OrderItem_Supershop] FOREIGN KEY([supershopid])
REFERENCES [dbo].[Supershop] ([id])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_Supershop]
GO
ALTER TABLE [dbo].[OrderPayment]  WITH CHECK ADD  CONSTRAINT [FK_OrderPayment_Order] FOREIGN KEY([orderid])
REFERENCES [dbo].[Order] ([id])
GO
ALTER TABLE [dbo].[OrderPayment] CHECK CONSTRAINT [FK_OrderPayment_Order]
GO
ALTER TABLE [dbo].[OrderPayment]  WITH CHECK ADD  CONSTRAINT [FK_OrderPayment_PaymentMethod] FOREIGN KEY([paymentmethodid])
REFERENCES [dbo].[PaymentMethod] ([id])
GO
ALTER TABLE [dbo].[OrderPayment] CHECK CONSTRAINT [FK_OrderPayment_PaymentMethod]
GO
ALTER TABLE [dbo].[OrderPayment]  WITH CHECK ADD  CONSTRAINT [FK_OrderPayment_Supershop] FOREIGN KEY([supershopid])
REFERENCES [dbo].[Supershop] ([id])
GO
ALTER TABLE [dbo].[OrderPayment] CHECK CONSTRAINT [FK_OrderPayment_Supershop]
GO
ALTER TABLE [dbo].[PaymentMethod]  WITH CHECK ADD  CONSTRAINT [FK_PaymentMethod_Supershop] FOREIGN KEY([supershopid])
REFERENCES [dbo].[Supershop] ([id])
GO
ALTER TABLE [dbo].[PaymentMethod] CHECK CONSTRAINT [FK_PaymentMethod_Supershop]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ProductType] FOREIGN KEY([producttypeid])
REFERENCES [dbo].[ProductType] ([id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_ProductType]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Supershop] FOREIGN KEY([supershopid])
REFERENCES [dbo].[Supershop] ([id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Supershop]
GO
ALTER TABLE [dbo].[ProductPurchase]  WITH CHECK ADD  CONSTRAINT [FK_ProductPurchase_Supershop] FOREIGN KEY([supershopid])
REFERENCES [dbo].[Supershop] ([id])
GO
ALTER TABLE [dbo].[ProductPurchase] CHECK CONSTRAINT [FK_ProductPurchase_Supershop]
GO
ALTER TABLE [dbo].[ProductPurchase]  WITH CHECK ADD  CONSTRAINT [FK_ProductPurchase_Supplier] FOREIGN KEY([supplierid])
REFERENCES [dbo].[Supplier] ([id])
GO
ALTER TABLE [dbo].[ProductPurchase] CHECK CONSTRAINT [FK_ProductPurchase_Supplier]
GO
ALTER TABLE [dbo].[ProductType]  WITH CHECK ADD  CONSTRAINT [FK_ProductType_Supershop] FOREIGN KEY([supershopid])
REFERENCES [dbo].[Supershop] ([id])
GO
ALTER TABLE [dbo].[ProductType] CHECK CONSTRAINT [FK_ProductType_Supershop]
GO
ALTER TABLE [dbo].[ReviewRating]  WITH CHECK ADD  CONSTRAINT [FK_ReviewRating_OrderItem] FOREIGN KEY([orderitemid])
REFERENCES [dbo].[OrderItem] ([id])
GO
ALTER TABLE [dbo].[ReviewRating] CHECK CONSTRAINT [FK_ReviewRating_OrderItem]
GO
ALTER TABLE [dbo].[ReviewRating]  WITH CHECK ADD  CONSTRAINT [FK_ReviewRating_Supershop] FOREIGN KEY([supershopid])
REFERENCES [dbo].[Supershop] ([id])
GO
ALTER TABLE [dbo].[ReviewRating] CHECK CONSTRAINT [FK_ReviewRating_Supershop]
GO
ALTER TABLE [dbo].[Supplier]  WITH CHECK ADD  CONSTRAINT [FK_Supplier_Supershop] FOREIGN KEY([supershopid])
REFERENCES [dbo].[Supershop] ([id])
GO
ALTER TABLE [dbo].[Supplier] CHECK CONSTRAINT [FK_Supplier_Supershop]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Supershop] FOREIGN KEY([supershopid])
REFERENCES [dbo].[Supershop] ([id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Supershop]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_UserType] FOREIGN KEY([usertypeid])
REFERENCES [dbo].[UserType] ([id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_UserType]
GO
ALTER TABLE [dbo].[UserType]  WITH CHECK ADD  CONSTRAINT [FK_UserType_Supershop] FOREIGN KEY([supershopid])
REFERENCES [dbo].[Supershop] ([id])
GO
ALTER TABLE [dbo].[UserType] CHECK CONSTRAINT [FK_UserType_Supershop]
GO
