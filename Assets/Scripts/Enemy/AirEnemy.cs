using Core;
using UnityEngine;

namespace Enemy
{
    public class AirEnemy : BaseEnemy
    {
        #region Methods

        protected override void ReSpawn()
        {
            if (followPath == null)
            {
                var originalPath = GameManager.Instance.GameBoard.GetAirPath();
                var path = new Vector3[originalPath.Length + 1];
                for (var i = 0; i < originalPath.Length; i++)
                {
                    path[i] = originalPath[i];
                }
                path[originalPath.Length] = GameManager.Instance.GameBoard.GetPlayerCorePosition();
                followPath = path;
            }
            transform.position = followPath[0];
            base.ReSpawn();
        }
        #endregion
    }
}