using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleGenerator : MonoBehaviour {

	public Transform generatePoint;
	public GameObject particle;

	public int delay;
	private int actualTime;

	public int minAngle;
	public int maxAngle;

	public GameObject[] particles;

	// Use this for initialization
	void Start () {
		actualTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//public  Quaternion rotation = Quaternion.identity;
		//Instantiate (particle, generatePoint.position, generatePoint.rotation);


		//Instantiate (particle, generatePoint.position, new Quaternion(0, 0, rnd., 0));
		actualTime++;

		if (actualTime == delay) {
			//Instantiate (particle, generatePoint.position, new Quaternion(0, 0, Mathf.Clamp(Random.rotation.z, 300,405), 
			//	Mathf.Clamp(Random.rotation.w, 300, 405)));

			Choose ();
			Instantiate(particle, generatePoint.position, Quaternion.Euler(0, 0, Random.Range(minAngle, maxAngle) + generatePoint.eulerAngles.z));

			actualTime = 0;
		}
	}

	void Choose() {
		int rnd = Random.Range (0, 10);

		particle = particles [rnd];

	}


}
