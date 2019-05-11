using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehaviour : MonoBehaviour {

	public bool visibleFromStart;
	private MeshRenderer mr;
	private BoxCollider bc;

	void Start () {
		bc = GetComponent<BoxCollider> ();
		mr = GetComponent<MeshRenderer> ();
		if (visibleFromStart) {
			mr.enabled = true;
			bc.enabled = true;
		} else {
			mr.enabled = false;
			bc.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Toggle(){
		mr.enabled = !mr.enabled;
		bc.enabled = !bc.enabled;
	}
}
