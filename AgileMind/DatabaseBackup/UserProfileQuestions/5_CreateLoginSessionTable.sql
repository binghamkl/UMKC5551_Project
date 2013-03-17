GO

/****** Object:  Table [dbo].[t_LoginSession]    Script Date: 03/17/2013 14:30:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[t_LoginSession](
	[LoginSessionId] [uniqueidentifier] NOT NULL,
	[LoginId] [int] NOT NULL,
	[ValidTill] [datetime] NOT NULL,
 CONSTRAINT [PK_t_LoginSession] PRIMARY KEY CLUSTERED 
(
	[LoginSessionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[t_LoginSession]  WITH CHECK ADD  CONSTRAINT [FK_t_LoginSession_Logins] FOREIGN KEY([LoginId])
REFERENCES [dbo].[Logins] ([LoginId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[t_LoginSession] CHECK CONSTRAINT [FK_t_LoginSession_Logins]
GO


