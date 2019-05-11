using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour {

	public Text modetxt;
	public Text nModetxt;
	public Text helptxt;
	public Image modeimg;
	public Sprite gooimg;
	public Sprite spongeimg;
	public Sprite fireimg;
	public Sprite neutralimg;
	public Sprite tackleimg;
	public Sprite completeimg;

	private Transform t;
	private Rigidbody rb;
	private float x;
	private float z;
	private bool grounded;
	private bool dry;
	private bool k;

	private int speed;

	private bool completed;

	private Vector3 cp;
	private GameObject dl;

	public Camera c;
	public Material dryM;
	public Material wetM;
	public Material goo;
	public Material tackleM;
	public Material fireM;
	public Material neutralM;

	public GameObject trailH;
	public GameObject trailV;

	private bool tackling;

	private string mode;
	private int modeIndex;
	private List<string> modes;

	private ParticleSystem ps;

	public AudioClip popSound;
	private AudioSource popSource;

	public AudioClip looseSound;
	private AudioSource looseSource;

	public AudioClip breakSound;
	private AudioSource breakSource;

	public AudioClip burnSound;
	private AudioSource burnSource;

	public AudioClip absorbSound;
	private AudioSource absorbSource;

	public AudioClip squezzeSound;
	private AudioSource squezzeSource;

	public AudioClip fallSound;
	private AudioSource fallSource;

	// Use this for initialization
	void Start () {

		completed = false;
		modeimg.enabled = false;
		modeimg.sprite = neutralimg;
		dl = GameObject.Find ("Directional Light");
		tackling = false;
		modetxt.color = new Color (0.6226415f,0.255518f,0.579991f);
		//nModetxt.color = new Color (0.6226415f,0.255518f,0.579991f);
		ps = GetComponent<ParticleSystem> ();
		mode = "Neutral";
		modes = new List<string> ();
		modes.Add ("Neutral");
		//modes.Add ("Goo");
		//modes.Add ("Sponge");
		//modes.Add ("Fire");
		//modes.Add ("Tackle");
		modeIndex = 0;
		GetComponent<Renderer> ().material = neutralM;
		ps.Stop();
		GetComponent<Light> ().enabled = false;
		speed = 7;
		dry = true;
		grounded = true;
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		t = GetComponent<Transform> ();
		rb = GetComponent<Rigidbody> ();
		cp = t.position;
		popSource = GetComponent<AudioSource> ();
		looseSource = GetComponent<AudioSource> ();
		breakSource = GetComponent<AudioSource> ();
		burnSource = GetComponent<AudioSource> ();
		absorbSource = GetComponent<AudioSource> ();
		squezzeSource = GetComponent<AudioSource> ();
		fallSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {

		//print (grounded);
		x = Input.GetAxisRaw ("Horizontal");
		z = Input.GetAxisRaw ("Vertical");

		if ((x != 0 || z != 0) && mode == "Tackle" && grounded && tackling) {
			rb.velocity = Vector3.zero;
		}

		t.Translate (Vector3.right * x * Time.deltaTime * speed);
		t.Translate (Vector3.forward * z * Time.deltaTime * speed);


		if (Time.timeScale == 1) {
			Quaternion a = c.transform.rotation;
			a.x = 0;
			a.z = 0;

			transform.rotation = a;
		}

		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 0.6f))
		{
			Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
			grounded = true;
			ComputeSpeed ();
		}
		else
		{
			grounded = false;
			ComputeSpeed ();
			Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 8f, Color.white);
		}

		if (Input.GetKeyDown (KeyCode.P) && !completed) {
			if (Time.timeScale == 1) {
				Time.timeScale = 0;
				helptxt.text = "";
				modetxt.enabled = false;
				modeimg.enabled = true;
				nModetxt.enabled = false;
			} else {
				Time.timeScale = 1;
				modetxt.enabled = true;
				modeimg.enabled = false;
				nModetxt.enabled = true;
			}
		}

		if (Input.GetKeyDown (KeyCode.Space) && grounded &&!tackling) {
			grounded = false;
			rb.AddForce (Vector3.up * 5, ForceMode.Impulse);
			ComputeSpeed ();
		}

		if (Input.GetKeyDown (KeyCode.Mouse1)) {
			if (mode == "Goo") {
				rb.useGravity = true;
			}
			modeIndex = (modeIndex + 1) % modes.Count;
			mode = modes [modeIndex];
			UpdateMode ();
			if (modes.Count > 1) {
				popSource.PlayOneShot (popSound, 1F);
			}
		}

		if (Input.GetKeyDown (KeyCode.Mouse0)&&mode=="Tackle"&&!tackling&&grounded) {
			StartCoroutine ("tackle");
		}
			
	}

	IEnumerator tackle() {
		Debug.DrawRay(transform.position, transform.TransformDirection(t.forward) * 10, Color.red);
		tackling = true;
		rb.AddForce (t.forward * 50, ForceMode.VelocityChange);
		yield return new WaitForSeconds (0.15f);
		rb.velocity = Vector3.zero;
		tackling = false;
		StopCoroutine ("tackle");
	}
		
	void OnCollisionEnter(Collision collision){
	
		if (collision.collider.name == "Goal") {
			modeimg.sprite = completeimg;
			completed = true;
			Time.timeScale = 0;
			helptxt.text = "";
			modetxt.enabled = false;
			modeimg.enabled = true;
			nModetxt.enabled = false;
		}
		if (tackling && collision.collider.name == "BreakableWall" && (dry || !collision.collider.GetComponent<DBreakable>().getUWD())) {
			breakSource.PlayOneShot (breakSound, 1F);
			collision.collider.GetComponent<Renderer> ().enabled = false;
			collision.collider.GetComponent<BoxCollider> ().enabled = false;
		} else if (tackling && collision.collider.name == "FWallC") {
			fallSource.PlayOneShot (fallSound, 1F);
			collision.collider.GetComponent<FallingBehaviour> ().FallBlock ();
		}

		if (collision.collider.name == "Powerup" && !collision.collider.GetComponent<Powerup>().isTaken()){
			helptxt.text = "Press 'Q' to take power up";
		}

		if (mode == "Fire" && dry && collision.collider.name == "BurnableTree") {
			burnSource.PlayOneShot (burnSound, 1F);
			collision.collider.GetComponent<BTreeBehaviour> ().Burne();
		}

			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
	} 

	public string getMode() {
		return mode;
	}

	public bool getDry() {
		return dry;
	}

	void OnCollisionStay(Collision collision){

		if (collision.collider.name == "Powerup" && Input.GetKeyDown(KeyCode.Q) && !collision.collider.GetComponent<Powerup>().isTaken()) {
			modes.Add(collision.collider.GetComponent<Powerup>().getName());
			modeIndex = modes.IndexOf (collision.collider.GetComponent<Powerup> ().getName ());
			mode = collision.collider.GetComponent<Powerup> ().getName ();
			collision.collider.GetComponent<Powerup> ().Take ();
			UpdateMode ();
			helptxt.text = "Press 'P' for information about your new mode";
		}

		if (collision.collider.name == "FWallC") {
			helptxt.text = "Press 'R' to reset dropped block";
		}

		if (Input.GetKeyDown(KeyCode.R) && collision.collider.name == "FWallC") {
			collision.collider.GetComponent<FallingBehaviour> ().reset();
		}

		if (mode == "Fire" && dry && collision.collider.name == "BurnableTree") {
			collision.collider.GetComponent<BTreeBehaviour> ().Burne();
		}

		if (mode == "Goo"&&collision.collider.name!="Limit") {
			if (Input.GetKey (KeyCode.Mouse0)) {
				rb.velocity = Vector3.zero;
				rb.angularVelocity = Vector3.zero;
			} 
			if (Input.GetKey (KeyCode.Mouse0)) {
				grounded = false;
				rb.useGravity = false;
			}
			if (Input.GetKeyUp (KeyCode.Mouse0)) {
				rb.useGravity = true;
			}

			if (collision.collider.tag == "gooH"&&grounded) {
				foreach (ContactPoint contact in collision.contacts) {
					GameObject instance = Instantiate (trailH);
					instance.GetComponent<Transform> ().position = contact.point;
					Destroy (instance, 1f);
				}
			} else if (collision.collider.tag == "gooV") {
				foreach (ContactPoint contact in collision.contacts) {
					GameObject instance = Instantiate (trailV);
					instance.GetComponent<Transform> ().position = contact.point;
					Destroy (instance, 1f);
				}
			}
		}
	}

	void OnCollisionExit(Collision collision) {
		ComputeSpeed ();
		rb.useGravity = true;
		if (collision.collider.name == "Powerup"){
			helptxt.text = "";
		}
		if (collision.collider.name == "FWallC") {
			helptxt.text = "";
		}
	}

	void OnTriggerEnter(Collider c){
		if (c.name == "respawn") {
			looseSource.PlayOneShot (looseSound, 1F);
			t.position = cp;
		} else if (c.name == "CP") {
			cp = c.GetComponent<CPBehaviour>().getCP();
		} else if (c.name == "Dark zone") {
			dl.GetComponent<Light> ().enabled = false;
			//modetxt.GetComponent<Outline>().effectColor = Color.white;
			RenderSettings.ambientIntensity = 0;
		} else if (c.name == "Light zone") {
			modetxt.GetComponent<Outline>().effectColor = Color.black;
			dl.GetComponent<Light> ().enabled = true;
			RenderSettings.ambientIntensity = 1;
		}
	}

	private void UpdateMode(){
		if (mode == "Goo") {
			modeimg.sprite = gooimg;
			modetxt.text = "GOO";
			modetxt.color = new Color (0.3676471f,1f,0.5536332f);
			GetComponent<Renderer> ().material = goo;
			ps.Stop ();
			GetComponent<Light> ().enabled = false;
		} else if (mode == "Sponge") {
			modeimg.sprite = spongeimg;
			modetxt.text = "SPONGE";
			modetxt.color = new Color (1f,0.9264706f,0.319f);
			ps.Stop ();
			GetComponent<Light> ().enabled = false;
			if (dry) {
				GetComponent<Renderer> ().material = dryM;
			} else {
				GetComponent<Renderer> ().material = wetM;
			}
		} else if (mode == "Fire") {
			modeimg.sprite = fireimg;
			modetxt.text = "FIRE";
			modetxt.color = new Color (1f,0.641f,0.7835208f);
			GetComponent<Renderer> ().material = fireM;
			if (dry) {
				GetComponent<Light> ().enabled = true;
				ps.Play ();
			} else {
				GetComponent<Light> ().enabled = false;
				ps.Stop ();
			}
		} else if (mode == "Tackle") {
			modeimg.sprite = tackleimg;
			modetxt.text = "TACKLE";
			modetxt.color = new Color (0.08962262f,0.7524709f,1f);
			GetComponent<Renderer> ().material = tackleM;
			ps.Stop ();
			GetComponent<Light> ().enabled = false;
		} else if (mode == "Neutral") {
			modeimg.sprite = neutralimg;
			modetxt.text = "NEUTRAL";
			modetxt.color = new Color (0.6226415f,0.255518f,0.579991f);
			GetComponent<Renderer> ().material = neutralM;
			ps.Stop ();
			GetComponent<Light> ().enabled = false;
		}
		/*if (modes [(modeIndex + 1) % modes.Count] == "Neutral") {
			nModetxt.text = "NEUTRAL";
			nModetxt.color = new Color (0.6226415f, 0.255518f, 0.579991f);
		} else if (modes [(modeIndex + 1) % modes.Count] == "Goo") {
			nModetxt.text = "GOO";
			nModetxt.color = new Color (0.3676471f, 1f, 0.5536332f);
		} else if (modes [(modeIndex + 1) % modes.Count] == "Tackle") {
			nModetxt.text = "TACKLE";
			nModetxt.color = new Color (0.08962262f,0.7524709f,1f);
		} else if (modes [(modeIndex + 1) % modes.Count] == "Fire") {
			nModetxt.text = "FIRE";
			nModetxt.color = new Color (1f,0.641f,0.7835208f);
		} else if (modes [(modeIndex + 1) % modes.Count] == "Sponge") {
			nModetxt.text = "SPONGE";
			nModetxt.color = new Color (1f,0.9264706f,0.319f);
		}*/
	}

	void OnTriggerStay(Collider c){
		if (c.name == "Water") {
			if (mode == "Sponge" && dry && c.GetComponent<WaterBehaviour> ().getMelt () && c.GetComponent<MeshRenderer> ().isVisible) {
				helptxt.text = "Click to absorb the water";
			} else if (mode == "Sponge" && c.GetComponent<WaterBehaviour> ().getMelt () && !dry && !c.GetComponent<MeshRenderer> ().isVisible) {
				helptxt.text = "Click to squeeze water out";
			} else {
				helptxt.text = "";
			}
		}
		if (mode == "Sponge") {
			if (c.name == "Water" && dry && c.GetComponent<WaterBehaviour>().getMelt() && Input.GetKeyDown (KeyCode.Mouse0) && c.GetComponent<MeshRenderer> ().isVisible) {
				absorbSource.PlayOneShot (absorbSound, 1F);
				dry = false;
				nModetxt.text = "*";
				c.GetComponent<WaterBehaviour> ().setDry (true);
				GetComponent<Renderer> ().material = wetM;
				c.GetComponent<MeshRenderer> ().enabled = false;
				rb.mass *= 1.5f;
				speed = 5;
			} else if (c.name == "Water" && c.GetComponent<WaterBehaviour>().getMelt() && !dry && Input.GetKeyDown (KeyCode.Mouse0) && !c.GetComponent<MeshRenderer> ().isVisible) {
				squezzeSource.PlayOneShot (squezzeSound, 1F);
				dry = true;
				nModetxt.text = "";
				c.GetComponent<WaterBehaviour> ().setDry (false);
				GetComponent<Renderer> ().material = dryM;
				c.GetComponent<MeshRenderer> ().enabled = true;
				rb.mass /= 1.5f;
				speed = 10;
			}
		} else if(mode == "Fire" && c.name=="Water" && !c.GetComponent<WaterBehaviour>().getDry()){
			ps.Stop ();
			GetComponent<Light> ().enabled = false;
		}

		if (Input.GetKeyDown (KeyCode.E) && c.name == "button") {
			c.GetComponent<ButtonBehaviour> ().Toggle ();
		}
			
	}

	void OnTriggerExit(Collider c){
		if (mode == "Fire" && c.name == "Water") {
			if (dry) {
				GetComponent<Light> ().enabled = true;
				ps.Play ();
			} else {
				GetComponent<Light> ().enabled = false;
				ps.Stop ();
			}
		}
		if (c.name == "Water") {
			helptxt.text = "";
		}
	}

	void ComputeSpeed(){
		if (grounded) {
			if (mode == "Goo") {
				speed = 15;
			} else {
				speed = 10;
			}
		} else {
			if (mode == "Goo") {
				speed = 7;
			} else {
				speed = 5;
			}
		}
	}
}
