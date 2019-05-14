using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FotonTrigger : MonoBehaviour {
	private FotonPosition ft;
	public GameObject foton;
	// Use this for initialization
	void Start () {
		ft = GetComponentInChildren<FotonPosition>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			GameObject fot = Instantiate (foton);
			fot.transform.position = ft.transform.position;
			Destroy (gameObject);
		}
	}
}
