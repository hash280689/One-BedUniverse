using HPP.Grid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HPP.Interface
{
    /// <summary>
    /// Interface for objects which get placed on grid
    /// </summary>
    public interface IGridPlaceable
    {
        public float YOffset { get; set; }
        public void PlaceOnGridNode(GridNode gridItem);

    }
}
