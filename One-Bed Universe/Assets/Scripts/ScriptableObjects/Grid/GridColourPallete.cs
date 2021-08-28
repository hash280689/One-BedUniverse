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
        public Color m_BaseMaterial0Colour;
        public Color m_Highlight0Colour;

        [Header("Mat 1")]
        public Material BaseMaterial1;
        public Color m_BaseMaterial1Colour;
        public Color m_Highlight1Colour;


        [ContextMenu("Update Materials")]
        public void UpdateMaterialColours()
        {
            BaseMaterial0.color = m_BaseMaterial0Colour;
            BaseMaterial1.color = m_BaseMaterial1Colour;
        }

        private void Awake()
        {
            UpdateMaterialColours();
        }
    }
}
