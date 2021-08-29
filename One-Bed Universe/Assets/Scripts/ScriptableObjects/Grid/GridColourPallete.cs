using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HPP.Grid
{
    /// <summary>
    /// Generated Grid Data
    /// </summary>
    [CreateAssetMenu(fileName = "Grid Colour Pallete", menuName = "ScriptableObjects/GridColourPallete")]
    public class GridColourPallete : ScriptableObject
    {
        [Header("Mat 0")]
        public Material BaseMaterial0;
        public Material HighlightMaterial0;

        [Header("Mat 1")]
        public Material BaseMaterial1;
        public Material HighlightMaterial1;
    }
}
