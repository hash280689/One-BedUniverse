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
        [Header("Variant 0")]
        public Material BaseMaterial0;
        public Color BaseColour0;
        public Color HighlightColour0;

        [Header("Variant 1")]
        public Material BaseMaterial1;
        public Color BaseColour1;
        public Color HighlightColour1;
    }
}
