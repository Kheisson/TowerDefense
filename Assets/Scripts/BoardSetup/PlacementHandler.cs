using Turrets;
using UI;
using UnityEngine;

namespace BoardSetup
{
    [RequireComponent(typeof(BaseBuildingPivot))]
    public class PlacementHandler : MonoBehaviour
    {
        #region Fields

        private BaseBuildingPivot _baseBuildingPivot;
        private static UIManager _uiManager;

        #endregion
        
        #region Methods

        private void Start()
        {
            _baseBuildingPivot = GetComponent<BaseBuildingPivot>();
            if (_uiManager == null)
            {
                _uiManager = GameObject.FindWithTag(nameof(UIManager)).GetComponent<UIManager>();
            }
        }

        private void OnMouseDown()
        {
            _uiManager.OpenStorePopup(_baseBuildingPivot);
        }

        #endregion
    }
}