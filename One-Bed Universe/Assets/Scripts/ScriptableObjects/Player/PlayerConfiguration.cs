using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HPP.GlobalEnums;

namespace HPP.Player
{
    /// <summary>
    /// Configuration of Players
    /// </summary>
    [CreateAssetMenu(fileName = "PlayerConfiguration", menuName = "ScriptableObjects/PlayerConfiguration")]
    public class PlayerConfiguration : ScriptableObject
    {
        public UniverseType UniverseType;
        public string Description;
        public int BaseMaxMovement;
        public int BaseMaxHealth;
        public int BaseRecoilForce;
    }
}
