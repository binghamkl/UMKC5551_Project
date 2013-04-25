#region -- using declarations --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgileMind.BLL.Results;
using AgileMind.WebService.Models;
using AgileMind.DAL.Data;

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

        #region -- FetchIndividualGameResults(String SessionId, int GameId) WS
        public JsonResult FetchIndividualGameResults(String SessionId, int GameId)
        {
            JsonResult jsonResult = new JsonResult();

            Guid sessionId = new Guid(SessionId);
            IndividualGameResults results = IndividualGameResults.FetchResultsForGameType(sessionId, GameId);
            List<GameResultData> resultList = new List<GameResultData>();
            foreach (t_GameResults result in results.GameResultList)
            {
                GameResultData newItem = new GameResultData();
                newItem.Created = result.Created;
                newItem.GameId = result.GameId;
                newItem.GameScoreId = result.GameScoreId;
                newItem.LoginId = result.LoginId;
                newItem.Score = result.Score;
                newItem.TestDuration = result.TestDuration.Value;
                newItem.Total = result.Total;
                newItem.CreatedString = result.Created.ToShortDateString();
                resultList.Add(newItem);
            }

            jsonResult = Json(resultList, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        #endregion

        #region -- FetchGameList(String SessionId) WS
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
