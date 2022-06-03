using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace Core
{
    public class WaveSpawner : MonoBehaviour
    {
        #region Editor
        [Header("Spawn Containers")]
        [SerializeField] private Transform groundContainer;
        [SerializeField] private Transform airContainer;
        [Header("Enemy Setup")]
        [SerializeField] private AirEnemy airEnemy;
        [SerializeField] private int airEnemySpawnSequence = 3;
        [SerializeField] private GroundEnemy groundEnemy;
        [SerializeField] private int groundEnemySpawnSequence = 8;

        #endregion

        #region Fields

        private List<BaseEnemy> _enemyPool = new List<BaseEnemy>();
        private int _airEnemiesInWave;
        private int _groundEnemiesInWave;
        private float _spawnDelay;
        private int _totalEnemiesInWave;
        private bool _checkProgression;

        #endregion

        #region Methods

        private void Start()
        {
            CreatePool();
        }

        private void Update()
        {
            if (!_checkProgression) return;
            
            if (_totalEnemiesInWave == 0 && GameManager.Instance.IsInGame)
            {
                _checkProgression = false;
                Invoke(nameof(CheckEnemiesLeft), 1f);
            }
        }

        private void CreatePool()
        {
            for (int i = 0; i < 10; i++)
            {
                if (i % 3 == 0)
                {
                    var airInstance = Instantiate(airEnemy, airContainer);
                    airInstance.gameObject.SetActive(false);
                    _enemyPool.Add(airInstance);
                }

                var groundInstance = Instantiate(groundEnemy, groundContainer);
                groundInstance.gameObject.SetActive(false);
                _enemyPool.Add(groundInstance);
            }
        }

        [ContextMenu("Start Spawning")]
        public void StartSpawning(float spawnDelay = 2f)
        {
            var waveNumber = GameManager.Instance.CurrentWave;
            _checkProgression = true;
            _spawnDelay = spawnDelay;
            var groundEnemiesToSpawn = groundEnemySpawnSequence * waveNumber;
            var airEnemiesToSpawn = waveNumber >= 3 ? airEnemySpawnSequence * waveNumber : 0;
            _totalEnemiesInWave = airEnemiesToSpawn + groundEnemiesToSpawn;
            StartCoroutine(SpawnAirEnemies(airEnemiesToSpawn));
            StartCoroutine(SpawnGroundEnemy(groundEnemiesToSpawn));
        }

        private IEnumerator SpawnAirEnemies(int amount)
        {
            if (!GameManager.Instance.IsInGame)
                yield break;
            if (amount == 0) yield break;
            for (int i = 0; i < amount; i++)
            {
                GetEnemyFromPool(airEnemy);
                _totalEnemiesInWave--;
                yield return new WaitForSecondsRealtime(_spawnDelay);
            }
        }

        private IEnumerator SpawnGroundEnemy(int amount)
        {
            if (!GameManager.Instance.IsInGame)
                yield break;
            for (int i = 0; i < amount; i++)
            {
                _totalEnemiesInWave--;
                GetEnemyFromPool(groundEnemy);
                yield return new WaitForSecondsRealtime(_spawnDelay);
            }
        }

        private void GetEnemyFromPool<T>(T type) where T : Component
        {
            foreach (var enemy in _enemyPool)
            {
                if (!enemy.gameObject.activeInHierarchy && enemy.TryGetComponent(out T foundEnemy))
                {
                    foundEnemy.gameObject.SetActive(true);
                    return;
                }
            }

            BaseEnemy newInstance;
            if (type is AirEnemy)
            {
                newInstance = Instantiate(airEnemy, airContainer);
            }
            newInstance = Instantiate(groundEnemy, groundContainer);
            _enemyPool.Add(newInstance);
        }

        private void CheckEnemiesLeft()
        {
            var passedCheck = true;
            foreach (var enemy in _enemyPool)
            {
                if (enemy.gameObject.activeInHierarchy)
                {
                    passedCheck = false;
                    _checkProgression = true;
                    break;
                }
            }

            if (passedCheck && GameManager.Instance.IsInGame)
            {
                GameManager.Instance.AdvanceWave();
            }
        }

        #endregion
    }
}