using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Turrets;
using UnityEngine;

namespace UI
{
    public class PurchasePopup : MonoBehaviour
    {
        #region Editor

        [SerializeField] private GameObject purchasingPopupPrefab;

        #endregion
        #region Fields

        private List<TurretButton> _turretButtons = new List<TurretButton>();
        private SellTurretButton _sellTurretButton;
        private BaseBuildingPivot _currentlySelectedPivot;
        private Camera _camera;
        
        #endregion

        #region Events

        public event Action PurchaseComplete;

        #endregion
        
        #region Methods

        private void Start()
        {
            _camera = Camera.main;
        }

        /// <summary>
        /// Displays the popup with buy buttons if there is no building on the mat
        /// Otherwise, displays the sell button with 50% of the cost of the currently placed building
        /// </summary>
        /// <param name="baseBuildingPivot">
        /// click on pivot which contains data about the placement map
        /// </param>
        public bool DisplayPopup(BaseBuildingPivot baseBuildingPivot)
        {
            if (baseBuildingPivot == null)
            {
                Debug.LogError("Cannot open purchase popup, invalid building pivot", gameObject);
                return false;
            }
            
            _currentlySelectedPivot = baseBuildingPivot;
            transform.position = AdjustPositionOfObjectToOverlaySpace(_currentlySelectedPivot.WorldPosition);
            purchasingPopupPrefab.SetActive(true);
            
            if (_currentlySelectedPivot.CurrentBuilding == null)
            {
                DisplayPurchaseButtons();
            }
            else
            {
                var sellAmount = Mathf.RoundToInt(_currentlySelectedPivot.CurrentBuilding.BuildCost * 0.5f).ToString();
                DisplaySellButton(sellAmount);
            }

            return true;
        }

        public void Purchase(BaseBuilding building)
        {
            var buildingCost = building.BuildCost;
            if (GameManager.Instance.PlayerState.TryTakeFunds(buildingCost))
            {
                var instance = Instantiate(building.gameObject).GetComponent<BaseBuilding>();
                _currentlySelectedPivot.SetBuilding(instance);
            }
            HidePopup();
            PurchaseComplete?.Invoke();
        }

        public void Sell()
        {
            var funds = Mathf.RoundToInt(_currentlySelectedPivot.CurrentBuilding.BuildCost * 0.5f);
            GameManager.Instance.PlayerState.AddFunds(funds);
            _currentlySelectedPivot.RemoveBuilding();
            HidePopup();
            PurchaseComplete?.Invoke();
        }

        public void HidePopup()
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        
        private void DisplayPurchaseButtons()
        {
            if (_turretButtons.Count == 0)
            {
                var buttons = GetComponentsInChildren<TurretButton>(includeInactive: true);
                if (buttons.Length == 0)
                {
                    Debug.LogError("No Purchase buttons were found in the popup", gameObject);
                    return;
                }
                _turretButtons = buttons.ToList();
            }
            
            foreach (var button in _turretButtons)
            {
                button.gameObject.SetActive(true);
            }

            if (_sellTurretButton != null)
            {
                _sellTurretButton.gameObject.SetActive(false);
            }
        }

        private void DisplaySellButton(string sellAmount)
        {
            if (_sellTurretButton == null)
            {
                var button = GetComponentInChildren<SellTurretButton>(includeInactive: true);
                if (button == null)
                {
                    Debug.LogError("No Sell buttons were found in the popup", gameObject);
                    return;
                }

                _sellTurretButton = button;
            }
            
            foreach (var turretButton in _turretButtons)
            {
                turretButton.gameObject.SetActive(false);
            }
            
            _sellTurretButton.gameObject.SetActive(true);
            _sellTurretButton.SetText(sellAmount);
        }

        private Vector3 AdjustPositionOfObjectToOverlaySpace(Vector3 worldSpace)
        {
            return _camera.WorldToScreenPoint(worldSpace);
        }

        #endregion
    }
}