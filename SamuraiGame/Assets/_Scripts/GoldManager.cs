using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Passively increments gold and adds kill Rewards
 * Deducts gold for units
 * */

public class GoldManager : MonoBehaviour
{
    public int archerCost;
    public int calvaryCost;
    public int infantryCost;
    public int passiveGoldIncrement;
    public float passiveGoldTimer;
    public int balance; //referenced by UIButtonInteract

    private float _goldTimer;

    public static GoldManager[] ManagerInstances;

    // Use this for initialization
    void Start()
    {
        balance = 0;
        _goldTimer = passiveGoldTimer;
    }

    // Update is called once per frame
    void Update()
    {
        _goldTimer -= Time.deltaTime;

        if (_goldTimer <= 0)
        {
            _goldTimer = passiveGoldTimer;
            AddGold(passiveGoldIncrement);
        }
    }

    //Add gold for passive increments and kill Rewards
    void AddGold(int amount)
    {
        balance += amount;
    }

    //Deduct gold for unit spawns if sufficient funds
    void DeductGold(int amount)
    {
        if (amount <= balance)
            balance -= amount;

        else
        {

        }

    }

}
