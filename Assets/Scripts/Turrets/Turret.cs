using UnityEngine;

namespace Turrets
{
    public class Turret : BaseBuilding
    {
        #region Editor

        [SerializeField, Tooltip("Name of the class of enemy you target")]
        private string targetType;

        #endregion
        
        #region Fields

        private TargetLocator _targetLocator;

        #endregion
        
        #region Methods

        private void Awake()
        {
            _targetLocator = GetComponentInChildren<TargetLocator>();
            _targetLocator.Init(targetType);
        }

        public override void OnRemoval()
        {
            IsPlaced = false;
            base.OnRemoval();
        }

        #endregion
    }
}