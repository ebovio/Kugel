using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBehaviour : MonoBehaviour {

	public GameObject w;
	public bool dryTree;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (!w.GetComponent<WaterBehaviour>().getDry()) {
			if (dryTree) {
				this.GetComponent<MeshRenderer> ().enabled = false;
				this.GetComponent<CapsuleCollider> ().enabled = false;
			} else {
				this.GetComponent<MeshRenderer> ().enabled = true;
				this.GetComponent<CapsuleCollider> ().enabled = true;
				this.GetComponent<BoxCollider> ().enabled = true;
			}
		}else {
			if (dryTree) {
				this.GetComponent<MeshRenderer> ().enabled = true;
				this.GetComponent<CapsuleCollider> ().enabled = true;
			} else {
				this.GetComponent<MeshRenderer> ().enabled = false;
				this.GetComponent<CapsuleCollider> ().enabled = false;
				this.GetComponent<BoxCollider> ().enabled = false;
			}
		}
	}
}
