USE [LearningApp]
GO

/****** Object:  Table [dbo].[standard]    Script Date: 12/11/2019 13:38:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[standard](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](30) NOT NULL,
	[seqno] [int] NOT NULL,
	[createddate] [datetime] NOT NULL,
	[createdby] [int] NOT NULL,
	[updateddate] [datetime] NULL,
	[updatedby] [int] NULL,
	[isdeleted] [bigint] NOT NULL,
 CONSTRAINT [standard_standard_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[standard] ADD  DEFAULT ((0)) FOR [isdeleted]
GO


