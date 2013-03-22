#region -- using declarations --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AgileMind.Website.GamesWS;

#endregion

namespace AgileMind.Website.Games
{
    public partial class UserProfileQuiz : System.Web.UI.Page
    {

        private ProfileQuizInformation _quizInfo;

        /*-- Constructors --*/

        #region -- Constructor() --
        #endregion

        /*-- Events --*/

        /*-- Properties --*/

        #region -- QuizInfo Property --
        public ProfileQuizInformation QuizInfo
        {
            get 
            {
                if (_quizInfo == null)
                {
                    if (Session["GAMEINFO"] != null)
                        _quizInfo = (ProfileQuizInformation)Session["GAMEINFO"];
                }
                return _quizInfo; 
            }
            set { _quizInfo = value; }
        }
        #endregion

        /*-- Methods --*/

        #region -- UpdateScreen() Method --
        private void UpdateScreen()
        {
            if (QuizInfo.CurrentQuestionIndex < QuizInfo.QuizQuestions.Count)
            {
                ProfileQuizQuestion question = QuizInfo.QuizQuestions[QuizInfo.CurrentQuestionIndex];

                uxQuestion.Text = question.Question;
                uxAnswer.Text = string.Empty;

                Session.Add("GAMEINFO", QuizInfo);

                String bar = "<div class=\"bar\" style=\"width: {0}%;\"></div>";
                uxQuestionBegin.Text = (QuizInfo.CurrentQuestionIndex + 1).ToString();
                uxQuestionCount.Text = QuizInfo.QuizQuestions.Count.ToString();
                uxBar.Text = string.Format(bar, (((decimal)(QuizInfo.CurrentQuestionIndex + 1)) / (decimal)QuizInfo.QuizQuestions.Count) * 100);


            }
            else          // Game is over.  Tally Results and show user.
            {
                DateTime finishTime = DateTime.Now;
                int correctScore = 0;
                foreach (ProfileQuizQuestion question in QuizInfo.QuizQuestions)
                {
                    if (question.Answer.Trim().Equals(question.UserAnswer.Trim(), StringComparison.CurrentCultureIgnoreCase))
                        correctScore++;
                }
                TimeSpan elapsedTime = finishTime - QuizInfo.StartTime;
                decimal seconds = ((decimal)elapsedTime.TotalMilliseconds / 1000);

                GamesWS.GamesService gamesService = new GamesService();
                gamesService.InsertGameResultWeb(User.Identity.Name, GameListEnum.UserProfileQuestions, correctScore, seconds, QuizInfo.QuizQuestions.Count);

                String queryString = string.Format("Correct={0}&Total={1}&Seconds={2}", Server.HtmlEncode(correctScore.ToString()), Server.HtmlEncode(QuizInfo.QuizQuestions.Count.ToString()),
                                            Server.HtmlEncode(seconds.ToString()));
                Response.Redirect("GameResults.aspx?" + queryString);
            }
        }
        #endregion


        /*-- Event Handlers --*/

        #region -- Page_Load(object sender, EventArgs e) Event Handler --
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.IsAuthenticated && Session["SessionId"] != null)
                {
                    Guid sessionId = new Guid(Session["SessionId"].ToString());
                    GamesWS.GamesService gamesService = new GamesWS.GamesService();
                    ProfileQuizQuestionRequest result = gamesService.FetchRandomQuizQuestions(sessionId, 10);
                    if (result.Success)
                    {
                        QuizInfo = new ProfileQuizInformation();
                        QuizInfo.QuizQuestions = result.QuestionList.ToList();
                        QuizInfo.StartTime = DateTime.Now;
                        QuizInfo.CurrentQuestionIndex = 0;

                        UpdateScreen();
                    }
                    else  // Show some error information.
                    {

                    }
                }

            }
        }
        #endregion

        #region -- uxNext_Click(object sender, EventArgs e)
        protected void uxNext_Click(object sender, EventArgs e)
        {
            ProfileQuizQuestion question = QuizInfo.QuizQuestions[QuizInfo.CurrentQuestionIndex];
            question.UserAnswer = uxAnswer.Text;
            QuizInfo.CurrentQuestionIndex++;
            UpdateScreen();
        }
        #endregion

    }
}