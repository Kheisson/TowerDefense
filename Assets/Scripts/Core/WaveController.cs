using UI;
using UnityEngine;

namespace Core
{
    public class WaveController : MonoBehaviour
    {
        #region Editor

        [SerializeField] private float waveStartDelay = 10f;
        [SerializeField] private float spawnRate = 2f;
        
        #endregion

        #region Fields

        private Announcement _announcementUI;
        private WaveSpawner _waveSpawner;

        #endregion

        #region Methods

        private void Start()
        {
            _announcementUI = FindObjectOfType<Announcement>();
            _waveSpawner = GetComponent<WaveSpawner>();
            _announcementUI.FinishedCountdown += StartWave;
        }

        public void InitializeWave()
        {
            _announcementUI.Show("10", 1f);
        }
        private void StartWave()
        {
            _waveSpawner.StartSpawning();
        }

        #endregion
    }
}