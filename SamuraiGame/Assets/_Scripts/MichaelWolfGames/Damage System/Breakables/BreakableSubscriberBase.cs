using System;

namespace MichaelWolfGames.DamageSystem
{
    public abstract class BreakableSubscriberBase : SubscriberBase<BreakableObject>
    {
        protected override void SubscribeEvents()
        {
            SubscribableObject.OnBreak += DoOnBreak;
        }

        protected override void UnsubscribeEvents()
        {
            SubscribableObject.OnBreak -= DoOnBreak;
        }

        protected abstract void DoOnBreak(object sender, Damage.DamageEventArgs e);
    }
}