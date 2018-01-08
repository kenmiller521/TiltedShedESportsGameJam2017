using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MichaelWolfGames;
using MichaelWolfGames.DamageSystem;

public class DestroyOnDealDamage : SubscriberBase<DamageSenderBase>
{
    protected override void SubscribeEvents()
    {
        SubscribableObject.OnDealDamage += DoOnDealDamage;
    }

    protected override void UnsubscribeEvents()
    {
        SubscribableObject.OnDealDamage -= DoOnDealDamage;
    }

    private void DoOnDealDamage(object sender, Damage.DamageEventArgs e)
    {
        Destroy(gameObject, 0.1f);
    }
}
