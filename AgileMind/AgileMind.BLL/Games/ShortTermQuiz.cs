#region -- using declarations --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace AgileMind.BLL.Games
{
    public class ShortTermQuiz
    {

        private String _statement;
        private List<ShortTermQuestion> _questionList = new List<ShortTermQuestion>();
        private int _shortTermQuizId;

        /*-- Constructors --*/

        #region -- Constructor() --
        public ShortTermQuiz()
        {

        }
        #endregion

        /*-- Events --*/

        /*-- Properties --*/

        #region -- Statement Property --
        public String Statement
        {
            get { return _statement; }
            set { _statement = value; }
        }
        #endregion

        #region -- QuestionList Property --
        public List<ShortTermQuestion> QuestionList
        {
            get { return _questionList; }
            set { _questionList = value; }
        }
        #endregion

        #region -- ShortTermQuizId Property --
        public int ShortTermQuizId
        {
            get { return _shortTermQuizId; }
            set { _shortTermQuizId = value; }
        }
        #endregion

        /*-- Methods --*/

        /*-- Event Handlers --*/
	
    }
}
