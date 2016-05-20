using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.Collections.Generic;

public class Session{
	
	public string player1;
	public string player2;
	public int id;

	public Session (int id, string player1) {
		this.id = id;
		this.player1 = player1;
	}

	public string toString() {
		return "id=" + this.id + "; player1=" + this.player1;
	}
}

public class CommunicationHandler: MonoBehaviour {

	public string sessionName;
	public int sessionID;
	public int status=0;
	public JSONArray sessionList;
	JSONClass listClass;
	public List<Session> sessions;
	private string url = "http://23.226.229.83/wcc/api/1.0/";

	//Request types:
	// 0 = create session
	// 1 = retrieve list

	public void createSession (string name) {
		string myURL = url + "newSession.php";
		string secundaryURL = "?name=";
		StartURLRequest (myURL, secundaryURL, name);
	}

	public void retrieveList () {
		string myURL = url + "listSessions.php";
		//sessionList = new List<Session>();
		StartURLRequest (myURL);
	}

	void StartURLRequest(string url, string secundaryURL, string data) {
		WWWForm form = new WWWForm();
		form.AddField ("name", 0);
		string myURL = url + secundaryURL + data;
		WWW www = new WWW(myURL, form);
		
		StartCoroutine(WaitForRequest(www, 0));
	}

	void StartURLRequest(string myurl) {
		//Debug.Log ("Request type: 1 (retrieve list)");
		//Debug.Log ("URL: "+myurl);
		//WWWForm form = new WWWForm();
		WWW www = new WWW(myurl/*, form*/);
		
		StartCoroutine(WaitForRequest(www, 1));
	}
	
	IEnumerator WaitForRequest(WWW www, int requestType) {
		yield return www;
		
		// check for errors
		if (www.error == null){
			Debug.Log("WWW Ok!: " + www.text);
			var N = JSON.Parse(www.text);
			if (requestType == 0) {
				status = N["status"].AsInt;
				sessionID = N["sessionid"].AsInt;
			} else if (requestType == 1) {
				status = N["status"].AsInt;
				sessionID = N["sessionid"].AsInt;
			}

			/*else if (requestType == 1) {
				int id;
				string name;
				sessionList = N.AsArray;
				foreach (var j in sessionList) {
					Debug.Log ("I'm retrieving an element from sessionList");
					id = JSON.Parse(j["id"]).AsInt;
					name = j["player1"];
					Debug.Log(id + "; "+name);
					sessions.Add(new Session(id, name));
					Debug.Log(id + "; "+name);
				}
				/*Debug.Log("Trying to retrieve list...");
				sessionList = N["sessionlist"].AsArray;
				Debug.Log(sessionList);
				foreach (JSONNode j in sessionList) {
					Debug.Log ("I'm retrieving an element from sessionList");
					var M = JSON.Parse();
					sessions.Add(new Session(M["id"].AsInt, M["player1"].AsObject.ToString()));
					Debug.Log(M["id"].AsInt+ "; "+M["player1"].AsObject.ToString());
				}

				foreach(Session j in sessions) {
					Debug.Log(j.toString());
				}*/

				/*var jsonObj = new JavaScriptSerializer().Deserialize<Session>(json);
				foreach (var obj in jsonObj.objectList){
					// do stuff
				}*/
			//}
		} else {
			Debug.Log("WWW Error: "+ www.error);
			status = 0;
		}
	}
}
