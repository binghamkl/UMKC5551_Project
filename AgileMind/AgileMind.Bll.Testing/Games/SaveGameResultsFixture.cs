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
    public class SaveGameResultsFixture
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

        /*-- Tests --*/

        #region -- CanCreateSaveRecord() Method --
        [Test()]
        public void CanCreateSaveRecord()
        {
            GameResults newGame = new GameResults();
            Assert.IsNotNull(newGame);
        }
        #endregion

        #region -- CallingSaveWillSaveTheRecordToTheDatabase() Method --
        [Test()]
        public void CallingSaveWillSaveTheRecordToTheDatabase()
        {

            decimal td = 28.2M;
            GameResults gameresults = GameResults.InsertGameResult(LOGINNAME, PASSWORD, GameListEnum.ColorQuiz, 5,td, 10);
            Assert.IsNotNull(gameresults);

            DeleteGameResults(gameresults.Game.GameScoreId);

        }
        #endregion

        #region -- CallingInsertWillFailIfTheLoginIsIncorrect() Method --
        [Test()]
        public void CallingInsertWillFailIfTheLoginIsIncorrect()
        {
            decimal td = 28.2M;
            GameResults gameresults = GameResults.InsertGameResult(LOGINNAME, "DOLE", GameListEnum.ColorQuiz, 5, td, 10);
            Assert.IsNotNull(gameresults);
            Assert.IsFalse(gameresults.Success);

        }
        #endregion

        #region -- CallingSaveWillSaveTheRecordToTheDatabase() Method --
        [Test()]
        public void CallingSaveWillReturnSuccessfullResult()
        {

            decimal td = 28.2M;
            GameResults gameresults = GameResults.InsertGameResult(LOGINNAME, PASSWORD, GameListEnum.ColorQuiz, 5, td, 10);
            Assert.IsNotNull(gameresults);
            Assert.IsTrue(gameresults.Success);
            DeleteGameResults(gameresults.Game.GameScoreId);

        }
        #endregion

        /*-- Helper Methods --*/
        
		#region -- DeleteGameResults() Method --
		public void DeleteGameResults(int GameResultsId)
		{
            AgileMindEntities db = new DAL.Data.AgileMindEntities();
            List<t_GameResults> resultList = (from data in db.t_GameResults where data.GameScoreId == GameResultsId select data).ToList();
            foreach (t_GameResults item in resultList)
            {
                db.DeleteObject(item);
            }
            db.SaveChanges();
        }
		#endregion
        
		#region -- GetFirstGameID() Method --
		public static int GetFirstGameID()
		{
            AgileMind.DAL.Data.AgileMindEntities db = new DAL.Data.AgileMindEntities();
            AgileMind.DAL.Data.t_Game tGame = (from data in db.t_Game select data).First();
            return tGame.GameId;
		}
		#endregion
		

    }
}
