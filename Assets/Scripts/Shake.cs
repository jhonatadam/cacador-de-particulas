using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour {
	private Vector3 originalpos;
	public float phase;
	public float frequency;
	public float amplitude;
	// Use this for initialization
	void Start () {
		originalpos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		float osc = Mathf.Sin ((Time.time +phase) * frequency)*amplitude;
		transform.position = new Vector3 (originalpos.x + osc, originalpos.y + osc / 2.0f, originalpos.z);
	}
}
