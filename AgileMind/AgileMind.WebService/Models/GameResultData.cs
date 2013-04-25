#region -- using declarations --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

#endregion

namespace AgileMind.WebService.Models
{
    public class GameResultData
    {

        private int _gameScoreId;
        private int _gameId;
        private int _loginId;
        private DateTime _created;
        private int _score;
        private int _total;
        private decimal _testDuration;
        private String _createdString;

        /*-- Constructors --*/

        #region -- Constructor() --
        public GameResultData()
        {

        }
        #endregion

        /*-- Events --*/

        /*-- Properties --*/

        #region -- GameScoreId Property --
        public int GameScoreId
        {
            get { return _gameScoreId; }
            set { _gameScoreId = value; }
        }
        #endregion

        #region -- GameId Property --
        public int GameId
        {
            get { return _gameId; }
            set { _gameId = value; }
        }
        #endregion

        #region -- LoginId Property --
        public int LoginId
        {
            get { return _loginId; }
            set { _loginId = value; }
        }
        #endregion

        #region -- Created Property --
        public DateTime Created
        {
            get { return _created; }
            set { _created = value; }
        }
        #endregion

        #region -- Score Property --
        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }
        #endregion

        #region -- Total Property --
        public int Total
        {
            get { return _total; }
            set { _total = value; }
        }
        #endregion

        #region -- TestDuration Property --
        public decimal TestDuration
        {
            get { return _testDuration; }
            set { _testDuration = value; }
        }
        #endregion

        #region -- CreatedString Property --
        public String CreatedString
        {
            get { return _createdString; }
            set { _createdString = value; }
        }
        #endregion

        /*-- Methods --*/

        /*-- Event Handlers --*/
	
    }
}