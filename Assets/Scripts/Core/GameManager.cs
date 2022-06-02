using System;
using System.Collections;
using BoardSetup;
using Player;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        #region Editor
        [SerializeField] private PlayerSettings playerSettings;
        #endregion
        
        #region Fields
        private static GameManager _instance;
        private WaveController _waveController;
        #endregion

        #region Properties
        public bool IsInGame { get; private set; }
        public Board GameBoard { get; private set; }
        public int CurrentWave { get; private set; }
        public PlayerState PlayerState { get; private set; }
        public static GameManager Instance => _instance;
        #endregion

        #region Events

        public event Action PlayerStateUpdated;

        #endregion

        #region Methods

        private void Awake()
        {
            CreateSingletonInstance();
            PlayerState = new PlayerState();
            CurrentWave = 1;
            GameBoard = FindObjectOfType<Board>();
            _waveController = GetComponent<WaveController>();
        }

        private void CreateSingletonInstance()
        {
            if (_instance == null)
                _instance = this;
            else
            {
                Destroy(this);
            }
        }

        private void Start()
        {
            StartNewGame();
        }

        private void StartNewGame()
        {
            CurrentWave = 1;
            PlayerState.AddFunds(playerSettings.PlayerStartingFundsValue);
            IsInGame = true;
            StartCoroutine(InGameCoroutine());
            _waveController.InitializeWave();
        }

        private IEnumerator InGameCoroutine()
        {
            while (IsInGame)
            {
                PlayerState.AddFunds(playerSettings.ProgressFundsIncrementValue);
                PlayerState.AddScore(playerSettings.ProgressScoreIncrementValue);
                PlayerStateUpdated?.Invoke();
                yield return new WaitForSecondsRealtime(playerSettings.ProgressTimer);
            }
        }
        #endregion
    }
}