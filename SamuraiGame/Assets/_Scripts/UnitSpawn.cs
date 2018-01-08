using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

public class UnitSpawn : MonoBehaviour
{
    public GameObject archer;
    public float archerBuildTime = 1;

    public GameObject infantry;
    public float infantryBuildTime = 1;

    public GameObject cavalry;
    public float cavalryBuildTime = 1;

    public Transform spawnPoint;

    public UnityEvent SpawnArcherEvent;
    public UnityEvent SpawnInfantryEvent;
    public UnityEvent SpawnCavalryEvent;

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
        StartCoroutine(SpawnArcherIEnum());
    }

    public IEnumerator SpawnArcherIEnum()
    {
        GoldManager.ManagerInstances[0].DeductGold(GoldManager.ManagerInstances[0].archerCost);
        yield return new WaitForSeconds(archerBuildTime);
        Instantiate(archer, spawnPoint.position, archer.transform.rotation);
    }

    public void SpawnInfantry()
    {
        StartCoroutine(SpawnInfantryIEnum());
    }

    public IEnumerator SpawnInfantryIEnum()
    {
        GoldManager.ManagerInstances[0].DeductGold(GoldManager.ManagerInstances[0].infantryCost);
        yield return new WaitForSeconds(infantryBuildTime);
        Instantiate(infantry, spawnPoint.position, infantry.transform.rotation);
    }

    public void SpawnCavalry()
    {
        StartCoroutine(SpawnCavalryIEnum());
    }

    public IEnumerator SpawnCavalryIEnum()
    {
        GoldManager.ManagerInstances[0].DeductGold(GoldManager.ManagerInstances[0].cavalryCost);
        yield return new WaitForSeconds(cavalryBuildTime);
        Instantiate(cavalry, spawnPoint.position, cavalry.transform.rotation);
    }

    
}
