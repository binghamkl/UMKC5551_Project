#region -- using declarations --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AgileMind.Website.GamesWS;
using System.Web.Security;

#endregion

namespace AgileMind.Website.Games
{
    public partial class UserProfileQuestions : System.Web.UI.Page
    {

        private List<vwQuestionAnswer> _profileQuestions;
        private int? _questionNumber;

        /*-- Constructors --*/

        #region -- Constructor() --
        public UserProfileQuestions()
        {

        }
        #endregion

        /*-- Events --*/

        /*-- Properties --*/

        #region -- ProfileQuestions Property --
        public List<vwQuestionAnswer> ProfileQuestions
        {
            get 
            {
                if (_profileQuestions == null)
                {
                    if (Session["ProfileQuestions"] != null)
                        ProfileQuestions = (List<vwQuestionAnswer>)Session["ProfileQuestions"];
                }
                return _profileQuestions; 
            }
            set { _profileQuestions = value; }
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

        #region -- SaveQuestionsIntoSession(List<vwQuestionAnswer> questionList) Method --
        private void SaveQuestionsIntoSession(List<vwQuestionAnswer> questionList)
        {
            Session.Add("ProfileQuestions", questionList);
            ProfileQuestions = questionList;
        }
        #endregion

        #region -- SetupUI() Event Handler --
        private void SetupUI()
        {
            if (QuestionNumber.HasValue)
            {
                uxQuestion.Text = ProfileQuestions[QuestionNumber.Value].Question;
                uxAnswer.Text = ProfileQuestions[QuestionNumber.Value].Answer;
                if (QuestionNumber + 1 >= ProfileQuestions.Count)
                    uxNextFinish.Text = "Finish";
                else
                    uxNextFinish.Text = "Next";
                if (QuestionNumber.Value == 0)
                    uxBack.Enabled = false;
                else
                    uxBack.Enabled = true;

                String bar = "<div class=\"bar\" style=\"width: {0}%;\"></div>";
                uxQuestionBegin.Text = (QuestionNumber.Value + 1).ToString();
                uxQuestionCount.Text = ProfileQuestions.Count.ToString();
                uxBar.Text = string.Format(bar, (((decimal)(QuestionNumber.Value + 1) ) / (decimal)ProfileQuestions.Count) * 100);
            }
        }
        #endregion

        #region -- SaveScreen() Method --
        private void SaveScreen()
        {
            if (QuestionNumber.HasValue) ;
            {
                ProfileQuestions[QuestionNumber.Value].Answer = uxAnswer.Text;
                if (String.IsNullOrEmpty(uxAnswer.Text))
                    ProfileQuestions[QuestionNumber.Value].NoAnswer = true;
            }
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
                    UserProfileQuestionsResults questionResults = gameClient.FetchUserProfileQuestions(sessionId);

                    QuestionNumber = 0;

                    SaveQuestionsIntoSession(questionResults.QuestionList.ToList());
                    SetupUI();
                }
                else
                {
                    FormsAuthentication.RedirectToLoginPage();
                }
            }
        }
        #endregion

        #region -- uxBack_Click(object sender, EventArgs e) Event Handler --
        protected void uxBack_Click(object sender, EventArgs e)
        {
            SaveScreen();

            QuestionNumber = QuestionNumber - 1;
            SetupUI();

        }
        #endregion

        #region -- uxNextFinish_Click(object sender, EventArgs e) Event Handler --
        protected void uxNextFinish_Click(object sender, EventArgs e)
        {

            SaveScreen();

            if (QuestionNumber.Value + 1 >= ProfileQuestions.Count)
            {
                // need to save and redirect.
                GamesWS.GamesService gameClient = new GamesWS.GamesService();
                Guid sessionId = (Guid)Session["SessionId"];
                gameClient.SaveUserProfileQuestions(ProfileQuestions.ToArray(), sessionId);
                Response.Redirect("~/Games/UserProfileComplete.aspx");
            }
            else
            {
                QuestionNumber = QuestionNumber + 1;
                SetupUI();
            }
        }
        #endregion

    }
}