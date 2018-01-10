using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MichaelWolfGames;
using MichaelWolfGames.DamageSystem;

public class DisablePhysics2DOnDeath : SubscriberBase<HealthManager>
{
    protected override void SubscribeEvents()
    {
        SubscribableObject.OnDeath += DoOnDeath;
    }

    protected override void UnsubscribeEvents()
    {
        SubscribableObject.OnDeath -= DoOnDeath;
    }

    private void DoOnDeath()
    {
        var rb = GetComponent<Rigidbody2D>();
        if(rb)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
        }
        foreach (var collider in this.GetComponentsInChildren<Collider2D>())
        {
            collider.enabled = false;
        }
    }
}
