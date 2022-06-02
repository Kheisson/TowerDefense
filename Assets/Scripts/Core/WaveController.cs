using UI;
using UnityEngine;

namespace Core
{
    public class WaveController : MonoBehaviour
    {
        #region Editor

        [SerializeField] private string waveCount = "10";
        [SerializeField] private float spawnRate = 2f;
        [SerializeField] private Announcement announcmentUI;
        
        #endregion

        #region Fields
        private WaveSpawner _waveSpawner;
        private Announcement _currentAnnouncmentUI;
        #endregion

        #region Methods

        private void Start()
        {
            _waveSpawner = GetComponent<WaveSpawner>();
        }

        public void InitializeWave()
        {
            var canvasPos = FindObjectOfType<Canvas>().gameObject.transform;
            _currentAnnouncmentUI = Instantiate(announcmentUI, canvasPos);
            _currentAnnouncmentUI.Show(waveCount, 1f);
            _currentAnnouncmentUI.FinishedCountdown += StartWave;
        }
        private void StartWave()
        {
            announcmentUI.FinishedCountdown -= StartWave;
            _waveSpawner.StartSpawning(spawnRate);
        }

        #endregion
    }
}