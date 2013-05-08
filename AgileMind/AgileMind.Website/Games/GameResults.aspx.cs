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
    public partial class GameResults : System.Web.UI.Page
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

            decimal decCorrect = 0;
            decimal decTotal = 0;
            if (decimal.TryParse(correct, out decCorrect) && decimal.TryParse(total, out decTotal))
            {
                if (decTotal > 0 && decCorrect > 0)
                {
                    if (decCorrect / decTotal * 100 < 60)
                    {
                        uxResultImage.ImageUrl = "~/img/Fail-small.png";
                    }
                }
            }
        }
        #endregion

    }
}