using System.Collections;
using System.Collections.Generic;
using MichaelWolfGames;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PauseMenuController _pauseMenu;
    [SerializeField] private GameObject _winCanvas;
    [SerializeField] private GameObject _loseCanvas;
    [SerializeField] private float _finishDelay = 0.5f;

    public UnityEvent OnWinGame;
    public UnityEvent OnLoseGame;

    public void WinGame()
    {
        this.InvokeAction(() =>
        {
            if (_pauseMenu)
            {
                _pauseMenu.ClosePausePanel();
                _pauseMenu.Pause();
            }
            _winCanvas.SetActive(true);
        }, _finishDelay);
    }

    public void LoseGame()
    {
        this.InvokeAction(() =>
        {
            if (_pauseMenu)
            {
                _pauseMenu.ClosePausePanel();
                _pauseMenu.Pause();
            }
            _loseCanvas.SetActive(true);
        }, _finishDelay);
    }

    public void Restart()
    {
        _pauseMenu.UnPause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        _pauseMenu.UnPause();
        SceneManager.LoadScene(0);
    }
}
