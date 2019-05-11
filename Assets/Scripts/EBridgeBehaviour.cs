using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBridgeBehaviour : MonoBehaviour
{
	public WaterBehaviour wb;
	public AudioClip metalwallSound;


    // Start is called before the first frame update
    void Start()
    {
		this.GetComponent<Renderer>().enabled = false;
		this.GetComponent<BoxCollider>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
		if (!wb.getDry ()) {
			this.GetComponent<Renderer>().enabled = true;
			this.GetComponent<BoxCollider>().enabled = true;
		}
    }
}
