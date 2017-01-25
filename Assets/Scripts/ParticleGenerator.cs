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

	public float particleStep;

	public float particleScale = 0.15f;

	public GameObject[] particles;

	public bool randomPosition;
	public float minPosition;
	public float maxPosition;

	// Use this for initialization
	void Start () {
		actualTime = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		actualTime++;



		if (actualTime == delay) {

			if (randomPosition) {
				RandomPosition ();
			}

			Choose ();
			GameObject part = Instantiate(particle, generatePoint.position, Quaternion.Euler(0, 0, Random.Range(minAngle, maxAngle) + generatePoint.eulerAngles.z));

			part.GetComponent<Particle> ().step = particleStep;
			part.transform.localScale = new Vector3 (particleScale, particleScale, particleScale);

			actualTime = 0;
		}
	}

	void Choose() {
		int rnd = Random.Range (0, particles.Length);

		particle = particles [rnd];

	}

	void RandomPosition () {
		generatePoint.position = new Vector3 (Random.Range (minPosition, maxPosition), generatePoint.position.y, generatePoint.position.z);
	}
}
