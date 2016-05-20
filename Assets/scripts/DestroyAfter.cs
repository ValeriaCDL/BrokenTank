using UnityEngine;
using System.Collections;

public class DestroyAfter : MonoBehaviour {

	public float lifeTime;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (lifeTime <= 0.0f) {
			Destroy (gameObject);
		}

		lifeTime -= Time.deltaTime;
	}
}
