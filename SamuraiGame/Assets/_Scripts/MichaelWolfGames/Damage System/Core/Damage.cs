using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MichaelWolfGames.DamageSystem
{
    /// <summary>
    /// Static Class used for data handling throughout the DamageSystem.
    /// Contains Declarations for:
    /// - DamageEventHandler Delegate.
    /// - DamageEventArgs Struct.
    /// - DamageType Enumeration.
    /// - Various Static Methods to be used throughout the DamageSystem.
    /// 
    /// Michael K. Wolf
    /// April 8, 2017
    /// </summary>
    public static class Damage
    {
        /// <summary>
        /// Core delegate used for DamageEvents.
        /// </summary>
        /// <param name="sender">Object that is sending the DamageEvent.</param>
        /// <param name="e">Damage Event Arguments</param>
        public delegate void DamageEventHandler(object sender, DamageEventArgs e);

        /// <summary>
        /// Arguments passed during DamageEvents.
        /// </summary>
        [System.Serializable]
        public struct DamageEventArgs
        {
            public float DamageValue;
            public DamageType DamageType;
            public Vector3 HitPoint;
            public Faction SourceFaction;
            public DamageEventArgs(float damageValue, Vector3 hitPoint, DamageType type = DamageType.Default, Faction faction = Faction.Generic)
            {
                DamageValue = damageValue;
                DamageType = type;
                HitPoint = hitPoint;
                SourceFaction = faction;
            }
        }
        /// <summary>
        /// Expandable Enumeration for different Damage Types. 
        /// </summary>
        public enum DamageType
        {
            Default,
            Melee,
            Bullet,
            Explosive,
            Energy,
            Fire,
            Drill
        }

        /// <summary>
        /// Expandable Enumeration for different Factions. This helps for immunity. 
        /// </summary>
        public enum Faction
        {
            Generic,
            Player,
            Enemy
        }

        /// <summary>
        /// ~EXAMPLE OF EXTENSIBILITY~
        /// Returns a subtractive modifier for damage dependent on amount of armor.
        /// Modify this to change how different damage types are subtracted by armor.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="armorValue"></param>
        /// <returns></returns>
        public static float GetArmorReductionModifier(DamageType type, float armorValue)
        {
            switch (type)
            {
                case DamageType.Default:
                    return armorValue;
                case DamageType.Melee:
                    return armorValue;
                case DamageType.Bullet:
                    return armorValue;
                case DamageType.Explosive:
                    return armorValue / 2f;
                case DamageType.Energy:
                    return 0f;
                default:
                    Debug.LogWarning("DamageType " + type + " is not supported in this method. Returning zero.");
                    return 0f;
            }
        }
    }
}