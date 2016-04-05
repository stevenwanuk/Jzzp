USE [RMS_DB]
GO
/****** Object:  Table [dbo].[bi_Bill]    Script Date: 05/04/2016 11:50:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[bi_Bill](
	[BillID] [varchar](50) NOT NULL,
	[TableID] [varchar](50) NULL,
	[SubTableID] [int] NULL,
	[TableName] [varchar](50) NULL,
	[TableFullName] [varchar](50) NULL,
	[TableAreaID] [varchar](50) NULL,
	[TableAreaName] [varchar](50) NULL,
	[TableTypeID] [varchar](50) NULL,
	[TableTypeName] [varchar](50) NULL,
	[MemberID] [varchar](50) NULL,
	[MemberCardID] [varchar](50) NULL,
	[MemberName] [varchar](50) NULL,
	[PeopleCount] [int] NULL,
	[ChargePersonID] [varchar](50) NULL,
	[ChargePerson] [varchar](50) NULL,
	[CreateTime] [datetime] NULL,
	[CreatePersonID] [varchar](50) NULL,
	[CreatePerson] [varchar](50) NULL,
	[IsCheckOuting] [bit] NULL,
	[CheckOutTime] [datetime] NULL,
	[CheckOutPersonID] [varchar](50) NULL,
	[CheckOutPerson] [varchar](50) NULL,
	[CheckOutNull] [bit] NULL,
	[DeleteTime] [datetime] NULL,
	[DeletePersonID] [varchar](50) NULL,
	[DeletePerson] [varchar](50) NULL,
	[DiscountID] [varchar](50) NULL,
	[DiscountName] [varchar](50) NULL,
	[DiscountRate] [money] NULL,
	[DiscountPersonID] [varchar](50) NULL,
	[DiscountPerson] [varchar](50) NULL,
	[SumOfConsume] [money] NULL,
	[ServiceRate] [money] NULL,
	[SumOfService] [money] NULL,
	[SumForDiscount] [money] NULL,
	[SumOfCarry] [money] NULL,
	[SumToPay] [money] NULL,
	[SumPaid] [money] NULL,
	[SumInCash] [money] NULL,
	[SumOfInvoice] [money] NULL,
	[SumOfCashPaid] [money] NULL,
	[SumOfCashBack] [money] NULL,
	[BillDate] [datetime] NULL,
	[BillPeriod] [varchar](50) NULL,
	[BillYear] [int] NULL,
	[BillMonth] [int] NULL,
	[BillDay] [int] NULL,
	[IsArchived] [bit] NULL,
	[IsUploaded] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[PrintCount] [int] NULL,
	[ReduceMantissa] [money] NULL,
	[BranchID] [varchar](50) NULL,
	[BranchName] [varchar](50) NULL,
	[WorkStationID] [varchar](50) NULL,
	[WorkStationName] [varchar](50) NULL,
	[Remark] [text] NULL,
	[OriginalID] [varchar](50) NULL,
	[ShiftID] [varchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[bi_BillItem]    Script Date: 05/04/2016 11:50:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[bi_BillItem](
	[BillItemID] [varchar](50) NOT NULL,
	[BillID] [varchar](50) NULL,
	[MenuID] [varchar](50) NULL,
	[MenuName] [varchar](50) NULL,
	[MenuUnitID] [varchar](50) NULL,
	[MenuUnitName] [varchar](50) NULL,
	[MenuTypeID] [varchar](50) NULL,
	[MenuTypeName] [varchar](50) NULL,
	[DepartID] [varchar](50) NULL,
	[DepartName] [varchar](50) NULL,
	[DepartTypeID] [varchar](50) NULL,
	[DepartTypeName] [varchar](50) NULL,
	[AmountOrder] [money] NULL,
	[AmountOnTable] [money] NULL,
	[AmountCancel] [money] NULL,
	[MenuPrice] [money] NULL,
	[MenuPrice2] [money] NULL,
	[SumOfConsume] [money] NULL,
	[SumForDiscount] [money] NULL,
	[SumOfService] [money] NULL,
	[SumOfCookWay] [money] NULL,
	[CookWayID] [varchar](255) NULL,
	[CookWay] [varchar](255) NULL,
	[CookWayPrice] [money] NULL,
	[MenuPart] [varchar](255) NULL,
	[Request] [varchar](255) NULL,
	[TasteType] [varchar](255) NULL,
	[CreatePersonID] [varchar](50) NULL,
	[CreatePerson] [varchar](50) NULL,
	[CreateTime] [datetime] NULL,
	[ServingState] [int] NULL,
	[IsSentMenu] [bit] NULL,
	[IsSent] [bit] NULL,
	[Remark] [text] NULL,
	[IsSpecialPrice] [bit] NULL,
	[IsDiscount] [bit] NULL,
	[DiscountRate] [money] NULL,
	[IsPrinted] [bit] NULL,
	[PrintTime] [datetime] NULL,
	[IsOnTable] [bit] NULL,
	[OnTableTime] [datetime] NULL,
	[MenuSetID] [varchar](255) NULL,
	[MenuSetName] [varchar](255) NULL,
	[MenuSetPrefix] [varchar](255) NULL,
	[MenuSetItemID] [varchar](255) NULL,
	[CostPrice] [money] NULL,
	[TaxRate] [money] NULL,
	[SumOfTax] [money] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[bi_BillPayment]    Script Date: 05/04/2016 11:50:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[bi_BillPayment](
	[BillID] [varchar](50) NOT NULL,
	[SubjectID] [varchar](50) NOT NULL,
	[SubjectName] [varchar](50) NULL,
	[ExchangeRate] [money] NULL,
	[PaySum] [money] NULL,
	[SumPaid] [money] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[bi_TempBill]    Script Date: 05/04/2016 11:50:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[bi_TempBill](
	[BillID] [varchar](50) NOT NULL,
	[TableID] [varchar](50) NULL,
	[SubTableID] [int] NULL,
	[TableName] [varchar](50) NULL,
	[TableFullName] [varchar](50) NULL,
	[TableAreaID] [varchar](50) NULL,
	[TableAreaName] [varchar](50) NULL,
	[TableTypeID] [varchar](50) NULL,
	[TableTypeName] [varchar](50) NULL,
	[MemberID] [varchar](50) NULL,
	[MemberCardID] [varchar](50) NULL,
	[MemberName] [varchar](50) NULL,
	[PeopleCount] [int] NULL,
	[ChargePersonID] [varchar](50) NULL,
	[ChargePerson] [varchar](50) NULL,
	[CreateTime] [datetime] NULL,
	[CreatePersonID] [varchar](50) NULL,
	[CreatePerson] [varchar](50) NULL,
	[IsCheckOuting] [bit] NULL,
	[CheckOutTime] [datetime] NULL,
	[CheckOutPersonID] [varchar](50) NULL,
	[CheckOutPerson] [varchar](50) NULL,
	[CheckOutNull] [bit] NULL,
	[DeleteTime] [datetime] NULL,
	[DeletePersonID] [varchar](50) NULL,
	[DeletePerson] [varchar](50) NULL,
	[DiscountID] [varchar](50) NULL,
	[DiscountName] [varchar](50) NULL,
	[DiscountRate] [money] NULL,
	[DiscountPersonID] [varchar](50) NULL,
	[DiscountPerson] [varchar](50) NULL,
	[SumOfConsume] [money] NULL,
	[ServiceRate] [money] NULL,
	[SumOfService] [money] NULL,
	[SumForDiscount] [money] NULL,
	[SumOfCarry] [money] NULL,
	[SumToPay] [money] NULL,
	[SumPaid] [money] NULL,
	[SumInCash] [money] NULL,
	[SumOfInvoice] [money] NULL,
	[SumOfCashPaid] [money] NULL,
	[SumOfCashBack] [money] NULL,
	[BillDate] [datetime] NULL,
	[BillPeriod] [varchar](50) NULL,
	[BillYear] [int] NULL,
	[BillMonth] [int] NULL,
	[BillDay] [int] NULL,
	[IsArchived] [bit] NULL,
	[IsUploaded] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[PrintCount] [int] NULL,
	[ReduceMantissa] [money] NULL,
	[BranchID] [varchar](50) NULL,
	[BranchName] [varchar](50) NULL,
	[WorkStationID] [varchar](50) NULL,
	[WorkStationName] [varchar](50) NULL,
	[Remark] [text] NULL,
	[OriginalID] [varchar](50) NULL,
	[ShiftID] [varchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[bi_TempBillItem]    Script Date: 05/04/2016 11:50:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[bi_TempBillItem](
	[BillItemID] [varchar](50) NOT NULL,
	[BillID] [varchar](50) NULL,
	[MenuID] [varchar](50) NULL,
	[MenuName] [varchar](50) NULL,
	[MenuUnitID] [varchar](50) NULL,
	[MenuUnitName] [varchar](50) NULL,
	[MenuTypeID] [varchar](50) NULL,
	[MenuTypeName] [varchar](50) NULL,
	[DepartID] [varchar](50) NULL,
	[DepartName] [varchar](50) NULL,
	[DepartTypeID] [varchar](50) NULL,
	[DepartTypeName] [varchar](50) NULL,
	[AmountOrder] [money] NULL,
	[AmountOnTable] [money] NULL,
	[AmountCancel] [money] NULL,
	[MenuPrice] [money] NULL,
	[MenuPrice2] [money] NULL,
	[SumOfConsume] [money] NULL,
	[SumForDiscount] [money] NULL,
	[SumOfService] [money] NULL,
	[SumOfCookWay] [money] NULL,
	[CookWayID] [varchar](255) NULL,
	[CookWay] [varchar](255) NULL,
	[CookWayPrice] [money] NULL,
	[MenuPart] [varchar](255) NULL,
	[Request] [varchar](255) NULL,
	[TasteType] [varchar](255) NULL,
	[CreatePersonID] [varchar](50) NULL,
	[CreatePerson] [varchar](50) NULL,
	[CreateTime] [datetime] NULL,
	[ServingState] [int] NULL,
	[IsSentMenu] [bit] NULL,
	[IsSent] [bit] NULL,
	[Remark] [text] NULL,
	[IsSpecialPrice] [bit] NULL,
	[IsDiscount] [bit] NULL,
	[DiscountRate] [money] NULL,
	[IsPrinted] [bit] NULL,
	[PrintTime] [datetime] NULL,
	[IsOnTable] [bit] NULL,
	[OnTableTime] [datetime] NULL,
	[MenuSetID] [varchar](255) NULL,
	[MenuSetName] [varchar](255) NULL,
	[MenuSetPrefix] [varchar](255) NULL,
	[MenuSetItemID] [varchar](255) NULL,
	[CostPrice] [money] NULL,
	[TaxRate] [money] NULL,
	[SumOfTax] [money] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TP_BillRef]    Script Date: 05/04/2016 11:50:56 ******/
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
	[DeliverMiles] [decimal](10, 2) NULL,
	[DeliverFee] [decimal](10, 2) NULL,
	[Status] [int] NOT NULL,
	[DeliverId_FK] [bigint] NULL,
 CONSTRAINT [PK_TP_BillRef] PRIMARY KEY CLUSTERED 
