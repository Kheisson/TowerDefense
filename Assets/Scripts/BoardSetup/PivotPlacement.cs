using Turrets;
using UnityEngine;

namespace BoardSetup
{
    public class PivotPlacement : MonoBehaviour
    {
        #region Editor
        [SerializeField] private GameObject placementPrefab;
        #endregion
        
        #region Methods

        private void Awake()
        {
            ResetPlacements();
        }

        [ContextMenu("Reset Placements")]
        //Auto-stacks the placement mats of the turret pivot container with the initial arrow placement prefab
        private void ResetPlacements()
        {
            foreach (Transform child in transform)
            {
                if (child.TryGetComponent(out BaseBuildingPivot baseBuilding))
                {
                    baseBuilding.RemoveBuilding();
                }
                else
                {
                    Instantiate(placementPrefab, child);
                }
            }
        }

        #endregion
    }
}