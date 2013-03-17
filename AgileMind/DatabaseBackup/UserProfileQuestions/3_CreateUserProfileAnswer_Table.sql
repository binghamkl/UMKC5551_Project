GO

/****** Object:  Table [dbo].[t_UserProfileAnswer]    Script Date: 03/17/2013 13:30:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[t_UserProfileAnswer](
	[UserProfileAnswerId] [int] IDENTITY(1,1) NOT NULL,
	[LoginId] [int] NOT NULL,
	[UserProfileQuestionId] [int] NOT NULL,
	[Answer] [varchar](30) NOT NULL,
	[Created] [datetime] NOT NULL,
	[NoAnswer] [bit] NOT NULL,
 CONSTRAINT [PK_t_UserProfileAnswer] PRIMARY KEY CLUSTERED 
(
	[UserProfileAnswerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[t_UserProfileAnswer]  WITH CHECK ADD  CONSTRAINT [FK_t_UserProfileAnswer_Logins] FOREIGN KEY([LoginId])
REFERENCES [dbo].[Logins] ([LoginId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[t_UserProfileAnswer] CHECK CONSTRAINT [FK_t_UserProfileAnswer_Logins]
GO

ALTER TABLE [dbo].[t_UserProfileAnswer]  WITH CHECK ADD  CONSTRAINT [FK_t_UserProfileAnswer_t_UserProfileQuestion] FOREIGN KEY([UserProfileQuestionId])
REFERENCES [dbo].[t_UserProfileQuestion] ([UserProfileQuestionId])
GO

ALTER TABLE [dbo].[t_UserProfileAnswer] CHECK CONSTRAINT [FK_t_UserProfileAnswer_t_UserProfileQuestion]
GO

ALTER TABLE [dbo].[t_UserProfileAnswer] ADD  CONSTRAINT [DF_t_UserProfileAnswer_Created]  DEFAULT (getdate()) FOR [Created]
GO

ALTER TABLE [dbo].[t_UserProfileAnswer] ADD  CONSTRAINT [DF_t_UserProfileAnswer_NoAnswer]  DEFAULT ((1)) FOR [NoAnswer]
GO


