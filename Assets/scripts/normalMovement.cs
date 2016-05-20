using UnityEngine;
using System.Collections;

public class normalMovement : MonoBehaviour {

	public float speed=1;
	Transform planet;
	Vector3 gravVector;
	Rigidbody rigidBody;

	void Start(){
		planet = GameObject.FindWithTag("Planet").transform;
		rigidBody = GetComponent<Rigidbody> ();
	}


	void Update () {
		gravVector = planet.transform.position - transform.position;

		rigidBody.AddForce (gravVector * speed);
		transform.Translate (0, speed * Time.deltaTime, 0); 
	}


}
