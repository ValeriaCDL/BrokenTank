using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameHandler : MonoBehaviour 
{
	public Canvas main;
	public Canvas log;
	public InputField val;
	public CommunicationHandler communicationHandler;
	bool connected;

	void Start() {
		main.enabled = true;
		connected = false;
		//communicationHandler = new CommunicationHandler ();
	}

	public void Login() {
		main.enabled = false;
	}

	public void Update() {
		if (communicationHandler.status == 100 && !connected) {
			Debug.Log ("Connection succesful");
			communicationHandler.retrieveList();
			connected = true;
		}
	}

	public void PlayLevel() {
		communicationHandler.createSession (val.text);

		if (communicationHandler.status == 200) {
			Debug.Log ("User name already used, can't create session");
		} else if (communicationHandler.status == 100) {
			//Application.LoadLevel ("try2");
			Debug.Log ("Connection succesful");
			communicationHandler.retrieveList();
		} else if (communicationHandler.status == 0) {
			Debug.Log ("Couldn't connect, try again");
		}
		/*if (val.text == "yes") {
			Application.LoadLevel("try2");
		}*/
	}




}
