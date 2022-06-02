using Core;
using UnityEngine;

namespace Enemy
{
    public class GroundEnemy : BaseEnemy
    {
        #region Consts

        private const string DIE_ANIMATION_NAME = "Die";

        #endregion
        
        #region Fields

        private int _deathId = Animator.StringToHash(DIE_ANIMATION_NAME);

        #endregion
        
        #region Methods

        protected override void ReSpawn()
        {
            if (followPath == null)
            {
                followPath = GameManager.Instance.GameBoard.GetGroundPath();
            }
            transform.position = followPath[0];
            base.ReSpawn();
        }

        protected override void OnDeath()
        {
            _animator.SetTrigger(_deathId);
            base.OnDeath();
        }

        #endregion
    }
}