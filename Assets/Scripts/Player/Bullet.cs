using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

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

	private void OnCollisionEnter2D(Collision2D other) {
		string tag = other.gameObject.tag;
		if (tag == "Ground") {
			Destroy (this.gameObject);
		}
	}
}
