#region -- using declarations --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgileMind.BLL.Util;

#endregion

namespace AgileMind.BLL.Games
{
    public class ColorGameResult : Result
    {

        private List<ColorGameQuestion> _questions = new List<ColorGameQuestion>();

        /*-- Constructors --*/

        #region -- Constructor() --
        public ColorGameResult()
        {

        }
        #endregion

        /*-- Events --*/

        /*-- Properties --*/

        #region -- Questions Property --
        public List<ColorGameQuestion> Questions
        {
            get { return _questions; }
            set { _questions = value; }
        }
        #endregion

        /*-- Methods --*/

        /*-- Event Handlers --*/

        #region -- CreateGame() Method --
        public static ColorGameResult CreateGame()
        {
            ColorGameResult result = new ColorGameResult();

            Random rand = new Random();
            for (int questionCount = 0; questionCount < 10; questionCount++)
            {
                ColorGameQuestion newQuestion = new ColorGameQuestion();

                newQuestion.LeftColor = RandomColor(rand);
                newQuestion.LeftWord = RandomColor(rand);
                newQuestion.RightColor = RandomColor(rand);
                newQuestion.RightWord = RandomColor(rand);

                result.Questions.Add(newQuestion);
            }

            result.Success = true;
            return result;
        }
        #endregion

        #region -- RandomColor() Method --
        private static string RandomColor(Random rand)
        {
            int color = rand.Next(3);
            switch (color)
            {
                case 0:
                    return "Red";
                case 1:
                    return "Green";
                case 2:
                    return "Blue";
                default:
                    return "";
                    break;
            }
        }
        #endregion

    }
}
