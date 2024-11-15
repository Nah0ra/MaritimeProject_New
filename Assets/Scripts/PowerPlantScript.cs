using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PowerPlantScript : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager _gameManager;

    private PhotonView photonView;
    private LubricationScript _lubricationScript;
    private CoolingScript _coolingScript;
    private CompressedAirScript _compressedAirScript;
    private MiscScript _miscScript;
    private bool shoreOn;
    public Button Dg1;
    public Button Dg2;
    public Button Dg3;

    public bool generator;
    public bool DG1;
    public bool DG2;
    public bool DG3;
    public bool Reset;
    
    [SerializeField] private GameObject DG1_Dial;
    [SerializeField] private GameObject DG2_Dial;
    [SerializeField] private GameObject DG3_Dial;

    //Alarms 
    [SerializeField] private Alarms _PPAlarm;
    [SerializeField] private Alarms _CoolingAlarm;
    [SerializeField] private Alarms _LubAlarm;
    [SerializeField] private Alarms _CAAlarm;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _lubricationScript=GameObject.Find("LubricationManager").GetComponent<LubricationScript>();
        _coolingScript= GameObject.Find("CoolingManager").GetComponent<CoolingScript>();
        _compressedAirScript = GameObject.Find("CompressedAirManager").GetComponent<CompressedAirScript>();
        _miscScript= GameObject.Find("MiscManager").GetComponent<MiscScript>();
        Dg1.interactable = false;
        Dg2.interactable = false;
        Dg3.interactable = false;
        photonView = gameObject.GetComponent<PhotonView>();
        _PPAlarm = _PPAlarm.GetComponent<Alarms>();
        _CoolingAlarm = _CoolingAlarm.GetComponent<Alarms>();
        _LubAlarm = _LubAlarm.GetComponent<Alarms>();
        _CAAlarm = _CAAlarm.GetComponent<Alarms>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameManager.AllClients) {
            if ((_lubricationScript.gaugeFullMe && _lubricationScript.gaugeFullDg && _gameManager.shore && _lubricationScript.LoHeaterCheck && _coolingScript.SWpumpOn && _compressedAirScript.AC1 && _compressedAirScript.AC2))
            {
                Dg1.interactable = true;
                Dg2.interactable = true;
                Dg3.interactable = true;

                generator = true;
                //Debug.Log("Power On");
            }
            else if ((!_lubricationScript.gaugeFullMe || !_lubricationScript.gaugeFullDg || !_gameManager.shore || !_lubricationScript.LoHeaterCheck || !_coolingScript.SWpumpOn || !_compressedAirScript.AC1 || !_compressedAirScript.AC2))
            {
                Dg1.interactable = false;
                Dg2.interactable = false;
                Dg3.interactable = false;

                generator = false;
                changeColourRed();
            }

            if (!generator)
            {
                DG1 = false;
                DG1_Dial.GetComponent<GaugeScript>().Active = true;
                DG1_Dial.GetComponent<GaugeScript>().Forward = false;
                DG1_Dial.GetComponent<GaugeScript>().Value = 0;

                DG2 = false;
                DG2_Dial.GetComponent<GaugeScript>().Active = true;
                DG2_Dial.GetComponent<GaugeScript>().Forward = false;
                DG2_Dial.GetComponent<GaugeScript>().Value = 0;

                DG3 = false;
                DG3_Dial.GetComponent<GaugeScript>().Active = true;
                DG3_Dial.GetComponent<GaugeScript>().Forward = false;
                DG3_Dial.GetComponent<GaugeScript>().Value = 0;
            }

            if (!generator && !DG1 && !DG2 && !DG3 && !_gameManager.shore)
            {
                //Lubrication
                _lubricationScript.LO.GetComponent<GaugeScript>().Forward = false;
                _lubricationScript.LoHeater.GetComponent<Image>().color = Color.red;
                _lubricationScript.check = false;

                _lubricationScript.MeLoIntakeButtonPressOff();
                _lubricationScript.DgLoButtonPressOff();


                //Cooling
                _coolingScript.SWpump1Off();
                _coolingScript.SWpump2Off();
                _coolingScript.DGFWpump1Off();
                _coolingScript.DGFWpump2Off();


                //Compressed Air
                _compressedAirScript.onAir2ButtonPressOff();
                _compressedAirScript.onAir1ButtonPressOff();


                //Alarms
                _CoolingAlarm.alarm = true;
                _LubAlarm.alarm = true;
                _CAAlarm.alarm = true;
                _PPAlarm.alarm = true;

                //GameManager
                _gameManager.ShorePower.SetActive(true);
            }
            else if (generator || DG1 || DG2 || DG3 || _gameManager.shore)
            {
                _CoolingAlarm.alarm = false;
                _LubAlarm.alarm = false;
                _CAAlarm.alarm = false;
                _PPAlarm.alarm = false;

            }

            if (!_gameManager.shore)
            {
                _gameManager.shore = false;
                _gameManager.ShoreButton.GetComponent<Image>().color = Color.red;
                //Debug.Log("Shore off");
            }
        }
    }


    public void changeColourGreen(string buttonName)
    {
        photonView.RPC("RPCchangeColourGreen", RpcTarget.All, buttonName);
    }   


    [PunRPC]
    public void RPCchangeColourGreen(string buttonName)
    {
        switch (buttonName)
        {
            case "Button1":
                Dg1.GetComponent<Image>().color = Color.green;
                break;

            case "Button2":
                Dg2.GetComponent<Image>().color = Color.green;
                break;

            case "Button3":      
                Dg3.GetComponent<Image>().color = Color.green;
                break;
        }
    }

    public void changeColourRed()
    {
       photonView.RPC("RPCchangeColourRed", RpcTarget.All);  
    }


    [PunRPC]
    public void RPCchangeColourRed()
    {
       Dg1.GetComponent<Image>().color = Color.red;
       Dg2.GetComponent<Image>().color = Color.red;
       Dg3.GetComponent<Image>().color = Color.red;     
    }
    
    public void onDG1BTNOn()
    {
        photonView.RPC("RPConDG1BTNOn", RpcTarget.All);
    }



    [PunRPC]
    public void RPConDG1BTNOn()
    {
        if (generator)
        {
            DG1 = true;
            DG1_Dial.GetComponent<GaugeScript>().Active = true;
            DG1_Dial.GetComponent<GaugeScript>().Forward = true;
            _gameManager.ShorePower.SetActive(false);

        }
    }
    

    public void onDG1BTNOff()
    {
        photonView.RPC("RPConDG1BTNOff", RpcTarget.All);
    }

    [PunRPC]
    public void RPConDG1BTNOff()
    {
        if (generator)
        {
            DG1 = false;
            DG1_Dial.GetComponent<GaugeScript>().Active = true;
            DG1_Dial.GetComponent<GaugeScript>().Forward = false;
            DG1_Dial.GetComponent<GaugeScript>().Value = 0;
            _gameManager.shore = false;
        }
    }
    
    public void onDG2BTNOn()
    {
        photonView.RPC("RPConDG2BTNOn",RpcTarget.All);
    }

    [PunRPC]
    public void RPConDG2BTNOn()
    {
        if (generator)
        {
            DG2 = true;
            DG2_Dial.GetComponent<GaugeScript>().Active = true;
            DG2_Dial.GetComponent<GaugeScript>().Forward = true;
            _gameManager.ShorePower.SetActive(false);
        }
    }
    
    public void onDG2BTNOff()
    {
       photonView.RPC("RPConDG2BTNOff", RpcTarget.All);
    }

    [PunRPC]
    public void RPConDG2BTNOff()
    {
        if (generator)
        {
            DG2 = false;
            DG2_Dial.GetComponent<GaugeScript>().Active = true;
            DG2_Dial.GetComponent<GaugeScript>().Forward = false;
            DG2_Dial.GetComponent<GaugeScript>().Value = 0;
            _gameManager.shore = false;
        }
    }
    
    public void onDG3BTNOn()
    {
        photonView.RPC("RPConDG3BTNOn", RpcTarget.All);
    }

    [PunRPC]
    public void RPConDG3BTNOn()
    {
        if (generator)
        {
            DG3 = true;
            DG3_Dial.GetComponent<GaugeScript>().Active = true;
            DG3_Dial.GetComponent<GaugeScript>().Forward = true;
            _gameManager.ShorePower.SetActive(false);
        }
    }
    
    public void onDG3BTNOff()
    {
        photonView.RPC("RPConDG3BTNOff", RpcTarget.All);
    }

    [PunRPC]
    public void RPConDG3BTNOff()
    {
        if (generator)
        {
            DG3 = false;
            DG3_Dial.GetComponent<GaugeScript>().Active = true;
            DG3_Dial.GetComponent<GaugeScript>().Forward = false;
            DG3_Dial.GetComponent<GaugeScript>().Value = 0;
            _gameManager.shore = false;
        }
    }
}
