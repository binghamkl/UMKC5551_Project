#region -- using declarations --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace AgileMind.BLL.Util
{
    public class Result
    {

        private bool _success;
        private String _error;

        /*-- Constructors --*/

        #region -- Constructor() --
        public Result()
        {

        }
        #endregion

        /*-- Events --*/

        /*-- Properties --*/

        #region -- Success Property --
        public bool Success
        {
            get { return _success; }
            set { _success = value; }
        }
        #endregion

        #region -- Error Property --
        public String Error
        {
            get { return _error; }
            set { _error = value; }
        }
        #endregion

        /*-- Methods --*/

        /*-- Event Handlers --*/
	
    }
}
