#region -- using declarations --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace AgileMind.BLL.Results
{
    public class UserGameResults : Results
    {

        private List<UserMeanGameScore> _meanGameScores;
        private decimal _userScore;

        /*-- Constructors --*/

        #region -- Constructor() --
        public UserGameResults()
        {

        }
        #endregion

        /*-- Events --*/

        /*-- Properties --*/

        #region -- MeanGameScores Property --
        public List<UserMeanGameScore> MeanGameScores
        {
            get { return _meanGameScores; }
            set { _meanGameScores = value; }
        }
        #endregion

        #region -- UserScore Property --
        public decimal UserScore
        {
            get { return _userScore; }
            set { _userScore = value; }
        }
        #endregion

        /*-- Methods --*/

        /*-- Event Handlers --*/
	    
        /*-- Load User Scores --*/

        
		#region -- FetchUserGameResults() Method --
		public UserGameResults FetchUserGameResults(Guid sessionId)
		{
            UserGameResults request = new UserGameResults();

            try
            {
                AgileMindEntities agileDB = new AgileMindEntities();


                t_LoginSession session = (from loginSession in agileDB.t_LoginSession where loginSession.LoginSessionId == SessionId && loginSession.ValidTill > DateTime.Now select loginSession).First();
                if (session != null)
                {
                    List<t_ShortTermQuiz> quizList = (from data in agileDB.t_ShortTermQuiz select data).ToList();
                    Random rand = new Random();
                    int questionIndex = rand.Next(quizList.Count);
                    request.Quiz = new ShortTermQuiz();
                    request.Quiz.Statement = quizList[questionIndex].QuestionStatement;
                    request.Quiz.ShortTermQuizId = quizList[questionIndex].ShortTermQuizId;

                    //Load Questions and then answers.
                    List<t_ShortTermQuestions> questionList = (from questions in agileDB.t_ShortTermQuestions where questions.ShortTermQuizId == request.Quiz.ShortTermQuizId select questions).ToList();
                    foreach (t_ShortTermQuestions question in questionList)
                    {
                        question.t_ShortTermAnswers.Load();
                    }


                    //Take the loaded information and put it into something that we'll send back.
                    ShortTermQuiz quiz = request.Quiz;
                    List<ShortTermQuestion> qList = new List<ShortTermQuestion>();
                    foreach (t_ShortTermQuestions question in questionList)
                    {
                        ShortTermQuestion newQuestion = new ShortTermQuestion();
                        newQuestion.Question = question.ShortTermQuestion;
                        foreach (t_ShortTermAnswers shortTermAnswer in question.t_ShortTermAnswers)
                        {
                            ShortTermAnswer newAnswer = new ShortTermAnswer();
                            newAnswer.Answer = shortTermAnswer.Answer;
                            newAnswer.IsCorrect = shortTermAnswer.IsCorrect;
                            newQuestion.AnswerList.Add(newAnswer);
                        }

                        newQuestion.AnswerList.Shuffle();

                        qList.Add(newQuestion);

                    }

                    qList.Shuffle();

                    request.Quiz.QuestionList = qList;

                    t_Settings setting = (from settingData in agileDB.t_Settings where settingData.Setting == "QuestionDelay" select settingData).First();
                    if (setting != null)
                        request.Quiz.QuestionDelay = int.Parse(setting.Value);
                    else
                        request.Quiz.QuestionDelay = 10;

                    request.Success = true;
                }
                else
                {
                    request.Error = "Could not find sessionId";
                }
            }
            catch (Exception ex)
            {
                request.Error = ex.Message;
            }
            return request;
        }
		#endregion
		
    }
}
