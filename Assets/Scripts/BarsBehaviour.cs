using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarsBehaviour : MonoBehaviour {

	public GameObject w;
	public bool displayedFromStart;
	private bool wInitial;

	void Awake(){
		wInitial = w.GetComponent<WaterBehaviour> ().getDry ();
	}

	// Use this for initialization
	void Start () {
		displayedFromStart = !displayedFromStart;
	}

	// Update is called once per frame
	void Update () {
		if (w.GetComponent<WaterBehaviour> ().getDry () == wInitial) {
			if (displayedFromStart) {
				this.GetComponent<BoxCollider> ().enabled = true;
			} else {
				this.GetComponent<BoxCollider> ().enabled = false;
			}
		} else {
			if (displayedFromStart) {
				this.GetComponent<BoxCollider> ().enabled = false;
			} else {
				this.GetComponent<BoxCollider> ().enabled = true;
			}
		}
	}
}
