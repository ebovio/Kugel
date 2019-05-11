using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPBehaviour : MonoBehaviour
{
	public Transform cp;

    // Start is called before the first frame update
    void Start()
    {
		   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnDrawGizmos() {
		Gizmos.color = Color.blue;
		Gizmos.DrawSphere (cp.position, 1);
		Gizmos.color = Color.green;
		Gizmos.DrawLine (cp.position, GetComponent<Transform> ().position);
	}

	public Vector3 getCP() {
		return cp.position;
	}
}
