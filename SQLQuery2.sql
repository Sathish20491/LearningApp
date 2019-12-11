USE [LearningApp]
GO

/****** Object:  Table [dbo].[imagedata]    Script Date: 12/11/2019 13:37:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[imagedata](
	[imgid] [int] IDENTITY(1,1) NOT NULL,
	[typeid] [int] NOT NULL,
	[id] [int] NOT NULL,
	[imageguid] [varchar](30) NOT NULL,
 CONSTRAINT [imagedata_imagedata_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[imgid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


