#region -- using declarations --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using AgileMind.BLL.Results;

#endregion

namespace AgileMind.SOAP
{
    /// <summary>
    /// Summary description for GameScoreService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class GameScoreService : System.Web.Services.WebService
    {

        [WebMethod]
        #region -- FetchUserGameResults(Guid SessionId) Method --
        public UserGameResults FetchUserGameResults(Guid SessionId)
        {
            return UserGameResults.FetchUserGameResults(SessionId);
        }
        #endregion

        [WebMethod]
		#region -- FetchIndividualGames() Method --
		public IndividualGameResults FetchIndividualGames(Guid SessionId, int GameId)
		{
            return IndividualGameResults.FetchResultsForGameType(SessionId, GameId);
		}
		#endregion
		
        [WebMethod]
		#region -- FetchGameList() Method --
		public GameListResults FetchGameList(Guid SessionId)
		{
            return GameListResults.FetchGameList(SessionId);
		}
		#endregion
		
    }
}
