using Enemy;
using UnityEngine;

namespace Turrets
{
    public class TargetLocator : MonoBehaviour
    {
        #region Editor

        [SerializeField] private ParticleSystem projectile;

        #endregion
        
        #region Fields

        private BaseEnemy _currentTarget = null;
        private int _damage;

        private enum TargetType
        {
            AirEnemy,
            GroundEnemy
        };

        private TargetType _targetType;

        #endregion

        #region Properties

        public int Damage => _damage;

        #endregion
        
        #region Methods

        private void Update()
        {
            RotateToTarget();
            Shoot();
        }

        private void FindTarget(Collider enemy)
        {
            _currentTarget = enemy.GetComponent<BaseEnemy>();
        }

        private void RotateToTarget()
        {
            if (_currentTarget != null)
            {
                transform.LookAt(_currentTarget.transform);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.name.Contains(_targetType.ToString()))
            {
                return;
            }
            FindTarget(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == _currentTarget.gameObject)
            {
                _currentTarget = null;
            }
        }

        private void Shoot()
        {
            if (_currentTarget != null && _currentTarget.gameObject.activeInHierarchy)
            {
                if (projectile.isPlaying)
                    return;
                projectile.Play();
            }
            else
            {
                projectile.Stop();
            }
        }

        public void Init(string targetType)
        {
            switch (targetType)
            {
                case nameof(AirEnemy):
                    _targetType = TargetType.AirEnemy;
                    _damage = 20;
                    break;
                case nameof(GroundEnemy):
                    _targetType = TargetType.GroundEnemy;
                    _damage = 10;
                    break;
            }
        }

        #endregion
    }
}