using Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameOverMenu : MonoBehaviour
    {
        #region Editor

        [SerializeField] private Text bestScoreText;

        #endregion

        #region Methods

        private void OnEnable()
        {
            bestScoreText.text = GameManager.Instance.PlayerData.HighScore.ToString();
        }

        #endregion
    }
}