using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MichaelWolfGames.DamageSystem
{
    /// <summary>
    /// Base class implementation of the IDamagable Interface.
    /// Hosts a helper function that wraps values in a Damage.DamageEventArgs before calling OnTakeDamage.
    /// 
    /// Michael Wolf
    /// April 8, 2017
    /// </summary>
    public abstract class DamagableBase : MonoBehaviour, IDamagable
    {
        public DamageSystem.Damage.Faction Faction = Damage.Faction.Enemy;
        public event Damage.DamageEventHandler OnTakeDamage = delegate { };
        public virtual void ApplyDamage(object sender, Damage.DamageEventArgs e)
        {
            OnTakeDamage(sender, e);
        }
        public virtual void ApplyDamage(object sender, float damage, Vector3 hitPoint)
        {
            ApplyDamage(sender, new Damage.DamageEventArgs(damage, hitPoint));
        }

        public virtual void ApplyDamage(float damage)
        {
            ApplyDamage(this, new Damage.DamageEventArgs(damage, this.transform.position));
        }
    }
}