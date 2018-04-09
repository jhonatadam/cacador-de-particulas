using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogueteJato : MonoBehaviour {
	private ParticleSystem ps;
	private GameObject cabine;
	// Use this for initialization
	void Start () {
		ps = GetComponentInChildren<ParticleSystem> ();
		cabine = GameObject.Find ("Cabine");
		cabine.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

	}
	void FixedUpdate(){

	}
	public void Activate(){
		ps.Play ();
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			other.gameObject.GetComponent<Player> ().PullTo(transform.position);
			cabine.SetActive (true);
			Activate ();
		}

	}
}
