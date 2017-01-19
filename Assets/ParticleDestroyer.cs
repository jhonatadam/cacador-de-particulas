using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("ASKDJHASKJDHSKDJH");
		Debug.Log (other.gameObject.tag);
		if (other.gameObject.tag == "Particle") {
			Destroy (other.gameObject);
		}
		//Destroy (other.gameObject);
	}

}
