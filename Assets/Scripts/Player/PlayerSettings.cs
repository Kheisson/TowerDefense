using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "New Player Setting", menuName = "Settings/Player Setting")]
    public class PlayerSettings : ScriptableObject
    {
        #region Editor

        [SerializeField, Tooltip("Interval for rewarding player in game while core is alive")]
        private float progressTimer;
        [SerializeField, Tooltip("Constant value added to the score while game is on going")]
        private int progressScoreIncrementValue;
        [SerializeField, Tooltip("Constant value added to the funds while game is on going")] 
        private int progressFundsIncrementValue;
        [SerializeField, Tooltip("Amount of funds the player starts with")] 
        private int playerStartingFundsValue;

        #endregion

        #region Properties

        public float ProgressTimer => progressTimer;
        public int ProgressScoreIncrementValue => progressScoreIncrementValue;
        public int ProgressFundsIncrementValue => progressFundsIncrementValue;
        public int PlayerStartingFundsValue => playerStartingFundsValue;

        #endregion
    }
}