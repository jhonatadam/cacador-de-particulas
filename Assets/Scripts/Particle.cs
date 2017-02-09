using UnityEngine;
using System.Collections;

public class Particle : MonoBehaviour {

	//Velocidade da partícula.
	public float step;
	//Raio do movimento circular.
	public float radious = 0.75f;
	private string TrSortingLayer;

	//Partículas para as quais pode-se decair.
	public GameObject[] daughters; 

	//Cauda da partícula.
	private TrailRenderer tail;

	private float num = 0;

	private float angle = 0;

	private ParticleSystem ps;

	private SpriteRenderer sr;

	private bool canDecay;

	public bool linearMovement = true;

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

		sr = GetComponent<SpriteRenderer> ();
		tail = GetComponent<TrailRenderer> ();

		if (GetComponent<ParticleSystem> () != null) {
			ps = GetComponent<ParticleSystem> ();
		}



		canDecay = true;

	}
	
	void FixedUpdate () {
		
		if (linearMovement) {
			LinearMovement ();
		} else {
			CircularMovement2 ();
		}

		if (Random.Range(0f,1f) >= 0.9998f) {
			if (canDecay) {
				Decay ();
			}
		}

	}

	void Update() {

	}

	void CircularMovement() {
		transform.position = initialPosition + new Vector3 (radious * Mathf.Sin (num), radious * Mathf.Cos (num), 0);
		num += step;
	}

	void CircularMovement2() {
		//initialPosition = transform.position;
		//transform.position = initialPosition + new Vector3 (radious * Mathf.Sin (num), radious * Mathf.Cos (num), 0);

		angle = transform.eulerAngles.magnitude * Mathf.Deg2Rad;

		rot += new Vector3 (0, 0, 0.5f);


		pos.x += (Mathf.Cos (angle) * step) * Time.deltaTime;
		pos.y += (Mathf.Sin (angle) * step) * Time.deltaTime;

		transform.position = pos;
		transform.rotation = Quaternion.Euler(rot);
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
		canDecay = false;

		if (daughters.Length == 0)
			return;

		float degrees = 25f;
		int size = daughters.Length;

		GameObject daughter;

		foreach (GameObject particle in daughters) {
			daughter = Instantiate (particle, transform.position, Quaternion.Euler (0f, 0f, transform.eulerAngles.z + degrees));

			daughter.GetComponent<Particle> ().step = this.step;
			//daughter.transform.localScale = this.transform.localScale;

			degrees -= 50 / size;
			size--;

		}
			
		destroyParticle ();
	}

	void destroyParticle() {
		step = 0;

		if(sr)
			sr.enabled = false;
		if(ps)
			ps.Stop ();
		Destroy (gameObject, tail.time);
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			destroyParticle ();
		}

		if (other.gameObject.tag == "Elevator") {
			destroyParticle ();
		}
	}

}
