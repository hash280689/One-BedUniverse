using HPP.GameFlow;
using HPP.Grid;
using System;
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
        private UniverseType m_CurrentPlayerUniverseType;

        private RaycastHit? m_FirstPassRaycastHit = null;
        private List<GridItem> m_HighlightedGridItems = new List<GridItem>();

        private void Awake()
        {
            if (m_PlayerCam == null)
            {
                m_PlayerCam.GetComponent<Camera>();
            }
            m_CurrentPlayerUniverseType = GameManager.Instance.CurrentPlayerUniverseType;
            GameManager.Instance.OnPlayerChanged += OnPlayerChanged;
        }

        void Update()
        {
            FirstPasRayCast();

            //FOR TESTING
            if (Input.GetMouseButtonDown(0))
            {
                DoReclaimationAction();
                m_CurrentGridItem = null;
            }
        }

        private void OnPlayerChanged(UniverseType playerUniverseType)
        {
            m_CurrentPlayerUniverseType = playerUniverseType;
        }

        // Update is called once per frame

        private void FirstPasRayCast()
        {
            m_FirstPassRaycastHit = DoRayCast(m_GridInteractionLayerMask);
            if (m_FirstPassRaycastHit != null)
            {
                GridController gridController = m_FirstPassRaycastHit.Value.transform.GetComponent<GridController>();
                if (gridController)
                {
                    GridItem closestGridItem = gridController.GetClosestGridItemToPoint(m_FirstPassRaycastHit.Value.point, m_CurrentPlayerUniverseType);
                    if (closestGridItem == m_CurrentGridItem)
                    {
                        return;
                    }

                    m_3DCursor.SetToGridItem(closestGridItem);

                    //FOR TESTING
                    DoReclaimationHighlight(gridController, closestGridItem);
                    
                    //End FOR TESTING

                    m_CurrentGridItem = closestGridItem;
                }
                else
                {
                    if (m_CurrentGridItem == null)
                    {
                        SetCursorToEndOfRay();
                    }
                }
            }
            else
            {
                if (m_CurrentGridItem == null)
                {
                    SetCursorToEndOfRay();
                }
            }
        }

        private void DoReclaimationHighlight(GridController gridController, GridItem gridItem)
        {
            for (int i = 0; i < gridController.GridItems.Count; i++)
            {
                gridController.GridItems[i].SetInteractiveStatus(InteractionType.DefaultState);
            }
            m_HighlightedGridItems.Clear();

            gridItem.SetInteractiveStatus(InteractionType.Reclaimable);
            m_HighlightedGridItems.Add(gridItem);

            var adjacentTiles = gridController.GetAdjacentGridItems(gridItem, true);
            for (int i = 0; i < adjacentTiles.Count; i++)
            {
                adjacentTiles[i].SetInteractiveStatus(InteractionType.Reclaimable);
                m_HighlightedGridItems.Add(adjacentTiles[i]);
            }
        }    

        private void DoReclaimationAction()
        {
            if (m_FirstPassRaycastHit == null || m_HighlightedGridItems.Count == 0)
            {
                return;
            }

            for (int i = 0; i < m_HighlightedGridItems.Count; i++)
            {
                m_HighlightedGridItems[i].SetUniverseType(m_CurrentPlayerUniverseType);
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
