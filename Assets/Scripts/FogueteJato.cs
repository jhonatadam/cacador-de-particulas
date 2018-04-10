using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogueteJato : MonoBehaviour {
	private ParticleSystem ps;
	private GameObject cabine;
	private GameObject teto;
	private Rigidbody2D rb2d;
	private bool active = false;
	public float force;
	private SpriteRenderer sr;
	private Color original;
	private GameObject camera;
	private float originalSize;
	public float cameraMaxSize;
	private GameObject blackScreen;
	public GameObject sceneTransition;
	private float transitionTime;
	private float time = 0;
	// Use this for initialization
	void Start () {
		ps = GetComponentInChildren<ParticleSystem> ();
		cabine = GameObject.Find ("Cabine");
		cabine.SetActive (false);
		teto = GameObject.Find ("SceneLimiteTeto");
		rb2d = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		original = sr.color;
		camera = GameObject.Find ("MainCamera");
		originalSize = camera.GetComponent<Camera> ().orthographicSize;
		blackScreen = GameObject.Find ("BlackScreen");
		transitionTime = blackScreen.GetComponent<GradientColorChanger> ().endChange;
	}
	
	// Update is called once per frame
	void Update () {
		if (active) {
			time += Time.deltaTime;
			if (time >= transitionTime) {
				sceneTransition.transform.position = transform.position;

			}
			rb2d.AddForce (new Vector2(0, force));
			sr.color = new Color (Mathf.Min (sr.color.r + Time.deltaTime / 0.5f, 1), Mathf.Min (sr.color.g + Time.deltaTime / 0.5f, 1), Mathf.Min (sr.color.b + Time.deltaTime / 0.5f, 1));
			camera.GetComponent<Camera> ().orthographicSize = Mathf.Min (camera.GetComponent<Camera> ().orthographicSize + Time.deltaTime/0.5f, cameraMaxSize); 
		}
	}
	void FixedUpdate(){

	}
	public void Activate(){
		ps.Play ();
		active = true;
		blackScreen.GetComponent<GradientColorChanger> ().enabled = true;
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			other.gameObject.GetComponent<Player> ().PullTo(transform.position);
			cabine.SetActive (true);
			teto.SetActive (false);

			Activate ();
		}

	}
}
