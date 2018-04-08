using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReparadorPoint : MonoBehaviour {
	public float reparingTime;
	public EnemyHealth eh;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		reparingTime -= Time.deltaTime;
		if (reparingTime <= 0) {
			Done ();
		}
	}
	void Done(){
		if (!Resources.ReferenceEquals (eh, null)) {
			eh.Rebirth ();
			Destroy (gameObject);
		}
	}
}
