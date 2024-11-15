using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using ExitGames.Client.Photon.StructWrapping;


public class GameManager : MonoBehaviourPunCallbacks
{
    //Panels
    private GameObject MainOBJ;
    private GameObject FuelOBJ;
    private GameObject LubeOBJ;
    private GameObject CompOBJ;
    private GameObject PowerOBJ;
    private GameObject SteamOBJ;
    private GameObject CoolOBJ;
    private GameObject MiscOBJ;
    private GameObject SaveGUI_OBJ;

    public GameObject Overlay;

    public GameObject ShorePower;

    //UI
    private GameObject MainUI;
    private GameObject FuelUI;
    private GameObject LubeUI;
    private GameObject CompUI;
    private GameObject PowerUI;
    private GameObject SteamUI;
    private GameObject CoolUI;
    private GameObject MiscUI;
    private GameObject SaveGUI;

    //Dials
    public GameObject MainDials;
    private GameObject FuelDials;
    private GameObject LubeDials;
    private GameObject LubeTanks;
    private GameObject CompDials;
    private GameObject PowerDials;
    private GameObject SteamDials;
    private GameObject CoolDials;
    private GameObject MiscDials;


    //Buttons
    private Button MainButton;
    private Button FuelButton;
    private Button LubeButton;
    private Button CompButton;
    private Button PowerButton;
    private Button SteamButton;
    private Button CoolButton;
    private Button MiscButton;
    private Button SaveGUIButton;
    public bool AllClients;



    // private Button SaveButton;
    // private Button LoadButton;

    // private InputField saveInputField;

    GameObject[] dials;

    public Button ShoreButton;
    public bool shore = false;
    PhotonView photonView;

    private GameObject SpeedDial;

    private void Awake()
    {
        Initialise();
        dials = GameObject.FindGameObjectsWithTag("Dial");
        photonView = PhotonView.Get(this);
        ShorePower.SetActive(false);
        ShoreButton.GetComponent<Image>().color = Color.red;
        SpeedDial = GameObject.Find("45");

        Connect();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected!");
        PhotonNetwork.JoinOrCreateRoom("Default", roomOptions:default, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        StartCoroutine(WaitForClients());
    }

    private IEnumerator WaitForClients()
    {
        while (PhotonNetwork.PlayerList.Length != 2)
        {
            AllClients = false;
            MainDials.SetActive(false);
            SpeedDial.SetActive(false);
            Overlay.SetActive(true);
            Debug.Log("Waiting for clients");
            yield return new WaitForSeconds(1f);
        }
        AllClients = true;
        SpeedDial.SetActive(true);
        MainDials.SetActive(true);
        Overlay.SetActive(false);
    }

    


    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }


    public void onButtonPress()
    {
        photonView.RPC("RPConButtonPress",RpcTarget.All);
    }

    [PunRPC]
    public void RPConButtonPress()
    {
        shore = !shore;

        if (shore)
        {
            shore = true;
            ShoreButton.GetComponent<Image>().color = Color.green;
            //Debug.Log("Shore on " + shore);
        }
        else
        { 
            shore = false;
            ShoreButton.GetComponent<Image>().color = Color.red;
            //Debug.Log("Shore off");
        }
    }

