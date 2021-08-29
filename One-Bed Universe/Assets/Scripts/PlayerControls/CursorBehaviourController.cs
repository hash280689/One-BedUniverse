using HPP.Grid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static HPP.GlobalEnums;

namespace HPP.PlayerControls
{
    /// <summary>
    /// Controls for governing the behaviour of the cursor
    /// </summary>
    public class CursorBehaviourController : MonoBehaviour
    {
        private InteractionType m_InteractionType;

        [SerializeField] private float m_YOffset = 0;

        [Header("Universe State")]
        public UnityEvent OnNeutral;
        public UnityEvent OnUniverseA;
        public UnityEvent OnUniverseB;

        [Header("Cursor Interaction Type")]
        public UnityEvent OnDefaultState;
        public UnityEvent OnBasicTyleHit;
        public UnityEvent OnInteractableHover;
        public UnityEvent OnReclamationIndicator;

        private UniverseType m_CurrentUniverseType;
        private InteractionType m_CurrentInteractionType;

        public void SetToGridItem(GridItem gridItem)
        {
            transform.position = gridItem.transform.position + (Vector3.up * (gridItem.transform.localScale.y + m_YOffset)); 

        }

        public void SetUniverseType(UniverseType universeType, bool force = false)
        {
            if (!force && m_CurrentUniverseType == universeType)
            {
                return;
            }

            switch (universeType)
            {
                case UniverseType.Neutral:
                    OnNeutral.Invoke();
                    break;
                case UniverseType.UniverseA:
                    OnUniverseA.Invoke();
                    break;
                case UniverseType.UniverseB:
                    OnUniverseB.Invoke();
                    break;
            }

            m_CurrentUniverseType = universeType;
        }

        public void SetInteractionType(InteractionType interactionType, bool force = false)
        {
            if (!force && m_CurrentInteractionType == interactionType)
            {
                return;
            }

            switch (interactionType)
            {
                case InteractionType.DefaultState:
                    OnDefaultState.Invoke();
                    break;
                case InteractionType.BasicTileHit:
                    OnBasicTyleHit.Invoke();
                    break;
                case InteractionType.Interactable:
                    OnInteractableHover.Invoke();
                    break;
                case InteractionType.Reclaimable:
                    OnReclamationIndicator.Invoke();
                    break;
            }

            m_CurrentInteractionType = interactionType;
        }
    }
}
