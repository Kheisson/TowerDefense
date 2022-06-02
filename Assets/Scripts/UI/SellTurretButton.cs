using Core;
using TMPro;
using UnityEngine;

namespace UI
{
    public class SellTurretButton : MonoBehaviour
    {
        #region Fields

        private const string SELLING_TEXT = "Sell Turret? - $";
        private TextMeshProUGUI _priceLabel;
        #endregion
        
        #region Methods

        private void Awake()
        {
            _priceLabel = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void SetText(string sellAmount)
        {
            _priceLabel.text = SELLING_TEXT + sellAmount;
        }

        #endregion
    }
}