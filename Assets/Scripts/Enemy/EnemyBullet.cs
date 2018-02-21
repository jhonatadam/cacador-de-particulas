using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

	private float damage;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

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
			Destroy (this.gameObject);
			other.gameObject.GetComponent<PlayerHealth> ().DamagePlayer(damage);
			if (other.gameObject.transform.position.x < this.gameObject.transform.position.x) {
				other.gameObject.GetComponent<Player> ().Knockback (-1);
			} else {
				other.gameObject.GetComponent<Player> ().Knockback (1);
			}
		}
	}
}
