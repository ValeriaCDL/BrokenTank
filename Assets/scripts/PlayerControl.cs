using UnityEngine;
using System.Collections;
using SimpleJSON;
using UnityEngine.Networking;

[RequireComponent(typeof(AudioSource))]
public class PlayerControl : NetworkBehaviour {


	public float firerate=1;
	float nextfire=1;

	public float rotationSpeed=100;

	public GameObject bala;
	public Transform barril;
	public Material material;

	//public AudioClip disparo;
	private AudioSource fuenteSonido;
	private float volBajo=0.4f;
	private float volAlto=1.0f;

	void Awake(){
		fuenteSonido = GetComponent<AudioSource> ();
	}

	void Start(){

		if (isLocalPlayer) {
			return;
		}
		
		GetComponentInChildren<Camera> ().enabled = false;
		GetComponentInChildren<AudioListener> ().enabled = false;

	}

    [Command]
    void CmdSendAudio()
    {
        RpcSendAudioToClients();
    }

    [ClientRpc]
    void RpcSendAudioToClients()
    {
        float vol = Random.Range(volBajo, volAlto);
        fuenteSonido.PlayOneShot(fuenteSonido.clip, vol);
    }

	void Update(){

		if (!isLocalPlayer) {return;}

		float hkeys = -(Input.GetAxis ("Horizontal") * Time.deltaTime * rotationSpeed);
		float hacceleration = -(Input.acceleration.x * Time.deltaTime * rotationSpeed); 

		transform.Rotate(0, 0, hkeys);  
		transform.Rotate(0, 0,hacceleration);

		if ( (Input.GetButton("Fire1") || Input.GetKeyDown(KeyCode.Space)) && Time.time > nextfire) {
			nextfire = Time.time + firerate;
			CmdFire (); //este cliente le dice al servidor q lo haga
		} 


	}

	[Command]
	void CmdFire(){

        //float vol = Random.Range (volBajo, volAlto);
        //fuenteSonido.PlayOneShot (fuenteSonido.clip,vol);
        CmdSendAudio();
		GameObject bullet = Instantiate (bala, barril.position, barril.rotation) as GameObject;

		//Le dice al servidor q lo instance en los clientes
		NetworkServer.Spawn(bullet);

	}

	public override void OnStartLocalPlayer(){
		base.OnStartLocalPlayer ();

		foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer> ()) {
			mr.material = material;
		}
			

	}
		
}
