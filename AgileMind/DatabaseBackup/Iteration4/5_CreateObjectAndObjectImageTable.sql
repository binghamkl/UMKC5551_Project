GO

/****** Object:  Table [dbo].[t_Object]    Script Date: 05/04/2013 12:51:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[t_Object](
	[ObjectId] [int] NOT NULL,
	[Object] [varchar](40) NOT NULL,
	[Created] [datetime] NOT NULL,
 CONSTRAINT [PK_Object] PRIMARY KEY CLUSTERED 
(
	[ObjectId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


GO

/****** Object:  Table [dbo].[t_ObjectImage]    Script Date: 05/04/2013 12:51:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[t_ObjectImage](
	[ObjectImageId] [int] NOT NULL,
	[ObjectId] [int] NOT NULL,
	[ImageURL] [varchar](200) NOT NULL,
	[Created] [datetime] NOT NULL,
 CONSTRAINT [PK_t_ObjectImage] PRIMARY KEY CLUSTERED 
(
	[ObjectImageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[t_ObjectImage]  WITH CHECK ADD  CONSTRAINT [FK_t_ObjectImage_t_Object] FOREIGN KEY([ObjectId])
REFERENCES [dbo].[t_Object] ([ObjectId])
GO

ALTER TABLE [dbo].[t_ObjectImage] CHECK CONSTRAINT [FK_t_ObjectImage_t_Object]
GO

