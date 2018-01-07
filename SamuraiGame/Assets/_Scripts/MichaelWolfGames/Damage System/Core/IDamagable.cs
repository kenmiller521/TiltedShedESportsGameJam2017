using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MichaelWolfGames.DamageSystem
{
    /// <summary>
    /// Fundamental interface for damage event recievers.
    /// 
    /// Michael Wolf
    /// April 8, 2017
    /// </summary>
    public interface IDamagable
    {
        event Damage.DamageEventHandler OnTakeDamage;

        void ApplyDamage(object sender, Damage.DamageEventArgs e);
        void ApplyDamage(object sender, float damage, Vector3 hitPoint);

    }
}