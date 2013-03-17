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

        private List<t_UserProfileQuestion> _questionList = new List<t_UserProfileQuestion>();

        /*-- Constructors --*/

        #region -- Constructor() --
        public UserProfileQuestionsResults()
        {

        }
        #endregion

        /*-- Events --*/

        /*-- Properties --*/

        #region -- QuestionList Property --
        public List<t_UserProfileQuestion> QuestionList
        {
            get { return _questionList; }
            set { _questionList = value; }
        }
        #endregion

        /*-- Methods --*/

        /*-- Event Handlers --*/

        /*-- Factory Methods --*/

        #region -- FetchUserProfileQuestions() Method --
        public static UserProfileQuestionsResults FetchUserProfileQuestions()
        {
            UserProfileQuestionsResults questionResults = new UserProfileQuestionsResults();
            try
            {
                AgileMindEntities agileDB = new AgileMindEntities();
                questionResults.QuestionList = (from data in agileDB.t_UserProfileQuestion where data.Active == true orderby data.Order select data).ToList();

                questionResults.Success = true;
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
	
    }
}
