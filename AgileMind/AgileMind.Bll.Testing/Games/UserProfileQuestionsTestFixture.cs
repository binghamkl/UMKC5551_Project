#region -- using declarations --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AgileMind.BLL.Games;
using AgileMind.Bll.Testing.Login;
using AgileMind.BLL.Login;
using AgileMind.DAL.Data;

#endregion

namespace AgileMind.Bll.Testing.Games
{
    [TestFixture()]
    public class UserProfileQuestionsTestFixture
    {

        private const String LOGINNAME = "TestAccount";
        private const String PASSWORD = "Password";
        private const String EMAIL = "mail@test.com";

        [TestFixtureSetUp]
        #region -- Initialize() Method --
        public void Initialize()
        {
            LoginInfoTest.DeleteLoginsFromDB(LOGINNAME);
            LoginResult loginResult = LoginResult.CreateLogin(LOGINNAME, PASSWORD, EMAIL);
        }
        #endregion

        [TestFixtureTearDown]
        #region -- Terminate() Method --
        public void Terminate()
        {
            LoginInfoTest.DeleteLoginsFromDB(LOGINNAME);
        }
        #endregion

        //*-- Testing --*//

        #region -- LoadQuestionsReturnsAListOfQuestionsFromTheDatabase() Method --
        [Test()]
        public void LoadQuestionsReturnsAListOfQuestionsFromTheDatabase()
        {
            UserProfileQuestionsResults results = UserProfileQuestionsResults.FetchUserProfileQuestions();
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Success);
            Assert.Greater(results.QuestionList.Count(), 0);
        }
        #endregion

        #region -- HasUserFilledOutQuestionsReturnsTrueIfTheUserFilledOutEvenASingleQuestion() Method --
        [Test()]
        public void HasUserFilledOutQuestionsReturnsTrueIfTheUserFilledOutEvenASingleQuestion()
        {
            AgileMindEntities agileDb = new AgileMindEntities();

            LoginResult.ValidateLogin(LOGINNAME, PASSWORD, "ip");

            AgileMind.DAL.Data.Login login = (from data in agileDb.Logins where data.LoginName == LOGINNAME select data).First();
            if (login != null)
            {
                t_LoginSession session = (from data in agileDb.t_LoginSession where data.LoginId == login.LoginId && data.ValidTill > DateTime.Now select data).First();
                if (session != null)
                {
                    t_UserProfileAnswer newAnswer = new t_UserProfileAnswer();
                    newAnswer.LoginId = login.LoginId;
                    newAnswer.NoAnswer = false;
                    newAnswer.UserProfileQuestionId = 1;
                    newAnswer.Answer = "one answer";
                    newAnswer.Created = DateTime.Now;
                    agileDb.t_UserProfileAnswer.AddObject(newAnswer);
                    agileDb.SaveChanges();

                    bool userFilledOutQuestion = UserProfileQuestionsResults.HasUserFilledOutAnyQuestions(session.LoginSessionId);
                    Assert.IsTrue(userFilledOutQuestion);
                }
                else
                {
                    Assert.Fail("Could not find the session to begin testing.");
                }
            }
            else
            {
                Assert.Fail("Could not find the login to start testing");
            }

        }
        #endregion

        #region -- HasUserFilledOutQuestionsReturnsFalseIfTheUserDidNotFillOutAnyQuestions() Method --
        [Test()]
        public void HasUserFilledOutQuestionsReturnsFalseIfTheUserDidNotFillOutAnyQuestions()
        {
            AgileMindEntities agileDb = new AgileMindEntities();

            LoginResult.ValidateLogin(LOGINNAME, PASSWORD, "ip");

            AgileMind.DAL.Data.Login login = (from data in agileDb.Logins where data.LoginName == LOGINNAME select data).First();
            if (login != null)
            {
                t_LoginSession session = (from data in agileDb.t_LoginSession where data.LoginId == login.LoginId && data.ValidTill > DateTime.Now select data).First();
                if (session != null)
                {

                    bool userFilledOutQuestion = UserProfileQuestionsResults.HasUserFilledOutAnyQuestions(session.LoginSessionId);
                    Assert.IsFalse(userFilledOutQuestion);
                }
                else
                {
                    Assert.Fail("Could not find the session to begin testing.");
                }
            }
            else
            {
                Assert.Fail("Could not find the login to start testing");
            }
        }
        #endregion
	

    }
}
