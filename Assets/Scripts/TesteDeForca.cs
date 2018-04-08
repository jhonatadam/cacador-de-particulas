using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteDeForca : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		other.gameObject.GetComponent<Rigidbody2D>().AddForce (new Vector2(0f, 1000f));
	}
}
