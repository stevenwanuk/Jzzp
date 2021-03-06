USE [RMS_DB]
GO
/****** Object:  Table [dbo].[TP_Address]    Script Date: 2016/5/15 17:30:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TP_Address](
	[AddressId] [bigint] IDENTITY(1,1) NOT NULL,
	[HouseNumber] [varchar](10) NULL,
	[AddressField1] [varchar](50) NULL,
	[AddressField2] [varchar](50) NULL,
	[AddressField3] [varchar](50) NULL,
	[TownCity] [varchar](50) NULL,
	[Postcode] [varchar](50) NULL,
	[Country] [varchar](50) NULL,
	[DeliveryMiles] [decimal](10, 2) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TP_BillRef]    Script Date: 2016/5/15 17:30:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TP_BillRef](
	[BillRefId] [bigint] IDENTITY(1,1) NOT NULL,
	[CallInId_FK] [bigint] NULL,
	[UserId_FK] [uniqueidentifier] NULL,
	[AddressId_FK] [bigint] NULL,
	[BillId_FK] [varchar](50) NULL,
	[DeliverFeeOrigin] [decimal](10, 2) NULL,
	[DeliverMiles] [decimal](10, 2) NULL,
	[DeliverFee] [decimal](10, 2) NULL,
	[Status] [int] NOT NULL,
	[DeliverId_FK] [bigint] NULL,
	[ShowOnMain] [bit] NOT NULL CONSTRAINT [DF_TP_BillRef_ShowOnMain]  DEFAULT ((1)),
 CONSTRAINT [PK_TP_BillRef] PRIMARY KEY CLUSTERED 
(
	[BillRefId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TP_CallIn]    Script Date: 2016/5/15 17:30:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TP_CallIn](
	[CallInId] [bigint] IDENTITY(1,1) NOT NULL,
	[CellNumber] [varchar](50) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[TerminalId] [int] NOT NULL,
 CONSTRAINT [PK_CallIn] PRIMARY KEY CLUSTERED 
(
	[CallInId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TP_Deliver]    Script Date: 2016/5/15 17:30:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TP_Deliver](
	[DeliverId] [bigint] IDENTITY(1,1) NOT NULL,
	[DriverId_FK] [bigint] NOT NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_TP_Delivers] PRIMARY KEY CLUSTERED 
(
	[DeliverId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TP_Driver]    Script Date: 2016/5/15 17:30:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TP_Driver](
	[DriverId] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[CellName] [varchar](50) NULL,
 CONSTRAINT [PK_TP_Driver] PRIMARY KEY CLUSTERED 
(
	[DriverId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TP_PrintSetting]    Script Date: 2016/5/15 17:30:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TP_PrintSetting](
	[PrintSettingId] [bigint] NOT NULL,
	[name] [varchar](50) NULL,
	[IsPrint] [tinyint] NOT NULL,
 CONSTRAINT [PK_TP_PrintSetting] PRIMARY KEY CLUSTERED 
(
	[PrintSettingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TP_User]    Script Date: 2016/5/15 17:30:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TP_User](
	[UserId] [uniqueidentifier] NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Gender] [int] NULL,
 CONSTRAINT [PK_TP_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TP_UserAddress]    Script Date: 2016/5/15 17:30:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TP_UserAddress](
	[UserAddressId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId_FK] [uniqueidentifier] NOT NULL,
	[HouseNumber] [varchar](10) NULL,
	[AddressField1] [varchar](50) NULL,
	[AddressField2] [varchar](50) NULL,
	[AddressField3] [varchar](50) NULL,
	[TownCity] [varchar](50) NULL,
	[Postcode] [varchar](50) NULL,
	[Country] [varchar](50) NULL,
	[DeliveryMiles] [decimal](10, 2) NULL,
 CONSTRAINT [PK_UserAddress] PRIMARY KEY CLUSTERED 
(
	[UserAddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TP_UserCell]    Script Date: 2016/5/15 17:30:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TP_UserCell](
	[UserCellId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId_FK] [uniqueidentifier] NOT NULL,
	[CellNumber] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TP_UserCell] PRIMARY KEY CLUSTERED 
(
	[UserCellId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[TP_BillRef]  WITH NOCHECK ADD  CONSTRAINT [FK_TP_BillRef_TP_CallIn] FOREIGN KEY([CallInId_FK])
REFERENCES [dbo].[TP_CallIn] ([CallInId])
GO
ALTER TABLE [dbo].[TP_BillRef] CHECK CONSTRAINT [FK_TP_BillRef_TP_CallIn]
GO
ALTER TABLE [dbo].[TP_BillRef]  WITH NOCHECK ADD  CONSTRAINT [FK_TP_BillRef_TP_Deliver] FOREIGN KEY([DeliverId_FK])
REFERENCES [dbo].[TP_Deliver] ([DeliverId])
GO
ALTER TABLE [dbo].[TP_BillRef] CHECK CONSTRAINT [FK_TP_BillRef_TP_Deliver]
GO
ALTER TABLE [dbo].[TP_BillRef]  WITH NOCHECK ADD  CONSTRAINT [FK_TP_BillRef_TP_User] FOREIGN KEY([UserId_FK])
REFERENCES [dbo].[TP_User] ([UserId])
GO
ALTER TABLE [dbo].[TP_BillRef] CHECK CONSTRAINT [FK_TP_BillRef_TP_User]
GO
ALTER TABLE [dbo].[TP_BillRef]  WITH NOCHECK ADD  CONSTRAINT [FK_TP_BillRef_TP_UserAddress] FOREIGN KEY([AddressId_FK])
REFERENCES [dbo].[TP_UserAddress] ([UserAddressId])
GO
ALTER TABLE [dbo].[TP_BillRef] CHECK CONSTRAINT [FK_TP_BillRef_TP_UserAddress]
GO
ALTER TABLE [dbo].[TP_Deliver]  WITH CHECK ADD  CONSTRAINT [FK_TP_Deliver_TP_Driver] FOREIGN KEY([DriverId_FK])
REFERENCES [dbo].[TP_Driver] ([DriverId])
GO
ALTER TABLE [dbo].[TP_Deliver] CHECK CONSTRAINT [FK_TP_Deliver_TP_Driver]
GO
ALTER TABLE [dbo].[TP_UserAddress]  WITH NOCHECK ADD  CONSTRAINT [FK_TP_UserAddress_TP_User] FOREIGN KEY([UserId_FK])
REFERENCES [dbo].[TP_User] ([UserId])
GO
ALTER TABLE [dbo].[TP_UserAddress] CHECK CONSTRAINT [FK_TP_UserAddress_TP_User]
GO
ALTER TABLE [dbo].[TP_UserCell]  WITH CHECK ADD  CONSTRAINT [FK_TP_UserCell_TP_UserCell] FOREIGN KEY([UserId_FK])
REFERENCES [dbo].[TP_User] ([UserId])
GO
ALTER TABLE [dbo].[TP_UserCell] CHECK CONSTRAINT [FK_TP_UserCell_TP_UserCell]
GO
