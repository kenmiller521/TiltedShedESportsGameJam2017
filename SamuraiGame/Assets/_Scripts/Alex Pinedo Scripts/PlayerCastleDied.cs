using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MichaelWolfGames;
using MichaelWolfGames.DamageSystem;

public class PlayerCastleDied : SubscriberBase<HealthManager>
{
    public GameObject LoseScreen;
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
        Debug.Log("You lose");

        LoseScreen.SetActive(true);
    }
}
