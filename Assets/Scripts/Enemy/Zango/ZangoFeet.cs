using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZangoFeet : MonoBehaviour {

	private ZangoBehavior behavior;
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		behavior = GetComponentInParent<ZangoBehavior> ();
		rb2d = GetComponentInParent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Ground" || other.tag == "Platform") {
			//rb2d.velocity = new Vector2 (0, rb2d.velocity.y);
			behavior.jumping = false;
			if (other.tag != "Ground")
				behavior.actualPlatform = other.gameObject;
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.tag == "Ground" || other.tag == "Platform") {
			behavior.jumping = false;
			if (other.tag != "Ground")
				behavior.actualPlatform = other.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Ground" || other.tag == "Platform") {
			if (other.name != "Wall-Roof-Ground")
				behavior.jumping = true;
			behavior.actualPlatform = null;
		}
	}
}
