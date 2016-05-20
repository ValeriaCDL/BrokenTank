using UnityEngine;
using System.Collections;

public class Status : MonoBehaviour {

	public int hp;
	public GameObject explosion;
	public GameObject cam;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (hp <= 0) {
			Instantiate(explosion, transform.position, transform.rotation);

			if (cam!=null)
				cam.transform.parent=null;

			Destroy (gameObject);
		}
	}
}
