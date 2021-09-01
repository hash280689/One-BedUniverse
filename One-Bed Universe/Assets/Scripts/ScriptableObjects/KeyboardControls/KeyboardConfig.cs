using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HPP.PlayerControls
{
    /// <summary>
    /// All keyboard shortcuts stored in one source
    /// </summary>
    /// 
    [CreateAssetMenu(fileName = "KeyboardConfig", menuName = "ScriptableObjects/KeyboardConfig")]
    public class KeyboardConfig : ScriptableObject
    {
        [Header("Testing Shortcuts")]
        public KeyCode UniverseA = KeyCode.F1;
        public KeyCode UniverseB = KeyCode.F2;

        [Header("Camera Control Shortcuts")]
        public KeyCode CameraClockwise = KeyCode.E;
        public KeyCode CameraAntiClockwise = KeyCode.Q;
        public KeyCode CameraZoomIn = KeyCode.W;
        public KeyCode CameraZoomOut = KeyCode.S;
    }
}