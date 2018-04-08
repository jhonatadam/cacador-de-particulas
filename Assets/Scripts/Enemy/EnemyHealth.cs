using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	public float maxHealth;
	public float health;
	private SpriteRenderer sr;
	public float piscar = 0;
	public float piscarTempo = 0.1f;
	private bool fadeOut = false;
	private float alpha = 1;
	[HideInInspector]
	public Rigidbody2D rb2d;
	private bool dead = false;
	private AudioManager audioManager;
	// Referência do animator
	public Animator animator;
	public float dyingTime = 0.5f;
	public int sparkleAmount = 4;
	public float sparkleInterval;
	private float lastSparkle = 0;
	public GameObject shortSparkle;
	public float sparkleRadius;
	// Use this for initialization
	void Start () {
		audioManager = AudioManager.instance;
		health = maxHealth;
		sr = gameObject.GetComponent<SpriteRenderer> ();
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		if (sr == null) {
			sr = gameObject.GetComponentInChildren<SpriteRenderer> ();
		}
		animator = GetComponent<Animator> ();
		if (animator == null) {
			animator = gameObject.GetComponentInChildren<Animator> ();
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (dead) {
			dyingTime -= Time.deltaTime;
			if (Time.time - lastSparkle > sparkleInterval && sparkleAmount > 0) {
				GameObject sprk = Instantiate (shortSparkle);
				sprk.transform.position = new Vector3 (transform.position.x + Random.Range (-sparkleRadius/2.0f, sparkleRadius/2.0f), transform.position.y + Random.Range (-sparkleRadius/2.0f, sparkleRadius/2.0f), transform.position.z);
				lastSparkle = Time.time;
				sparkleAmount--;
			}
		}
		if (health <= 0 && !dead) {
			KillEnemy ();
		}
		if (!dead) {
			if (piscar > 0) {
				piscar = piscar - Time.deltaTime;
			}
			if (piscar > piscarTempo / 2.0f) {
				sr.color = new Color (1, 0, 0);
			} else if (piscar > 0) {
				sr.color = new Color (0, 0, 1);
			} else if (piscar < 0) {
				sr.color = new Color (1, 1, 1);
				piscar = 0;
			}
		}
	}

	public virtual void DamageEnemy(float damage) {

		if (health - damage < 0) {
			health = 0;
			return;
		}
		piscar = piscarTempo;
		health -= damage;
		if (Random.Range (0.0f, 1.0f) > 0.5f) {
			audioManager.PlaySound ("Bot1");
		} else {
			audioManager.PlaySound ("Bot2");
		}
	}


	/* Função que cura o HP do player
	 * 
	 * 
	 * 
	 * */
	public void CureEnemy(float cure) {
		if (health + cure > maxHealth) {
			health = maxHealth;
			return;
		}

		health += cure;
	}

	public void KillEnemy() {
		rb2d.velocity = new Vector2 (0, 0);
		audioManager.PlaySound ("Enemy Death");
		animator.SetTrigger ("Dead");
		rb2d.constraints = RigidbodyConstraints2D.None;
		sr.color = new Color (0.3f, 0.23f, 0.35f, 0.9f);
		//TODO essa é apenas uma morte provisória, é preciso fazer corretamente. Colocar animações e etc.
		dead = true;
		GetComponent<EnemyBehavior> ().dead = true;
		gameObject.tag = "Ground";
		rb2d.velocity = new Vector2 (0, rb2d.velocity.y);
	}
	void OnDestroy(){
		sr = null;
		//rb2d = null;
		Resources.UnloadUnusedAssets ();
	}
}
