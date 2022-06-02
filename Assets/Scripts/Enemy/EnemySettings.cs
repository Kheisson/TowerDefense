using Core;
using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "New Enemy Setting", menuName = "Settings/Enemy Setting")]
    public class EnemySettings : ScriptableObject
    {
        #region Editor
        [SerializeField, Tooltip("Score added when enemy destroyed")]
        private int enemyScoreGrant;
        [SerializeField, Tooltip("Funds added when enemy destroyed")]
        private int enemyFundsGrant;
        [SerializeField, Tooltip("Enemy Starting Health")]
        private int startingHealth;
        [SerializeField, Tooltip("Health added to Enemy per round")] 
        private int incrementHealthValue;
        [SerializeField, Tooltip("Enemy Starting Speed")] 
        private int startingSpeed;
        [SerializeField, Tooltip("Speed added to Enemy per round")]
        private int incrementSpeedValue;
        [SerializeField, Tooltip("Enemy Damage to player core")] 
        private int damage;
        [SerializeField, Tooltip("Particles activated when enemy dies")]
        private ParticleSystem deathParticles;
        [SerializeField, Tooltip("Particles activated when enemy hit the player core")]
        private ParticleSystem successfulHitParticles;
        [SerializeField, Tooltip("Particles activated when enemy despawns")]
        private ParticleSystem despawnParticles;

        #endregion

        #region Properties
        public int EnemyScoreGrant => enemyScoreGrant;
        public int EnemyFundsGrant => enemyFundsGrant;
        public int EnemyHealth => GetCurrentValueBasedOnWave(startingHealth,incrementHealthValue);
        public int EnemySpeed => GetCurrentValueBasedOnWave(startingSpeed, incrementSpeedValue);
        public int EnemyDamage => damage;
        public ParticleSystem EnemyDeathParticles => deathParticles;
        public ParticleSystem SuccessfulHitParticles => successfulHitParticles;
        public ParticleSystem DespawnParticles => despawnParticles;
        #endregion

        #region Method

        public int GetCurrentValueBasedOnWave(int startingValue, int incrementalValue)
        {
            return startingValue + (incrementalValue * (GameManager.Instance.CurrentWave - 1));
        }

        #endregion
    }
}