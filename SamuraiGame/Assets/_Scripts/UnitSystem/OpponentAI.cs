using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentAI : MonoBehaviour
{
    [SerializeField] private GoldManager _goldManager;
    [SerializeField] private UnitSpawn _unitSpawn;

    public int InfantryCost = 100;
    public int ArcherCost = 150;
    public int CalveryCost = 200;

    public enum EnemyType
    {
        Infantry,
        Archer,
        Calvery
    }

    private Queue<EnemyType> choiceQueue = new Queue<EnemyType>();

	
	void Update ()
    {
		if(CheckIfCanAffordChoice(choiceQueue.Peek()))
        {

        }
	}

    private void MakeChoice()
    {

    }

    private bool CheckIfCanAffordChoice(EnemyType choice)
    {
        switch (choice)
        {
            case EnemyType.Infantry:
                return _goldManager.balance > InfantryCost;
            case EnemyType.Archer:
                return _goldManager.balance > InfantryCost;
            case EnemyType.Calvery:
                return _goldManager.balance > InfantryCost;
            default:
                return false;
        }
    }

    private void SpawnEnemy(EnemyType choice)
    {

    }
}
