
/****** Object:  Table [dbo].[t_UserProfileQuestion]    Script Date: 03/17/2013 13:11:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[t_UserProfileQuestion](
	[UserProfileQuestionId] [int] NOT NULL,
	[Question] [varchar](75) NOT NULL,
	[Order] [tinyint] NOT NULL,
	[Created] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_t_UserProfileQuestionId] PRIMARY KEY CLUSTERED 
(
	[UserProfileQuestionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[t_UserProfileQuestion] ADD  CONSTRAINT [DF_t_UserProfileQuestionId_Created]  DEFAULT (getdate()) FOR [Created]
GO

ALTER TABLE [dbo].[t_UserProfileQuestion] ADD  CONSTRAINT [DF_t_UserProfileQuestion_Active]  DEFAULT ((1)) FOR [Active]
GO


/****** Object:  Index [IX_t_UserProfileQuestionId]    Script Date: 03/17/2013 13:12:10 ******/
CREATE NONCLUSTERED INDEX [IX_t_UserProfileQuestionId] ON [dbo].[t_UserProfileQuestion] 
(
	[Order] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

