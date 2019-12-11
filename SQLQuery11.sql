USE [LearningApp]
GO

/****** Object:  Table [dbo].[videodata]    Script Date: 12/11/2019 13:39:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[videodata](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userid] [int] NOT NULL,
	[authorid] [int] NOT NULL,
	[standardid] [int] NOT NULL,
	[subjectid] [int] NOT NULL,
	[topicsid] [int] NOT NULL,
	[subtopicsid] [int] NOT NULL,
	[videoid] [int] NOT NULL,
	[seqno] [int] NOT NULL,
 CONSTRAINT [videodata_videodata_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


