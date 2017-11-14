using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetJetpack : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<Player> ().hasJetpack = true;
			other.gameObject.GetComponent<Player> ().SetJetpackActive (true);
			Destroy (this.gameObject);
		}
	}
}
