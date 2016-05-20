using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float speed = 15f;
	private Vector3 moveDir;
    Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody>();
	
	}
	
	// Update is called once per frame
	void Update () {
		moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
	}

	void FixedUpdate() {
		rb.MovePosition(rb.position + transform.TransformDirection(moveDir * speed * Time.deltaTime));
		rb.transform.Rotate(moveDir);
	}
}
