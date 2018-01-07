using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class OptionsMenuController : MonoBehaviour {
    public Slider MasterSlider;
    public Slider SoundEffectsSlider;
    public Slider MusicSlider;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CloseOptions()
    {
        gameObject.SetActive(false);
    }
}
