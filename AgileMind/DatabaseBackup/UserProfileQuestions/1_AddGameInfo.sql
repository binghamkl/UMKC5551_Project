IF (NOT EXISTS ( Select * from t_Game where GameId = 2))
BEGIN

	INSERT INTO [AgileMind].[dbo].[t_Game]
			   ([GameId]
			   ,[Game]
			   ,[Description])
		 VALUES
			   (2
			   ,'ProfileQuiz'
			   ,'Questions for the user about their profile')

END


