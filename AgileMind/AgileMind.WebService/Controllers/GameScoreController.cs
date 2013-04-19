#region -- using declarations --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgileMind.BLL.Results;

#endregion

namespace AgileMind.WebService.Controllers
{
    public class GameScoreController : Controller
    {
        //
        // GET: /GameScore/

        public ActionResult Index()
        {
            return View();
        }

        #region -- FetchUserGameResults(String SessionId) WS
        public JsonResult FetchUserGameResults(String SessionId)
        {
            JsonResult jsonResult = new JsonResult();

            Guid sessionId = new Guid(SessionId);
            UserGameResults results = UserGameResults.FetchUserGameResults(sessionId);

            jsonResult = Json(results, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        #endregion

        #region -- FetchUserGameResults(String SessionId, int GameId) WS
        public JsonResult FetchIndividualGameResults(String SessionId, int GameId)
        {
            JsonResult jsonResult = new JsonResult();

            Guid sessionId = new Guid(SessionId);
            IndividualGameResults results = IndividualGameResults.FetchResultsForGameType(sessionId, GameId);

            jsonResult = Json(results, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        #endregion

        #region -- FetchUserGameResults(String SessionId) WS
        public JsonResult FetchGameList(String SessionId)
        {
            JsonResult jsonResult = new JsonResult();

            Guid sessionId = new Guid(SessionId);
            GameListResults results = GameListResults.FetchGameList(sessionId);

            jsonResult = Json(results, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        #endregion


    }
}
