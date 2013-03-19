-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kendall Binghamm
-- Create date: 3.18.2013
-- Description:	Fetch 
-- =============================================
CREATE PROCEDURE dbo.FetchQuestionAnswer_ByLoginId
	-- Add the parameters for the stored procedure here
	@LoginId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [UserProfileQuestionId],[Question],[Order],[Created],[Active],
			[UserProfileAnswerId],[LoginId],[Answer],[AnswerCreated],[NoAnswer]
	FROM [vwQuestionAnswer]
		WHERE 
			(LoginId = @LoginId OR LoginId IS NULL)
			AND Active = 1


END
GO
