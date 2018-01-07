using System;
using UnityEngine;

namespace MichaelWolfGames.DamageSystem
{
    /// <summary>
    /// Damage Sender that sends damage events via collisions.
    /// Use or Extend this for most physical based damage senders. 
    /// NOTE: For Triggers, see the DamageTrigger class, which extends from this.
    /// 
    /// Michael Wolf
    /// April 8, 2017
    /// </summary>
    public class DamageCollider : DamageSenderBase
	{
        public Action OnDealDamageSuccessEvent = delegate { };
        public Action OnDealDamageFailedEvent = delegate { };

        public bool DealDamageOncePerActivation = true;
        //public bool UseTrigger = false;

        protected virtual void OnCollisionEnter(Collision other)
        {
            if (TryDealDamage(other.gameObject, GetDamageEventArgumentsFromCollision(other)))
            {
                OnDealDamageSuccess();
                if (DealDamageOncePerActivation)
                {
                    CanDealDamage = false;
                }
            }
            else
            {
                OnDealDamageFailed();
            }
        }
     //   /// <summary>
     //   /// ToDo: Seperate this into another script AND replace all uses of it as a trigger.
     //   /// </summary>
     //   /// <param name="other"></param>
	    //protected virtual void OnTriggerEnter(Collider other)
     //   {
     //       if (!UseTrigger) return;
     //       var go = (other.attachedRigidbody) ? other.attachedRigidbody.gameObject : other.gameObject;
     //       if (TryDealDamage(go, new Damage.DamageEventArgs(base.DamageValue, other.ClosestPointOnBounds(this.transform.position), damageType, faction)))
     //       {
     //           OnDealDamageSuccess();
     //           //Debug.Log(this.gameObject.name + "Successfully Dealt Damage to " + go + ".");
     //           if (DealDamageOncePerActivation)
     //           {
     //               CanDealDamage = false;
     //           }
     //       }
     //       else
     //       {
     //           OnDealDamageFailed();
     //           //Debug.Log(this.gameObject.name + "Failed To Deal Damage to " + go + ".");
     //       }
     //   }

        protected virtual void OnDealDamageSuccess()
        {
            OnDealDamageSuccessEvent();
        }

        protected virtual void OnDealDamageFailed()
        {
            OnDealDamageFailedEvent();
        }

        protected virtual Damage.DamageEventArgs GetDamageEventArgumentsFromCollision(Collision collision)
        {
            return new Damage.DamageEventArgs(DamageValue, collision.contacts[0].point, damageType, faction);
        }
    }
}