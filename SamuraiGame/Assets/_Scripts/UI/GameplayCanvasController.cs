using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayCanvasController : MonoBehaviour {
    public GoldManager goldManager;
    public Text currentGoldText; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        currentGoldText.text = goldManager.balance.ToString();
	}
}
