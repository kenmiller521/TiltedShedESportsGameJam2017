using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Passively increments gold and adds kill Rewards
 * Deducts gold for units
 * */

public class GoldManager : MonoBehaviour
{
    public int OwnerNumber = 0;

    public int archerCost;
    public int calvaryCost;
    public int infantryCost;
    public int passiveGoldIncrement;
    public float passiveGoldTimer;
    public int balance; //referenced by UIButtonInteract

    private float _goldTimer;

    public static GoldManager[] ManagerInstances = new GoldManager[2];

    private void Awake()
    {
        ManagerInstances[OwnerNumber] = this;
    }

    // Use this for initialization
    private void Start()
    {
        balance = 0;
        _goldTimer = passiveGoldTimer;
    }

    // Update is called once per frame
    private void Update()
    {
        if (passiveGoldTimer <= 0f) return;
        _goldTimer -= Time.deltaTime;

        if (_goldTimer <= 0)
        {
            _goldTimer = passiveGoldTimer;
            AddGold(passiveGoldIncrement);
        }
    }

    //Add gold for passive increments and kill Rewards
    public void AddGold(int amount)
    {
        balance += amount;
    }

    //Deduct gold for unit spawns if sufficient funds
    public void DeductGold(int amount)
    {
        if (amount <= balance)
            balance -= amount;

        else
        {

        }

    }
    public static int GetOtherInstanceIndex(int currentInstanceIndex)
    {
        return (currentInstanceIndex == 0) ? 1 : 0;
    }
    public GoldManager GetOtherInstance()
    {
        return ManagerInstances[GetOtherInstanceIndex(OwnerNumber)];
    }

}
