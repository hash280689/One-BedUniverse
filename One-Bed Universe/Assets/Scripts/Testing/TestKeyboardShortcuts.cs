using HPP.GameFlow;
using HPP.PlayerControls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HPP.Testing
{
    /// <summary>
    /// Keyboard shortcuts for testing
    /// </summary>
    
    public class TestKeyboardShortcuts : MonoBehaviour
    {
        [SerializeField] KeyboardConfig m_KeyboardConfig;

#if UNITY_EDITOR
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(m_KeyboardConfig.UniverseA))
            {
                GameManager.Instance.SetUniverseA();
            }
            else  if (Input.GetKeyDown(m_KeyboardConfig.UniverseB))
            {
                GameManager.Instance.SetUniverseB();
            }
        }

#endif
    }
}
