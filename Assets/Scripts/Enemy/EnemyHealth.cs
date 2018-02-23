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
	private AudioSource sound1;
	private AudioSource deathSound;
	private bool dead = false;

	// Use this for initialization
	void Start () {
		health = maxHealth;
		sr = gameObject.GetComponent<SpriteRenderer> ();
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		if (sr == null) {
			sr = gameObject.GetComponentInChildren<SpriteRenderer> ();
		}
		sound1 = gameObject.GetComponents<AudioSource> ()[0];
		deathSound = gameObject.GetComponents<AudioSource> () [1];
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0 && !dead) {
			KillEnemy ();
		}
		if (piscar > 0) {
			piscar = piscar - Time.deltaTime;
		}
		if (piscar > piscarTempo/2.0f) {
			sr.color = new Color (1, 0, 0);
		} else if (piscar > 0) {
			sr.color = new Color (0, 0, 1);
		} else if (piscar < 0) {
			sr.color = new Color (1, 1, 1);
			piscar = 0;
		}
		if (fadeOut) {
			alpha -= Time.deltaTime;
			if (alpha < 0) {
				alpha = 0;
			}
			sr.color = new Color (1, 1, 1, alpha);
		}
	}

	public void DamageEnemy(float damage) {

		if (health - damage < 0) {
			health = 0;
			return;
		}
		sound1.Play ();
		piscar = piscarTempo;
		health -= damage;

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
		//TODO essa é apenas uma morte provisória, é preciso fazer corretamente. Colocar animações e etc.
		fadeOut = true;
		dead = true;
		deathSound.Play ();
		rb2d.velocity = new Vector2 (0, rb2d.velocity.y);
		Destroy (this.gameObject, 1.0f);
	}
}
