using System;
using System.Collections;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        #region Fields
        
        private static GameManager _instance;

        #endregion

        #region Properties

        public bool IsInGame { get; private set; }
        public PlayerState PlayerState { get; private set; }

        public static GameManager Instance => _instance;
        #endregion

        #region Events

        public event Action PlayerStateUpdated;

        #endregion

        #region Methods

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
            else
            {
                Destroy(this);
            }
            
            PlayerState = new PlayerState();
            
        }

        private void Start()
        {
            StartNewGame();
        }

        private void StartNewGame()
        {
            PlayerState.AddFunds(100);
            IsInGame = true;
            StartCoroutine(InGameCoroutine());
        }

        private IEnumerator InGameCoroutine()
        {
            while (IsInGame)
            {
                PlayerState.AddFunds(10);
                PlayerState.AddScore(10);
                PlayerStateUpdated?.Invoke();
                yield return new WaitForSecondsRealtime(1f);
            }
        }
        #endregion
    }
}