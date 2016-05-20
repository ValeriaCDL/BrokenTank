using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CustomHUD : MonoBehaviour {

	private NetworkManager manager;

	public UnityEngine.UI.InputField input_field;

	public Canvas hud;
	private Transform[] canvasChildren;

	private int offsetX;
    private int offsetY;

	void Awake()
	{
		manager = GetComponent<NetworkManager>();
	}

	void Start(){
		canvasChildren = hud.GetComponentsInChildren<Transform> ();
	}
		

	public void StartHost(){
		if (!NetworkClient.active && !NetworkServer.active && manager.matchMaker == null)
		{
				manager.StartHost(); //host match
				Debug.Log( "Server: port=" + manager.networkPort);
				ShowExit ();
		}
	}

	public void StartClient(){

		if (input_field.text == null)
			return;

		if (!NetworkClient.active && !NetworkServer.active && manager.matchMaker == null)
		{
			manager.networkAddress = input_field.text;
			manager.StartClient(); //join match
			Debug.Log( "Client: address=" + manager.networkAddress + " port=" + manager.networkPort);
			ShowExit ();
		}
	}

	public void ExitGame(){
		
		if (NetworkServer.active) {
			//heyyyy wait.. ur gonna.. ummm...kill the game
			manager.StopHost ();
			Debug.Log ("stop host");
			ShowMenu ();

		} else if (NetworkClient.active) {
			manager.StopClient ();
			Debug.Log ("stop client");
			ShowMenu ();

		} else
			Debug.Log ("nobody active");

	}


	private void ShowMenu(){
		Debug.Log ("Show menu");
		foreach(Transform child in canvasChildren){
			if(child.gameObject.tag == "Menu"){
				child.gameObject.SetActive (true);
			}
		}
	}

	private void ShowExit(){
		Debug.Log ("Show exit");
		foreach(Transform child in canvasChildren){
			if(child.gameObject.tag == "Menu"){
				child.gameObject.SetActive (false);
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
			Debug.Log ("Hey3");
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
