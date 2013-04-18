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
-- Author:		Kendall Bingham
-- Create date: 4.17.2010
-- Description:	Deliver the average of the users std deviation
-- =============================================
CREATE PROCEDURE dbo.UserDevAveragePerGame 
	-- Add the parameters for the stored procedure here
	@LoginId int 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Gameid,  AVG( ( cast(score as float) / cast(total as float)) * 100 / TestDuration  ) as [Mean]
			INTO #UserDeviation
		FROM t_GameResults
			WHERE Total > 0 
				AND TestDuration > 0
				AND Score > 0
				AND LoginId = @LoginId
		GROUP BY GameId

	SELECT ud.Mean as UserMean, tg.GameId, tg.Game, tg.Mean as [GameMean], tg.stDev as [GameDeviation], (ud.Mean - tg.Mean) as MeanDiff, (ud.Mean - tg.Mean) / tg.StDev as UserDeflection
		FROM #UserDeviation as ud
			INNER JOIN t_Game as tg on tg.GameId = ud.GameId

	DROP TABLE #UserDeviation	


END
GO
