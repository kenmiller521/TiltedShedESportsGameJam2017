using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using System;

public class UnitSpawn : MonoBehaviour
{
    public GameObject archer;
    public float archerBuildTime = 2;

    public GameObject infantry;
    public float infantryBuildTime = 1;

    public GameObject cavalry;
    public float cavalryBuildTime = 3;

    public Transform spawnPoint;

    public int ownerNumber;

    //events for FMod
    public UnityEvent SpawnArcherEvent;
    public UnityEvent SpawnInfantryEvent;
    public UnityEvent SpawnCavalryEvent;

    //Build Icons
    public HorizontalLayoutGroup buildIcons;
    public Image archerBuildImage;
    public Image infantryBuildImage;
    public Image cavalryBuildImage;

    public void SpawnArcher()
    {
        if (GoldManager.ManagerInstances[ownerNumber].balance >= GoldManager.ManagerInstances[ownerNumber].archerCost)
        {
            StartCoroutine(SpawnArcherIEnum());

            if(archerBuildImage)
            {
                Instantiate(archerBuildImage, buildIcons.transform);
                LayoutRebuilder.MarkLayoutForRebuild(buildIcons.transform as RectTransform);
            }        
        }
    }

    public IEnumerator SpawnArcherIEnum()
    {
        GoldManager.ManagerInstances[ownerNumber].DeductGold(GoldManager.ManagerInstances[ownerNumber].archerCost);
        yield return new WaitForSeconds(archerBuildTime);
        Instantiate(archer, spawnPoint.position, archer.transform.rotation);
    }

    public void SpawnInfantry()
    {
        if (GoldManager.ManagerInstances[ownerNumber].balance >= GoldManager.ManagerInstances[ownerNumber].infantryCost)
        {
            StartCoroutine(SpawnInfantryIEnum());

            if(infantryBuildImage)
            {
                Instantiate(infantryBuildImage, buildIcons.transform);
                LayoutRebuilder.MarkLayoutForRebuild(buildIcons.transform as RectTransform);
            }           
        }
    }

    public IEnumerator SpawnInfantryIEnum()
    {       
        GoldManager.ManagerInstances[ownerNumber].DeductGold(GoldManager.ManagerInstances[ownerNumber].infantryCost);
        yield return new WaitForSeconds(infantryBuildTime);
        Instantiate(infantry, spawnPoint.position, infantry.transform.rotation);
    }

    public void SpawnCavalry()
    {
        if (GoldManager.ManagerInstances[ownerNumber].balance >= GoldManager.ManagerInstances[ownerNumber].cavalryCost)
        {         
            StartCoroutine(SpawnCavalryIEnum());

            if(cavalryBuildImage)
            {
                Instantiate(cavalryBuildImage, buildIcons.transform);
                LayoutRebuilder.MarkLayoutForRebuild(buildIcons.transform as RectTransform);
            }
        }           
    }

    public IEnumerator SpawnCavalryIEnum()
    {
        GoldManager.ManagerInstances[ownerNumber].DeductGold(GoldManager.ManagerInstances[ownerNumber].cavalryCost);
        yield return new WaitForSeconds(cavalryBuildTime);
        Instantiate(cavalry, spawnPoint.position, cavalry.transform.rotation);
    }

    
}
