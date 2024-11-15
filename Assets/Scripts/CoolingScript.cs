using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class CoolingScript : MonoBehaviour
{
    private GameManager _gameManager;
    public PowerPlantScript _powerPlantScript;

    private PhotonView photonView;

    private bool shoreOn;

    // buttons
    public bool SWpump1;
    public bool SWpump2;
    public bool SWpumpOn;

    public bool DGFWpump1;
    public bool DGFWpump2;
    public bool DGFWpumpOn;

    // gauges

    [SerializeField]
    private GameObject SWafterME;

    [SerializeField]
    private GameObject SWbeforeME1;
    [SerializeField]
    private GameObject SWbeforeME2;

    [SerializeField]
    private GameObject DGFW;

    [SerializeField]
    private GameObject FWbef1;

    [SerializeField]
    private GameObject FWbef2;


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
            if(_gameManager.shore)
            {
                shoreOn = true;
            }
        }
    }

    // if SWpump1 && SWpump1 == true, then SWafterME, SWbeforeME1, SWbeforeME2 gauges are active and forward


    public void seaWaterOn()
    {
        photonView.RPC("RPCseaWaterOn", RpcTarget.All);
    }

    [PunRPC]
    public void RPCseaWaterOn()
    {    
        if (shoreOn)
        {
            if (SWpump1 == true && SWpump2 == true)
            {
                //Debug.Log("Sea Water On");
                SWafterME.GetComponent<GaugeScript>().Active = true;
                SWafterME.GetComponent<GaugeScript>().Forward = true;

                SWbeforeME1.GetComponent<GaugeScript>().Active = true;
                SWbeforeME1.GetComponent<GaugeScript>().Forward = true;

                SWbeforeME2.GetComponent<GaugeScript>().Active = true;
                SWbeforeME2.GetComponent<GaugeScript>().Forward = true;
                SWpumpOn = true;
            }

        }

        if ((SWpump1 == false && SWpump2 == false))
        {

            //Debug.Log("Sea Water Off");
            SWafterME.GetComponent<GaugeScript>().Active = true;
            SWafterME.GetComponent<GaugeScript>().Forward = false;

            SWbeforeME1.GetComponent<GaugeScript>().Active = true;
            SWbeforeME1.GetComponent<GaugeScript>().Forward = false;

            SWbeforeME2.GetComponent<GaugeScript>().Active = true;
            SWbeforeME2.GetComponent<GaugeScript>().Forward = false;
            SWpumpOn = false;
        }

        if (_powerPlantScript.DG1 || _powerPlantScript.DG2 || _powerPlantScript.DG3)
        {
            if (DGFWpump1 == true && DGFWpump2 == true)
            {
                //Debug.Log("DGFW on");
                DGFW.GetComponent<GaugeScript>().Active = true;
                DGFW.GetComponent<GaugeScript>().Forward = true;

                FWbef1.GetComponent<GaugeScript>().Active = true;
                FWbef1.GetComponent<GaugeScript>().Forward = true;

                FWbef2.GetComponent<GaugeScript>().Active = true;
                FWbef2.GetComponent<GaugeScript>().Forward = true;
            }

            if (DGFWpump1 == false && DGFWpump2 == false)
            {
                //Debug.Log("DGFW off");
                DGFW.GetComponent<GaugeScript>().Active = true;
                DGFW.GetComponent<GaugeScript>().Forward = false;

                FWbef1.GetComponent<GaugeScript>().Active = true;
                FWbef1.GetComponent<GaugeScript>().Forward = false;

                FWbef2.GetComponent<GaugeScript>().Active = true;
                FWbef2.GetComponent<GaugeScript>().Forward = false;
            }
        }
        
    }
    
    public void SWpump1On()
    {
        photonView.RPC("RPCSWpump1On", RpcTarget.All);
    }
    
    [PunRPC]
    public void RPCSWpump1On()
    {
        SWpump1 = true;
        seaWaterOn();
    }


    public void SWpump2On()
    {
       photonView.RPC("RPCSWpump2On", RpcTarget.All);
    }
    
    [PunRPC]
    public void RPCSWpump2On()
    {
        SWpump2 = true;
        seaWaterOn();
    }

    public void DGFWpump1On()
    {
        photonView.RPC("RPCDGFWpump1On", RpcTarget.All);
    }

    [PunRPC]
    public void RPCDGFWpump1On()
    {
        DGFWpump1 = true;
        seaWaterOn();
    }


    public void DGFWpump2On()
    {
        photonView.RPC("RPCDGFWpump2On", RpcTarget.All);
    }

    [PunRPC]
    public void RPCDGFWpump2On()
    {
        DGFWpump2 = true;
        seaWaterOn();
    }


    public void DGFWpump1Off()
    {
        photonView.RPC("RPCDGFWpump1Off", RpcTarget.All);
    }

    [PunRPC]
    public void RPCDGFWpump1Off()
    {
        DGFWpump1 = false;
        seaWaterOn();
    }

    public void DGFWpump2Off()
    {
        photonView.RPC("RPCDGFWpump2Off", RpcTarget.All);
    }

    [PunRPC]
    public void RPCDGFWpump2Off()
    {
        DGFWpump2 = false;
        seaWaterOn();
    }

    public void SWpump1Off()
    {
        photonView.RPC("RPCSWpump1Off", RpcTarget.All);
    }

    [PunRPC]
    public void RPCSWpump1Off()
    {
        SWpump1 = false;
        seaWaterOn();
    }

    public void SWpump2Off()
    {
        photonView.RPC("RPCSWpump2Off", RpcTarget.All);
    }

    [PunRPC]
    public void RPCSWpump2Off()
    {
        SWpump2 = false;
        seaWaterOn();
    }
}