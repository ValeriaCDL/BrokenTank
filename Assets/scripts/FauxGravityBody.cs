using UnityEngine;
using System.Collections;

public class FauxGravityBody : MonoBehaviour 
{
	public FauxGravityAttractor attractor;
	private Transform whatev;
	Rigidbody rb;
	// Use this for initialization
	void Start () 
	{
		rb = gameObject.GetComponent<Rigidbody>();
		rb.constraints = RigidbodyConstraints.FreezeRotation;
		rb.useGravity = false;
		whatev = gameObject.transform;
		attractor = GameObject.FindWithTag ("Planet").GetComponent<FauxGravityAttractor>();
	}
	
	// Update is called once per frame
	void Update () {
		attractor.Attract(whatev);
	}
}
