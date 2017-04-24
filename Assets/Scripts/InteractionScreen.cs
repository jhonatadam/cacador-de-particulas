using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScreen : MonoBehaviour {

	public SpriteRenderer arrow;
	public GameObject screen;

	// Use this for initialization
	void Start () {
		arrow.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			arrow.enabled = true;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			arrow.enabled = false;
		}
	}

	void showScreen() {

	}
}
