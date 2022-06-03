using System;
using UnityEngine;

namespace SpecialEffects
{
    public class Explode : MonoBehaviour
    {
        #region Fields

        [SerializeField] private float magnitude = 2f;
        [SerializeField] private float duration = 5f;

        #endregion

        #region Methods

        private void OnEnable()
        {
            MakeExplosion();
        }

        private void MakeExplosion()
        {
            while (duration > Mathf.Epsilon)
            {
                duration -= Time.deltaTime;
                foreach (Transform child in transform)
                {
                    child.Translate(Vector3.forward * magnitude * Time.deltaTime);
                }
            }
        }

        #endregion
    }
}