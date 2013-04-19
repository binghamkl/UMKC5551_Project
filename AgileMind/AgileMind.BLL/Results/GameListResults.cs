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
    public class GameListResults : Result
    {

        private List<t_Game> _gameList = new List<t_Game>();

        /*-- Constructors --*/

        #region -- Constructor() --
        public GameListResults()
        {

        }
        #endregion

        /*-- Events --*/

        /*-- Properties --*/

        #region -- GameList Property --
        public List<t_Game> GameList
        {
            get { return _gameList; }
            set { _gameList = value; }
        }
        #endregion

        /*-- Methods --*/

        /*-- Event Handlers --*/

	    /*-- Factory Method --*/
        
		#region -- FetchGameList() Method --
		public static GameListResults FetchGameList(Guid SessionId)
		{
            GameListResults request = new GameListResults();

            try
            {
                AgileMindEntities agileDB = new AgileMindEntities();

                t_LoginSession session = (from loginSession in agileDB.t_LoginSession where loginSession.LoginSessionId == SessionId && loginSession.ValidTill > DateTime.Now select loginSession).First();
                if (session != null)
                {

                    List<t_Game> gameList = (from gameData in agileDB.t_Game select gameData).ToList();
                    request.GameList = gameList;
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
