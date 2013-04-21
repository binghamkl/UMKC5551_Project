#region -- using declarations --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

#endregion

namespace AgileMind.Website
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {


        /*-- Constructors --*/

        #region -- Constructor() --
        public SiteMaster()
        {

        }
        #endregion

        /*-- Events --*/

        /*-- Properties --*/

        /*-- Methods --*/

        /*-- Event Handlers --*/

        #region -- Page_Load(object sender, EventArgs e) Event Handler --
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["SessionId"] == null)
                {
                    Session.Abandon();
                    FormsAuthentication.SignOut();
                }
            }
        }
        #endregion

        #region -- HeadLoginView_ViewChanged(object sender, EventArgs e) Event Handler --
        protected void HeadLoginView_ViewChanged(object sender, EventArgs e)
        {
            
        }
        #endregion

        protected void HeadLoginStatus_LoggedOut(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
        }

    }
}
