using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingController : MonoBehaviour {
	private ParticleSystem ring;
	// Use this for initialization
	void Start () {
		ring = gameObject.GetComponent<ParticleSystem> ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Emit(int n){
		print ("bbbbbb b >:" + n);
		gameObject.GetComponent<ParticleSystem> ().Emit (n);
	}
	public void Stop(){
		ring.Stop ();
	}
}
