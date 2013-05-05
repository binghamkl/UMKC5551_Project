#region -- using declarations --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace AgileMind.BLL.Games
{
    public class IdentifyAnswer
    {

        private String _answer;
        private bool _isCorrect;

        /*-- Constructors --*/

        #region -- Constructor() --
        public IdentifyAnswer()
        {

        }
        #endregion

        /*-- Events --*/

        /*-- Properties --*/

        #region -- Answer Property --
        public String Answer
        {
            get { return _answer; }
            set { _answer = value; }
        }
        #endregion

        #region -- IsCorrect Property --
        public bool IsCorrect
        {
            get { return _isCorrect; }
            set { _isCorrect = value; }
        }
        #endregion

        /*-- Methods --*/

        /*-- Event Handlers --*/
	
    }
}
