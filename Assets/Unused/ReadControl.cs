using UnityEngine;
using System.Collections;
using SimpleJSON;

public class ReadControl : MonoBehaviour {


	public float firerate=1;
	float nextfire=1;
	public GameObject bala;
	public Transform barril;
	string url;
	public int session;
	public int player;

	int[] data = new int[3];
	// 0 = left
	// 1 = right
	// 2 = fire

	void Start() {
		data [0] = 0;
		data [1] = 0;
		data [2] = 0;
		url = "http://23.226.229.83/wcc/api/1.0/out.php?session="+session+"&player="+player;
		InvokeRepeating ("CustomUpdate", 0.0f, 0.05f);
	}

	void CustomUpdate() {
		StartURLRequest (url);
	}
	 
	void FixedUpdate () {
		if (data[0]==1 && data[1]==1) {
		} else if (data[0]==1) {
			transform.Rotate(0, 0, Time.deltaTime * 100);
		} else if (data[1]==1) {
			transform.Rotate(0, 0, -Time.deltaTime * 100);
		}

		if (data[2] == 1 && Time.time > nextfire) {
			nextfire = Time.time + firerate;
			Instantiate (bala, barril.position, barril.rotation);
		}
	}

	void StartURLRequest(string url) {
		//WWWForm form = new WWWForm();
		WWW www = new WWW (url);
		
		StartCoroutine(WaitForRequest(www));
	}

	IEnumerator WaitForRequest(WWW www) {
		yield return www;
		
		// check for errors
		if (www.error == null){
			//Debug.Log("WWW Ok!: " + www.text);
			
			var N = JSON.Parse(www.text);
			data[0] = N["left"].AsInt;
			data[1] = N["right"].AsInt;
			data[2] = N["shoot"].AsInt;
			
		} else {
			//Debug.Log("WWW Error: "+ www.error);
		}
	}
}
