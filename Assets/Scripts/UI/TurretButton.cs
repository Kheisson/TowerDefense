using System;
using TMPro;
using Turrets;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TurretButton : MonoBehaviour
    {
        #region Editor

        [SerializeField] private BaseBuilding turretPrefab;
        [SerializeField] private TextMeshProUGUI unitNamePlacement;
        [SerializeField] private TextMeshProUGUI unitPricePlacement;
        [SerializeField] private Image unitImagePlacement;

        #endregion

        #region Methods

        private void OnEnable()
        {
            if (turretPrefab == null)
            {
                Debug.LogError("Turret prefab is missing from purchase button", gameObject);
                return;
            }
            unitNamePlacement.text = turretPrefab.Name;
            unitPricePlacement.text = $"${turretPrefab.BuildCost}";
            unitImagePlacement.sprite = turretPrefab.UIIcon;
        }

        #endregion
    }
}