using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayCanvasController : MonoBehaviour
{
    public GoldManager goldManager;
    public Text currentGoldText; 
	// Use this for initialization
	void Start ()
    {
        if (!goldManager) goldManager = GoldManager.ManagerInstances[0];
	}
	
	// Update is called once per frame
	void Update () {
        currentGoldText.text = goldManager.balance.ToString();
	}
}
