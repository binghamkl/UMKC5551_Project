#region -- using declarations --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgileMind.BLL.Util;
using AgileMind.DAL.Data;

#endregion

namespace AgileMind.BLL.Login
{
    public class LoginResult : Result
    {

        private vwLoginInfo _loginInfo;

        /*-- Constructors --*/

        #region -- Constructor() --
        public LoginResult()
        {

        }
        #endregion

        /*-- Events --*/

        /*-- Properties --*/

        #region -- LoginInfo Property --
        public vwLoginInfo LoginInfo
        {
            get { return _loginInfo; }
            set { _loginInfo = value; }
        }
        #endregion

        /*-- Methods --*/

        /*-- Event Handlers --*/

        #region -- LoginResult CreateLogin(string LoginName, string Password, string EmailAddress) Method --
        public static LoginResult CreateLogin(string LoginName, string Password, string EmailAddress)
        {
            LoginResult loginInfo = new LoginResult();
            try
            {

                AgileMind.DAL.Data.AgileMindEntities agileMindDB = new AgileMindEntities();

                //Test for an account that already has a loginname in it.
                List<AgileMind.DAL.Data.Login> loginList = (from data in agileMindDB.Logins where data.LoginName == LoginName select data).ToList();
                if (loginList.Count > 0)
                {
                    loginInfo.Success = false;
                    loginInfo.Error = "There is already a Login User of that name";
                    return loginInfo;
                }
                if (LoginName.Length < 5)
                {
                    loginInfo.Success = false;
                    loginInfo.Error = "Username must be greater than 5";
                    return loginInfo;
                }
                if (Password.Length < 6)
                {
                    loginInfo.Success = false;
                    loginInfo.Error = "Password must be greater than 6.";
                    return loginInfo;
                }

                AgileMind.DAL.Data.Login login = new DAL.Data.Login();
                login.Active = true;
                login.Created = DateTime.Now;
                login.EmailAddress = EmailAddress;
                login.LoginName = LoginName;
                login.Password = Password;

                agileMindDB.Logins.AddObject(login);
                agileMindDB.SaveChanges();

                List<vwLoginInfo> loginInfoList = agileMindDB.vw_LoginInfo_FetchByLoginId(login.LoginId).ToList();
                if (loginInfoList.Count == 1)
                {
                    loginInfo.LoginInfo = loginInfoList[0];
                    loginInfo.Success = true;
                }
                else
                {
                    loginInfo.Error = "Did not return the proper # of logins for added entry " + loginInfoList.Count;
                    loginInfo.Success = false;
                }

            }
            catch (Exception ex)
            {
                loginInfo.Success = false;
                loginInfo.Error = ex.Message;
            }
            

            return loginInfo;
        }
        #endregion

        #region -- ValidateLogin(string loginName, string password) Method --
        public static LoginResult ValidateLogin(string loginName, string password)
        {
            LoginResult result = new LoginResult();

            try
            {
                AgileMindEntities agileMindDB = new AgileMindEntities();
                List<AgileMind.DAL.Data.Login> loginList = agileMindDB.Logins_CheckLogin(loginName, password).ToList();
                if (loginList.Count == 1)
                {
                    int loginId = loginList[0].LoginId;
                    List<vwLoginInfo> loginInfoList = (from data in agileMindDB.vwLoginInfoes where data.LoginId == loginId select data).ToList();
                    if (loginInfoList.Count == 1)
                    {
                        if (loginInfoList[0].Active)
                        {
                            result.Success = true;
                            result.LoginInfo = loginInfoList[0];
                        }
                        else
                        {
                            result.Success = false;
                            result.Error = "Account has been deactivated.";
                        }
                    }
                    else
                    {
                        result.Success = false;
                        result.Error = "Did not return a LoginName and Password match ";  // should not happen since were just returning the view of it.
                    }
                }
                else
                {
                    result.Success = false;
                    result.Error = "Could not find the LoginName and Password Specified";
                }

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Error = ex.Message;
            }

            return result;
        }
        #endregion

    }
}
