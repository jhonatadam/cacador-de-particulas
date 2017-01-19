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

	public bool randomPosition;
	public float minPosition;
	public float maxPosition;

	// Use this for initialization
	void Start () {
		actualTime = 0;
	}
	
	// Update is called once per frame
	void Update () {

		actualTime++;



		if (actualTime == delay) {

			if (randomPosition) {
				RandomPosition ();
			}

			Choose ();
			Instantiate(particle, generatePoint.position, Quaternion.Euler(0, 0, Random.Range(minAngle, maxAngle) + generatePoint.eulerAngles.z));

			actualTime = 0;
		}
	}

	void Choose() {
		int rnd = Random.Range (0, particles.GetLength());

		particle = particles [rnd];

	}

	void RandomPosition () {
		generatePoint.position = new Vector3 (Random.Range (minPosition, maxPosition), generatePoint.position.y, generatePoint.position.z);
	}
}
