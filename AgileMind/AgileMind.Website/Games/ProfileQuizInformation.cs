#region -- using declarations --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

#endregion

namespace AgileMind.Website.Games
{
    public class ProfileQuizInformation
    {

        private List<GamesWS.ProfileQuizQuestion> _quizQuestions = new List<GamesWS.ProfileQuizQuestion>();
        private DateTime _startTime;
        private int _currentQuestionIndex;

        /*-- Constructors --*/

        #region -- Constructor() --
        public ProfileQuizInformation()
        {

        }
        #endregion

        /*-- Events --*/

        /*-- Properties --*/

        #region -- QuizQuestions Property --
        public List<GamesWS.ProfileQuizQuestion> QuizQuestions
        {
            get { return _quizQuestions; }
            set { _quizQuestions = value; }
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