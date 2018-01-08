using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UnitSpawn : MonoBehaviour
{
    public GameObject archer;
    public float archerBuildTime = 1;

    public GameObject infantry;
    public float infantryBuildTime = 1;

    public GameObject cavalry;
    public float cavalryBuildTime = 1;

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

    public void SpawnCavalry()
    {
        GoldManager.ManagerInstances[0].DeductGold(GoldManager.ManagerInstances[0].cavalryCost);
        StartCoroutine(SpawnCavalryIEnum());
    }

    public IEnumerator SpawnCavalryIEnum()
    {
        yield return new WaitForSeconds(cavalryBuildTime);
        Instantiate(cavalry, spawnPoint.position, cavalry.transform.rotation);
    }

    
}
