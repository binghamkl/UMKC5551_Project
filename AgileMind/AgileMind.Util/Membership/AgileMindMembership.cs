#region -- using declarations --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using AgileMind.Util.LoginWS;
using System.Web;

#endregion

namespace AgileMind.Util.Membership
{
    public class AgileMindMembership : System.Web.Security.SqlMembershipProvider
    {


        /*-- Constructors --*/

        #region -- Constructor() --
        public AgileMindMembership() : base()
        {

        }
        #endregion

        /*-- Events --*/

        /*-- Properties --*/

        /*-- Methods --*/

        #region -- CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out System.Web.Security.MembershipCreateStatus status) Method --
        public override System.Web.Security.MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out System.Web.Security.MembershipCreateStatus status)
        {
            LoginWS.Login client = new LoginWS.Login();
            LoginResult loginResult = client.CreateLogin(username, password, email);
            if (loginResult.Success)
            {
                MembershipUser user = new MembershipUser("AspNetSqlMembershipProvider", loginResult.LoginInfo.LoginName, loginResult.LoginInfo.LoginId, loginResult.LoginInfo.EmailAddress, 
                                            string.Empty, string.Empty, loginResult.LoginInfo.Active, !loginResult.LoginInfo.Active, loginResult.LoginInfo.Created, DateTime.Now, 
                                            DateTime.Now, DateTime.Now, DateTime.Now);
                status = MembershipCreateStatus.Success;
                return user;

            }
            else
            {
                status = MembershipCreateStatus.UserRejected;
                return null;
            }
        }
        #endregion

        #region -- ValidateUser(string username, string password) Method --
        public override bool ValidateUser(string username, string password)
        {

            string szRemoteAddr = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            string szXForwardedFor = HttpContext.Current.Request.ServerVariables["X_FORWARDED_FOR"];
            string szIP = "";

            if (szXForwardedFor == null)
            {
                szIP = szRemoteAddr;
            }
            else
            {
                szIP = szXForwardedFor;
            }


            LoginWS.Login client = new LoginWS.Login();
            LoginResult loginResult = client.ValidateLogin(username, password, szIP);
            HttpContext.Current.Session.Add("SessionId", loginResult.SessionId);

            return loginResult.Success;
        }
        #endregion

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            MembershipUser user = new MembershipUser("AgileMindProvider", "test", "test", "test",
                                        string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now,
                                        DateTime.Now, DateTime.Now, DateTime.Now);
            return user;    
        }
        /*-- Event Handlers --*/
	
    }
}
