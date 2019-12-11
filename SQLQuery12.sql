USE [LearningApp]
GO

/****** Object:  Table [dbo].[viewlist]    Script Date: 12/11/2019 13:39:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[viewlist](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userid] [int] NOT NULL,
	[subjectid] [int] NOT NULL,
	[topicsid] [int] NOT NULL,
	[subtopicsid] [int] NOT NULL,
	[videoid] [int] NOT NULL,
	[starttime] [datetime] NOT NULL,
	[endtime] [datetime] NOT NULL,
	[completed] [bigint] NOT NULL,
 CONSTRAINT [viewlist_viewlist_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[viewlist] ADD  DEFAULT ((0)) FOR [completed]
GO


