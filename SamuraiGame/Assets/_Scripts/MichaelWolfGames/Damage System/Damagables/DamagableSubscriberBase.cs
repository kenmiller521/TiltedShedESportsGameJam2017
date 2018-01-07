using System;

namespace MichaelWolfGames.DamageSystem
{
    public abstract class DamagableSubscriberBase : SubscriberBase<DamagableBase>
    {
        public DamagableBase Damagable { get { return ((DamagableBase) SubscribableObject); } }
        protected override void SubscribeEvents()
        {
            SubscribableObject.OnTakeDamage += DoOnTakeDamage;
        }

        protected override void UnsubscribeEvents()
        {
            SubscribableObject.OnTakeDamage -= DoOnTakeDamage;
        }

        protected abstract void DoOnTakeDamage(object sender, Damage.DamageEventArgs damageEventArgs);

    }
}