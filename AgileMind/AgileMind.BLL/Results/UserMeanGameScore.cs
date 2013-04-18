#region -- using declaration --

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace AgileMind.BLL.Results
{
    public class UserMeanGameScore
    {

        private decimal _userMean;
        private int _gameId;
        private String _game;
        private decimal _gameMean;
        private decimal _gameDeviation;
        private decimal _meanDiff;
        private decimal _userDeflection;

        /*-- Constructors --*/

        #region -- Constructor() --
        public UserMeanGameScore()
        {

        }
        #endregion

        /*-- Events --*/

        /*-- Properties --*/

        #region -- UserMean Property --
        public decimal UserMean
        {
            get { return _userMean; }
            set { _userMean = value; }
        }
        #endregion

        #region -- GameId Property --
        public int GameId
        {
            get { return _gameId; }
            set { _gameId = value; }
        }
        #endregion

        #region -- Game Property --
        public String Game
        {
            get { return _game; }
            set { _game = value; }
        }
        #endregion

        #region -- GameMean Property --
        public decimal GameMean
        {
            get { return _gameMean; }
            set { _gameMean = value; }
        }
        #endregion

        #region -- GameDeviation Property --
        public decimal GameDeviation
        {
            get { return _gameDeviation; }
            set { _gameDeviation = value; }
        }
        #endregion

        #region -- MeanDiff Property --
        public decimal MeanDiff
        {
            get { return _meanDiff; }
            set { _meanDiff = value; }
        }
        #endregion

        #region -- UserDeflection Property --
        public decimal UserDeflection
        {
            get { return _userDeflection; }
            set { _userDeflection = value; }
        }
        #endregion

        /*-- Methods --*/

        /*-- Event Handlers --*/
	
    }
}
