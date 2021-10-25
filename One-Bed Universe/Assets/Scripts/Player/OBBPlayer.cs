using HPP.Grid;
using HPP.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HPP.GlobalEnums;

namespace HPP.Player
{
    /// <summary>
    /// Player scripts for governing player character functions
    /// </summary>
    public class OBBPlayer : MonoBehaviour, IGridPlaceable
    {
        [SerializeField]
        private bool m_InitialiseOnAwake = true;

        [SerializeField]
        private PlayerConfiguration m_PlayerConfiguration;

        private int m_AvailableMovement = 0;
        private int m_CurrentHealth = 0;

        public UniverseType UniverseType { get; private set; }
        public int AvailableMovement => m_AvailableMovement;
        public int CurrentHealth => m_CurrentHealth;

        public float YOffset { get; set; }

        protected virtual void Awake()
        {
            if (m_InitialiseOnAwake)
            {
                Initialise();
            }
        }

        public virtual void Initialise()
        {
            if (m_PlayerConfiguration == null)
            {
                Debug.LogError("Player Configuration not Assigned");
                return;
            }

            UniverseType = m_PlayerConfiguration.UniverseType;
            m_AvailableMovement = m_PlayerConfiguration.BaseMaxMovement;
            m_CurrentHealth = m_PlayerConfiguration.BaseMaxHealth;
        }

        public void PlaceOnGridNode(GridNode gridItem)
        {
            transform.position = gridItem.transform.position + (Vector3.up * (gridItem.transform.localScale.y + YOffset));
        }
    }
}