    private void LoadPanel()
    {
        //Determine the tag of the currently selected button
        string currentTag = EventSystem.current.currentSelectedGameObject.tag;
        switch (currentTag)
        {
            case "MainButton":
                MainUI.SetActive(true);
                FuelUI.SetActive(false);
                LubeUI.SetActive(false);
                LubeTanks.SetActive(false);
                CoolUI.SetActive(false);
                CompUI.SetActive(false);
                PowerUI.SetActive(false);
                SteamUI.SetActive(false);
                MiscUI.SetActive(false);
                SaveGUI.SetActive(false);
                ShorePower.SetActive(false);

                foreach (Transform child in MainDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = false;

                foreach (Transform child in FuelDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in LubeDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in CoolDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in CompDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in PowerDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in SteamDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                break;

            case "FuelButton":
                MainUI.SetActive(false);
                FuelUI.SetActive(true);
                LubeUI.SetActive(false);
                LubeTanks.SetActive(false);
                CoolUI.SetActive(false);
                CompUI.SetActive(false);
                PowerUI.SetActive(false);
                SteamUI.SetActive(false);
                MiscUI.SetActive(false);
                SaveGUI.SetActive(false);
                ShorePower.SetActive(false);

                foreach (Transform child in MainDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in FuelDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = false;

                foreach (Transform child in LubeDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in CoolDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in CompDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in PowerDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in SteamDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                break;

            case "LubeButton":
                MainUI.SetActive(false);
                FuelUI.SetActive(false);
                LubeUI.SetActive(true);
                LubeTanks.SetActive(false);
                CoolUI.SetActive(false);
                CompUI.SetActive(false);
                PowerUI.SetActive(false);
                SteamUI.SetActive(false);
                MiscUI.SetActive(false);
                SaveGUI.SetActive(false);
                ShorePower.SetActive(false);

                LubeUI.transform.GetChild(0).gameObject.SetActive(true);

                foreach (Transform child in MainDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in FuelDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in LubeDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = false;

                foreach (Transform child in CoolDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in CompDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in PowerDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in SteamDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                break;


            case "CoolButton":
                MainUI.SetActive(false);
                FuelUI.SetActive(false);
                LubeUI.SetActive(false);
                LubeTanks.SetActive(false);
                CoolUI.SetActive(true);
                CompUI.SetActive(false);
                PowerUI.SetActive(false);
                SteamUI.SetActive(false);
                MiscUI.SetActive(false);
                SaveGUI.SetActive(false);
                ShorePower.SetActive(false);

                foreach (Transform child in MainDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in FuelDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in LubeDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in CoolDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = false;

                foreach (Transform child in CompDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in PowerDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in SteamDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                break;

            case "CompButton":
                MainUI.SetActive(false);
                FuelUI.SetActive(false);
                LubeUI.SetActive(false);
                LubeTanks.SetActive(false);
                CoolUI.SetActive(false);
                CompUI.SetActive(true);
                PowerUI.SetActive(false);
                SteamUI.SetActive(false);
                MiscUI.SetActive(false);
                SaveGUI.SetActive(false);
                ShorePower.SetActive(false);

                foreach (Transform child in MainDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in FuelDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in LubeDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in CoolDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in CompDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = false;

                foreach (Transform child in PowerDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in SteamDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                break;

            case "PowerButton":
                MainUI.SetActive(false);
                FuelUI.SetActive(false);
                LubeUI.SetActive(false);
                LubeTanks.SetActive(false);
                CoolUI.SetActive(false);
                CompUI.SetActive(false);
                PowerUI.SetActive(true);
                SteamUI.SetActive(false);
                MiscUI.SetActive(false);
                SaveGUI.SetActive(false);
                ShorePower.SetActive(true);

                foreach (Transform child in MainDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in FuelDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in LubeDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in CoolDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in CompDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in PowerDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = false;

                foreach (Transform child in SteamDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                break;

            case "SteamButton":
                MainUI.SetActive(false);
                FuelUI.SetActive(false);
                LubeUI.SetActive(false);
                LubeTanks.SetActive(false);
                CoolUI.SetActive(false);
                CompUI.SetActive(false);
                PowerUI.SetActive(false);
                SteamUI.SetActive(true);
                MiscUI.SetActive(false);
                SaveGUI.SetActive(false);
                ShorePower.SetActive(false);

                foreach (Transform child in MainDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in FuelDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in LubeDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in CoolDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in CompDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in PowerDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in SteamDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = false;

                break;

            case "MiscButton":
                MainUI.SetActive(false);
                FuelUI.SetActive(false);
                LubeUI.SetActive(false);
                LubeTanks.SetActive(false);
                CoolUI.SetActive(false);
                CompUI.SetActive(false);
                PowerUI.SetActive(false);
                SteamUI.SetActive(false);
                MiscUI.SetActive(true);
                SaveGUI.SetActive(false);
                ShorePower.SetActive(false);

                foreach (Transform child in MainDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in FuelDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in LubeDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in CoolDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in CompDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in PowerDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in SteamDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                break;
            case "SaveGUIButton":
                MainUI.SetActive(false);
                FuelUI.SetActive(false);
                LubeUI.SetActive(false);
                LubeTanks.SetActive(false);
                CoolUI.SetActive(false);
                CompUI.SetActive(false);
                PowerUI.SetActive(false);
                SteamUI.SetActive(false);
                MiscUI.SetActive(false);
                SaveGUI.SetActive(true);
                ShorePower.SetActive(false);

                GameObject.FindGameObjectWithTag("SaveButton").GetComponent<Button>().onClick.AddListener(CheckInputField);
                GameObject.FindGameObjectWithTag("LoadButton").GetComponent<Button>().onClick.AddListener(CheckInputField);


                foreach (Transform child in MainDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in FuelDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in LubeDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in CoolDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in CompDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in PowerDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                foreach (Transform child in SteamDials.transform)
                    child.GetComponent<SimpleGaugeMaker>().Hide = true;

                break;

            default:
                //Debug.Log("Button Unassigned");
                break;
        }



    }

    //Initialise the scene and load the appropriate dials
    private void Initialise()
    {
        //Assigning panels
        MainOBJ = GameObject.Find("Main_OBJ");
        FuelOBJ = GameObject.Find("Fuel_OBJ");
        LubeOBJ = GameObject.Find("Lubricating_OBJ");
        LubeTanks = GameObject.Find("Lubricating_Tanks_OBJ");
        CoolOBJ = GameObject.Find("Cooling_OBJ");
        CompOBJ = GameObject.Find("Compressed_OBJ");
        PowerOBJ = GameObject.Find("Power_Plant_OBJ");
        SteamOBJ = GameObject.Find("Steam_OBJ");
        MiscOBJ = GameObject.Find("Miscellaneous_OBJ");
        SaveGUI_OBJ = GameObject.Find("SaveGUI_OBJ");
        ShorePower = GameObject.Find("Shore Power");

        //Assigning UI
        MainUI = MainOBJ.transform.GetChild(0).gameObject;
        FuelUI = FuelOBJ.transform.GetChild(0).gameObject;
        LubeUI = LubeOBJ.transform.GetChild(0).gameObject;
        CoolUI = CoolOBJ.transform.GetChild(0).gameObject;
        CompUI = CompOBJ.transform.GetChild(0).gameObject;
        PowerUI = PowerOBJ.transform.GetChild(0).gameObject;
        SteamUI = SteamOBJ.transform.GetChild(0).gameObject;
        MiscUI = MiscOBJ.transform.GetChild(0).gameObject;
        SaveGUI = SaveGUI_OBJ.transform.GetChild(0).gameObject;
        FuelUI.SetActive(false);
        LubeUI.SetActive(false);
        LubeTanks.SetActive(false);
        CoolUI.SetActive(false);
        CompUI.SetActive(false);
        PowerUI.SetActive(false);
        SteamUI.SetActive(false);
        MiscUI.SetActive(false);
        SaveGUI.SetActive(false);

        //Assigning Dials
        MainDials = MainOBJ.transform.GetChild(1).gameObject;
        FuelDials = FuelOBJ.transform.GetChild(1).gameObject;
        LubeDials = LubeOBJ.transform.GetChild(1).gameObject;
        CoolDials = CoolOBJ.transform.GetChild(1).gameObject;
        CompDials = CompOBJ.transform.GetChild(1).gameObject;
        PowerDials = PowerOBJ.transform.GetChild(1).gameObject;
        SteamDials = SteamOBJ.transform.GetChild(1).gameObject;
        MiscDials = MiscOBJ.transform.GetChild(0).gameObject;

        foreach (Transform child in FuelDials.transform)
            child.GetComponent<SimpleGaugeMaker>().Hide = true;

        foreach (Transform child in LubeDials.transform)
                child.GetComponent<SimpleGaugeMaker>().Hide = true;

        foreach (Transform child in CoolDials.transform)
            child.GetComponent<SimpleGaugeMaker>().Hide = true;

        foreach (Transform child in CompDials.transform)
            child.GetComponent<SimpleGaugeMaker>().Hide = true;

        foreach (Transform child in PowerDials.transform)
            child.GetComponent<SimpleGaugeMaker>().Hide = true;

        foreach (Transform child in SteamDials.transform)
            child.GetComponent<SimpleGaugeMaker>().Hide = true;

        //Assinging Buttons
        MainButton = GameObject.FindGameObjectWithTag("MainButton").GetComponent<Button>();
        FuelButton = GameObject.FindGameObjectWithTag("FuelButton").GetComponent<Button>();
        LubeButton = GameObject.FindGameObjectWithTag("LubeButton").GetComponent<Button>();
        CompButton = GameObject.FindGameObjectWithTag("CompButton").GetComponent<Button>();
        PowerButton = GameObject.FindGameObjectWithTag("PowerButton").GetComponent<Button>();
        SteamButton = GameObject.FindGameObjectWithTag("SteamButton").GetComponent<Button>();
        CoolButton = GameObject.FindGameObjectWithTag("CoolButton").GetComponent<Button>();
        MiscButton = GameObject.FindGameObjectWithTag("MiscButton").GetComponent<Button>();
        SaveGUIButton = GameObject.FindGameObjectWithTag("SaveGUIButton").GetComponent<Button>();
        // SaveButton = GameObject.FindGameObjectWithTag("SaveButton").GetComponent<Button>();
        // LoadButton = GameObject.FindGameObjectWithTag("LoadButton").GetComponent<Button>();

        //Adding event listeners to buttons
        MainButton.onClick.AddListener(LoadPanel);
        FuelButton.onClick.AddListener(LoadPanel);
        LubeButton.onClick.AddListener(LoadPanel);
        CompButton.onClick.AddListener(LoadPanel);
        PowerButton.onClick.AddListener(LoadPanel);
        SteamButton.onClick.AddListener(LoadPanel);
        CoolButton.onClick.AddListener(LoadPanel);
        MiscButton.onClick.AddListener(LoadPanel);
        SaveGUIButton.onClick.AddListener(LoadPanel);
        // SaveButton.onClick.AddListener(CheckInputField);
        // LoadButton.onClick.AddListener(CheckInputField);
    }

    private void CheckInputField()
    {
        // InputField saveInputField = GameObject.FindGameObjectWithTag("InputField").GetComponent<InputField>().text;
        string inputValue = GameObject.FindGameObjectWithTag("InputField").GetComponent<TMP_InputField>().text;
        string currentTag = EventSystem.current.currentSelectedGameObject.tag;

        if (string.IsNullOrEmpty(inputValue))
        {
            //Debug.Log("Error: Input field cannot be empty!");
            // errorMessageText.text = "Error: Input field cannot be empty!";
        }
        else
        {
            // errorMessageText.text = "";
            //Debug.Log("InputField Value: " + inputValue);
            switch (currentTag)
            {
                case "SaveButton":
                    break;
                case "LoadButton":
                    break;
            }
        }
    }


    public void EnableLubeTanks()
    {
        photonView.RPC("RPCEnableLubeTanks", RpcTarget.All);
    }

    [PunRPC]
    public void RPCEnableLubeTanks()
    {
        if (!LubeTanks.activeSelf)
        {
            LubeTanks.SetActive(true);
            LubeUI.transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("30").GetComponent<SimpleGaugeMaker>().Hide = true;
            GameObject.Find("32").GetComponent<SimpleGaugeMaker>().Hide = true;
            GameObject.Find("33").GetComponent<SimpleGaugeMaker>().Hide = true;
            GameObject.Find("35").GetComponent<SimpleGaugeMaker>().Hide = true;
            GameObject.Find("36").GetComponent<SimpleGaugeMaker>().Hide = true;
        }
        else
        {   
            LubeTanks.SetActive(false);
            LubeUI.transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("30").GetComponent<SimpleGaugeMaker>().Hide = false;
            GameObject.Find("32").GetComponent<SimpleGaugeMaker>().Hide = false;
            GameObject.Find("33").GetComponent<SimpleGaugeMaker>().Hide = false;
            GameObject.Find("35").GetComponent<SimpleGaugeMaker>().Hide = false;
            GameObject.Find("36").GetComponent<SimpleGaugeMaker>().Hide = false;
        }
    }
}
