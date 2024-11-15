using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class CompressedAirScript : MonoBehaviour
{
    private GameManager _gameManager;

    private PhotonView photonView;

    private bool shoreOn;
    public bool AC1;
    public bool AC2;
    
    [SerializeField]
    private GameObject AAC1;
    [SerializeField]
    private GameObject AR1;
    [SerializeField]
    private GameObject AAC2;
    [SerializeField]
    private GameObject AR2;
    [SerializeField]
    private GameObject SA;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        photonView = gameObject.GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameManager.AllClients)
        {
            if (_gameManager.shore)
            {
                shoreOn = true;
            }

            if (AC1 && AC2)
            {
                SA.GetComponent<GaugeScript>().Active = true;
                SA.GetComponent<GaugeScript>().Forward = true;
            }
            else
            {
                SA.GetComponent<GaugeScript>().Active = true;
                SA.GetComponent<GaugeScript>().Forward = false;
                SA.GetComponent<GaugeScript>().Value = 0;
            }
        }
    }
    
    public void onAir1ButtonPressOn()
    {
        photonView.RPC("RPConAir1ButtonPressOn", RpcTarget.All);
    }

    [PunRPC]
    public void RPConAir1ButtonPressOn()
    {
        if (shoreOn)
        {
            //Debug.Log("Air Compressor 1 ON");
            AAC1.GetComponent<GaugeScript>().Active = true;
            AR1.GetComponent<GaugeScript>().Active = true;
            AAC1.GetComponent<GaugeScript>().Forward = true;
            AR1.GetComponent<GaugeScript>().Forward = true;
            AC1 = true;
        }
        
    }
    
    public void onAir2ButtonPressOn()
    {
        photonView.RPC("RPConAir2ButtonPressOn", RpcTarget.All);
    }


    [PunRPC]
    public void RPConAir2ButtonPressOn()
    {
        if (shoreOn)
        {
            //Debug.Log("Air Compressor 2 ON");
            AAC2.GetComponent<GaugeScript>().Active = true;
            AR2.GetComponent<GaugeScript>().Active = true;
            AAC2.GetComponent<GaugeScript>().Forward = true;
            AR2.GetComponent<GaugeScript>().Forward = true;
            AC2 = true;
        }
    }
    

    public void onAir1ButtonPressOff()
    {
        photonView.RPC("RPConAir1ButtonPressOff", RpcTarget.All);
    }

    [PunRPC]
    public void RPConAir1ButtonPressOff()
    {
        if (shoreOn)
        {
            //Debug.Log("Air Compressor 1 OFF");
            AAC1.GetComponent<GaugeScript>().Active = true;
            AR1.GetComponent<GaugeScript>().Active = true;
            AAC1.GetComponent<GaugeScript>().Forward = false;
            AR1.GetComponent<GaugeScript>().Forward = false;
            AR1.GetComponent<GaugeScript>().Value = 0;
            AAC1.GetComponent<GaugeScript>().Value = 0;
            AC1 = false;
        }
    }
    
    public void onAir2ButtonPressOff()
    {
        photonView.RPC("RPConAir2ButtonPressOff", RpcTarget.All);
    }

    [PunRPC]
    public void RPConAir2ButtonPressOff()
    {
        if (shoreOn)
        {
            //Debug.Log("Air Compressor 2 OFF");
            AAC2.GetComponent<GaugeScript>().Active = true;
            AR2.GetComponent<GaugeScript>().Active = true;
            AAC2.GetComponent<GaugeScript>().Forward = false;
            AR2.GetComponent<GaugeScript>().Forward = false;
            AR2.GetComponent<GaugeScript>().Value = 0;
            AAC2.GetComponent<GaugeScript>().Value = 0;
            AC2 = false;
        }
    }
}
