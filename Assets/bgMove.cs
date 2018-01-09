using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgMove : MonoBehaviour {
	public float amount = 0.3f;
	public float frequency = 1;
	private float last = 0;
	private Vector3 original;
	private bool turn = false;
	// Use this for initialization
	void Start () {
		original = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - last > frequency) {
			if (turn) {
				transform.position = original;
			} else {
				transform.position = new Vector3 (transform.position.x + Random.Range (0, amount), transform.position.y + Random.Range (0, amount), transform.position.z);
			}
			last = Time.time;
		}
	}
}
