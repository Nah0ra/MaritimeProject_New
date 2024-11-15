using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;

public class MiscScript : MonoBehaviour
{

    public bool SlowTurning;
    public bool MEPreLubPump;
    public bool IndicatorCocks;
    public bool ask1;
    public bool ask2;
    public bool ask3;
    public bool start;
    public PowerPlantScript powerPlant;
    public GameObject overlay;
    public PhotonView photonview;


    public void Start()
    {
        start = true;
        photonview = GetComponent<PhotonView>();
    }
    public void Stop()
    {
        photonview.RPC("RPCStop", RpcTarget.All);
    }

    [PunRPC]
    public void RPCStop()
    {
        start = true;
    }

    public void Ask1On()
    {
        photonview.RPC("RPCAsk1On", RpcTarget.All);
    }

    [PunRPC]
    public void RPCAsk1On()
    {
        ask1 = true;
    }

    public void Ask2On()
    {
        photonview.RPC("RPCAsk2On", RpcTarget.All);
    }

    [PunRPC]
    public void RPCAsk2On()
    {
        if (IndicatorCocks && MEPreLubPump && SlowTurning && start && (powerPlant.DG1 || powerPlant.DG2 || powerPlant.DG3)) 
        {
            overlay.SetActive(true);
            overlay.transform.GetChild(0).gameObject.transform.GetComponent<TextMeshProUGUI>().text ="Slow turning was successfull ship is ready to leave port";
        }

    }
    public void Ask3On()
    {
        photonview.RPC("RPCAsk3On", RpcTarget.All);
    }

    [PunRPC]
    public void RPCAsk3On()
    {
        ask1 = true;
    }

    public void SlowTurningOn()
    {
        photonview.RPC("RPCSlowTurningOn", RpcTarget.All);
    }

    [PunRPC]
    public void RPCSlowTurningOn()
    {
        SlowTurning = true;
    }

    public void SlowTurningOff()
    {
        photonview.RPC("RPCSlowTurningOff", RpcTarget.All);
    }

    [PunRPC]
    public void RPCSlowTurningOff()
    {
        SlowTurning = false;
    }


    public void MEPreLubPumpOn()
    {
        photonview.RPC("RPCMEPreLubPumpOn", RpcTarget.All);
    }

    [PunRPC]
    public void RPCMEPreLubPumpOn()
    {
        MEPreLubPump = true;
    }


    public void MEPreLubPumpOff()
    {
        photonview.RPC("RPCMEPreLubPumpOff", RpcTarget.All);
    }

    [PunRPC]
    public void RPCMEPreLubPumpOff()
    {
        MEPreLubPump = false;
    }

    public void IndicatorCocksOn()
    {
        photonview.RPC("RPCIndicatorCocksOn", RpcTarget.All);
    }

    [PunRPC]
    public void RPCIndicatorCocksOn()
    {
        IndicatorCocks = true;
    }

    public void IndicatorCocksOff()
    {
        photonview.RPC("RPCIndicatorCocksOff", RpcTarget.All);
    }

    [PunRPC]
    public void RPCIndicatorCocksOff()
    {
        IndicatorCocks = false;
    }
}