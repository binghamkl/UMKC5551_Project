#region -- using declarations --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using AgileMind.BLL.Login;

#endregion

namespace AgileMind.WebService.SOAP
{
    /// <summary>
    /// Summary description for Login
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Login : System.Web.Services.WebService
    {

        [WebMethod]
        #region -- CreateLogin(string LoginName, string Password, string EmailAddress) WebMethod --
        public LoginResult CreateLogin(string LoginName, string Password, string EmailAddress)
        {
            return LoginResult.CreateLogin(LoginName, Password, EmailAddress);
        }
        #endregion

        [WebMethod()]
        #region -- ValidateLogin(string loginName, string password) Method --
        public LoginResult ValidateLogin(string loginName, string password)
		{
            return LoginResult.ValidateLogin(loginName, password);
		}
		#endregion

    }
}
