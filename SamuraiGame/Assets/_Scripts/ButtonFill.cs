using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MichaelWolfGames;
using UnityEngine.UI;

public class ButtonFill : MonoBehaviour
{
    public Image buildIcon;
    public float buildTime = 1;

    private float _timer;

    void Start()
    {
        _timer = 0;
    }

    void Update()
    {
        _timer += Time.deltaTime;
        buildIcon.fillAmount = _timer / buildTime;
    }

}
