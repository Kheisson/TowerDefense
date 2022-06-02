using Core;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerStatsPanel : MonoBehaviour
    {
        [SerializeField]
        private Text _scoreText = null;

        [SerializeField]
        private Text _fundsText = null;

        [SerializeField]
        private Text _coreHealthText = null;

        private GameManager _gameManager;

        private PlayerCore _playerCore;

        private void Awake()
        {
            _playerCore = FindObjectOfType<PlayerCore>();
        }

        private void Start()
        {
            GameManager.Instance.PlayerStateUpdated += OnPlayerStateUpdated;
        }

        private void OnPlayerStateUpdated()
        {
            _scoreText.text = GameManager.Instance.PlayerState.Score.ToString();
            _fundsText.text = GameManager.Instance.PlayerState.Funds.ToString();
            _coreHealthText.text = _playerCore.Hp.ToString();
        }

        private void OnDestroy()
        {
            GameManager.Instance.PlayerStateUpdated -= OnPlayerStateUpdated;
        }
    }
}
