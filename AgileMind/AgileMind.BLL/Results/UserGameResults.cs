#region -- using declarations --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgileMind.BLL.Util;
using AgileMind.DAL.Data;

#endregion

namespace AgileMind.BLL.Results
{
    public class UserGameResults : Result
    {

        private List<UserMeanGameScore> _meanGameScores = new List<UserMeanGameScore>();
        private decimal _userScore;

        /*-- Constructors --*/

        #region -- Constructor() --
        public UserGameResults()
        {

        }
        #endregion

        /*-- Events --*/

        /*-- Properties --*/

        #region -- MeanGameScores Property --
        public List<UserMeanGameScore> MeanGameScores
        {
            get { return _meanGameScores; }
            set { _meanGameScores = value; }
        }
        #endregion

        #region -- UserScore Property --
        public decimal UserScore
        {
            get { return _userScore; }
            set { _userScore = value; }
        }
        #endregion

        /*-- Methods --*/

        /*-- Event Handlers --*/
	    
        /*-- Load User Scores --*/
        
		#region -- FetchUserGameResults() Method --
		public static UserGameResults FetchUserGameResults(Guid SessionId)
		{
            UserGameResults request = new UserGameResults();

            try
            {
                AgileMindEntities agileDB = new AgileMindEntities();

                t_LoginSession session = (from loginSession in agileDB.t_LoginSession where loginSession.LoginSessionId == SessionId && loginSession.ValidTill > DateTime.Now select loginSession).First();
                if (session != null)
                {

                    List<t_GameResults> userResults = (from gameResultsData in agileDB.t_GameResults where gameResultsData.LoginId == session.LoginId && gameResultsData.Total > 0 && gameResultsData.TestDuration > 0 orderby gameResultsData.GameId select gameResultsData).ToList();
                    List<t_Game> gameList = (from gameData in agileDB.t_Game select gameData).ToList();
                    foreach (t_Game game in gameList)
                    {
                        List<t_GameResults> gameResultsList = userResults.FindAll(delegate(t_GameResults findResults) { return findResults.GameId == game.GameId; });
                        if (gameResultsList.Count > 0)
                        {
                            decimal gameScoreTotal = 0;
                            foreach (t_GameResults gameResult in gameResultsList)
                            {
                                gameScoreTotal += ((decimal)gameResult.Score / (decimal)gameResult.Total) * 100 / gameResult.TestDuration.Value;

                            }
                            UserMeanGameScore newGameScore = new UserMeanGameScore();
                            newGameScore.Game = game.Game;
                            newGameScore.GameDeviation = (decimal)game.stdev;
                            newGameScore.GameId = game.GameId;
                            newGameScore.GameMean = (decimal)game.Mean;
                            newGameScore.UserMean = gameScoreTotal / gameResultsList.Count;
                            newGameScore.MeanDiff = newGameScore.UserMean - newGameScore.GameMean;
                            newGameScore.UserDeflection = newGameScore.MeanDiff / newGameScore.GameDeviation;

                            request.MeanGameScores.Add(newGameScore);
                            
                        }

                    }

                    decimal userDeflectionTotal = 0;
                    foreach (UserMeanGameScore mgs in request.MeanGameScores)
                    {
                        userDeflectionTotal += mgs.UserDeflection;
                    }
                    request.UserScore = userDeflectionTotal / request.MeanGameScores.Count;

                    request.Success = true;
                }
                else
                {
                    request.Error = "Could not find sessionId";
                }
            }
            catch (Exception ex)
            {
                request.Error = ex.Message;
            }
            return request;
        }
		#endregion
		
    }
}
