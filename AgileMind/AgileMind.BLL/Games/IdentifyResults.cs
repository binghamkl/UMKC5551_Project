#region -- using declarations --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgileMind.BLL.Util;
using AgileMind.DAL.Data;

#endregion

namespace AgileMind.BLL.Games
{
    public class IdentifyResults : Result
    {

        private List<IdentifyQuestion> _questionList = new List<IdentifyQuestion>();

        /*-- Constructors --*/

        #region -- Constructor() --
        public IdentifyResults()
        {

        }
        #endregion

        /*-- Events --*/

        /*-- Properties --*/

        #region -- QuestionList Property --
        public List<IdentifyQuestion> QuestionList
        {
            get { return _questionList; }
            set { _questionList = value; }
        }
        #endregion

        /*-- Methods --*/

        /*-- Event Handlers --*/

        /*-- Static Method --*/

        #region -- FetchQuestionList(Guid SessionId) Method --
        public static IdentifyResults FetchQuestionList(Guid SessionId)
		{
            IdentifyResults request = new IdentifyResults();

            try
            {
                AgileMindEntities agileDB = new AgileMindEntities();

                t_LoginSession session = (from loginSession in agileDB.t_LoginSession where loginSession.LoginSessionId == SessionId && loginSession.ValidTill > DateTime.Now select loginSession).First();
                if (session != null)
                {

                    List<t_Object> objectList = (from objects in agileDB.t_Object select objects).ToList();
                    List<t_ObjectImage> objectImageList = (from objectImage in agileDB.t_ObjectImage select objectImage).ToList();

                    Random randObj = new Random();
                    for (int i = 0; i < 10; i++)
                    {

                        t_Object chosenObject = objectList[randObj.Next(objectList.Count)];
                        List<t_ObjectImage> availableImages = objectImageList.FindAll(delegate(t_ObjectImage findImage) { return findImage.ObjectId == chosenObject.ObjectId; });
                        t_ObjectImage chosenImage = availableImages[randObj.Next(availableImages.Count)];
                        List<t_Object> availableAnswers = objectList.FindAll(delegate(t_Object findObject) { return findObject.ObjectId != chosenObject.ObjectId; });
                        
                        IdentifyQuestion newQuestion = new IdentifyQuestion();
                        newQuestion.Object = chosenObject.Object;
                        newQuestion.ObjectURL = chosenImage.ImageURL;

                        IdentifyAnswer newAnswer = new IdentifyAnswer();
                        newAnswer.Answer = chosenObject.Object;
                        newAnswer.IsCorrect = true;
                        newQuestion.AnswerList.Add(newAnswer);

                        for (int addAnswers = 0; addAnswers < 3; addAnswers++)
                        {
                            newAnswer = new IdentifyAnswer();
                            t_Object randomAnswer = availableAnswers[randObj.Next(availableAnswers.Count)];
                            availableAnswers.Remove(randomAnswer);
                            newAnswer.Answer = randomAnswer.Object;
                            newQuestion.AnswerList.Add(newAnswer);
                        }
                        newQuestion.AnswerList.Shuffle();
                        newQuestion.AnswerList.Shuffle();

                        request.QuestionList.Add(newQuestion);

                    }

                    request.Success = true;
                }
                else
                {
                    request.Error = "Could not find sessionId";
                }
            }
            catch (Exception ex)
            {
                request.Error = ex.Message;
            }
            return request;
        }
		#endregion
		
	
    }
}
