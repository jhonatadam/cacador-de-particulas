using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergiaReatorBehavior : MonoBehaviour {

	// Use this for initialization
	Vector3 tempPos;
	private ParticleSystem ps;
	public void Start(){
		ps = gameObject.GetComponent<ParticleSystem>();
		ps.Emit(1);

	}

	void Update() {
		
	}
		
}
