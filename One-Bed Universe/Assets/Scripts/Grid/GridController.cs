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
        [SerializeField] private GameObject m_GridItemPrefab;
        [SerializeField] private int m_XCount = 2;
        [SerializeField] private int m_YCount = 2;
        [SerializeField] private float m_GridItemSize = 1;
        [SerializeField] private Transform m_GridItemContainer;
        [SerializeField] private BoxCollider m_BoxCollider;
        [SerializeField] private List<GridItem> m_GridItems;

        public List<GridItem> GridItems => m_GridItems;
        private const float M_GRID_ITEM_Y_SCALE = 1;
        private bool m_IsBaseColour0 = false;
        
        [ContextMenu("GenerateGrid")]
        public void GenerateGrid()
        {
            ClearGrid();
            if (m_GridItemContainer == null)
            {
                m_GridItemContainer = this.transform;
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

                    m_GridItems.Add(InitialiseGridItem(x,y, universeType));
                    m_IsBaseColour0 = !m_IsBaseColour0;
                }
            }
            m_BoxCollider.size = new Vector3(m_XCount * m_GridItemSize, M_GRID_ITEM_Y_SCALE, m_YCount * m_GridItemSize);
        }

        private GridItem InitialiseGridItem(int xRef, int yRef, UniverseType universeType = UniverseType.Neutral)
        {
            GridItem newGridItem = Instantiate(m_GridItemPrefab).GetComponent<GridItem>();
            newGridItem.gameObject.name = "G-" + xRef + "-" + yRef;
            newGridItem.transform.localScale = new Vector3(m_GridItemSize, M_GRID_ITEM_Y_SCALE, m_GridItemSize);
            float xPos = xRef - ((m_GridItemSize * m_XCount) / 2);
            float yPos = yRef - ((m_GridItemSize * m_YCount) / 2);
            newGridItem.SetListRefs(xRef, yRef);
            newGridItem.SetLocalPosition(xPos, yPos);
            newGridItem.transform.parent = m_GridItemContainer;
            newGridItem.SetGridItemProperties(m_IsBaseColour0, universeType, InteractionType.Null);
            return newGridItem;
        }

        [ContextMenu("Clear Grid")]
        public void ClearGrid()
        {
            if (m_GridItems == null || m_GridItems.Count == 0)
            {
                m_GridItems = new List<GridItem>();
                return;
            }

            for (int i = 0; i < m_GridItems.Count; i++)
            {
                if (m_GridItems[i].gameObject != null)
                {
                    DestroyImmediate(m_GridItems[i].gameObject);
                }

            }
            m_GridItems.Clear();
            m_GridItems = new List<GridItem>();
            m_BoxCollider.size = Vector3.zero;
        }

        public GridItem GetClosestGridItemToPoint(Vector3 point)
        {
            float closestDistance = Mathf.Infinity;
            GridItem closestGridItem = null;
            for (int i = 0; i < m_GridItems.Count; i++)
            {
                if (m_GridItems[i].gameObject != null)
                {
                    float newDistance = Vector3.Distance(point, m_GridItems[i].transform.position);
                    if (newDistance < closestDistance)
                    {
                        closestGridItem = m_GridItems[i];
                        closestDistance = newDistance;
                    }
                }
            }
            return closestGridItem;
        }

        public GridItem GetClosestGridItemToPoint(Vector3 point, UniverseType ? universeType = null)
        {
            float closestDistance = Mathf.Infinity;
            GridItem closestGridItem = null;
            for (int i = 0; i < m_GridItems.Count; i++)
            {
                if (universeType == null || (universeType != null && universeType == m_GridItems[i].GridUniverseType))
                {
                    float newDistance = Vector3.Distance(point, m_GridItems[i].transform.position);
                    if (newDistance < closestDistance)
                    {
                        closestGridItem = m_GridItems[i];
                        closestDistance = newDistance;
                    }
                }
            }
            return closestGridItem;
        }

        public List<GridItem> GetAdjacentGridItems(GridItem gridItem, bool useDiagonal = true)
        {
            List<GridItem> adjacentGridItems = new List<GridItem>();
            List<Vector2> searchCases = new List<Vector2>();

            int baseX = gridItem.XRef;
            int baseY = gridItem.YRef;

            searchCases.Add(new Vector2(baseX, baseY + 1));
            searchCases.Add(new Vector2(baseX, baseY - 1));
            searchCases.Add(new Vector2(baseX + 1, baseY));
            searchCases.Add(new Vector2(baseX - 1, baseY));

            if(useDiagonal)
            {
                searchCases.Add(new Vector2(baseX + 1, baseY + 1));
                searchCases.Add(new Vector2(baseX - 1, baseY - 1));
                searchCases.Add(new Vector2(baseX + 1, baseY - 1));
                searchCases.Add(new Vector2(baseX - 1, baseY + 1));
            }

            foreach (Vector2 searchCase in searchCases)
            {
                GridItem potentialGridItem = SearchForGridItemByListRef(
                    (int)searchCase.x, (int)searchCase.y);
                if (potentialGridItem != null)
                {
                    adjacentGridItems.Add(potentialGridItem);
                }
            }

            return adjacentGridItems;
        }

        //private bool ValidateListRef(int xRef, int yRef)
        //{
        //    if (xRef < 0 || xRef >= m_XCount)
        //        return false;
        //    if (yRef < 0 || yRef >= m_YCount)
        //        return false;
        //    return true;
        //}

        private GridItem SearchForGridItemByListRef(int xRef, int yRef)
        {
            for (int i = 0; i < m_GridItems.Count; i++)
            {
                if (m_GridItems[i].XRef == xRef && m_GridItems[i].YRef == yRef)
                {
                    return m_GridItems[i];
                }
            }
            return null;
        }
    }
}
