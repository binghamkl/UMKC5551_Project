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
    public partial class ShortTerm : System.Web.UI.Page
    {


        /*-- Constructors --*/

        #region -- Constructor() --
        public ShortTerm()
        {

        }
        #endregion

        /*-- Events --*/

        /*-- Properties --*/

        /*-- Methods --*/

        #region -- SaveQuestionsIntoSession(ShortTermQuiz quiz) Method --
        private void SaveQuestionsIntoSession(ShortTermQuiz quiz)
        {
            // TODO: Implement this method
            Session.Add("ShortTermQuiz", quiz);
        }
        #endregion

        #region -- SetupUI() Method --
        private void SetupUI()
        {
            // TODO: Implement this method
            ShortTermQuiz quiz = (ShortTermQuiz)Session["ShortTermQuiz"];
            uxStory.Text = quiz.Statement;
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
                    ShortTermQuizResult questionResults = gameClient.FetchShortTermMemoryQuiz(sessionId);

                    SaveQuestionsIntoSession(questionResults.Quiz);
                    SetupUI();
                }
                else
                {
                    FormsAuthentication.RedirectToLoginPage();
                }
            }
        }
        #endregion

        #region -- uxContinue_Click(object sender, EventArgs e) Event Handler --
        protected void uxContinue_Click(object sender, EventArgs e)
        {
            Response.Redirect("ShortTermQuestionsForm.aspx");
        }
        #endregion

    }
}