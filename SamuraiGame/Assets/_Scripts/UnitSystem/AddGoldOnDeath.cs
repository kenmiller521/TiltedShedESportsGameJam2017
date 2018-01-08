using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MichaelWolfGames;
using MichaelWolfGames.DamageSystem;

public class AddGoldOnDeath : SubscriberBase<HealthManager>
{
    [SerializeField] private Unit _unit;
    [SerializeField] private int _goldValue = 50;

    protected override void Start()
    {
        base.Start();
        if (!_unit) _unit = this.GetComponentInParent<Unit>();
    }
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
        if (GoldManager.ManagerInstances[GoldManager.GetOtherInstanceIndex(_unit.OwnerNumber)] != null)
        {
            GoldManager.ManagerInstances[GoldManager.GetOtherInstanceIndex(_unit.OwnerNumber)].AddGold(_goldValue);
        }
    }
}