(
	[BillRefId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TP_CallIn]    Script Date: 05/04/2016 11:50:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TP_CallIn](
	[CallInId] [bigint] IDENTITY(1,1) NOT NULL,
	[CellNumber] [varchar](50) NOT NULL,
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
/****** Object:  Table [dbo].[TP_Deliver]    Script Date: 05/04/2016 11:50:56 ******/
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
/****** Object:  Table [dbo].[TP_Driver]    Script Date: 05/04/2016 11:50:56 ******/
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
/****** Object:  Table [dbo].[TP_PrintSetting]    Script Date: 05/04/2016 11:50:56 ******/
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
/****** Object:  Table [dbo].[TP_User]    Script Date: 05/04/2016 11:50:56 ******/
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
/****** Object:  Table [dbo].[TP_UserAddress]    Script Date: 05/04/2016 11:50:56 ******/
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
 CONSTRAINT [PK_UserAddress] PRIMARY KEY CLUSTERED 
(
	[UserAddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TP_UserCell]    Script Date: 05/04/2016 11:50:56 ******/
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
ALTER TABLE [dbo].[TP_BillRef]  WITH CHECK ADD  CONSTRAINT [FK_TP_BillRef_TP_CallIn] FOREIGN KEY([CallInId_FK])
REFERENCES [dbo].[TP_CallIn] ([CallInId])
GO
ALTER TABLE [dbo].[TP_BillRef] CHECK CONSTRAINT [FK_TP_BillRef_TP_CallIn]
GO
ALTER TABLE [dbo].[TP_BillRef]  WITH CHECK ADD  CONSTRAINT [FK_TP_BillRef_TP_Deliver] FOREIGN KEY([DeliverId_FK])
REFERENCES [dbo].[TP_Deliver] ([DeliverId])
GO
ALTER TABLE [dbo].[TP_BillRef] CHECK CONSTRAINT [FK_TP_BillRef_TP_Deliver]
GO
ALTER TABLE [dbo].[TP_BillRef]  WITH CHECK ADD  CONSTRAINT [FK_TP_BillRef_TP_User] FOREIGN KEY([UserId_FK])
REFERENCES [dbo].[TP_User] ([UserId])
GO
ALTER TABLE [dbo].[TP_BillRef] CHECK CONSTRAINT [FK_TP_BillRef_TP_User]
GO
ALTER TABLE [dbo].[TP_BillRef]  WITH CHECK ADD  CONSTRAINT [FK_TP_BillRef_TP_UserAddress] FOREIGN KEY([AddressId_FK])
REFERENCES [dbo].[TP_UserAddress] ([UserAddressId])
GO
ALTER TABLE [dbo].[TP_BillRef] CHECK CONSTRAINT [FK_TP_BillRef_TP_UserAddress]
GO
ALTER TABLE [dbo].[TP_Deliver]  WITH CHECK ADD  CONSTRAINT [FK_TP_Deliver_TP_Driver] FOREIGN KEY([DriverId_FK])
REFERENCES [dbo].[TP_Driver] ([DriverId])
GO
ALTER TABLE [dbo].[TP_Deliver] CHECK CONSTRAINT [FK_TP_Deliver_TP_Driver]
GO
ALTER TABLE [dbo].[TP_UserAddress]  WITH CHECK ADD  CONSTRAINT [FK_TP_UserAddress_TP_User] FOREIGN KEY([UserId_FK])
REFERENCES [dbo].[TP_User] ([UserId])
GO
ALTER TABLE [dbo].[TP_UserAddress] CHECK CONSTRAINT [FK_TP_UserAddress_TP_User]
GO
ALTER TABLE [dbo].[TP_UserCell]  WITH CHECK ADD  CONSTRAINT [FK_TP_UserCell_TP_UserCell] FOREIGN KEY([UserId_FK])
REFERENCES [dbo].[TP_User] ([UserId])
GO
ALTER TABLE [dbo].[TP_UserCell] CHECK CONSTRAINT [FK_TP_UserCell_TP_UserCell]
GO
