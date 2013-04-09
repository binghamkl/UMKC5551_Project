INSERT INTO [t_ShortTermQuiz] ([ShortTermQuizId],[QuestionStatement],[Created])
		SELECT 1, 'It is the beginning of March. The cold starts to slowly fade away, bringing in signs of spring. Blue Spring High opens up its bake sale drives, which were previously closed due to snow. Shoveling equipment are put back into their respective garages, and spring cleaning is begun in most houses.', GETDATE()
			UNION
		SELECT 2, 'It is Spring Break in Missouri and new vacation spots have opened up. The Lake of Ozarks has offered several cabins up for rent next week. On Tuesday, they will be opened and cleaned. First Friday parties in the Plaza, next to the Clock Tower have been started, since St Patty''s on March 17th.', GETDATE()
			UNION
		SELECT 3, 'Currently, the apartment association of Overland Park, Kansas are fighting the landlords on tax. The current year 2013 has witnessed an escalation of property tax, causing some concern over the residents of Overland Park. To avoid future hikes in rent, a meeting was set to be organized tomorrow. Those wishing to attend it must join the sign on sheet in the Nichols Library by tomorrow noon. ', GETDATE()
			UNION
		SELECT 4, 'The Corner Pet Store on Brookside Boulevard has a sale next week. Dogs and cats are for sale. Puppies and kittens are set to have a larger discount. Canaries, and mynah birds are available as well. The sale will go on for a week. Different pet food is also available at low costs. ', GETDATE()

GO

INSERT INTO [t_ShortTermQuestions]
           ([ShortTermQuestionsId],[ShortTermQuizId],[ShortTermQuestion],[Created])
		SELECT 1, 1, 'What month is it? ', GETDATE()
			UNION
		SELECT 2, 1, 'Which season nearly ends? ', GETDATE()
			UNION
		SELECT 3, 1, 'Which season nearly starts? ', GETDATE()
			UNION
		SELECT 4, 1, 'What does Blue Spring High start doing in spring that was previously closed in winter?  ', GETDATE()
			UNION
		SELECT 5, 2, 'When was St. Patty''s day? ', GETDATE()
			UNION
		SELECT 6, 2, 'In which area are First Friday parties held? ', GETDATE()
			UNION
		SELECT 7, 2, 'Which vacation spot has offered several cabins up for rent', GETDATE()
			UNION
		SELECT 8, 2, 'In which state has new vacation spots opened up in? (a. Missouri', GETDATE()
			UNION
		SELECT 9, 3, 'Where is the apt association located? ', GETDATE()
			UNION
		SELECT 10, 3, 'What is the current year? ', GETDATE()
			UNION
		SELECT 11, 3, 'What has this year witnessed an escalation of ?', GETDATE()
			UNION
		SELECT 12, 3, 'When is the meeting set to be organized ?', GETDATE()
			UNION
		SELECT 13, 4, 'Where is the corner pet store ?', GETDATE()
			UNION
		SELECT 14, 4, 'Which pets are set to have a larger discount?', GETDATE()
			UNION
		SELECT 15, 4, 'What kinds of birds are on sale?', GETDATE()
			UNION
		SELECT 16, 4, 'When is the sale?', GETDATE()


GO

INSERT INTO [t_ShortTermAnswers]
           ([ShortTermAnswerId],[ShortTermQuestionsId],[Answer],[IsCorrect],[Created])
		SELECT 1, 1, 'March', 1, GETDATE()
			UNION
		SELECT 2, 1, 'December', 0, GETDATE()
			UNION
		SELECT 3, 1, 'Febuary', 0, GETDATE()
			UNION
		SELECT 4, 2, 'Winter', 1, GETDATE()
			UNION
		SELECT 5, 2, 'Summer', 0, GETDATE()
			UNION
		SELECT 6, 2, 'Fall', 0, GETDATE()
			UNION
		SELECT 7, 3, 'Spring', 1, GETDATE()
			UNION
		SELECT 8, 3, 'Winter', 0, GETDATE()
			UNION
		SELECT 9, 3, 'Fall', 0, GETDATE()
			UNION
		SELECT 10, 4, 'Bake Sales', 0, GETDATE()
			UNION
		SELECT 11, 4, 'Irish Fest', 0, GETDATE()
			UNION
		SELECT 12, 4, 'Spring Cleaning', 1, GETDATE()
			UNION
		SELECT 13, 5, 'March 17', 1, GETDATE()
			UNION
		SELECT 14, 5, 'Dec 25', 0, GETDATE()
			UNION
		SELECT 15, 5, 'Jan 1', 0, GETDATE()
			UNION
		SELECT 16, 6, 'The Plaza', 1, GETDATE()
			UNION
		SELECT 17, 6, 'Wisconsin', 0, GETDATE()
			UNION
		SELECT 18, 6, 'St Regis Park', 0, GETDATE()
			UNION
		SELECT 19, 7, 'The Lake of the Ozarks', 1, GETDATE()
			UNION
		SELECT 20, 7, 'Missouri Highlanded', 0, GETDATE()
			UNION
		SELECT 21, 7, 'Tahoe', 0, GETDATE()
			UNION
		SELECT 22, 8, 'Missouri', 1, GETDATE()
			UNION
		SELECT 23, 8, 'Wisconsin', 0, GETDATE()
			UNION
		SELECT 24, 8, 'Nebraska', 0, GETDATE()
			UNION
		SELECT 26, 9, 'Kansas City', 0, GETDATE()
			UNION
		SELECT 27, 9, 'Overland Park, KS', 1, GETDATE()
			UNION
		SELECT 28, 9, 'Mississipi', 0, GETDATE()
			UNION
		SELECT 29, 10, '2011', 0, GETDATE()
			UNION
		SELECT 30, 10, '2012', 0, GETDATE()
			UNION
		SELECT 31, 10, '2013', 1, GETDATE()
			UNION
		SELECT 32, 11, 'Bookstore prices', 0, GETDATE()
			UNION
		SELECT 33, 11, 'Property Tax', 1, GETDATE()
			UNION
		SELECT 34, 11, 'Weather', 0, GETDATE()
			UNION
		SELECT 35, 12, 'Next Week', 0, GETDATE()
			UNION
		SELECT 36, 12, 'Next Year', 0, GETDATE()
			UNION
		SELECT 37, 12, 'Tomorrow', 1, GETDATE()
			UNION
		SELECT 38, 13, 'Brookside Boulevard', 1, GETDATE()
			UNION
		SELECT 39, 13, 'Overland Park', 0, GETDATE()
			UNION
		SELECT 41, 13, 'North Hills', 0, GETDATE()
			UNION
		SELECT 42, 14, 'Puppies', 1, GETDATE()
			UNION
		SELECT 43, 14, 'Lizards', 0, GETDATE()
			UNION
		SELECT 44, 14, 'Birds', 0, GETDATE()
			UNION
		SELECT 45, 15, 'Canaries', 1, GETDATE()
			UNION
		SELECT 46, 15, 'Swan', 0, GETDATE()
			UNION
		SELECT 47, 15, 'Crows', 0, GETDATE()
			UNION
		SELECT 48, 16, 'Tomorrow', 0, GETDATE()
			UNION
		SELECT 49, 16, 'Yesterday', 0, GETDATE()
			UNION
		SELECT 50, 16, 'Next Week', 1, GETDATE()

GO

