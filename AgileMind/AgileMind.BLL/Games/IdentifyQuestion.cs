#region -- using declarations --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace AgileMind.BLL.Games
{
    public class IdentifyQuestion
    {

        private String _object;
        private String _objectURL;
        private String _userAnswer;
        private List<IdentifyAnswer> _answerList = new List<IdentifyAnswer>();

        /*-- Constructors --*/

        #region -- Constructor() --
        public IdentifyQuestion()
        {

        }
        #endregion

        /*-- Events --*/

        /*-- Properties --*/

        #region -- Object Property --
        public String Object
        {
            get { return _object; }
            set { _object = value; }
        }
        #endregion

        #region -- ObjectURL Property --
        public String ObjectURL
        {
            get { return _objectURL; }
            set { _objectURL = value; }
        }
        #endregion

        #region -- UserAnswer Property --
        public String UserAnswer
        {
            get { return _userAnswer; }
            set { _userAnswer = value; }
        }
        #endregion

        #region -- AnswerList Property --
        public List<IdentifyAnswer> AnswerList
        {
            get { return _answerList; }
            set { _answerList = value; }
        }
        #endregion

        /*-- Methods --*/

        /*-- Event Handlers --*/
	
    }
}
