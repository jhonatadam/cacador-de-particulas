using UnityEngine;
using System.Collections;

public class Particle : MonoBehaviour {

	//Velocidade da partícula.
	public float speed;

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

	private bool canDamage = true;
	public float impulseFrequence = 1;
	private float lastImpulse = 0;

	public GameObject particleRipple;
	public GameObject particleRippleD;

	//transform
	Transform trans;
	//posição
	Vector3 pos;
	//rotação
	Vector3 rot;

	Rigidbody2D rb;

	private Vector3 initialPosition;

	public Vector2 force;

	public float charge = 0f;

	public float energy = 10f;
	public float damage = 10f;
	public bool isInFlask = false;

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
		 
		rb = GetComponent<Rigidbody2D> ();
		//rb.velocity = new Vector2 (0, -step);

		canDecay = true;

		rb.velocity = speed * new Vector2 (Mathf.Cos (rot.z * Mathf.Deg2Rad), Mathf.Sin (rot.z * Mathf.Deg2Rad));


	}
	
	void FixedUpdate () {

		if (Random.Range(0f,1f) >= 0.9998f) {
			if (canDecay) {
				Decay ();
			}
		}

	}

	void Update() {
		if (isInFlask && Time.time - lastImpulse > impulseFrequence ) {
			rb.AddForce( new Vector2 (Random.Range (10, 100)*Random.Range (-1.0f, 1.0f), Random.Range (10, 100)*Random.Range (-1.0f, 1.0f)));
			lastImpulse = Time.time;
		}
		
	}

	void Decay() {
		canDecay = false;

		if (daughters.Length == 0)
			return;

		float degrees = 25f;
		int size = daughters.Length;

		GameObject daughter;

		// definindo angulo da velocidade dentro do circulo unitário
		float angle = 
			Vector2.Angle (new Vector2 (Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y), 0), rb.velocity);	
		angle *= (rb.velocity.y < 0 ? -1 : 1);  


		foreach (GameObject particle in daughters) {
			daughter = Instantiate (particle, transform.position, Quaternion.Euler (0f, 0f, angle + degrees));

			//daughter.GetComponent<Particle> ().step = this.step;
			//daughter.transform.localScale = this.transform.localScale;

			degrees -= 50 / size;
			size--;

		}
			
		destroyParticle ();
	}

	public void destroyParticle() {
		canDamage = false;
		canDecay = false;

		rb.velocity = new Vector2 (0, 0);

		if(sr)
			sr.enabled = false;
		if(ps)
			ps.Stop ();

		Destroy (gameObject, tail.time);
	}

	void OnTriggerStay2D(Collider2D other)
	{
		GameObject ripple;
		if (other.gameObject.tag == "Player") {
			if (canDamage) {
				other.GetComponent<PlayerHealth> ().ParticleDamagePlayer (damage);
				other.GetComponent<PlayerEnergy> ().ChargeEnergy (energy);
				canDamage = false;
				ripple = Instantiate (particleRippleD, transform);
			} else {
				ripple = Instantiate (particleRipple, transform);
			}

			destroyParticle ();
		}

		if (other.gameObject.tag == "Elevator") {
			destroyParticle ();
		}
	}

}
