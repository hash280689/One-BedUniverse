using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using static HPP.GlobalEnums;

namespace HPP.Grid
{
    /// <summary>
    /// Grid Generation Control with context menu controls for Editor
    /// </summary>
    public class GridController : MonoBehaviour
    {
        [SerializeField] private GameObject m_GridNodePrefab;
        [SerializeField] private int m_XCount = 2;
        [SerializeField] private int m_YCount = 2;
        [SerializeField] private float m_GridNodeSize = 1;
        [SerializeField] private Transform m_GridNodesContainer;
        [SerializeField] private BoxCollider m_BoxCollider;
        [SerializeField] private List<GridNode> m_GridNodes;

        public List<GridNode> GridNodes => m_GridNodes;
        private const float M_GRID_NODE_Y_SCALE = 1;
        private bool m_IsBaseColour0 = false;
        
        [ContextMenu("GenerateGrid")]
        public void GenerateGrid()
        {
            ClearGrid();
            if (m_GridNodesContainer == null)
            {
                m_GridNodesContainer = this.transform;
            }

            for (int x = 0; x < m_XCount; x++)
            {
                if (m_YCount%2 == 0)
                {
                    m_IsBaseColour0 = !m_IsBaseColour0;
                }

                for (int y = 0; y < m_YCount; y++)
                {
                    UniverseType universeType = UniverseType.Neutral;

                    if (y < m_YCount / 2)
                    {
                        universeType = UniverseType.UniverseA;
                    }
                    else
                    {
                        universeType = UniverseType.UniverseB;
                    }

                    if (m_YCount%2 == 1 && y == ((m_YCount - 1)/2))
                    {
                        universeType = UniverseType.Neutral;
                    }

                    m_GridNodes.Add(InitialiseGridNode(x,y, universeType));
                    m_IsBaseColour0 = !m_IsBaseColour0;
                }
            }

            SetAllAdjacentGridNodes();
            m_BoxCollider.size = new Vector3(m_XCount * m_GridNodeSize, M_GRID_NODE_Y_SCALE, m_YCount * m_GridNodeSize);
        }

        [ContextMenu("SetAllAdjacentGridNodes")]
        public void SetAllAdjacentGridNodes()
        {
            foreach (GridNode gridNode in m_GridNodes)
            {
                SetAdjacentGridNodes(gridNode);
            }
        }

        private GridNode InitialiseGridNode(int xRef, int yRef, UniverseType universeType = UniverseType.Neutral)
        {
            GridNode newGridNode = Instantiate(m_GridNodePrefab).GetComponent<GridNode>();
            newGridNode.gameObject.name = "G-" + xRef + "-" + yRef;
            newGridNode.transform.localScale = new Vector3(m_GridNodeSize, M_GRID_NODE_Y_SCALE, m_GridNodeSize);
            float xPos = xRef - ((m_GridNodeSize * m_XCount) / 2);
            float yPos = yRef - ((m_GridNodeSize * m_YCount) / 2);
            newGridNode.SetListRefs(xRef, yRef);
            newGridNode.SetLocalPosition(xPos, yPos);
            newGridNode.transform.parent = m_GridNodesContainer;
            newGridNode.SetGridNodeProperties(m_IsBaseColour0, universeType, InteractionType.Null);
            return newGridNode;
        }

        [ContextMenu("Clear Grid")]
        public void ClearGrid()
        {
            if (m_GridNodes == null || m_GridNodes.Count == 0)
            {
                m_GridNodes = new List<GridNode>();
                return;
            }

            for (int i = 0; i < m_GridNodes.Count; i++)
            {
                if (m_GridNodes[i].gameObject != null)
                {
                    DestroyImmediate(m_GridNodes[i].gameObject);
                }

            }
            m_GridNodes.Clear();
            m_GridNodes = new List<GridNode>();
            m_BoxCollider.size = Vector3.zero;
        }

        public GridNode GetClosestGridNodeToPoint(Vector3 point)
        {
            float closestDistance = Mathf.Infinity;
            GridNode closestGridNode = null;
            for (int i = 0; i < m_GridNodes.Count; i++)
            {
                if (m_GridNodes[i].gameObject != null)
                {
                    float newDistance = Vector3.Distance(point, m_GridNodes[i].transform.position);
                    if (newDistance < closestDistance)
                    {
                        closestGridNode = m_GridNodes[i];
                        closestDistance = newDistance;
                    }
                }
            }
            return closestGridNode;
        }

        public GridNode GetClosestGridNodeToPoint(Vector3 point, UniverseType ? universeType = null)
        {
            float closestDistance = Mathf.Infinity;
            GridNode closestGridNode = null;
            for (int i = 0; i < m_GridNodes.Count; i++)
            {
                if (universeType == null || (universeType != null && universeType == m_GridNodes[i].GridUniverseType))
                {
                    float newDistance = Vector3.Distance(point, m_GridNodes[i].transform.position);
                    if (newDistance < closestDistance)
                    {
                        closestGridNode = m_GridNodes[i];
                        closestDistance = newDistance;
                    }
                }
            }
            return closestGridNode;
        }


        private GridNode SearchForGridNodeByListRef(int xRef, int yRef)
        {
            for (int i = 0; i < m_GridNodes.Count; i++)
            {
                if (m_GridNodes[i].XRef == xRef && m_GridNodes[i].YRef == yRef)
                {
                    return m_GridNodes[i];
                }
            }
            return null;
        }

        public void SetAdjacentGridNodes(GridNode gridNode)
        {
            int baseX = gridNode.XRef;
            int baseY = gridNode.YRef;

            gridNode.AdjacentGridNodes = new AdjacentGridNodes
            {
                NorthGN = SearchForGridNodeByListRef(baseX, baseY + 1),
                SouthGN = SearchForGridNodeByListRef(baseX, baseY - 1),
                EastGN = SearchForGridNodeByListRef(baseX + 1, baseY),
                WestGN = SearchForGridNodeByListRef(baseX - 1, baseY),
                NorthEastGN = SearchForGridNodeByListRef(baseX + 1, baseY + 1),
                SouthEastGN = SearchForGridNodeByListRef(baseX + 1, baseY - 1),
                SouthWestGN = SearchForGridNodeByListRef(baseX - 1, baseY - 1),
                NorthWestGN = SearchForGridNodeByListRef(baseX - 1, baseY + 1)
            };
        }    
    }
}
