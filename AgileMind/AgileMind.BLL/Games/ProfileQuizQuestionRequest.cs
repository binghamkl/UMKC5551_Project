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
    public class ProfileQuizQuestionRequest : Result
    {

        private List<ProfileQuizQuestion> _questionList = new List<ProfileQuizQuestion>();

        /*-- Constructors --*/

        #region -- Constructor() --
        public ProfileQuizQuestionRequest()
        {

        }
        #endregion

        /*-- Events --*/

        /*-- Properties --*/

        #region -- QuestionList Property --
        public List<ProfileQuizQuestion> QuestionList
        {
            get { return _questionList; }
            set { _questionList = value; }
        }
        #endregion

        /*-- Methods --*/

        /*-- Event Handlers --*/

        /*-- Factory Method --*/

        #region -- FetchRandomQuizQuestions(Guid SessionId, int QuestionCount) Method --
        public static ProfileQuizQuestionRequest FetchRandomQuizQuestions(Guid SessionId, int QuestionCount)
		{

            ProfileQuizQuestionRequest request = new ProfileQuizQuestionRequest();

            try
            {
                AgileMindEntities agileDB = new AgileMindEntities();

                t_LoginSession session = (from loginSession in agileDB.t_LoginSession where loginSession.LoginSessionId == SessionId && loginSession.ValidTill > DateTime.Now select loginSession).First();
                if (session != null)
                {
                    List<vwQuestionAnswer> questionList = agileDB.FetchQuestionAnswer_ByLoginId(session.LoginId).ToList();

                    //Make sure we only get active and ones with an actual answer.
                    questionList = questionList.FindAll(delegate(vwQuestionAnswer findQA) 
                    {
                        return findQA.Active && !string.IsNullOrEmpty(findQA.Answer);
                    });

                    Random rand = new Random();
                    for (int count = 0; count < QuestionCount; count++)
                    {
                        ProfileQuizQuestion newQA = new ProfileQuizQuestion();

                        int index = rand.Next(questionList.Count);
                        vwQuestionAnswer foundA = questionList[index];
                        newQA.Answer = foundA.Answer;
                        newQA.Question = foundA.Question;

                        request.QuestionList.Add(newQA);
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
