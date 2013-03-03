#region -- using declarations --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AgileMind.BLL.Login;
using System.Data;
using System.Data.Entity;

#endregion

namespace AgileMind.Bll.Testing.Login
{
    [TestFixture()]
    public class LoginInfoTest
    {

        /*-- Testing --*/

        /*-- CreateLogin Testing --*/

        #region -- CallingCreateLoginReturnsLoginInfo() Method --
        [Test()]
        public void CallingCreateLoginReturnsLoginInfo()
        {
            String loginName = "TestName";
            String password = "Password";
            String email = "Email";

            DeleteLoginsFromDB(loginName);

            LoginResult loginInfo = LoginResult.CreateLogin(loginName, password, email);
            Assert.IsNotNull(loginInfo);

            DeleteLoginsFromDB(loginName);
        }
        #endregion

        #region -- CallingCreateLoginInfoAddsTheLoginPersonIntoTheDatabase() Method --
        [Test()]
        public void CallingCreateLoginInfoAddsTheLoginPersonIntoTheDatabase()
        {

            String loginName = "TestName";
            String password = "Password";
            String email = "Email";

            DeleteLoginsFromDB(loginName);
            LoginResult loginInfo = LoginResult.CreateLogin(loginName, password, email);

            AgileMind.DAL.Data.AgileMindEntities agileMind = new DAL.Data.AgileMindEntities();
            List<AgileMind.DAL.Data.Login> loginList = agileMind.Logins_CheckLogin(loginName, password).ToList();
            Assert.AreEqual(1, loginList.Count, "After insertion of new login there should be one login");
            Assert.AreEqual(loginName, loginList[0].LoginName, "Login Name should be set equal");
            Assert.AreEqual(password, loginList[0].Password, "Password should be equal");
            Assert.AreEqual(email, loginList[0].EmailAddress);

            DeleteLoginsFromDB(loginName);

        }
        #endregion

        #region -- CallingCreateLoginInfoAddsTheLoginPersonAndReturnsItInLoginInfoObject() Method --
        [Test()]
        public void CallingCreateLoginInfoAddsTheLoginPersonAndReturnsItInLoginInfoObject()
        {
            String loginName = "TestName";
            String password = "Password";
            String email = "Email";

            DeleteLoginsFromDB(loginName);
            LoginResult loginInfo = LoginResult.CreateLogin(loginName, password, email);

            Assert.IsNotNull(loginInfo.LoginInfo);
            Assert.AreEqual(loginName, loginInfo.LoginInfo.LoginName);
            Assert.AreEqual(email, loginInfo.LoginInfo.EmailAddress);

            DeleteLoginsFromDB(loginName);
        }
        #endregion

        #region -- CallingCreateLoginReturnsTrueInLoginResult() Method --
        [Test()]
        public void CallingCreateLoginReturnsTrueInLoginResult()
        {
            String loginName = "TestName";
            String password = "Password";
            String email = "Email";

            DeleteLoginsFromDB(loginName);
            LoginResult loginResult = LoginResult.CreateLogin(loginName, password, email);

            Assert.IsTrue(loginResult.Success);

            DeleteLoginsFromDB(loginName);
        }
        #endregion

        #region -- CallingCreateLoginFailsIfTheUserNameAlreadyExists() Method --
        [Test()]
        public void CallingCreateLoginFailsIfTheUserNameAlreadyExists()
        {
            String loginName = "TestName";
            String password = "Password";
            String email = "Email";

            DeleteLoginsFromDB(loginName);
            LoginResult loginResult = LoginResult.CreateLogin(loginName, password, email);

            LoginResult duplicateResult = LoginResult.CreateLogin(loginName, password, email);

            Assert.IsFalse(duplicateResult.Success);


            DeleteLoginsFromDB(loginName);

        }
        #endregion

        #region -- CallingCreateLogiNFailsIfUserNameIsLessthan5Long() Method --
        [Test()]
        public void CallingCreateLogiNFailsIfUserNameIsLessthan5Long()
        {
            String loginName = "Test";
            String password = "Password";
            String email = "Email";

            DeleteLoginsFromDB(loginName);
            LoginResult loginResult = LoginResult.CreateLogin(loginName, password, email);

            Assert.IsFalse(loginResult.Success);


            DeleteLoginsFromDB(loginName);
        }
        #endregion

