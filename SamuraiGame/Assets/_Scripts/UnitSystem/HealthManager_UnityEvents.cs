using MichaelWolfGames;
using MichaelWolfGames.DamageSystem;
using UnityEngine.Events;

namespace UnitSystem
{
    public class HealthManager_UnityEvents : SubscriberBase<HealthManager>
    {
        public UnityEvent OnTakeDamageEvent;
        public UnityEvent OnDeathEvent;

        protected override void SubscribeEvents()
        {
            SubscribableObject.OnTakeDamage += DoOnTakeDamage;
            SubscribableObject.OnDeath += DoOnDeath;
        }

        protected override void UnsubscribeEvents()
        {
            SubscribableObject.OnTakeDamage -= DoOnTakeDamage;
            SubscribableObject.OnDeath -= DoOnDeath;
        }

        private void DoOnTakeDamage(object sender, Damage.DamageEventArgs e)
        {
            OnTakeDamageEvent.Invoke();
        }

        private void DoOnDeath()
        {
            OnDeathEvent.Invoke();
        }
    }
}