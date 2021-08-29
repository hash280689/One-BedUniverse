using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HPP.PlayerControls
{
    /// <summary>
    /// Config for different weapons and interactions which can reclaim space
    /// </summary>

    [CreateAssetMenu(fileName = "Reclaimation Config", menuName = "ScriptableObjects/ReclaimationConfig")]
    public class ReclaimationConfig : ScriptableObject
    {
        public int GridDistance = 1;
        public bool Diagonal = false;
    }
}
