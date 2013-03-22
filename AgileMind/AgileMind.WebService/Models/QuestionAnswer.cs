#region -- using declarations --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

#endregion

namespace AgileMind.WebService.Models
{
    public class QuestionAnswer
    {

        private String _question;
        private int _userProfileQuestionId;
        private int _userProfileAnswerId;

        #region -- Answer Property --
        public String Answer
        {
            get { return _question; }
            set { _question = value; }
        }
        #endregion

        #region -- UserProfileQuestionId Property --
        public int UserProfileQuestionId
        {
            get { return _userProfileQuestionId; }
            set { _userProfileQuestionId = value; }
        }
        #endregion

        #region -- UserProfileAnswerId Property --
        public int UserProfileAnswerId
        {
            get { return _userProfileAnswerId; }
            set { _userProfileAnswerId = value; }
        }
        #endregion


    }
}