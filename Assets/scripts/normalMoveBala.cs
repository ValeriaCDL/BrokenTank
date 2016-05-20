using UnityEngine;
using System.Collections;

public class normalMoveBala : MonoBehaviour {

	float espeed=10;
	Transform target;
	Rigidbody rigidBody;
	Quaternion quaternion;
	Vector3 gravVector;
	
	void Start(){
		target = GameObject.Find ("Sphere").transform;
		rigidBody = GetComponent<Rigidbody>();
	}

	void Update () { 
		gravVector = target.transform.position - transform.position;

		rigidBody.AddForce (gravVector);
		transform.Translate (0, espeed * Time.deltaTime, 0); 
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "Planet") {
			Destroy (this.gameObject);
		}
			
	}
}
