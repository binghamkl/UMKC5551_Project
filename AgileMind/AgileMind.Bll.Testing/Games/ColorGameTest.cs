#region -- using declarations --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AgileMind.BLL.Games;

#endregion

namespace AgileMind.Bll.Testing.Games
{
    [TestFixture()]
    public class ColorGameTest
    {

        #region -- CreateColorGameReturnsColorGameResult() Method --
        [Test()]
        public void CreateColorGameReturnsColorGameResult()
        {
            AgileMind.BLL.Games.ColorGameResult result = AgileMind.BLL.Games.ColorGameResult.CreateGame();
            Assert.IsNotNull(result);
        }
        #endregion
	
		#region -- ColorGameQuestionHasPropertiesForLeftWordLeftColorRightWordRightColorIsMatchAndUserCorrect() Method --
		[Test()]
        public void ColorGameQuestionHasPropertiesForLeftWordLeftColorRightWordRightColorIsMatchAndUserCorrect()
		{
            AgileMind.BLL.Games.ColorGameQuestion gameQuestion = new AgileMind.BLL.Games.ColorGameQuestion();
            gameQuestion.LeftColor = "Red";
            Assert.AreEqual("Red", gameQuestion.LeftColor);

            gameQuestion.LeftWord = "Blue";
            Assert.AreEqual("Blue", gameQuestion.LeftWord);

            gameQuestion.RightColor = "Green";
            Assert.AreEqual("Green", gameQuestion.RightColor);

            gameQuestion.RightWord = "Purple";
            Assert.AreEqual("Purple", gameQuestion.RightWord);

            gameQuestion.UserCorrect = true;
            Assert.IsTrue(gameQuestion.UserCorrect);
		}
		#endregion

        #region -- ColorGameQuestionReturnsIsMatchIfLeftWordEqualsRightColorElseReturnsFalse() Method --
        [Test()]
        public void ColorGameQuestionReturnsIsMatchIfLeftWordEqualsRightColorElseReturnsFalse()
        {
            AgileMind.BLL.Games.ColorGameQuestion gameQuestion = new AgileMind.BLL.Games.ColorGameQuestion();

            gameQuestion.LeftWord = "Blue";
            gameQuestion.RightColor = "Blue";

            Assert.IsTrue(gameQuestion.IsMatch);

            gameQuestion.RightColor = "NotBlue";
            Assert.IsFalse(gameQuestion.IsMatch);
        }
        #endregion

        #region -- CreateColorGameReturnsColorGameResultsWithTenQuestions() Method --
        [Test()]
        public void CreateColorGameReturnsColorGameResultsWithTenQuestions()
        {
            AgileMind.BLL.Games.ColorGameResult result = AgileMind.BLL.Games.ColorGameResult.CreateGame();
            List<ColorGameQuestion> questions = result.Questions;
            Assert.IsNotNull(questions);
            Assert.AreEqual(10, questions.Count);

        }
        #endregion
	
    }
}
