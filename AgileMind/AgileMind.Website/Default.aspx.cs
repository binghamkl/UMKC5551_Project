#region -- using declarations --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AgileMind.Website.GamesScoreWS;
using System.Web.UI.DataVisualization.Charting;

#endregion

namespace AgileMind.Website
{
    public partial class _Default : System.Web.UI.Page
    {

        private String _userTotal;

        /*-- Constructors --*/

        #region -- Constructor() --
        public _Default()
        {

        }
        #endregion

        /*-- Events --*/

        /*-- Properties --*/

        #region -- UserTotal Property --
        public String UserTotal
        {
            get { return _userTotal; }
            set { _userTotal = value; }
        }
        #endregion

        /*-- Methods --*/

        /*-- Event Handlers --*/

        #region -- Page_Load(object sender, EventArgs e) Event Handler --
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["SessionId"] != null)
                {
                    uxLoggedIn.Visible = true;
                    Guid sessionId = new Guid(Session["SessionId"].ToString());

                    GamesScoreWS.GameScoreService gsClient = new GamesScoreWS.GameScoreService();
                    UserGameResults userTotalResults = gsClient.FetchUserGameResults(sessionId);
                    if (userTotalResults.Success)
                    {
                        UserTotal = userTotalResults.UserScore.ToString("#.000");
                        uxGamesRepeater.DataSource = userTotalResults.MeanGameScores;
                        uxGamesRepeater.DataBind();
                        
                    }
                }
            }
        }
        #endregion

        #region -- uxGamesRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e) Event Handler --
        protected void uxGamesRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            UserMeanGameScore mgs = (UserMeanGameScore)e.Item.DataItem;
            //e.Item.FindControl("uxIndividualCharge");

            if (Session["SessionId"] != null)
            {
                Guid sessionId = new Guid(Session["SessionId"].ToString());
                GamesScoreWS.GameScoreService gsClient = new GamesScoreWS.GameScoreService();
                IndividualGameResults igResults = gsClient.FetchIndividualGames(sessionId, mgs.GameId);
                if (igResults.Success)
                {
                    Chart createChart = (Chart)e.Item.FindControl("uxIndividualCharge");
                    createChart.Titles.Add(mgs.Game);
                    Series resultsSeries = createChart.Series["GameScores"];
                    foreach (t_GameResults gr in igResults.GameResultList)
                    {
                        DataPoint dp = new DataPoint();
                        decimal indexical = ((decimal)gr.Score / (decimal)gr.Total) * 100;
                        dp.SetValueXY(gr.Created.ToShortDateString(), indexical);
                        dp.ToolTip = string.Format("{0} out of {1} in {2} seconds", gr.Score, gr.Total, gr.TestDuration);
                        resultsSeries.Points.Add(dp);
                    }
                }
            }

            
        }
        #endregion

    }
}
