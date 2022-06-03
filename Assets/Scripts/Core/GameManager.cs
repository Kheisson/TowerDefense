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
        private PlayerCore _playerCore;
        #endregion

        #region Properties
        public bool IsInGame { get; private set; }
        public Board GameBoard { get; private set; }
        public int CurrentWave { get; private set; }
        public PlayerState PlayerState { get; private set; }
        public PlayerData PlayerData { get; private set; }
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
            PlayerData = new PlayerData();
            InitiateFirstRound();
        }

        private void InitiateFirstRound()
        {
            CurrentWave = 1;
            GameBoard = FindObjectOfType<Board>();
            _waveController = GetComponent<WaveController>();
            _playerCore = FindObjectOfType<PlayerCore>();

            _playerCore.PlayerDeath += GameOver;
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
            Time.timeScale = 1f;
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

        private IEnumerator GameOverSequence()
        {
            yield return new WaitForSecondsRealtime(1f);
            var sceneLoader = Instantiate(new GameObject("SceneLoader"),transform);
            sceneLoader.AddComponent<SceneController>().LoadGameOverScene();
            Time.timeScale = 0f;
        }

        public void GameOver()
        {
            PlayerData.SetHighScore(PlayerState.Score);
            IsInGame = false;
            StartCoroutine(GameOverSequence());
        }

        public void UpdateScore(int score, int funds)
        {
            PlayerState.AddScore(score);
            PlayerState.AddFunds(funds);
        }

        public void AdvanceWave()
        {
            IsInGame = false;
            CurrentWave++;
            _waveController.InitializeWave();
        }

        public void LevelStarted()
        {
            IsInGame = true;
            StartCoroutine(InGameCoroutine());
        }

        #endregion
    }
}