GO

/****** Object:  Table [dbo].[t_Game]    Script Date: 04/17/2013 19:26:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

ALTER TABLE [dbo].[t_Game]
	ADD [Mean] [float] NOT NULL DEFAULT 0,
	[stdev] [float] NOT NULL DEFAULT 0

