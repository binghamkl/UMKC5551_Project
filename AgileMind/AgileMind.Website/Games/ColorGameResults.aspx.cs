#region -- using declarations --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace AgileMind.Website.Games
{
    public partial class ColorGameResults : System.Web.UI.Page
    {

        /*-- Constructors --*/

        #region -- Constructor() --
        #endregion

        /*-- Events --*/

        /*-- Properties --*/

        /*-- Methods --*/

        /*-- Event Handlers --*/

        #region -- Page_Load(object sender, EventArgs e) Event Handler --
        protected void Page_Load(object sender, EventArgs e)
        {
            String correct = Server.HtmlDecode(Request.QueryString["Correct"]);
            String total = Server.HtmlDecode(Request.QueryString["Total"]);
            String seconds = Server.HtmlDecode(Request.QueryString["Seconds"]);
            uxCorrect.Text = correct;
            uxTimeSpan.Text = seconds;
            uxTotal.Text = total;
        }
        #endregion

    }
}