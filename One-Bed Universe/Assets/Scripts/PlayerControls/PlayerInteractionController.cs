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

        private GridNode m_CurrentGridNode = null;
        private UniverseType m_CurrentPlayerUniverseType;

        private RaycastHit? m_FirstPassRaycastHit = null;
        private List<GridNode> m_HighlightedGridNodes = new List<GridNode>();

        private void Start()
        {
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
                m_CurrentGridNode = null;
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
                    GridNode closestGridItem = gridController.GetClosestGridNodeToPoint(m_FirstPassRaycastHit.Value.point, m_CurrentPlayerUniverseType);
                    if (closestGridItem == m_CurrentGridNode)
                    {
                        return;
                    }

                    m_3DCursor.PlaceOnGridNode(closestGridItem);

                    //FOR TESTING
                    DoReclaimationHighlight(gridController, closestGridItem);
                    
                    //End FOR TESTING

                    m_CurrentGridNode = closestGridItem;
                }
                else
                {
                    if (m_CurrentGridNode == null)
                    {
                        SetCursorToEndOfRay();
                    }
                }
            }
            else
            {
                if (m_CurrentGridNode == null)
                {
                    SetCursorToEndOfRay();
                }
            }
        }

        private void DoReclaimationHighlight(GridController gridController, GridNode gridNode)
        {
            for (int i = 0; i < gridController.GridNodes.Count; i++)
            {
                gridController.GridNodes[i].SetInteractiveStatus(InteractionType.DefaultState);
                gridController.SetAdjacentGridNodes(gridController.GridNodes[i]);
            }
            m_HighlightedGridNodes.Clear();

            gridNode.SetInteractiveStatus(InteractionType.Reclaimable);
            m_HighlightedGridNodes.Add(gridNode);

            var adjacentTiles = gridNode.GetAdjacentNodes(true);
            for (int i = 0; i < adjacentTiles.Count; i++)
            {
                adjacentTiles[i].SetInteractiveStatus(InteractionType.Reclaimable);
                m_HighlightedGridNodes.Add(adjacentTiles[i]);
            }
        }    

        private void DoReclaimationAction()
        {
            if (m_FirstPassRaycastHit == null || m_HighlightedGridNodes.Count == 0)
            {
                return;
            }

            for (int i = 0; i < m_HighlightedGridNodes.Count; i++)
            {
                m_HighlightedGridNodes[i].SetUniverseType(m_CurrentPlayerUniverseType);
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
