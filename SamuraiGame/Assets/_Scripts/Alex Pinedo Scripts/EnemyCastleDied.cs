using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MichaelWolfGames;
using MichaelWolfGames.DamageSystem;

public class EnemyCastleDied : SubscriberBase<HealthManager>
{
    public GameObject WinScreen;
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
        Debug.Log("You Win");
        WinScreen.SetActive(true);
    }
}
