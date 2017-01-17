using UnityEngine;
using System.Collections;

public class Particle : MonoBehaviour {

	public float step;
	public float radious;
	public string TrSortingLayer;

	private float num = 0;

	private Vector3 initialPosition;


	void Start () {
		initialPosition = transform.position;
		TrailRenderer tr = GetComponent<TrailRenderer> ();
		tr.sortingLayerName = TrSortingLayer;
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
		//transform.position = initialPosition + new Vector3 (num, num, 180);
		transform.position = initialPosition + transform.forward;
		num += step;
	}
}
