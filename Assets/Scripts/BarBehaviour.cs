using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarBehaviour : MonoBehaviour {

	public GameObject w;
	public bool displayedFromStart;
	private bool wInitial;

	void Awake(){
		wInitial = w.GetComponent<WaterBehaviour> ().getDry ();
	}

	void Start () {
		displayedFromStart = !displayedFromStart;
	}

	// Update is called once per frame
	void Update () {
		if (w.GetComponent<WaterBehaviour> ().getDry () == wInitial) {
			if (displayedFromStart) {
				this.GetComponent<MeshRenderer> ().enabled = true;
				this.GetComponent<CapsuleCollider> ().enabled = true;
			} else {
				this.GetComponent<MeshRenderer> ().enabled = false;
				this.GetComponent<CapsuleCollider> ().enabled = false;
			}
		} else {
			if (displayedFromStart) {
				this.GetComponent<MeshRenderer> ().enabled = false;
				this.GetComponent<CapsuleCollider> ().enabled = false;
			} else {
				this.GetComponent<MeshRenderer> ().enabled = true;
				this.GetComponent<CapsuleCollider> ().enabled = true;
			}
		}
	}
}