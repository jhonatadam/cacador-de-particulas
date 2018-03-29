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
	void OnCollisionEnter2D(Collision2D coll){
		GameObject exp = Instantiate (explosion);
		exp.transform.position = transform.position;
	}
}
