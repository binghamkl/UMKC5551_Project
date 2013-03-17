#region -- using declarations --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgileMind.DAL.Data;
using AgileMind.BLL.Login;
using AgileMind.BLL.Util;

#endregion

namespace AgileMind.BLL.Games
{
    public partial class GameResults : Result
    {

        private t_GameResults _gameResults;

        /*-- Constructors --*/

        /*-- Events --*/

        /*-- Properties --*/

        #region -- Game Property --
        public t_GameResults Game
        {
            get { return _gameResults; }
            set { _gameResults = value; }
        }
        #endregion

        /*-- Methods --*/
        
        /*-- Event Handlers --*/

        /*-- Factory Methods --*/

        #region -- InsertGameResult(String UserName, String Password, GameListEnum gameType, int Score, decimal TestDuration, int Total) Method --
        public static GameResults InsertGameResult(String UserName, String Password, GameListEnum gameType, int Score, decimal TestDuration, int Total)
		{
            GameResults results = new GameResults();
            try 
	        {	        
                LoginResult loginResult = LoginResult.ValidateLogin(UserName, Password, String.Empty);
                if (loginResult.Success)
                {
                    AgileMindEntities db = new AgileMindEntities();
                    t_GameResults gameResults = new t_GameResults();
                    gameResults.Created = DateTime.Now;
                    gameResults.GameId = (int)gameType;
                    gameResults.LoginId = loginResult.LoginInfo.LoginId;
                    gameResults.Score = Score;
                    gameResults.TestDuration = TestDuration;
                    gameResults.Total = Total;
                    db.t_GameResults.AddObject(gameResults);
                    db.SaveChanges();

                    results.Success = true;
                    results.Game = gameResults;

                }
                else
                {
                    results.Error = "Could not login.  Invalid Username/Password";
                }
                
	        }
	        catch (Exception ex)
	        {
                results.Error = ex.Message;
	        }
            return results;
		}
		#endregion

        #region -- InsertGameResultLoginId(String UserName, GameListEnum gameType, int Score, decimal TestDuration, int Total) Method --
        public static GameResults InsertGameResultLoginId(String UserName, GameListEnum gameType, int Score, decimal TestDuration, int Total)
        {
            GameResults results = new GameResults();
            try
            {

                AgileMindEntities db = new AgileMindEntities();
                vwLoginInfo login = (from data in db.vwLoginInfoes where data.LoginName == UserName select data).Single();

                if (login != null)
                {
                    t_GameResults gameResults = new t_GameResults();
                    gameResults.Created = DateTime.Now;
                    gameResults.GameId = (int)gameType;
                    gameResults.LoginId = login.LoginId;
                    gameResults.Score = Score;
                    gameResults.TestDuration = TestDuration;
                    gameResults.Total = Total;
                    db.t_GameResults.AddObject(gameResults);
                    db.SaveChanges();

                    results.Success = true;
                    results.Game = gameResults;

                }
                else
                {
                    results.Error = "Could not login.  Invalid Username/Password";
                }

            }
            catch (Exception ex)
            {
                results.Error = ex.Message;
            }
            return results;
        }
        #endregion
        

    }
}
