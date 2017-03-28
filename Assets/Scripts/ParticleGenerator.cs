using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleGenerator : MonoBehaviour {

	public Transform generatePoint;
	private GameObject particle;

	//Delay em frames para a criação de partículas.
	public int delay;
	private int actualTime;

	//Ângulo mínimo e máximo para a direção das partículas.
	public int minAngle;
	public int maxAngle;

	//Partículas que podem ser geradas.
	public GameObject[] particles;


	public bool randomPosition;
	public float minPosition;
	public float maxPosition;

	public int startParticlesNum;
	public Rect particleStartRegion;


	void Start () {
		actualTime = 0;

		for (int i = 0; i < startParticlesNum; i++) {

			Vector3 partPosition = 
				new Vector3 (
					Random.Range(particleStartRegion.x, particleStartRegion.x + particleStartRegion.width), 
					Random.Range(particleStartRegion.y - particleStartRegion.height, particleStartRegion.y),
					generatePoint.position.z);
	
			Choose ();
			Instantiate (particle, partPosition, Quaternion.Euler(0, 0, Random.Range(minAngle, maxAngle) + generatePoint.eulerAngles.z));
		}
	}


	void FixedUpdate () {

		actualTime++;



		if (actualTime == delay) {

			if (randomPosition) {
				RandomPosition ();
			}

			Choose ();

			if (particle != null) {
				GameObject part = Instantiate (particle, generatePoint.position, Quaternion.Euler (0, 0, Random.Range (minAngle, maxAngle) + generatePoint.eulerAngles.z));
			}
			//part.GetComponent<Particle> ().step = particleStep;
			//part.transform.localScale = new Vector3 (particleScale, particleScale, particleScale);

			actualTime = 0;
		}
	}

	void Choose() {
		int rnd = Random.Range (0, particles.Length);

		if (particles.Length != 0) {
			particle = particles [rnd];
		} else {
			particle = null;
		}

	}

	void RandomPosition () {
		generatePoint.position = new Vector3 (Random.Range (minPosition, maxPosition), generatePoint.position.y, generatePoint.position.z);
	}
}
