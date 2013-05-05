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
    public partial class Identify : System.Web.UI.Page
    {

        private int? _questionNumber;
        private List<IdentifyQuestion> _questionList;

        /*-- Constructors --*/

        #region -- Constructor() --
        #endregion

        /*-- Events --*/

        /*-- Properties --*/

        #region -- QuestionList Property --
        public List<IdentifyQuestion> QuestionList
        {
            get 
            {
                if (_questionList == null)
                {
                    if (Session["Quiz"] != null)
                    {
                        QuestionList = ((IdentifyQuestion[])Session["Quiz"]).ToList();
                    }
                }
                return _questionList; 
            }
            set { _questionList = value; }
        }
        #endregion

        #region -- QuestionNumber Property --
        public int? QuestionNumber
        {
            get
            {
                if (!_questionNumber.HasValue)
                {
                    String questionNumberString = uxQuestionNumber.Value;
                    if (!string.IsNullOrEmpty(questionNumberString))
                    {
                        int qNum;
                        if (int.TryParse(questionNumberString, out qNum))
                            _questionNumber = qNum;
                    }
                }
                return _questionNumber;
            }
            set
            {
                _questionNumber = value;
                uxQuestionNumber.Value = _questionNumber.ToString();
            }
        }
        #endregion

        /*-- Methods --*/

        #region -- SetupUI() Method --
        private void SetupUI()
        {
            if (QuestionNumber.HasValue && QuestionNumber.Value < QuestionList.Count)
            {
                uxImage.ImageUrl = QuestionList[QuestionNumber.Value].ObjectURL;
                uxAnswers.DataSource = QuestionList[QuestionNumber.Value].AnswerList;
                uxAnswers.DataBind();

                String bar = "<div class=\"bar\" style=\"width: {0}%;\"></div>";
                uxQuestionBegin.Text = (QuestionNumber.Value + 1).ToString();
                uxQuestionCount.Text = QuestionList.Count.ToString();
                uxBar.Text = string.Format(bar, (((decimal)(QuestionNumber.Value + 1)) / (decimal)QuestionList.Count) * 100);

            }
        }
        #endregion

        #region -- SaveQuestionsIntoSession(IdentifyQuestion[] identifyQuestion) Method --
        private void SaveQuestionsIntoSession(IdentifyQuestion[] identifyQuestion)
        {
            Session.Add("Quiz", identifyQuestion);
        }
        #endregion

        /*-- Event Handlers --*/

        #region -- Page_Load(object sender, EventArgs e) Event Handler --
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["SessionId"] != null)
                {
                    GamesWS.GamesService gameClient = new GamesWS.GamesService();
                    Guid sessionId = (Guid)Session["SessionId"];
                    IdentifyResults questionResults = gameClient.FetchIdentifyQuestions(sessionId);

                    SaveQuestionsIntoSession(questionResults.QuestionList);
                    Session.Add("BeginTime", DateTime.Now);
                    QuestionNumber = 0;
                    SetupUI();
                }
                else
                {
                    FormsAuthentication.RedirectToLoginPage();
                }
            }
        }
        #endregion

        #region -- uxNextQuestion_Click(object sender, EventArgs e) Event handler --
        protected void uxNextQuestion_Click(object sender, EventArgs e)
        {
            if (uxAnswers.SelectedIndex > -1)
            {
                String answer = uxAnswers.SelectedItem.Text;
                QuestionList[QuestionNumber.Value].UserAnswer = answer;
            }

            QuestionNumber = QuestionNumber + 1;

            if (QuestionNumber.Value >= QuestionList.Count)
            {
                DateTime finishTime = DateTime.Now;
                int correctScore = 0;
                foreach (IdentifyQuestion question in QuestionList)
                {
                    if (question.Object.Equals(question.UserAnswer, StringComparison.CurrentCultureIgnoreCase))
                        correctScore++;
                }
                DateTime startTime = (DateTime)Session["BeginTime"];
                TimeSpan elapsedTime = finishTime - startTime;
                decimal seconds = ((decimal)elapsedTime.TotalMilliseconds / 1000);

                GamesWS.GamesService gamesService = new GamesService();
                gamesService.InsertGameResultWeb(User.Identity.Name, GameListEnum.Identify, correctScore, seconds, QuestionList.Count);

                String queryString = string.Format("Correct={0}&Total={1}&Seconds={2}", Server.HtmlEncode(correctScore.ToString()), Server.HtmlEncode(QuestionList.Count().ToString()),
                                            Server.HtmlEncode(seconds.ToString()));
                Response.Redirect("GameResults.aspx?" + queryString);
            }
            else
            {
                SetupUI();
            }
            

        }
        #endregion

    }
}