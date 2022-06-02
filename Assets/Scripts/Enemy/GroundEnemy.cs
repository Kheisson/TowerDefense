using Core;

namespace Enemy
{
    public class GroundEnemy : BaseEnemy
    {
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
        #endregion
    }
}