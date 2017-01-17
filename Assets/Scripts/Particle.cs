using UnityEngine;
using System.Collections;

public class Particle : MonoBehaviour {

	public float step;
	public float radious;
	public string TrSortingLayer;

	private float num = 0;

	private float angle;
	//transform
	Transform trans;
	//posição
	Vector3 pos;
	//rotação
	Vector3 rot;

	private Vector3 initialPosition;


	void Start () {
		initialPosition = transform.position;
		TrailRenderer tr = GetComponent<TrailRenderer> ();
		tr.sortingLayerName = TrSortingLayer;

		trans = transform;
		pos = trans.position;
		rot = trans.rotation.eulerAngles;

		Destroy (gameObject, 5f);
	}
	
	void FixedUpdate () {
		//CircularMovement ();
		LinearMovement();
	}

	void CircularMovement() {
		transform.position = initialPosition + new Vector3 (radious * Mathf.Sin (num), radious * Mathf.Cos (num), 0);
		num += step;
	}

	void LinearMovement() {
		
		//Convertendo euler's angle para radiano.
		angle = transform.eulerAngles.magnitude * Mathf.Deg2Rad;


		pos.x += (Mathf.Cos (angle) * step) * Time.deltaTime;
		pos.y += (Mathf.Sin (angle) * step) * Time.deltaTime;
	
		num += step;

		transform.position = pos;
		transform.rotation = Quaternion.Euler(rot);
	}
}
