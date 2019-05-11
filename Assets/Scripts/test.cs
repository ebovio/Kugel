using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

	public Transform PlayerTransform;
	private Vector3 _cameraOffSet;

	[Range(0.01f,1.0f)]
	public float SmoothFacotr = 0.5f;

	public bool LookAtPlayer = false;

	public bool RotateAroundPlayer = true;

	public float RotationSpeed = 5.0f;

	// Use this for initialization
	void Start () {
		_cameraOffSet = transform.position - PlayerTransform.position;
	}
	
	// Update is called once per frame

	void LateUpdate () {

		if (Time.timeScale == 1) {
			RotateAroundPlayer = true;
		} else {
			RotateAroundPlayer = false; 
		}

		if (RotateAroundPlayer) {
			Quaternion camTurnAngle = Quaternion.AngleAxis (Input.GetAxis ("Mouse X") * RotationSpeed, Vector3.up);
			_cameraOffSet = camTurnAngle * _cameraOffSet;
		}

		Vector3 newPos = PlayerTransform.position + _cameraOffSet;

		transform.position = Vector3.Slerp (transform.position, newPos, SmoothFacotr);

			if (LookAtPlayer || RotateAroundPlayer) {
				transform.LookAt (PlayerTransform);
			}
	}
}
