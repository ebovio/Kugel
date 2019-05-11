using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeBehaviour : MonoBehaviour {

	public GameObject w;
	public bool displayedFromStart;
	private bool wInitial;

	// Use this for initialization
	void Start () {
		wInitial = w.GetComponent<WaterBehaviour> ().getDry ();
	}
	
	// Update is called once per frame
	void Update () {
		if (w.GetComponent<WaterBehaviour> ().getDry () == wInitial) {
			if (displayedFromStart) {
				this.GetComponent<MeshRenderer> ().enabled = true;
				this.GetComponent<BoxCollider> ().enabled = true;
			} else {
				this.GetComponent<MeshRenderer> ().enabled = false;
				this.GetComponent<BoxCollider> ().enabled = false;
			}
		} else {
			if (displayedFromStart) {
				this.GetComponent<MeshRenderer> ().enabled = false;
				this.GetComponent<BoxCollider> ().enabled = false;
			} else {
				this.GetComponent<MeshRenderer> ().enabled = true;
				this.GetComponent<BoxCollider> ().enabled = true;
			}
		}
	}
}
