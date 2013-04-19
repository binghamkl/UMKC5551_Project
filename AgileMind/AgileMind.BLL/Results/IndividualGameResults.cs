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
    public class IndividualGameResults : Result
    {

        private List<t_GameResults> _gameResultList = new List<t_GameResults>();

        /*-- Constructors --*/

        #region -- Constructor() --
        public IndividualGameResults()
        {

        }
        #endregion

        /*-- Events --*/

        /*-- Properties --*/

        #region -- GameResultList Property --
        public List<t_GameResults> GameResultList
        {
            get { return _gameResultList; }
            set { _gameResultList = value; }
        }
        #endregion

        /*-- Methods --*/

        /*-- Event Handlers --*/

        /*-- Factory Method --*/

        #region -- FetchResultsForGameType(Guid SessionId, int GameId) Method --
        public static IndividualGameResults FetchResultsForGameType(Guid SessionId, int GameId)
		{
            IndividualGameResults request = new IndividualGameResults();

            try
            {
                AgileMindEntities agileDB = new AgileMindEntities();

                t_LoginSession session = (from loginSession in agileDB.t_LoginSession where loginSession.LoginSessionId == SessionId && loginSession.ValidTill > DateTime.Now select loginSession).First();
                if (session != null)
                {

                    List<t_GameResults> userResults = (from gameResultsData in agileDB.t_GameResults where gameResultsData.LoginId == session.LoginId && gameResultsData.GameId == GameId && gameResultsData.Total > 0 && gameResultsData.TestDuration > 0 orderby gameResultsData.Created select gameResultsData).ToList();
                    request.GameResultList = userResults;
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
