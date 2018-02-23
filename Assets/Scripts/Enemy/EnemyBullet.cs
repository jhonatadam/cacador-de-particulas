using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

	private float damage;
	private SpriteRenderer sr;
	public RingController ring;
	private ParticleSystem ps;
	private CircleCollider2D cl;

	public bool dead = false;

	// Use this for initialization
	void Start () {
		ps = gameObject.GetComponent<ParticleSystem> ();
		sr = gameObject.GetComponent<SpriteRenderer> ();

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

	private void OnTriggerEnter2D(Collider2D other) {
		string tag = other.gameObject.tag;

		print ("En emy bullet colidiu com " + tag);
		if (tag == "Ground") {
			Destroy (this.gameObject);
		}
		if (tag == "Player") {
			other.gameObject.GetComponent<PlayerHealth> ().DamagePlayer(damage);
			if (other.gameObject.transform.position.x < this.gameObject.transform.position.x) {
				other.gameObject.GetComponent<Player> ().Knockback (-1, 0.5f, 0.1f, 2);
			} else {
				other.gameObject.GetComponent<Player> ().Knockback (1, 0.5f, 0.1f, 2);
			}
		}
		sr.color = new Color (0, 0, 0, 0);
		ring.Emit (3);
		ring.Stop ();
		cl.enabled = false;
		ps.Stop ();
		dead = true;
	}
}
