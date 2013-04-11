#region -- using declarations --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using AgileMind.Website.GamesWS;

#endregion

namespace AgileMind.Website.Games
{
    public partial class ShortTermQuestionsForm : System.Web.UI.Page
    {

        private ShortTermQuiz _quiz;
        private int? _questionNumber;

        /*-- Constructors --*/

        #region -- Constructor() --
        #endregion

        /*-- Events --*/

        /*-- Properties --*/

        #region -- Quiz Property --
        public ShortTermQuiz Quiz
        {
            get 
            {
                if (_quiz == null && Session["ShortTermQuiz"] != null)
                {
                    Quiz = (ShortTermQuiz)Session["ShortTermQuiz"];
                }
                return _quiz; 
            }
            set 
            { 
                _quiz = value; 
            }
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
            if (QuestionNumber.HasValue)
            {
                uxQuestion.Text = Quiz.QuestionList[QuestionNumber.Value].Question;
                uxAnswers.DataSource = Quiz.QuestionList[QuestionNumber.Value].AnswerList;
                uxAnswers.DataBind();
            }
        }
        #endregion

        /*-- Event Handlers --*/

        #region -- Page_Load(object sender, EventArgs e) Event Handler --
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Quiz == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            else
	        {
                if (!Page.IsPostBack)
                {
                    if (Session["SessionId"] != null)
                    {

                        DateTime now = DateTime.Now;
                        DateTime future = now.AddSeconds(Quiz.QuestionDelay);
                        Session.Add("QuizTime", future);
                        uxTimeLeft.Text = Quiz.QuestionDelay.ToString();
                
                    }
                    else
                    {
                        FormsAuthentication.RedirectToLoginPage();
                    }
                }

	        }
        }
        #endregion

        #region -- uxCountdown_Tick(object sender, EventArgs e) Event Handler --
        protected void uxCountdown_Tick(object sender, EventArgs e)
        {
            DateTime current = DateTime.Now;
            DateTime endPoint = (DateTime)Session["QuizTime"];
            if (Session["QuizTime"] != null)
            {
                if (current < endPoint)
                {
                    TimeSpan variable = endPoint - current;

                    int secondsLeft = (int)variable.TotalSeconds;
                    uxTimeLeft.Text = secondsLeft.ToString();
                }
                else
                {
                    uxQuestionsPanel.Visible = true;
                    uxCountDownPanel.Visible = false;
                    uxTimeLeft.Enabled = false;
                    QuestionNumber = 0;
                    SetupUI();

                    Session.Add("BeginTime", DateTime.Now);

                }
            }
        }
        #endregion

        #region -- uxNextQuestion_Click(object sender, EventArgs e) Event Handler --
        protected void uxNextQuestion_Click(object sender, EventArgs e)
        {

            if (uxAnswers.SelectedIndex > -1)
            {
                String answer = uxAnswers.SelectedItem.Text;
                ShortTermAnswer foundAnswer = Quiz.QuestionList[QuestionNumber.Value].AnswerList.ToList().Find(delegate(ShortTermAnswer findAnswer) 
                {
                    return findAnswer.Answer == answer;
                });
                if (foundAnswer.IsCorrect)
                    Quiz.QuestionList[QuestionNumber.Value].UserCorrect = true;
            }

            QuestionNumber = QuestionNumber + 1;

            if (QuestionNumber.Value >= Quiz.QuestionList.Count())
            {
                DateTime finishTime = DateTime.Now;
                int correctScore = 0;
                foreach (ShortTermQuestion question in Quiz.QuestionList)
                {
                    if (question.UserCorrect)
                        correctScore++;
                }
                DateTime startTime = (DateTime)Session["BeginTime"];
                TimeSpan elapsedTime = finishTime - startTime;
                decimal seconds = ((decimal)elapsedTime.TotalMilliseconds / 1000);

                GamesWS.GamesService gamesService = new GamesService();
                gamesService.InsertGameResultWeb(User.Identity.Name, GameListEnum.UserProfileQuestions, correctScore, seconds, Quiz.QuestionList.Count());

                String queryString = string.Format("Correct={0}&Total={1}&Seconds={2}", Server.HtmlEncode(correctScore.ToString()), Server.HtmlEncode(Quiz.QuestionList.Count().ToString()),
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