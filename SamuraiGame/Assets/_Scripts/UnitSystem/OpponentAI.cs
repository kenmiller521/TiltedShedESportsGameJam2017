﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentAI : MonoBehaviour
{
    [SerializeField] private GoldManager _goldManager;
    [SerializeField] private UnitSpawn _unitSpawn;
    [SerializeField] private EnemyType[] _initialQueue;

    public int InfantryCost = 100;
    public int ArcherCost = 150;
    public int CalveryCost = 200;

    public enum EnemyType
    {
        Infantry,
        Archer,
        Calvery
    }

    private Queue<EnemyType> _choiceQueue = new Queue<EnemyType>();

    private void Start()
    {
        _choiceQueue = new Queue<EnemyType>(_initialQueue);
    }
	
	void FixedUpdate ()
    {
        if (_choiceQueue.Count == 0)
            MakeChoice();
		if(CheckIfCanAffordChoice(_choiceQueue.Peek()))
        {
            SpawnEnemy(_choiceQueue.Dequeue());
            MakeChoice();
        }
	}

    private void MakeChoice()
    {
        EnemyType choice = (EnemyType)Random.Range(0, 3);
        _choiceQueue.Enqueue(choice);
    }

    private bool CheckIfCanAffordChoice(EnemyType choice)
    {
        switch (choice)
        {
            case EnemyType.Infantry:
                return _goldManager.balance > InfantryCost;
            case EnemyType.Archer:
                return _goldManager.balance > ArcherCost;
            case EnemyType.Calvery:
                return _goldManager.balance > CalveryCost;
            default:
                return false;
        }
    }

    private void SpawnEnemy(EnemyType choice)
    {
        //Debug.Log("Spawning");
        switch (choice)
        {
            case EnemyType.Infantry:
                _unitSpawn.SpawnInfantry();
                break;
            case EnemyType.Archer:
                _unitSpawn.SpawnArcher();
                break;
            case EnemyType.Calvery:
                _unitSpawn.SpawnCavalry();
                break;
            default:
                break;
        }
    }
}
