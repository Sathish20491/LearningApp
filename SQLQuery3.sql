USE [LearningApp]
GO

/****** Object:  Table [dbo].[payment]    Script Date: 12/11/2019 13:37:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[payment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userid] [int] NOT NULL,
	[subjectid] [int] NOT NULL,
	[amount] [float] NOT NULL,
	[validfrom] [datetime] NOT NULL,
	[validto] [datetime] NOT NULL,
	[referenceno] [int] NOT NULL,
	[paymentmode] [varchar](100) NOT NULL,
	[paymentstatus] [varchar](100) NOT NULL,
 CONSTRAINT [payment_payment_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


