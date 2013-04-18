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
-- Create date: 4.16.2013
-- Description:	Computes the Std Devation and Mean and updates hte values in the t_game Table
-- =============================================
CREATE PROCEDURE RecalculateGameMeanAndStdDev 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Gameid,  AVG( ( cast(score as float) / cast(total as float)) * 100 / TestDuration  ) as [Mean]
			INTO #TempAverage
		FROM t_GameResults
			WHERE Total > 0 
				AND TestDuration > 0
				AND Score > 0
		GROUP BY GameId
	UPDATE t_Game set [Mean] = ta.Mean
		FROM t_Game
			INNER JOIN #tempaverage as ta on ta.gameId = t_Game.GameId
	DROP TABLE #TempAverage
	
	SELECT Gameid,  stdev( ( cast(score as float) / cast(total as float)) * 100 / TestDuration  ) as [StdDev]
		INTO #TempStdDev
		FROM t_GameResults
			WHERE Total > 0 
				AND TestDuration > 0
				AND Score > 0
		GROUP BY GameId
	UPDATE t_Game set [stdev] = ts.StdDev
		FROM t_Game
			INNER JOIN #TempStdDev as ts on ts.gameId = t_Game.GameId
	
	DROP TABLE #TempStdDev
	
END
GO

EXEC RecalculateGameMeanAndStdDev