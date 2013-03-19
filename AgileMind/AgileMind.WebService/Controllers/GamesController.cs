#region -- using declarations --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgileMind.BLL.Games;
using AgileMind.DAL.Data;

#endregion

namespace AgileMind.WebService.Controllers
{
    public class GamesController : Controller
    {
        //
        // GET: /Games/

        public ActionResult Index()
        {
            return View();
        }

        #region -- CreateColorGame() WS
        public JsonResult CreateColorGame()
        {
            JsonResult jsonResult = new JsonResult();

            ColorGameResult result = ColorGameResult.CreateGame();

            jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        #endregion
        
		#region -- InsertGameResult() Method --
		public JsonResult InsertGameResult(String UserName, String Password, GameListEnum gameType, int Score, decimal TestDuration, int Total)
		{
            JsonResult jsonResult = new JsonResult();

            GameResults result = GameResults.InsertGameResult(UserName, Password, gameType, Score, TestDuration, Total);

            jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
		#endregion

        #region -- FetchUserProfileQuestions() WS
        public JsonResult FetchUserProfileQuestions(String SessionId)
        {
            JsonResult jsonResult = new JsonResult();

            Guid sessionGuid = new Guid(SessionId);

            UserProfileQuestionsResults result = UserProfileQuestionsResults.FetchUserProfileQuestions(sessionGuid);

            jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        #endregion

        #region -- HasUserFilledOutQuestions(String SessionId) WS
        public JsonResult HasUserFilledOutQuestions(String SessionId)
        {
            JsonResult jsonResult = new JsonResult();

            Guid sessionId = new Guid(SessionId);
            bool hasResults = UserProfileQuestionsResults.HasUserFilledOutAnyQuestions(sessionId);

            jsonResult = Json(hasResults, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        #endregion

        #region -- SaveUserProfileQuestions(List<vwQuestionAnswer> QuestionAnswerList, Guid SessionId) Method --
        public JsonResult SaveUserProfileQuestions(List<vwQuestionAnswer> QuestionAnswerList, Guid SessionId)
        {
            JsonResult result = Json(UserProfileQuestionsResults.SaveUserQuestions(QuestionAnswerList, SessionId), JsonRequestBehavior.AllowGet);
            return result;
        }
        #endregion

    }
}
