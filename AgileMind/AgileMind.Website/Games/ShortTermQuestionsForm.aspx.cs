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

                        Guid sessionId = (Guid)Session["SessionId"];
                            DateTime now = DateTime.Now;
                            DateTime future = now.AddSeconds(Quiz.QuestionDelay);
                            Session.Add("QuizTime", future);
                
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
                }
            }
        }
        #endregion

    }
}