using UnityEngine;
using System.Collections;

public class DestroyOnImpact : MonoBehaviour {

	public GameObject explosion;
	public int damage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision collision) {
		Collider other = collision.collider;
		//Debug.Log ("Collision");
		if (other.tag == "Player") {
			other.GetComponent<Status>().hp -= damage;
		}
		if (other.tag != "Planet") {
			Instantiate (explosion, transform.position, transform.rotation);
			Destroy (gameObject);
		}
	}
}
