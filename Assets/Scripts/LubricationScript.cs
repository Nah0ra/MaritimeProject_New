using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LubricationScript : MonoBehaviour
{
    private GameManager _gameManager;

    private PhotonView photonView;
    public PowerPlantScript _powerPlantScript;
    private bool shoreOn;
    [SerializeField]
    public Button LoHeater;
    public Slider MeLoSlider;
    public Slider DgLoSlider;
    public bool DgLoSliderOn;
    public bool check;
    public bool LoHeaterCheck;
    public bool gaugeFullMe;
    public bool gaugeFullDg;
    [SerializeField]
    public GameObject LO;

    public GameObject MELoPump1;
    public GameObject Turbocharger;

    public bool MeLoPump1B;
    public bool TurbochargerB;
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
        }
    }

    public void changeColour()
    {
        photonView.RPC("RPCchangeColour", RpcTarget.All);
    }

    [PunRPC]
    public void RPCchangeColour()
    {
        if(shoreOn)
        {
            LoHeaterCheck = !LoHeaterCheck;
            if (LoHeaterCheck)
            {
                LoHeater.GetComponent<Image>().color = Color.green;
                LO.GetComponent<GaugeScript>().Active = true;
                LO.GetComponent<GaugeScript>().Forward = true;
            }
            else
            {
                LoHeater.GetComponent<Image>().color = Color.red;
                LO.GetComponent<GaugeScript>().Active = true;
                LO.GetComponent<GaugeScript>().Forward = false;
                LO.GetComponent<GaugeScript>().Value = 0;
            }
        }
        
    }
    private void CheckPump()
    {
        if (_powerPlantScript.DG1 || _powerPlantScript.DG2 || _powerPlantScript.DG3)
        {
            if (MeLoPump1B && TurbochargerB)
            {
                MELoPump1.GetComponent<GaugeScript>().Active = true;
                MELoPump1.GetComponent<GaugeScript>().Forward = true;
                Turbocharger.GetComponent<GaugeScript>().Active = true;
                Turbocharger.GetComponent<GaugeScript>().Forward = true;
            }
            else if (!MeLoPump1B && !TurbochargerB)
            {
                Debug.Log(MeLoPump1B);
                Debug.Log(Turbocharger);
                Turbocharger.GetComponent<GaugeScript>().Active = true;
                MELoPump1.GetComponent<GaugeScript>().Active = true;
                MELoPump1.GetComponent<GaugeScript>().Forward = false;
                Turbocharger.GetComponent<GaugeScript>().Forward = false;
            }
        }
    }

    public void MELoPump1On()
    {
        photonView.RPC("RPCMELoPump1On", RpcTarget.All);
    }

    [PunRPC]
    public void RPCMELoPump1On()
    {
        MeLoPump1B = true;
        CheckPump();
    }

    public void MELoPump1Off()
    {
        photonView.RPC("RPCMELoPump1Off", RpcTarget.All);
    }

    [PunRPC]
    public void RPCMELoPump1Off()
    {
        MeLoPump1B = false;
        CheckPump();
    }

    public void TurbochargerOn()
    {
        photonView.RPC("RPCTurbochargerOn", RpcTarget.All);
    }

    [PunRPC]
    public void RPCTurbochargerOn()
    {
        TurbochargerB = true;
        CheckPump();
    }


    public void TurbochargerOff()
    {
        photonView.RPC("RPCTurbochargerOff", RpcTarget.All);
    }

    [PunRPC]
    public void RPCTurbochargerOff()
    {
        TurbochargerB = false;
        CheckPump();
    }

    public void MeLoIntakeButtonPressOn()
    {
        photonView.RPC("RPCMeLoIntakeButtonPressOn",RpcTarget.All);
    }

    [PunRPC]
    public void RPCMeLoIntakeButtonPressOn()
    {
        if (shoreOn)
        {
            check = true;
            //Debug.Log("Lub Tank ON");
            StopCoroutine(MeLoTankOff());
            StartCoroutine(MeLoTankOn());
            DgLoSliderOn = true;
        }
    }

    public void MeLoIntakeButtonPressOff()
    {
        photonView.RPC("RPCMeLoIntakeButtonPressOff", RpcTarget.All);
    }

    [PunRPC]
    public void RPCMeLoIntakeButtonPressOff()
    {
        if (shoreOn)
        {
            check = false;
            //Debug.Log("Lub Tank OFF");
            StopCoroutine(MeLoTankOn());
            StartCoroutine(MeLoTankOff());
            DgLoSliderOn = false;
        }
    }

    public void DgLoButtonPressOn()
    {
        photonView.RPC("RPCDgLoButtonPressOn", RpcTarget.All);
    }

    [PunRPC]
    public void RPCDgLoButtonPressOn()
    {
        if (shoreOn)
        {
            check = true;
            //Debug.Log("DgLo Tank ON");
            StopCoroutine(DgLoTankOff());
            StartCoroutine(DgLoTankOn());
            DgLoSliderOn=true;
        }
    }

    public void DgLoButtonPressOff()
    {
        photonView.RPC("RPCDgLoButtonPressOff", RpcTarget.All);
    }

    [PunRPC]
    public void RPCDgLoButtonPressOff()
    {
        if (shoreOn)
        {
            check= false;
            //Debug.Log("Lub Tank OFF");
            StopCoroutine(DgLoTankOn());
            StartCoroutine(DgLoTankOff());
            DgLoSliderOn=false;
        }
    }

    public IEnumerator MeLoTankOn()
    {
        while (check)
        {
            MeLoSlider.value+=0.1f;
            yield return new WaitForSeconds(1f);
            if(MeLoSlider.value == MeLoSlider.maxValue)
            {
                check = false;
                gaugeFullMe = true;
            }
        }
    }    
    public IEnumerator DgLoTankOn()
    {
        while (check)
        {
            DgLoSlider.value+=0.1f;
            yield return new WaitForSeconds(1f);
            if (DgLoSlider.value == DgLoSlider.maxValue)
            {
                check = false;
                gaugeFullDg = true;
            }
        }
    }    
    public IEnumerator MeLoTankOff()
    {
        while (check == false)
        {
            MeLoSlider.value -= 0.1f;
            yield return new WaitForSeconds(1f);
            if (MeLoSlider.value == MeLoSlider.minValue)
            {
                check = true;
                gaugeFullMe = false;
            }
        }
    }    
    public IEnumerator DgLoTankOff()
    {
        while (check == false)
        {
            DgLoSlider.value -= 0.1f;
            yield return new WaitForSeconds(1f);
            if (DgLoSlider.value == DgLoSlider.minValue)
            {
                check = true;
                gaugeFullDg = false;
            }
        }
    }
}
