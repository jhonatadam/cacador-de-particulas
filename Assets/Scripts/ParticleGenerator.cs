using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleGenerator : MonoBehaviour {

	public Transform generatePoint;
	public GameObject particle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//public  Quaternion rotation = Quaternion.identity;
		//Instantiate (particle, generatePoint.position, generatePoint.rotation);
		Instantiate (particle, generatePoint.position, new Quaternion(0, 0, 360, 0));
	}
}
