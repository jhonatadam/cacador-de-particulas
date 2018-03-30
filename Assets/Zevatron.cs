using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zevatron : MonoBehaviour {
	public GameObject explosion;
	private ParticleSystem[] ps;
	// Use this for initialization
	void Start () {
		ps = GetComponentsInChildren<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Enemy") {
			other.GetComponent<EnemyHealth> ().DamageEnemy (10000);
			GameObject exp = Instantiate (explosion);
			exp.transform.position = transform.position;
			AutoDestroy ();
		}
	}
	void AutoDestroy(){
		ps [0].Stop ();
		ps [1].Stop ();
		ps [2].Stop ();
		Destroy (gameObject, Mathf.Max (ps [0].main.startLifetime.constantMax, ps [1].main.startLifetime.constantMax, ps [2].main.startLifetime.constantMax));
	}
}
