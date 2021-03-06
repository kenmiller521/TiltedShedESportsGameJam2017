﻿using UnityEngine;

namespace MichaelWolfGames.DamageSystem
{
    /// <summary>
    /// Extends DamageCollider, overriding its OnCollision functionality and replacing it with an OnTrigger approach.
    /// 
    /// Michael Wolf
    /// April 8, 2017
    /// </summary>
    public class DamageTrigger2D : DamageCollider
    {
        // Override to do nothing.
        protected override void OnCollisionEnter(Collision other) { }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            var go = (other.attachedRigidbody) ? other.attachedRigidbody.gameObject : other.gameObject;
            if (TryDealDamage(go, GetDamageEventArgumentsFromCollider(other)))
            {
                OnDealDamageSuccess();
            }
            else
            {
                OnDealDamageFailed();
            }
        }
        protected virtual Damage.DamageEventArgs GetDamageEventArgumentsFromCollider(Collider2D col)
        {
            Vector3 point = col.bounds.ClosestPoint(this.transform.position);
            //transform.position;// ClosestPointOnBounds(transform.position);
            return new Damage.DamageEventArgs(DamageValue, point, damageType);
        }

        private void OnEnable()
        {
            if(DealDamageOncePerActivation)
                CanDealDamage = true;
        }
    }
}