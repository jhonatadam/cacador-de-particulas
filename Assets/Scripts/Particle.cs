using UnityEngine;
using System.Collections;

public class Particle : MonoBehaviour {

	public float step;
	public float radious;
	public string TrSortingLayer;
	public float lifeTime;

	public GameObject daughter1;
	public GameObject daughter2;

	private TrailRenderer tail;

	private float currentLife = 0f;

	private float num = 0;

	private float angle;

	private ParticleSystem ps;

	private SpriteRenderer sr;

	//transform
	Transform trans;
	//posição
	Vector3 pos;
	//rotação
	Vector3 rot;

	private Vector3 initialPosition;


	void Start () {
		initialPosition = transform.position;
		TrailRenderer tr = GetComponent<TrailRenderer> ();
		tr.sortingLayerName = TrSortingLayer;

		trans = transform;
		pos = trans.position;
		rot = trans.rotation.eulerAngles;

		Destroy (gameObject, lifeTime);
		sr = GetComponent<SpriteRenderer> ();
		tail = GetComponent<TrailRenderer> ();
		ps = GetComponent<ParticleSystem> ();
	}
	
	void FixedUpdate () {
		//CircularMovement ();
		LinearMovement();

		if (Random.Range(0f,1f) >= 0.998f) {
			Decay ();
		}

	}

	void Update(){
		currentLife += Time.deltaTime;
		//sr.color = new Color (sr.color.r, sr.color.g, sr.color.b, currentLife/lifeTime);



	}

	void CircularMovement() {
		transform.position = initialPosition + new Vector3 (radious * Mathf.Sin (num), radious * Mathf.Cos (num), 0);
		num += step;
	}

	void LinearMovement() {
		
		//Convertendo euler's angle para radiano.
		angle = transform.eulerAngles.magnitude * Mathf.Deg2Rad;


		pos.x += (Mathf.Cos (angle) * step) * Time.deltaTime;
		pos.y += (Mathf.Sin (angle) * step) * Time.deltaTime;
	
		num += step;

		transform.position = pos;
		transform.rotation = Quaternion.Euler(rot);
	}

	void Decay() {
		if (daughter1 != null && daughter2 != null) {
			
			Instantiate (daughter1, transform.position, Quaternion.Euler (0, 0, transform.eulerAngles.z + 25));
			Instantiate (daughter2, transform.position, Quaternion.Euler (0, 0, transform.eulerAngles.z - 25));

			step = 0;
			sr.enabled = false;
			ps.Stop ();
			Destroy (gameObject, tail.time);
		}
	}
}
