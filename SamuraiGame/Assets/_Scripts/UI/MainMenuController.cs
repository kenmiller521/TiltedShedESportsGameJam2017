using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MainMenuController : MonoBehaviour {
    public GameObject optionsPanel;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
