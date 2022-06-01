using Turrets;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class UIManager : MonoBehaviour, IPointerDownHandler
    {
        #region Editor

        [SerializeField] private GameObject darkBackgroundOverlay;

        #endregion

        #region Fields

        private PurchasePopup _purchasePopup;

        #endregion

        #region Methods

        private void Awake()
        {
            gameObject.tag = nameof(UIManager);
            _purchasePopup = GetComponentInChildren<PurchasePopup>(includeInactive:true);
        }

        public void OpenStorePopup(BaseBuildingPivot baseBuildingPivot)
        {
            var popup = _purchasePopup.DisplayPopup(baseBuildingPivot);
            _purchasePopup.PurchaseComplete += OnPurchaseComplete;
            darkBackgroundOverlay.SetActive(popup);
        }

        public void CloseStorePopup()
        {
            darkBackgroundOverlay.SetActive(false);
            _purchasePopup.HidePopup();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!darkBackgroundOverlay.activeInHierarchy) return;
            CloseStorePopup();
        }
        
        private void OnPurchaseComplete()
        {
            darkBackgroundOverlay.SetActive(false);
        }
        
        
        #endregion

        
    }
}