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
using AgileMind.BLL.Util;

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
            AgileMindEntities agileDb = new AgileMindEntities();

            LoginResult.ValidateLogin(LOGINNAME, PASSWORD, "ip");
            AgileMind.DAL.Data.Login login = (from data in agileDb.Logins where data.LoginName == LOGINNAME select data).First();
            if (login != null)
            {
                t_LoginSession session = (from data in agileDb.t_LoginSession where data.LoginId == login.LoginId && data.ValidTill > DateTime.Now select data).First();
                if (session != null)
                {
                    UserProfileQuestionsResults results = UserProfileQuestionsResults.FetchUserProfileQuestions(session.LoginSessionId);
                    Assert.IsNotNull(results);
                    Assert.IsTrue(results.Success);
                    Assert.Greater(results.QuestionList.Count(), 0);
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
	
        //*-- Save Answer Testing *--//

        #region -- SaveAnswersWillTakeTheViewAndSaveTheInformation() Method --
        [Test()]
        public void SaveAnswersWillTakeTheViewAndSaveTheInformation()
        {
            AgileMindEntities agileDb = new AgileMindEntities();

            LoginResult.ValidateLogin(LOGINNAME, PASSWORD, "ip");
            AgileMind.DAL.Data.Login login = (from data in agileDb.Logins where data.LoginName == LOGINNAME select data).First();
            if (login != null)
            {
                t_LoginSession session = (from data in agileDb.t_LoginSession where data.LoginId == login.LoginId && data.ValidTill > DateTime.Now select data).First();
                if (session != null)
                {
                    UserProfileQuestionsResults results = UserProfileQuestionsResults.FetchUserProfileQuestions(session.LoginSessionId);
                    Assert.IsNotNull(results);
                    Assert.IsTrue(results.Success);
                    Assert.Greater(results.QuestionList.Count(), 0);

                    results.QuestionList[0].Answer = "12345";
                    List<vwQuestionAnswer> questionList = new List<vwQuestionAnswer>();
                    questionList.Add(results.QuestionList[0]);
                    Result saveResult = UserProfileQuestionsResults.SaveUserQuestions(questionList, session.LoginSessionId);
                    Assert.IsTrue(saveResult.Success);

                    int userProfileQuestionId = results.QuestionList[0].UserProfileQuestionId;
                    t_UserProfileAnswer savedAnswer = (from data in agileDb.t_UserProfileAnswer where data.LoginId == login.LoginId && data.UserProfileQuestionId == userProfileQuestionId select data).First();
                    Assert.IsNotNull(savedAnswer);
                    Assert.AreEqual(savedAnswer.Answer, "12345");

                    agileDb.DeleteObject(savedAnswer);
                    agileDb.SaveChanges();

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

        #region -- SaveAnswersWillUpdateTheAnswerWithSaveInformation() Method --
        [Test()]
        public void SaveAnswersWillUpdateTheAnswerWithSaveInformation()
        {
            AgileMindEntities agileDb = new AgileMindEntities();

            LoginResult.ValidateLogin(LOGINNAME, PASSWORD, "ip");
            AgileMind.DAL.Data.Login login = (from data in agileDb.Logins where data.LoginName == LOGINNAME select data).First();
            if (login != null)
            {
                t_LoginSession session = (from data in agileDb.t_LoginSession where data.LoginId == login.LoginId && data.ValidTill > DateTime.Now select data).First();
                if (session != null)
                {
                    UserProfileQuestionsResults results = UserProfileQuestionsResults.FetchUserProfileQuestions(session.LoginSessionId);
                    Assert.IsNotNull(results);
                    Assert.IsTrue(results.Success);
                    Assert.Greater(results.QuestionList.Count(), 0);

                    results.QuestionList[0].Answer = "12345";
                    List<vwQuestionAnswer> questionList = new List<vwQuestionAnswer>();
                    questionList.Add(results.QuestionList[0]);
                    Result saveResult = UserProfileQuestionsResults.SaveUserQuestions(questionList, session.LoginSessionId);
                    Assert.IsTrue(saveResult.Success);

                    int userProfileQuestionId = results.QuestionList[0].UserProfileQuestionId;
                    t_UserProfileAnswer savedAnswer = (from data in agileDb.t_UserProfileAnswer where data.LoginId == login.LoginId && data.UserProfileQuestionId == userProfileQuestionId select data).First();
                    Assert.IsNotNull(savedAnswer);
                    Assert.AreEqual(savedAnswer.Answer, "12345");

                    results = UserProfileQuestionsResults.FetchUserProfileQuestions(session.LoginSessionId);
                    Assert.IsTrue(results.Success);
                    vwQuestionAnswer foundView = results.QuestionList.Find(delegate(vwQuestionAnswer findView) 
                    {
                        return findView.UserProfileQuestionId == userProfileQuestionId;
                    });
                    if (foundView != null)
                    {
                        if (foundView.UserProfileAnswerId != null && foundView.UserProfileAnswerId != 0)
                        {
                            questionList = new List<vwQuestionAnswer>();
                            questionList.Add(foundView);
                            foundView.Answer = "54321";

                            saveResult = UserProfileQuestionsResults.SaveUserQuestions(questionList, session.LoginSessionId);
                            Assert.IsTrue(saveResult.Success);

                            int userProfileAnswerId = foundView.UserProfileAnswerId.Value;
                            agileDb.Dispose();
                            agileDb = new AgileMindEntities();


                            savedAnswer = (from data in agileDb.t_UserProfileAnswer where data.UserProfileAnswerId == userProfileAnswerId select data).First();
                            Assert.IsNotNull(savedAnswer);
                            Assert.AreEqual(savedAnswer.Answer, "54321");


                        }
                        else
                        {
                            Assert.Fail("Did nto save with the userprofile Id");
                        }
                    }
                    else
                    {
                        Assert.Fail("Could not find the question returned back.  seriously ");
                    }

                    agileDb.DeleteObject(savedAnswer);
                    agileDb.SaveChanges();

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
