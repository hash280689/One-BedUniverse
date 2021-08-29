using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HPP.GlobalEnums;

namespace HPP.GameFlow
{
    /// <summary>
    /// Manager Class for Game Flow and keeping track of player order 
    /// </summary>

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public Action<UniverseType> OnPlayerChanged;

        [SerializeField] private UniverseType m_CurrentPlayerType;

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

            OnPlayerChanged?.Invoke(m_CurrentPlayerType);
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