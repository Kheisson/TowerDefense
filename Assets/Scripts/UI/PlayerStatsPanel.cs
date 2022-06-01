using System;
using System.Runtime.InteropServices;
using Core;
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
            _gameManager = GameManager.Instance;
            _playerCore = FindObjectOfType<PlayerCore>();
        }

        private void Start()
        {
            _gameManager.PlayerStateUpdated += OnPlayerStateUpdated;
        }

        private void OnPlayerStateUpdated()
        {
            _scoreText.text = _gameManager.PlayerState.Score.ToString();
            _fundsText.text = _gameManager.PlayerState.Funds.ToString();
            _coreHealthText.text = _playerCore.Hp.ToString();
        }

        private void OnDestroy()
        {
            _gameManager.PlayerStateUpdated -= OnPlayerStateUpdated;
        }
    }
}
