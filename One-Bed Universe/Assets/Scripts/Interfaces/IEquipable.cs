using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HPP.GlobalEnums;

namespace HPP.Interface
{
    /// <summary>
    /// Interface for weapons and usables
    /// </summary>
    public interface IEquipable
    {
        public UniverseType CompatibleUniverseType { get; set; }
        public int CurrentUseCount { get; set; }
        public int MaxUseCount { get; set; }

    }
}
