#region -- using declarations --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace AgileMind.BLL.Games
{
    public class ColorGameQuestion
    {

        private String _leftWord;
        private String _leftColor;
        private String _rightWord;
        private String _rightColor;
        private bool _userCorrect;

        /*-- Constructors --*/

        #region -- Constructor() --
        public ColorGameQuestion()
        {

        }
        #endregion

        /*-- Events --*/

        /*-- Properties --*/

        #region -- LeftWord Property --
        public String LeftWord
        {
            get { return _leftWord; }
            set { _leftWord = value; }
        }
        #endregion

        #region -- LeftColor Property --
        public String LeftColor
        {
            get { return _leftColor; }
            set { _leftColor = value; }
        }
        #endregion

        #region -- RightWord Property --
        public String RightWord
        {
            get { return _rightWord; }
            set { _rightWord = value; }
        }
        #endregion

        #region -- RightColor Property --
        public String RightColor
        {
            get { return _rightColor; }
            set { _rightColor = value; }
        }
        #endregion

        #region -- IsMatch Property --
        public bool IsMatch
        {
            get { return LeftWord.Equals(RightColor); }
        }
        #endregion

        #region -- UserCorrect Property --
        public bool UserCorrect
        {
            get { return _userCorrect; }
            set { _userCorrect = value; }
        }
        #endregion

        /*-- Methods --*/

        /*-- Event Handlers --*/
	
    }
}
