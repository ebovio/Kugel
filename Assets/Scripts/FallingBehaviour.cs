using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBehaviour : MonoBehaviour
{
	private Vector3 startPosition;
	private Quaternion startRotation;

	public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
		startRotation = rb.GetComponent<Transform> ().rotation;
		startPosition = rb.GetComponent<Transform> ().position;
		rb.useGravity = false;
    }

	public void FallBlock() {
		rb.useGravity = true;
	}

	public void reset() {
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		rb.useGravity = false;
		rb.GetComponent<Transform> ().position = startPosition;
		rb.GetComponent<Transform> ().rotation = startRotation;
	}
}
