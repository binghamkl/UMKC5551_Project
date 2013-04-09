#region -- using declarations --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace AgileMind.BLL.Games
{
    public class ShortTermQuestion
    {

        private String _question;
        private bool _userCorrect;
        private List<ShortTermAnswer> _answerList = new List<ShortTermAnswer>();

        /*-- Constructors --*/

        #region -- Constructor() --
        public ShortTermQuestion()
        {

        }
        #endregion

        /*-- Events --*/

        /*-- Properties --*/

        #region -- Question Property --
        public String Question
        {
            get { return _question; }
            set { _question = value; }
        }
        #endregion

        #region -- UserCorrect Property --
        public bool UserCorrect
        {
            get { return _userCorrect; }
            set { _userCorrect = value; }
        }
        #endregion

        #region -- AnswerList Property --
        public List<ShortTermAnswer> AnswerList
        {
            get { return _answerList; }
            set { _answerList = value; }
        }
        #endregion

        /*-- Methods --*/

        /*-- Event Handlers --*/
	
    }
}
