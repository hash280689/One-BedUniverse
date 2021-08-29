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
        public Color BaseColour0;
        public Color HighlightColour0;

        [Header("Mat 1")]
        public Material BaseMaterial1;
        public Color BaseColour1;
        public Color HighlightColour1;


        [ContextMenu("Update Materials")]
        public void UpdateMaterialColours()
        {
            BaseMaterial0.color = BaseColour0;
            BaseMaterial1.color = BaseColour1;
        }

        private void Awake()
        {
            UpdateMaterialColours();
        }
    }
}
