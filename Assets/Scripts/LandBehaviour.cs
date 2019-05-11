using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandBehaviour : MonoBehaviour {

	public GameObject w;
	public Material dryM;
	public Material wetM;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (!w.GetComponent<WaterBehaviour>().getDry()) {
			this.GetComponent<Renderer> ().material = wetM;
		} else {
			this.GetComponent<Renderer> ().material = dryM;
		}

	}
}
