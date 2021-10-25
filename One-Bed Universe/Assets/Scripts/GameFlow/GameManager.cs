using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HPP.GlobalEnums;
using HPP.Player;
using HPP.Grid;

namespace HPP.GameFlow
{
    /// <summary>
    /// Manager Class for Game Flow and keeping track of player order 
    /// </summary>

    public class GameManager : MonoBehaviour
    {
        [Serializable]
        public class PlayerInitialisationConfig
        {
            public string Lable;
            public OBBPlayer Player;
            public GridNode StartingGridNode;

            public void Initialise()
            {
                Player.gameObject.SetActive(true);
                Player.PlaceOnGridNode(StartingGridNode);   
            }
        }

        public static GameManager Instance;

        public Action<UniverseType> OnPlayerChanged;

        [SerializeField] 
        private UniverseType m_CurrentPlayerType;
        [SerializeField] 
        private PlayerInitialisationConfig m_PlayerA;
        [SerializeField] 
        private PlayerInitialisationConfig m_PlayerB;

        

        public UniverseType CurrentPlayerUniverseType => m_CurrentPlayerType;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }

            m_PlayerA.Initialise();
            m_PlayerB.Initialise();
            SetPlayerType(m_CurrentPlayerType);
        }

        public void SetPlayerType(UniverseType universeType)
        {
            if (universeType == UniverseType.Neutral)
            {
                return;
            }

            m_CurrentPlayerType = universeType;
            OnPlayerChanged?.Invoke(m_CurrentPlayerType);
        }

        [ContextMenu("Universe A")]
        public void SetUniverseA()
        {
            SetPlayerType(UniverseType.UniverseA);
        }

        [ContextMenu("Universe B")]
        public void SetUniverseB()
        {
            SetPlayerType(UniverseType.UniverseB);
        }
    }
}