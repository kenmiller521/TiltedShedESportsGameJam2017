using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PauseMenuController : OptionsMenuController {
    public GameObject pausePanel;
    UnityEvent pauseEvent;
    public bool gameIsPaused;
    public bool isOpen = false;

    void Start ()
	{
	    Time.timeScale = 1f;
        if (pauseEvent == null)
            pauseEvent = new UnityEvent();

        pauseEvent.AddListener(OpenPausePanel);
        pauseEvent.AddListener(ClosePausePanel);
    }
	
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gameIsPaused)
            {
                OpenPausePanel();
            }
            else
            {
                ClosePausePanel();
            }
        }
	}

    public void OpenPausePanel()
    {
        isOpen = true;
        Pause();
        pausePanel.SetActive(true);
    }
    public void ClosePausePanel()
    {
        isOpen = false;
        UnPause();
        pausePanel.SetActive(false);
    }
    public void LoadMainMenu(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Pause()
    {
        gameIsPaused = true;
        Time.timeScale = 0;
    }
    public void UnPause()
    {
        gameIsPaused = false;
        Time.timeScale = 1;
    }
}
