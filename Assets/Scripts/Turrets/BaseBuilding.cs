﻿using UnityEngine;

namespace Turrets
{
    public class BaseBuilding : MonoBehaviour
    {
        [SerializeField]
        private int _buildCost = 40;

        [SerializeField]
        private int _sellPrice = 20;

        [SerializeField]
        private string _name = null;

        [SerializeField]
        private Sprite _uiIcon = null;

        public string Name => _name;

        public Sprite UIIcon => _uiIcon;

        public int BuildCost => _buildCost;

        public int SellPrice => _sellPrice;

        public bool IsPlaced { get; protected set; }

        public void SetParentPivot(Transform pivotTransform)
        {
            transform.SetParent(pivotTransform);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }

        public virtual void OnPlacement() { IsPlaced = true; }

        public virtual void OnRemoval() { Destroy(gameObject); }
    }
}