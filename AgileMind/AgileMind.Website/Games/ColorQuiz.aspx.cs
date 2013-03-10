#region -- using declarations --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using AgileMind.Website.GamesWS;

#endregion

namespace AgileMind.Website.Games
{
    public partial class ColorQuiz : System.Web.UI.Page
    {

        private ColorGameInformation _colorGame;

        /*-- Constructors --*/

        #region -- Constructor() --
        #endregion

        /*-- Events --*/

        /*-- Properties --*/

        #region -- ColorGameInfo Property --
        public ColorGameInformation ColorGameInfo
        {
            get 
            {
                if (_colorGame == null)
                {
                    if (Session["COLORGAMEINFO"] != null)
                        _colorGame = (ColorGameInformation)Session["COLORGAMEINFO"];
                }
                return _colorGame; 
            }
            set { _colorGame = value; }
        }
        #endregion

        /*-- Methods --*/

        #region -- UpdateScreen() Method --
        private void UpdateScreen()
        {
            if (ColorGameInfo.CurrentQuestionIndex < ColorGameInfo.QuestionList.Count())
            {
                ColorGameQuestion question = ColorGameInfo.QuestionList[ColorGameInfo.CurrentQuestionIndex];
                //uxLeftScreen.Text = "";
                uxLeftScreen.Text = question.LeftWord;
                uxLeftScreen.ForeColor = System.Drawing.Color.FromName(question.LeftColor);
                uxRightScreen.Text = question.RightWord;
                uxRightScreen.ForeColor = System.Drawing.Color.FromName(question.RightColor);

                Session.Add("COLORGAMEINFO", ColorGameInfo);
            }
            else          // Game is over.  Tally Results and show user.
            {
                DateTime finishTime = DateTime.Now;
                int correctScore = 0;
                foreach (ColorGameQuestion question in ColorGameInfo.QuestionList)
                {
                    if (question.UserCorrect)
                        correctScore++;
                }
                TimeSpan elapsedTime = finishTime - ColorGameInfo.StartTime;
                decimal seconds = ((decimal)elapsedTime.TotalMilliseconds / 1000);

                GamesWS.GamesService gamesService = new GamesService();
                gamesService.InsertGameResultWeb(User.Identity.Name, GameListEnum.ColorQuiz, correctScore, seconds, ColorGameInfo.QuestionList.Count());

                String queryString = string.Format("Correct={0}&Total={1}&Seconds={2}", Server.HtmlEncode(correctScore.ToString()), Server.HtmlEncode(ColorGameInfo.QuestionList.Count().ToString()), 
                                            Server.HtmlEncode(seconds.ToString()));
                Response.Redirect("ColorGameResults.aspx?" + queryString);
            }
        }
        #endregion

        /*-- Event Handlers --*/

        #region -- Page_Load(object sender, EventArgs e) Event Handler --
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.IsAuthenticated)
                {
                    GamesWS.GamesService gamesService = new GamesWS.GamesService();
                    ColorGameResult result = gamesService.FetchColorGame();
                    if (result.Success)
                    {
                        ColorGameInfo = new ColorGameInformation();
                        ColorGameInfo.QuestionList = result.Questions;
                        ColorGameInfo.StartTime = DateTime.Now;
                        ColorGameInfo.CurrentQuestionIndex = 0;
                        
                        UpdateScreen();
                    }
                    else  // Show some error information.
                    {
                        
                    }
                }
                
            }
        }
        #endregion

        #region -- uxLeftChoice_Click(object sender, EventArgs e) Event Handler --
        protected void uxNoMatchChoice_Click(object sender, EventArgs e)
        {
            ColorGameQuestion question = ColorGameInfo.QuestionList[ColorGameInfo.CurrentQuestionIndex];
            if (question.IsMatch)
                question.UserCorrect = false;
            else
                question.UserCorrect = true;
            ColorGameInfo.CurrentQuestionIndex++;
            UpdateScreen();
        }
        #endregion

        #region -- uxRightChoice_Click(object sender, EventArgs e) Event Handler --
        protected void uxMatchChoice_Click(object sender, EventArgs e)
        {
            ColorGameQuestion question = ColorGameInfo.QuestionList[ColorGameInfo.CurrentQuestionIndex];
            if (question.IsMatch)
                question.UserCorrect = true;
            else
                question.UserCorrect = false;
            ColorGameInfo.CurrentQuestionIndex++;
            UpdateScreen();
        }
        #endregion

    }
}