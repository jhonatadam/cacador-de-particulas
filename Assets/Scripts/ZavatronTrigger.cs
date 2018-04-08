using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZavatronTrigger : MonoBehaviour {
	private ZevatronPosition zp;
	public GameObject zevatron;
	// Use this for initialization
	void Start () {
		zp = GetComponentInChildren<ZevatronPosition> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			GameObject zev = Instantiate (zevatron);
			zev.transform.position = zp.transform.position;
			Destroy (gameObject);
		}
	}
}
