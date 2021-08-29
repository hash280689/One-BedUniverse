using HPP.Grid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HPP.GlobalEnums;

namespace HPP.PlayerControls
{
    /// <summary>
    /// Player Controls for main camera operator
    /// </summary>
    public class PlayerInteractionController : MonoBehaviour
    {
        [SerializeField] private Camera m_PlayerCam;
        [SerializeField] private LayerMask m_GridInteractionLayerMask;
        [SerializeField] private CursorBehaviourController m_3DCursor;
        [SerializeField] private float m_MaxRayDistance = 10;

        private GridItem m_CurrentGridItem = null;
        //private GridController currentGridController = null;

        private void Awake()
        {
            if (m_PlayerCam == null)
            {
                m_PlayerCam.GetComponent<Camera>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            FirstPasRayCast();
        }

        private void FirstPasRayCast()
        {
            RaycastHit? firstPassRaycastHit = DoRayCast(m_GridInteractionLayerMask);
            if (firstPassRaycastHit != null)
            {
                GridController gridController = firstPassRaycastHit.Value.transform.GetComponent<GridController>();
                if (gridController)
                {
                    GridItem closestGridItem = gridController.GetClosestGridItemToPoint(firstPassRaycastHit.Value.point);
                    if (closestGridItem == m_CurrentGridItem)
                    {
                        return;
                    }

                    m_3DCursor.SetToGridItem(closestGridItem);

                    //FOR TESTING

                    for (int i = 0; i < gridController.GridItems.Count; i++)
                    {
                        gridController.GridItems[i].SetInteractiveStatus(InteractionType.DefaultState);
                    }
                    var adjacentTiles = gridController.GetAdjacentGridItems(closestGridItem, true);
                    closestGridItem.SetInteractiveStatus(InteractionType.Reclaimable);
                    for (int i = 0; i < adjacentTiles.Count; i++)
                    {
                        adjacentTiles[i].SetInteractiveStatus(InteractionType.Reclaimable);
                    }
                    m_CurrentGridItem = closestGridItem;
                }
                else
                {
                    SetCursorToEndOfRay();
                }
            }
            else
            {
                SetCursorToEndOfRay();
            }
        }

        private RaycastHit ? DoRayCast(LayerMask layerMask)
        {
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, m_MaxRayDistance, layerMask))
            {
                return hit;
            }
            return null;
        }

        private void SetCursorToEndOfRay()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            m_3DCursor.transform.position = ray.origin + (ray.direction * m_MaxRayDistance);
        }
    }
}
