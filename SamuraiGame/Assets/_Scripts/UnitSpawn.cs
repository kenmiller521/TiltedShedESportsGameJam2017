using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using System;

public class UnitSpawn : MonoBehaviour
{
    public GameObject archer;
    public float archerBuildTime = 1;

    public GameObject infantry;
    public float infantryBuildTime = 1;

    public GameObject cavalry;
    public float cavalryBuildTime = 1;

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
            Instantiate(archerBuildImage, buildIcons.transform);
            LayoutRebuilder.MarkLayoutForRebuild(buildIcons.transform as RectTransform);
        }
    }

    public IEnumerator SpawnArcherIEnum()
    {
        GoldManager.ManagerInstances[ownerNumber].DeductGold(GoldManager.ManagerInstances[ownerNumber].archerCost);
        //float timer = 0;

        //while(timer < archerBuildTime)
        //{
        //    timer += Time.deltaTime;
        //    buttonFill.UpdateArcherButton(timer / archerBuildTime);
        //    yield return null;
        //}
        yield return new WaitForSeconds(archerBuildTime);
        Instantiate(archer, spawnPoint.position, archer.transform.rotation);
    }

    public void SpawnInfantry()
    {
        if (GoldManager.ManagerInstances[ownerNumber].balance >= GoldManager.ManagerInstances[ownerNumber].infantryCost)
        {
            StartCoroutine(SpawnInfantryIEnum());
            Instantiate(infantryBuildImage, buildIcons.transform);
            LayoutRebuilder.MarkLayoutForRebuild(buildIcons.transform as RectTransform);
        }
    }

    public IEnumerator SpawnInfantryIEnum()
    {       
        GoldManager.ManagerInstances[ownerNumber].DeductGold(GoldManager.ManagerInstances[ownerNumber].infantryCost);
       
        //float timer = 0;

        //while (timer < infantryBuildTime)
        //{
        //    timer += Time.deltaTime;
        //    buttonFill.UpdateInfantryButton(timer / infantryBuildTime);
        //    yield return null;
        //}
        yield return new WaitForSeconds(infantryBuildTime);

        Instantiate(infantry, spawnPoint.position, infantry.transform.rotation);
        
       
    }

    public void SpawnCavalry()
    {
        if (GoldManager.ManagerInstances[ownerNumber].balance >= GoldManager.ManagerInstances[ownerNumber].cavalryCost)
        {
            Instantiate(cavalryBuildImage, buildIcons.transform);
            LayoutRebuilder.MarkLayoutForRebuild(buildIcons.transform as RectTransform);
            StartCoroutine(SpawnCavalryIEnum());
        }           
    }

    public IEnumerator SpawnCavalryIEnum()
    {
        GoldManager.ManagerInstances[ownerNumber].DeductGold(GoldManager.ManagerInstances[ownerNumber].cavalryCost);

       
        //float timer = 0;

        //while (timer < cavalryBuildTime)
        //{
        //    timer += Time.deltaTime;
        //    buttonFill.UpdateCalvaryButton(timer / cavalryBuildTime);
        //    yield return null;
        //}

        yield return new WaitForSeconds(cavalryBuildTime);

        Instantiate(cavalry, spawnPoint.position, cavalry.transform.rotation);
    }

    
}
