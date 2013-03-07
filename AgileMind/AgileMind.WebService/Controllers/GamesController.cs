#region -- using declarations --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgileMind.BLL.Games;

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
		
    }
}
