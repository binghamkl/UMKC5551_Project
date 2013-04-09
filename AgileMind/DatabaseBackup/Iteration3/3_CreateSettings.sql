GO

/****** Object:  Table [dbo].[t_Settings]    Script Date: 04/06/2013 17:16:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[t_Settings](
	[SettingsId] [int] NOT NULL,
	[Setting] [varchar](30) NOT NULL,
	[Value] [varchar](30) NOT NULL,
	[Created] [datetime] NOT NULL,
 CONSTRAINT [PK_t_Settings] PRIMARY KEY CLUSTERED 
(
	[SettingsId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


INSERT INTO [AgileMind].[dbo].[t_Settings]
           ([SettingsId]
           ,[Setting]
           ,[Value]
           ,[Created])
     VALUES
           (1
           ,''
           ,'60'
           ,GETDATE())
GO

