using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour {

	public GameObject[] walls; 

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Toggle(){
		foreach (GameObject wall in walls){
			wall.GetComponent<WallBehaviour>().Toggle ();
		}
	}
}
