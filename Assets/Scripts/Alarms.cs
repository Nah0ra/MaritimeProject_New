using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class Alarms : MonoBehaviour
{
    public bool alarm;
    [SerializeField]
    private Image _alarmImage;
    private Color color1 = new Color(0.925f, 0.412f, 0.412f); // #EC6969
    private Color color2 = Color.red; // #FF0000

    private GameManager _gameManager;

    private PhotonView photonView;

    // private IEnumerator coroutine;

    private void Start()
    {
        _alarmImage.GetComponent<Image>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //coroutine = ChangeColor();
    }

    private void Update()
    {
        if (_gameManager.AllClients)
        {
            if (alarm) 
            _alarmImage.color = _alarmImage.color == color1 ? color2 : color1;
            else
            {
                _alarmImage.color = color1;
            }
        }
    }

    // public void ToggleAlarm()
    // {
    //     alarm = !alarm;
    //     if (alarm)
    //     {
    //         StartCoroutine(coroutine);
    //     }
    //     else
    //     {
    //         StopCoroutine(coroutine);
    //         _alarmImage.color = color1;
    //     }
    // }

    // private IEnumerator ChangeColor()
    // {
    //     while (alarm)
    //     {
    //         _alarmImage.color = _alarmImage.color == color1 ? color2 : color1;
    //         yield return new WaitForSeconds(0.5f);
    //     }
    // }
}
