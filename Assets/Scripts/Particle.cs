using UnityEngine;
using System.Collections;

public class Particle : MonoBehaviour {

	public float step;
	public float radious;
	public string TrSortingLayer;

	public GameObject daughter1;
	public GameObject daughter2;

	public GameObject[] daughters; 

	private TrailRenderer tail;

	private float currentLife = 0f;

	private float num = 0;

	private float angle;

	private ParticleSystem ps;

	private SpriteRenderer sr;

	private bool canDecay;

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
		ps = GetComponent<ParticleSystem> ();

		canDecay = true;

	}
	
	void FixedUpdate () {
		//CircularMovement ();
		LinearMovement();

		if (Random.Range(0f,1f) >= 0.9998f) {
			if (canDecay) {
				Decay ();
			}
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

	/*void Decay() {
		if (daughter1 != null && daughter2 != null) {

			canDecay = false;
			
			daughter1 = Instantiate (daughter1, transform.position, Quaternion.Euler (0, 0, transform.eulerAngles.z + 25));
			daughter2 = Instantiate (daughter2, transform.position, Quaternion.Euler (0, 0, transform.eulerAngles.z - 25));

			daughter1.GetComponent<Particle> ().step = this.step;
			daughter2.GetComponent<Particle> ().step = this.step;

			daughter1.transform.localScale = this.transform.localScale;
			daughter2.transform.localScale = this.transform.localScale;

			step = 0;
			sr.enabled = false;
			ps.Stop ();
			Destroy (gameObject, tail.time);
		}
	}*/

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

		print ("parou");
		step = 0;
		sr.enabled = false;
		ps.Stop ();
		Destroy (gameObject, tail.time);
	}

}
