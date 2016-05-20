using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

	public void PlayMultiplayer(){

		Debug.Log ("Hey Im herezz");

		SceneManager.LoadScene ("Multiplayer_menu");

		Debug.Log ("Hey Im herezz2");
	}
}
