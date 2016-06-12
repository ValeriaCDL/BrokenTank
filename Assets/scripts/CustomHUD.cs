using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;

public class CustomHUD : MonoBehaviour
{

    private NetworkManager manager;

    public UnityEngine.UI.InputField input_field;

    public Canvas hud;
    private Transform[] canvasChildren;
    public Text Ip;

    private int offsetX;
    private int offsetY;

    void Awake()
    {
        manager = GetComponent<NetworkManager>();
        Ip.text = string.Format("Give this ip if you're host:" + Environment.NewLine + "{0}" + Environment.NewLine, GetIP());
    }

    void Start()
    {
        canvasChildren = hud.GetComponentsInChildren<Transform>();
    }

    string GetIP()
    {
        var strHostName = "";
        strHostName = System.Net.Dns.GetHostName();

        var ipEntry = System.Net.Dns.GetHostEntry(strHostName);

        var addr = ipEntry.AddressList;

        return addr[addr.Length - 1].ToString();

    }
    public void StartHost()
    {
        if (!NetworkClient.active && !NetworkServer.active && manager.matchMaker == null)
        {
            manager.StartHost(); //host match
            Debug.Log("Server: port=" + manager.networkPort);
            ShowExit();
        }
    }

    public void StartClient()
    {

        if (input_field.text == null)
            return;

        if (!NetworkClient.active && !NetworkServer.active && manager.matchMaker == null)
        {
            manager.networkAddress = input_field.text;
            manager.StartClient(); //join match
            Debug.Log("Client: address=" + manager.networkAddress + " port=" + manager.networkPort);
            ShowExit();
        }
    }

    public void ExitGame()
    {
        manager.StopHost();
        Debug.Log("stop host");
        ShowMenu();
    }


    private void ShowMenu()
    {
        Debug.Log("Show menu");
        foreach (Transform child in canvasChildren)
        {
            if (child.gameObject.tag == "Menu")
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    private void ShowExit()
    {
        Debug.Log("Show exit");
        foreach (Transform child in canvasChildren)
        {
            if (child.gameObject.tag == "Menu")
            {
                child.gameObject.SetActive(false);
            }
        }
    }


    void OnGUI()
    {

        int xpos = 10 + offsetX;
        int ypos = 40 + offsetY;
        int spacing = 24;

        //Este lo dejo porque... psss para saber q esta pasando..pero no lo he visto
        if (NetworkClient.active && !ClientScene.ready)
        {
            Debug.Log("Hey3");
            if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Client Ready"))
            {
                ClientScene.Ready(manager.client.connection);

                if (ClientScene.localPlayers.Count == 0)
                {
                    ClientScene.AddPlayer(0);
                }
            }
            ypos += spacing;

        }

    }

}
