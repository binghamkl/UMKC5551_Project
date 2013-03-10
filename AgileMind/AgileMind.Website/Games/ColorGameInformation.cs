#region -- using declarations --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

#endregion

namespace AgileMind.Website.Games
{
    public class ColorGameInformation
    {

        private GamesWS.ColorGameQuestion[] _questionList;
        private DateTime _startTime;
        private int _currentQuestionIndex;

        /*-- Constructors --*/

        #region -- Constructor() --
        public ColorGameInformation()
        {

        }
        #endregion

        /*-- Events --*/

        /*-- Properties --*/

        #region -- QuestionList Property --
        public GamesWS.ColorGameQuestion[] QuestionList
        {
            get { return _questionList; }
            set { _questionList = value; }
        }
        #endregion

        #region -- StartTime Property --
        public DateTime StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }
        #endregion

        #region -- CurrentQuestionIndex Property --
        public int CurrentQuestionIndex
        {
            get { return _currentQuestionIndex; }
            set { _currentQuestionIndex = value; }
        }
        #endregion

        /*-- Methods --*/

        /*-- Event Handlers --*/
	
    }
}