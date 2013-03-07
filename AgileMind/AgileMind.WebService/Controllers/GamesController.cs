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

    }
}
