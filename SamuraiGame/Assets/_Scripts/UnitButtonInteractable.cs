using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Makes unit spawn buttons interactable based on whether there is enough gold for them
public class UnitButtonInteractable : MonoBehaviour
{
    public Button infantryButton;
    public Button calvaryButton;
    public Button archerButton;
    //player insance of GoldManager
    public GoldManager goldManager;

    void Start()
    {
        if (!goldManager) goldManager = GoldManager.ManagerInstances[0];
    }

    // Update is called once per frame
    void Update()
    {
        //Calvary Button
        if (goldManager.balance >= goldManager.cavalryCost)
            calvaryButton.interactable = true;

        else
            calvaryButton.interactable = false;

        //Infantry Button
        if (goldManager.balance >= goldManager.infantryCost)
            infantryButton.interactable = true;

        else
            infantryButton.interactable = false;

        //Archer Button
        if (goldManager.balance >= goldManager.archerCost)
            archerButton.interactable = true;

        else
            archerButton.interactable = false;
    }
}
