using System;
using SpecialEffects;
using UnityEngine;

namespace Player
{
    public class PlayerCore : MonoBehaviour
    {
        [SerializeField]
        private int _hp = 200;

        public bool IsAlive => _hp > 0;

        public int Hp => _hp;

        public event Action PlayerDeath;

        public void TakeDamage(int amount)
        {
            if (!IsAlive)
            {
                return;
            }
            _hp = Mathf.Max(0, _hp - amount);
            if (!IsAlive)
            {
                OnDeath();
            }
        }

        [ContextMenu("Player Core Death")]
        private void OnDeath()
        {
            gameObject.AddComponent<Explode>();
            PlayerDeath?.Invoke();
        }
    }
}
