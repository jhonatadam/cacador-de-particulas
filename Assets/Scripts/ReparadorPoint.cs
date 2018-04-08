using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReparadorPoint : MonoBehaviour {
	public float reparingTime;
	private float reparingElapsed;
	public EnemyHealth eh;
	public bool reparing = false;
	// Use this for initialization
	void Start () {
		transform.SetParent (GameObject.Find ("ReparadorPointsController").transform);
		reparingElapsed = reparingTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (reparing) {
			reparingElapsed -= Time.deltaTime;
			if (reparingElapsed <= 0) {
				Done ();
			}
		}
	}
	void Done(){
		if (eh != null) {
			eh.Rebirth ();
			Destroy (gameObject);
		}
	}
}
