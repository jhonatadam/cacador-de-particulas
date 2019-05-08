using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	public float maxHealth;
	public float health;
	protected SpriteRenderer sr;
	public float piscar = 0;
	public float piscarTempo = 0.1f;
	private bool fadeOut = false;
	protected float alpha = 1;
	[HideInInspector]
	public Rigidbody2D rb2d;
	public bool dead = false;
	protected AudioManager audioManager;
	// Referência do animator
	public Animator animator;
	public float dyingTime = 0.5f;
	protected float dyingElapsed;
	public int sparkleAmount = 4;
	public float sparkleInterval;
	protected float lastSparkle = 0;
	public GameObject shortSparkle;
	public float sparkleRadius;
	public GameObject dropHP;
	public GameObject hitParticlePrefab;
	[Range(0.0f, 1.0f)]
	public float dropHpChance = 0.5f;
	private ReparadorPoints rpoints;
	public float rebirthTime = 2.0f;
	protected float gravityOriginal;
	// Use this for initialization
	protected FreezerInHit frezee;
	void Start () {
		dyingElapsed = dyingTime;
		audioManager = AudioManager.instance;
        frezee = FreezerInHit.instance;
		health = maxHealth;
		sr = gameObject.GetComponent<SpriteRenderer> ();
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		gravityOriginal = rb2d.gravityScale;
		
		GameObject mgr = GameObject.FindWithTag("FrezzeManager");

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
			dyingElapsed -= Time.deltaTime;
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
		GameObject hitParticle = Instantiate(hitParticlePrefab) as GameObject;
		hitParticle.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        if (frezee) {
            frezee.Freeze();
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
		rb2d.gravityScale = 0;
		audioManager.PlaySound ("Enemy Death");
		animator.SetTrigger ("Dead");
		Collider2D[] colliders = GetComponentsInChildren<Collider2D> ();
		for (int i = 0; i < colliders.Length; i++) {
			colliders [i].enabled = false;
		}
		if (dropHP != null && Random.Range (0.0f, 1.0f) < dropHpChance) {
			GameObject drophp = Instantiate (dropHP);
			drophp.transform.position = new Vector3 (transform.position.x + Random.Range (-0.2f, -0.2f), transform.position.y + Random.Range (-0.2f, -0.2f), transform.position.z);
		}
		sr.color = new Color (0.3f, 0.23f, 0.35f, 0.9f);
		//if (gameObject.GetComponent<ReparadorBehavior> () == null) {
		//	rpoints.CreatePoint (new Vector2 (transform.position.x, transform.position.y), rebirthTime, this);
		//}
		//TODO essa é apenas uma morte provisória, é preciso fazer corretamente. Colocar animações e etc.
		dead = true;
        EnemyBehavior eb = GetComponent<EnemyBehavior>();
        if (eb != null) {
            GetComponent<EnemyBehavior>().dead = true;
        }

	}
	public void Rebirth(){
		animator.SetTrigger ("Alive");
		rb2d.gravityScale = gravityOriginal;
		Collider2D[] colliders = GetComponentsInChildren<Collider2D> ();
		for (int i = 0; i < colliders.Length; i++) {
			colliders [i].enabled = true;
		}
		sparkleAmount = 5;
		sr.color = new Color (1, 1, 1, 1);
		dead = false;
		GetComponent<EnemyBehavior> ().dead = false;
		health = maxHealth;
	}
	void OnDestroy(){
		sr = null;
		//rb2d = null;
		Resources.UnloadUnusedAssets ();
	}
}