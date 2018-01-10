using MichaelWolfGames;
using MichaelWolfGames.DamageSystem;
using UnityEngine;

namespace UnitSystem
{
    public class UnitBonusDamage: SubscriberBase<DamageSenderBase>
    {
        [SerializeField] private float _bonusDamage = 10f;
        [SerializeField] private Unit.UnitType TargetType;
        protected override void SubscribeEvents()
        {
            SubscribableObject.OnDealDamage += DealBonusDamage;
        }

        protected override void UnsubscribeEvents()
        {
            SubscribableObject.OnDealDamage -= DealBonusDamage;
        }

        private void DealBonusDamage(object receiver, Damage.DamageEventArgs e)
        {
            //Debug.Log("Calling Deal Bonus Damage.");
            if (receiver.GetType() == typeof(DamagableBase) || receiver.GetType().IsSubclassOf(typeof(DamagableBase)))
            {
                //Debug.Log("2...");
                var damagable = (DamagableBase) receiver;
                var go = damagable.gameObject;
                var u = go.GetComponent<Unit>();
                if (u)
                {
                    //Debug.Log("3...");
                    if (u.Type == TargetType)
                    {
                        //Debug.Log("Dealing Bonus Damage.");
                        damagable.ApplyDamage(_bonusDamage);
                    }
                }
            }
        }
    }
}