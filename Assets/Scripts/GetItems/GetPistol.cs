﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPistol : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<Player> ().hasPistol = true;
			other.gameObject.GetComponent<Player> ().SetPistolActive (true);
			other.gameObject.GetComponent<PlayerEnergy> ().OnPistolEnable ();
			Destroy (this.gameObject);
		}
	}
}