        #region -- CallingCreateLoginFailsIfPasswordIsLessthan6Long() Method --
        [Test()]
        public void CallingCreateLoginFailsIfPasswordIsLessthan6Long()
        {
            String loginName = "TestAccount";
            String password = "Passw";
            String email = "Email";

            DeleteLoginsFromDB(loginName);
            LoginResult loginResult = LoginResult.CreateLogin(loginName, password, email);

            Assert.IsFalse(loginResult.Success);


            DeleteLoginsFromDB(loginName);

        }
        #endregion

        /*-- Validate Login testing --*/

        #region -- CallingValidateLoginWithAuthorizedCredentialsReturnsTrueAndTheLoginInformation() Method --
        [Test()]
        public void CallingValidateLoginWithAuthorizedCredentialsReturnsTrueAndTheLoginInformation()
        {
            String loginName = "TestAccount";
            String password = "Password";
            String email = "Email@test.com";

            DeleteLoginsFromDB(loginName);
            LoginResult loginResult = LoginResult.CreateLogin(loginName, password, email);

            LoginResult validateResult = LoginResult.ValidateLogin(loginName, password);

            Assert.IsTrue(validateResult.Success);
            Assert.IsNotNull(validateResult.LoginInfo);
            Assert.AreEqual(loginName, validateResult.LoginInfo.LoginName);

            DeleteLoginsFromDB(loginName);
        }
        #endregion

        #region -- CallingValidateLoginWithAuthorizedInvalidUserNamereturnsFalse() Method --
        [Test()]
        public void CallingValidateLoginWithAuthorizedInvalidUserNamereturnsFalse()
        {
            String loginName = "TestAccount";
            String password = "Password";
            String email = "Email@test.com";

            DeleteLoginsFromDB(loginName);
            LoginResult loginResult = LoginResult.CreateLogin(loginName, password, email);

            LoginResult validateResult = LoginResult.ValidateLogin("IncorrectAccount", password);

            Assert.IsFalse(validateResult.Success);

            DeleteLoginsFromDB(loginName);
        }
        #endregion

        #region -- CallingValidateLoginWithInvalidPasswordReturnsFalse() Method --
        [Test()]
        public void CallingValidateLoginWithInvalidPasswordReturnsFalse()
        {
            String loginName = "TestAccount";
            String password = "Password";
            String email = "Email@test.com";

            DeleteLoginsFromDB(loginName);
            LoginResult loginResult = LoginResult.CreateLogin(loginName, password, email);

            LoginResult validateResult = LoginResult.ValidateLogin(loginName, "nope");

            Assert.IsFalse(validateResult.Success);

            DeleteLoginsFromDB(loginName);
        }
        #endregion

        #region -- CallingCreat() Method --
        [Test()]
        public void CallingValidateLoginReturnsFalseIfTheLoginNameAndPasswordAreCorrectButActiveIsFalse()
        {
            String loginName = "TestAccount";
            String password = "Password";
            String email = "Email@test.com";

            DeleteLoginsFromDB(loginName);
            LoginResult loginResult = LoginResult.CreateLogin(loginName, password, email);


            AgileMind.DAL.Data.AgileMindEntities agileMindDB = new DAL.Data.AgileMindEntities();
            List<AgileMind.DAL.Data.Login> loginList = (from data in agileMindDB.Logins where data.LoginName == loginName select data).ToList();
            foreach (AgileMind.DAL.Data.Login login in loginList)
            {
                login.Active = false;
            }
            agileMindDB.SaveChanges();

            LoginResult validateResult = LoginResult.ValidateLogin(loginName, password);

            Assert.IsFalse(validateResult.Success);

            DeleteLoginsFromDB(loginName);
        }
        #endregion
	
	
        /*-- Helper Methods --*/
        
		#region -- DeleteLoginsFromDB() Method --
		private void DeleteLoginsFromDB(String LoginName)
		{
            AgileMind.DAL.Data.AgileMindEntities agileMindDB = new DAL.Data.AgileMindEntities();

            List<AgileMind.DAL.Data.Login> loginList = (from p in agileMindDB.Logins where p.LoginName == LoginName select p).ToList();
            foreach (AgileMind.DAL.Data.Login loginToDelete in loginList)
            {
                agileMindDB.DeleteObject(loginToDelete);
            }
            agileMindDB.SaveChanges();

		}
		#endregion
		

    }
}
