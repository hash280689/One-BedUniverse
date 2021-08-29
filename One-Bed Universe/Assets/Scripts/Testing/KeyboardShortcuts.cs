using HPP.GameFlow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HPP.Testing
{
    /// <summary>
    /// Keyboard shortcuts for testing
    /// </summary>
    
    public class KeyboardShortcuts : MonoBehaviour
    {
        [SerializeField] private KeyCode UniverseA = KeyCode.F1;
        [SerializeField] private KeyCode UniverseB = KeyCode.F2;

#if UNITY_EDITOR
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(UniverseA))
            {
                GameManager.Instance.SetUniverseA();
            }
            else  if (Input.GetKeyDown(UniverseB))
            {
                GameManager.Instance.SetUniverseB();
            }
        }

#endif
    }
}
