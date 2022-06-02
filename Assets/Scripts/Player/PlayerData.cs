using UnityEngine;

namespace Player
{
    public class PlayerData
    {
        #region Consts

        private const string HIGHSCORE_KEY = "highscore";

        #endregion
        #region Properties
        public int HighScore { get; private set; }

        #endregion
        
        #region Constructor
        public PlayerData()
        {
            HighScore = PlayerPrefs.GetInt(HIGHSCORE_KEY, 0);
        }
        #endregion

        #region Methods

        public void SetHighScore(int score)
        {
            if (score > HighScore)
            {
                HighScore = score;
                PlayerPrefs.SetInt(HIGHSCORE_KEY, HighScore);
            }
        }

        #endregion
    }
}