#region -- using declarations --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgileMind.BLL.Util;
using AgileMind.DAL.Data;

#endregion

namespace AgileMind.BLL.Games
{
    public class UserProfileQuestionsResults : Result
    {

        private List<vwQuestionAnswer> _questionList = new List<vwQuestionAnswer>();

        /*-- Constructors --*/

        #region -- Constructor() --
        public UserProfileQuestionsResults()
        {

        }
        #endregion

        /*-- Events --*/

        /*-- Properties --*/

        #region -- QuestionList Property --
        public List<vwQuestionAnswer> QuestionList
        {
            get { return _questionList; }
            set { _questionList = value; }
        }
        #endregion

        /*-- Methods --*/

        /*-- Event Handlers --*/

        /*-- Factory Methods --*/

        #region -- FetchUserProfileQuestions(Guid SessionId) Method --
        public static UserProfileQuestionsResults FetchUserProfileQuestions(Guid SessionId)
        {
            UserProfileQuestionsResults questionResults = new UserProfileQuestionsResults();
            try
            {

                AgileMindEntities agileDB = new AgileMindEntities();

                t_LoginSession session = (from loginSession in agileDB.t_LoginSession where loginSession.LoginSessionId == SessionId && loginSession.ValidTill > DateTime.Now select loginSession).First();
                if (session != null)
                {
                    questionResults.QuestionList = agileDB.FetchQuestionAnswer_ByLoginId(session.LoginId).ToList();
                    questionResults.Success = true;
                }
                else
                {
                    questionResults.Error = "Could not find sessionId";
                }

            }
            catch (Exception ex)
            {
                questionResults.Error = ex.Message;
            }

            return questionResults;

        }
        #endregion
		
		#region -- HasUserFilledOutAnyQuestions() Method --
		public static bool HasUserFilledOutAnyQuestions(Guid SessionId)
		{

            AgileMindEntities agileDb = new AgileMindEntities();
            t_LoginSession loginSession = (from data in agileDb.t_LoginSession where data.LoginSessionId == SessionId && data.ValidTill > DateTime.Now select data).First();
            if (loginSession != null)
            {

                List<t_UserProfileAnswer> userAnswers = (from data in agileDb.t_UserProfileAnswer where data.LoginId == loginSession.LoginId && data.NoAnswer == false select data).ToList();
                if (userAnswers.Count > 0)
                    return true;

            }

            return false;
		}
		#endregion
	
		#region -- SaveUserQuestions(List<vwQuestionAnswer> QuestionAnswerList, Guid SessionId) Method --
		public static Result SaveUserQuestions(List<vwQuestionAnswer> QuestionAnswerList, Guid SessionId)
		{
            Result saveResults = new Result();

            AgileMindEntities agileDB = new AgileMindEntities();

            t_LoginSession session = (from loginSession in agileDB.t_LoginSession where loginSession.LoginSessionId == SessionId && loginSession.ValidTill > DateTime.Now select loginSession).First();
            if (session != null)
	        {
                List<t_UserProfileAnswer> answerList = (from data in agileDB.t_UserProfileAnswer where data.LoginId == session.LoginId select data).ToList();

                foreach (vwQuestionAnswer questionAnswer in QuestionAnswerList)
                {
                    if (questionAnswer.UserProfileAnswerId == null || questionAnswer.UserProfileAnswerId == 0)
                    {
                        t_UserProfileAnswer newAnswer = new t_UserProfileAnswer();
                        newAnswer.Answer = questionAnswer.Answer;
                        newAnswer.Created = DateTime.Now;
                        newAnswer.LoginId = session.LoginId;
                        newAnswer.UserProfileQuestionId = questionAnswer.UserProfileQuestionId;
                        if (questionAnswer.NoAnswer.HasValue)
                        {
                            newAnswer.NoAnswer = questionAnswer.NoAnswer.Value;
                        }
                        else
                        {
                            newAnswer.NoAnswer = false;
                        }
                        agileDB.t_UserProfileAnswer.AddObject(newAnswer);
                    }
                    else
                    {
                        t_UserProfileAnswer foundAnswer = (from data in agileDB.t_UserProfileAnswer where data.UserProfileAnswerId == questionAnswer.UserProfileAnswerId select data).First();

                        if (foundAnswer != null)
                        {
                            foundAnswer.Answer = questionAnswer.Answer;

                            if (questionAnswer.NoAnswer.HasValue)
                            {
                                foundAnswer.NoAnswer = questionAnswer.NoAnswer.Value;
                            }
                            else
                            {
                                foundAnswer.NoAnswer = false;
                            }
                            
                        }
                    }
                }

                agileDB.SaveChanges();

                saveResults.Success = true;
	        }
            else
            {
                saveResults.Error = "Could not find session";
            }

            return saveResults;
		}
		#endregion
		
    }
}
