#region -- using declarations --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgileMind.BLL.Games;
using AgileMind.DAL.Data;
using System.Runtime.Serialization.Json;
using AgileMind.WebService.Models;

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

        #region -- FetchUserProfileQuestions(String SessionId) WS
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
        public JsonResult SaveUserProfileQuestions(String QuestionAnswerList, String SessionId)
        {
            try
            {
                Guid SessionIdGuid = new Guid(SessionId);
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<QuestionAnswer>));
                System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(System.Text.Encoding.ASCII.GetBytes(QuestionAnswerList));
                List<QuestionAnswer> questionList = (List<QuestionAnswer>)serializer.ReadObject(memoryStream);

                List<vwQuestionAnswer> submitQA = new List<vwQuestionAnswer>();
                foreach (QuestionAnswer qa in questionList)
                {
                    vwQuestionAnswer newQA = new vwQuestionAnswer();
                    newQA.Answer = qa.Answer;
                    newQA.UserProfileQuestionId = qa.UserProfileQuestionId;
                    newQA.UserProfileAnswerId = qa.UserProfileAnswerId;
                    submitQA.Add(newQA);
                }

                JsonResult result = Json(UserProfileQuestionsResults.SaveUserQuestions(submitQA, SessionIdGuid), JsonRequestBehavior.AllowGet);
                return result;
                //return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            //return Json(QuestionAnswerList, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region -- FetchRandomUsreProfileQuizQuestions(String SessionId, int QuestionCount) Method --
        public JsonResult FetchRandomUsreProfileQuizQuestions(String SessionId, int QuestionCount)
		{
            JsonResult jsonResult = new JsonResult();

            Guid sessionGuid = new Guid(SessionId);

            ProfileQuizQuestionRequest result = ProfileQuizQuestionRequest.FetchRandomQuizQuestions(sessionGuid, QuestionCount);

            jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
		#endregion

        #region -- FetchShortTermMemoryQuestions(String SessionId) Method --
        public JsonResult FetchShortTermMemoryQuestions(String SessionId)
        {
            JsonResult jsonResult = new JsonResult();

            Guid sessionGuid = new Guid(SessionId);

            ShortTermQuizResult result = ShortTermQuizResult.Fetchquiz(sessionGuid);

            jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        #endregion

    }
}
