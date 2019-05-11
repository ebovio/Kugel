using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBehaviour : MonoBehaviour {

	public bool presentFromStart;
	public bool iced;
	public GameObject ice;
	private bool dry;
	private GameObject player;
	private bool melt;

	// Use this for initialization
	void Start () {
		dry = !presentFromStart;
		this.GetComponent<MeshRenderer>().enabled = presentFromStart && !iced;
		ice.GetComponent<Renderer> ().enabled = iced;
		ice.GetComponent<BoxCollider> ().enabled = iced;
		melt = !iced;
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		float d = Vector3.Distance (transform.position, player.transform.position);
		if (d < 1.5f && iced && player.GetComponent<Move>().getMode()=="Fire" && player.GetComponent<Move>().getDry()) {
			this.GetComponent<MeshRenderer>().enabled = true;
			ice.GetComponent<Renderer> ().enabled = false;
			ice.GetComponent<BoxCollider> ().enabled = false;
			melt = true;
			dry = false;
		}
	}

	public bool getDry(){
		return dry;
	}

	public bool getMelt() {
		return melt;
	}

	public void setDry(bool b){
		dry = b;
	}
}
