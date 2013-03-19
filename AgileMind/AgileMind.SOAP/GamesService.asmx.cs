#region -- using declarations --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using AgileMind.BLL.Games;
using AgileMind.BLL.Util;
using AgileMind.DAL.Data;

#endregion

namespace AgileMind.SOAP
{
    /// <summary>
    /// Summary description for GamesService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class GamesService : System.Web.Services.WebService
    {

        [WebMethod]
        #region -- FetchColorGame() WebService --
        public ColorGameResult FetchColorGame()
        {
            return ColorGameResult.CreateGame();
        }
        #endregion

        [WebMethod]
        #region -- InsertGameResult(String UserName, String Password, GameListEnum gameType, int Score, decimal TestDuration, int Total) Method --
        public GameResults InsertGameResult(String UserName, String Password, GameListEnum gameType, int Score, decimal TestDuration, int Total)
		{
            return GameResults.InsertGameResult(UserName, Password, gameType, Score, TestDuration, Total);
		}
		#endregion

        [WebMethod]
        #region -- InsertGameResultWeb(String UserName, GameListEnum gameType, int Score, decimal TestDuration, int Total) Method --
        public GameResults InsertGameResultWeb(String UserName, GameListEnum gameType, int Score, decimal TestDuration, int Total)
        {
            return GameResults.InsertGameResultLoginId(UserName, gameType, Score, TestDuration, Total);
        }
        #endregion

        [WebMethod]
		#region -- FetchUserProfileQuestions() Method --
		public UserProfileQuestionsResults FetchUserProfileQuestions(Guid SessionId)
		{
            return UserProfileQuestionsResults.FetchUserProfileQuestions(SessionId);
		}
		#endregion
		
        [WebMethod]
		#region -- HasUserFilledOutQuestions() Method --
		public bool HasUserFilledOutQuestions(Guid SessionId)
		{
            return UserProfileQuestionsResults.HasUserFilledOutAnyQuestions(SessionId);
		}
		#endregion
		
        [WebMethod]
        #region -- SaveUserProfileQuestions(List<vwQuestionAnswer> QuestionAnswerList, Guid SessionId) Method --
        public Result SaveUserProfileQuestions(List<vwQuestionAnswer> QuestionAnswerList, Guid SessionId)
		{
            return UserProfileQuestionsResults.SaveUserQuestions(QuestionAnswerList, SessionId);
		}
		#endregion
		
    }
}
