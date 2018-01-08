using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawn : MonoBehaviour
{
    public GameObject archer;
    public float archerBuildTime = 1;

    public GameObject infantry;
    public float infantryBuildTime = 1;

    public GameObject calvary;
    public float calvaryBuildTime = 1;

    public Transform spawnPoint;

	// Use this for initialization
	void Start ()
    {		
	}
    
	// Update is called once per frame
	void Update ()
    {		
	}

    public void SpawnArcher()
    {
        GoldManager.ManagerInstances[0].DeductGold(GoldManager.ManagerInstances[0].archerCost);
        StartCoroutine(SpawnArcherIEnum());
    }

    public IEnumerator SpawnArcherIEnum()
    {
        yield return new WaitForSeconds(archerBuildTime);
        Instantiate(archer, spawnPoint.position, archer.transform.rotation);
    }

    public void SpawnInfantry()
    {
        GoldManager.ManagerInstances[0].DeductGold(GoldManager.ManagerInstances[0].infantryCost);
        StartCoroutine(SpawnInfantryIEnum());
    }

    public IEnumerator SpawnInfantryIEnum()
    {
        yield return new WaitForSeconds(infantryBuildTime);
        Instantiate(infantry, spawnPoint.position, infantry.transform.rotation);
    }

    public void SpawnCalvary()
    {
        GoldManager.ManagerInstances[0].DeductGold(GoldManager.ManagerInstances[0].calvaryCost);
        StartCoroutine(SpawnCalvaryIEnum());
    }

    public IEnumerator SpawnCalvaryIEnum()
    {
        yield return new WaitForSeconds(calvaryBuildTime);
        Instantiate(calvary, spawnPoint.position, calvary.transform.rotation);
    }
}
