using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour {

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Particle") {
			other.gameObject.GetComponent<Particle> ().destroyParticle ();
		}
		//Destroy (other.gameObject);
	}

}
