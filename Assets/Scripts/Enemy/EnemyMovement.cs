using System;
using System.Collections;
using Core;
using UnityEngine;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {

        #region Methods

        private IEnumerator FollowPath(Vector3[] path, int speed, Action onCompletePath)
        {
            if (!GameManager.Instance.IsInGame)
            {
                StopMovement();
                yield break;
            }
                
            for (int i = 1; i < path.Length; i++)
            {
                var startingPosition = transform.position;
                var endPosition = path[i];
                var reach = 0f;
                
                transform.LookAt(endPosition);

                while (reach < 1f)
                {
                    reach += Time.deltaTime * speed;
                    transform.position = Vector3.Lerp(startingPosition, endPosition, reach);
                    yield return new WaitForEndOfFrame();
                }
            }
            onCompletePath?.Invoke();
        }

        public void StartFollowingPath(Vector3[] path, int speed, Action onCompletePath)
        {
            StartCoroutine(FollowPath(path, speed, onCompletePath));
        }

        private void StopMovement()
        {
            GetComponent<BaseEnemy>().StopRunningAnimation();
        }

        #endregion
    }
}