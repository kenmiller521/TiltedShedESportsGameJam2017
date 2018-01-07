using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PauseMenuController : OptionsMenuController {
    public GameObject pausePanel;
    UnityEvent pauseEvent;
    public bool gameIsPaused;
	// Use this for initialization
	void Start () {
        if (pauseEvent == null)
            pauseEvent = new UnityEvent();

        pauseEvent.AddListener(OpenPausePanel);
        pauseEvent.AddListener(ClosePausePanel);
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            if (gameIsPaused == true)
            {
                Time.timeScale = 0;
                pausePanel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                pausePanel.SetActive(false);
            }
        }
	}

    public void OpenPausePanel()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
    public void ClosePausePanel()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
    public void LoadMainMenu(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
