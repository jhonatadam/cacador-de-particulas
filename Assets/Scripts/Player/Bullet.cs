using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private float damage;
	public RingController ring;
	private ParticleSystem ps;
	private SpriteRenderer sr;
	private bool dead = false;
	private CircleCollider2D cl;
	// Use this for initialization
	void Start () {
		sr = gameObject.GetComponent<SpriteRenderer> ();
		ps = gameObject.GetComponent<ParticleSystem> ();
		ring.Emit (30);
		cl = gameObject.GetComponent<CircleCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (dead) {
			Destroy (this.gameObject, 1.0f);
		}
	}

	public void setDamage(float damage) {
		this.damage = damage;
	}

	private void OnCollisionEnter2D(Collision2D other) {
		string tag = other.gameObject.tag;
		if (tag == "Enemy") {
			other.gameObject.GetComponent<EnemyHealth> ().DamageEnemy (damage);
		}
		ring.Emit (3);
		ring.Stop ();
		ps.Stop ();
		sr.color = new Color (0, 0, 0, 0);
		cl.enabled = false;
		dead = true;
	}
}
