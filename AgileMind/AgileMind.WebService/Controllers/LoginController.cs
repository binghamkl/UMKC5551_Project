#region -- using declarations --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgileMind.BLL.Login;

#endregion

namespace AgileMind.WebService.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }

        #region -- LoginUser(String UserName, String Password) WS
        public JsonResult LoginUser(String UserName, String Password)
        {
            JsonResult jsonResult = new JsonResult();

            LoginResult loginResult = LoginResult.ValidateLogin(UserName, Password);

            jsonResult = Json(loginResult, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        #endregion
        
		#region -- CreateUser() Method --
        public JsonResult CreateUser(String UserName, String Password, String Email)
		{
            JsonResult jsonResult = new JsonResult();

            LoginResult loginResult = LoginResult.CreateLogin(UserName, Password, Email);

            jsonResult = Json(loginResult, JsonRequestBehavior.AllowGet);
            return jsonResult;
		}
		#endregion
		
    }
}
