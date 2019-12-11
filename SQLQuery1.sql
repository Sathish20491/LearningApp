USE [LearningApp]
GO

/****** Object:  Table [dbo].[author]    Script Date: 12/11/2019 13:36:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[author](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[subjectid] [int] NOT NULL,
	[fullname] [varchar](30) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[mobile] [varchar](20) NOT NULL,
	[email] [varchar](40) NOT NULL,
	[createddate] [datetime] NOT NULL,
	[createdby] [int] NOT NULL,
	[updateddate] [datetime] NULL,
	[updatedby] [int] NULL,
	[isdeleted] [bigint] NOT NULL,
 CONSTRAINT [author_author_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[author] ADD  DEFAULT ((0)) FOR [isdeleted]
GO


