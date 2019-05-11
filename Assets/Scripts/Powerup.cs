using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

	private ParticleSystem ps;
	public int dist; 
	private bool taken;
	public Transform player;
	public string pName;
	public AudioClip powerSound;
	private AudioSource powerSource;

    void Start()
    {
		taken = false;
		ps = GetComponentInChildren<ParticleSystem> ();
		powerSource = GetComponent<AudioSource> ();
    }

	void Update() {
		float d = Vector3.Distance (transform.position, player.position);

		if (d <= dist && !ps.isPlaying && !taken) {
			ps.Play ();
		} else if (d > dist && ps.isPlaying) {
			ps.Stop ();
		}
	}

	public bool isTaken(){
		return taken;
	}

	public string getName(){
		return pName;
	}

	public void Take(){
		powerSource.PlayOneShot (powerSound, 1F);
		taken = true;
		ps.Stop ();
	}
}
