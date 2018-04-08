using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZangoBullet : MonoBehaviour {
	public GameObject explosion;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag != "Enemy") {
			//print ("explosão");
			GameObject exp = Instantiate (explosion);
			exp.transform.position = transform.position;
		}
	}
}
