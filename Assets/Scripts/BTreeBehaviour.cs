using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTreeBehaviour : MonoBehaviour
{
	public bool isNew;
	private ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
		ps = GetComponent<ParticleSystem> ();
		ps.Stop();
    }

    // Update is called once per frame
	public void Burne() {
		StartCoroutine ("Burn");
	}

	IEnumerator Burn(){
		ps.Play ();
		yield return new WaitForSeconds (2);
		ps.Stop ();
		GetComponent<Renderer>().enabled = false;
		if (!isNew) {
			GetComponent<CapsuleCollider> ().enabled = false;
		} else {
			GetComponent<BoxCollider> ().enabled = false;
		}
		StopCoroutine ("Burn");
	}

	public bool getNew(){
		return isNew;
	}
}
