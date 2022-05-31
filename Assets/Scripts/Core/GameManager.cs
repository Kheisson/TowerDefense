using System;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        #region Fields

        #endregion

        #region Properties

        public bool IsInGame { get; private set; }
        public PlayerState PlayerState { get; private set; }

        #endregion

        #region Methods

        private void Awake()
        {
            PlayerState = new PlayerState();
        }

        #endregion
    }
}