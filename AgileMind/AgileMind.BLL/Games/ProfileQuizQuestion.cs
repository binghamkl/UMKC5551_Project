#region -- using declarations --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace AgileMind.BLL.Games
{
    public class ProfileQuizQuestion
    {

        private String _question;
        private String _answer;
        private String _userAnswer;

        /*-- Constructors --*/

        #region -- Constructor() --
        public ProfileQuizQuestion()
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

        #region -- Answer Property --
        public String Answer
        {
            get { return _answer; }
            set { _answer = value; }
        }
        #endregion

        #region -- UserAnswer Property --
        public String UserAnswer
        {
            get { return _userAnswer; }
            set { _userAnswer = value; }
        }
        #endregion

        /*-- Methods --*/

        /*-- Event Handlers --*/
	
    }
}
