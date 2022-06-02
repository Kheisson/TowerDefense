using System;
using System.Collections;
using Player;
using Turrets;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Animator), typeof(Collider))]
    public class BaseEnemy : MonoBehaviour
    {
        #region Editor

        [SerializeField] protected EnemySettings enemySettings;

        #endregion
        
        #region Fields

        private EnemyMovement _enemyMovementScript;
        private int _runAnimationId;
        private int _attackAnimationId;
        private int _enemySpeed;
        private int _enemyHealth;

        private ParticleSystem _deathParticles;
        private ParticleSystem _despawnParticles;
        private ParticleSystem _coreHitParticles;

        protected Animator _animator;
        protected Vector3[] followPath;

        #endregion

        #region Events

        public event Action<int, int> EnemyDestroyed;

        #endregion
        
        #region Consts

        protected const string RUN_ANIMATION_NAME = "Run";
        protected const string ATTACK_ANIMATION_NAME = "Attack";

        #endregion

        #region Methods

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _runAnimationId = Animator.StringToHash(RUN_ANIMATION_NAME);
            _attackAnimationId = Animator.StringToHash(ATTACK_ANIMATION_NAME);
            _enemyMovementScript = gameObject.AddComponent<EnemyMovement>();
            InitiateParticles();
        }

        //Populates enemy data based on the current wave we are in
        private void OnEnable()
        {
            _enemyHealth = enemySettings.EnemyHealth;
            _enemySpeed = enemySettings.EnemySpeed;
            ReSpawn();
        }

        //Notifies enemy has been killed, stops running and plays the death particles
        protected virtual void OnDeath()
        {
            EnemyDestroyed?.Invoke(enemySettings.EnemyScoreGrant, enemySettings.EnemyFundsGrant);
            StopRunningAnimation(); 
            _deathParticles.Play();
            DisableWithDelay();
        }

        //Stops running, triggers the attack animation and plays the core hit particles
        protected virtual void OnSuccessfulHit()
        {
            StopRunningAnimation(); 
            _animator.SetTrigger(_attackAnimationId);
            _coreHitParticles.Play();
            DisableWithDelay();
        }

        protected virtual void ReSpawn()
        {
            _deathParticles.Stop();
            _despawnParticles.Stop();
            _coreHitParticles.Stop();
            _animator.SetBool(_runAnimationId, true);
            _enemyMovementScript.StartFollowingPath(followPath, _enemySpeed, OnSuccessfulHit);
        }

        private void StopRunningAnimation() => _animator.SetBool(_runAnimationId, false);

        private void DisableWithDelay() => StartCoroutine(Disable());

        //When the enemy reaches the core, play despawn particles and disable the gameObject
        private IEnumerator Disable()
        {
            yield return new WaitForSeconds(0.5f);
            _despawnParticles.Play();
            yield return new WaitForSeconds(0.5f);
            gameObject.SetActive(false);
        }

        private void InitiateParticles()
        {
            _deathParticles = Instantiate(enemySettings.EnemyDeathParticles, transform);
            _deathParticles.Stop();
            _despawnParticles = Instantiate(enemySettings.DespawnParticles, transform);
            _despawnParticles.Stop();
            _coreHitParticles = Instantiate(enemySettings.SuccessfulHitParticles, transform);
            _coreHitParticles.Stop();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerCore core))
            {
                core.TakeDamage(enemySettings.EnemyDamage);
            }
        }

        private void OnParticleCollision(GameObject other)
        {
            var turret = other.GetComponentInParent<TargetLocator>();
            Damage(turret.Damage);
        }

        private void Damage(int amount)
        {
            _enemyHealth -= amount;
            if (_enemyHealth < 0)
            {
                OnDeath();
            }
        }

        #endregion
    }
}