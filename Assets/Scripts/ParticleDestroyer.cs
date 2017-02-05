using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Particle") {
			Destroy (other.gameObject);
		}
		//Destroy (other.gameObject);
	}

}
