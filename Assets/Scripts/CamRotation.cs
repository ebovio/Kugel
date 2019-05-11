using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotation : MonoBehaviour {

	private float x;
	private float y;
	private Vector3 rotateValue;
	
	// Update is called once per frame
	void Update () {
		/*
		if (Time.timeScale != 0) {
			Cursor.visible = false;
			y = Input.GetAxis ("Mouse X");
			x = Input.GetAxis ("Mouse Y");
			rotateValue = new Vector3 (x * 2, y * -2, 0);
			transform.eulerAngles = transform.eulerAngles - rotateValue;
		}
		*/
	}
}
