GO

/****** Object:  Table [dbo].[t_ShortTermQuiz]    Script Date: 04/06/2013 17:11:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[t_ShortTermQuiz](
	[ShortTermQuizId] [int] NOT NULL,
	[QuestionStatement] [varchar](2000) NOT NULL,
	[Created] [datetime] NOT NULL,
 CONSTRAINT [PK_t_ShortTermQuiz] PRIMARY KEY CLUSTERED 
(
	[ShortTermQuizId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


GO

/****** Object:  Table [dbo].[t_ShortTermQuestions]    Script Date: 04/06/2013 17:11:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[t_ShortTermQuestions](
	[ShortTermQuestionsId] [int] NOT NULL,
	[ShortTermQuizId] [int] NOT NULL,
	[ShortTermQuestion] [varchar](200) NOT NULL,
	[Created] [datetime] NULL,
 CONSTRAINT [PK_t_ShortTermQuestions] PRIMARY KEY CLUSTERED 
(
	[ShortTermQuestionsId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[t_ShortTermQuestions]  WITH CHECK ADD  CONSTRAINT [FK_t_ShortTermQuestions_t_ShortTermQuiz] FOREIGN KEY([ShortTermQuizId])
REFERENCES [dbo].[t_ShortTermQuiz] ([ShortTermQuizId])
GO

ALTER TABLE [dbo].[t_ShortTermQuestions] CHECK CONSTRAINT [FK_t_ShortTermQuestions_t_ShortTermQuiz]
GO

GO

/****** Object:  Table [dbo].[t_ShortTermAnswers]    Script Date: 04/06/2013 17:12:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[t_ShortTermAnswers](
	[ShortTermAnswerId] [int] NOT NULL,
	[ShortTermQuestionsId] [int] NOT NULL,
	[Answer] [varchar](60) NOT NULL,
	[IsCorrect] [bit] NOT NULL,
	[Created] [datetime] NOT NULL,
 CONSTRAINT [PK_t_ShortTermAnswers] PRIMARY KEY CLUSTERED 
(
	[ShortTermAnswerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[t_ShortTermAnswers]  WITH CHECK ADD  CONSTRAINT [FK_t_ShortTermAnswers_t_ShortTermQuestions] FOREIGN KEY([ShortTermQuestionsId])
REFERENCES [dbo].[t_ShortTermQuestions] ([ShortTermQuestionsId])
GO

ALTER TABLE [dbo].[t_ShortTermAnswers] CHECK CONSTRAINT [FK_t_ShortTermAnswers_t_ShortTermQuestions]
GO

