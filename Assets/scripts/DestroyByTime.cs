using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour {

	public float timeLeft;
	public GameObject explosion;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (timeLeft <= 0.0f) {
			Instantiate(explosion, transform.position, transform.rotation);
			Destroy (gameObject);
		}
		timeLeft -= Time.deltaTime;
	}
}
