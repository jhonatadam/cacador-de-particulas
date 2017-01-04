using UnityEngine;
using System.Collections;

public class ParticleController : MonoBehaviour {

	public float step;
	public float radious;

	private float num = 0;

	void Start () {
	
	}
	
	void FixedUpdate () {
		transform.position = new Vector3 (radious * Mathf.Sin (num), radious * Mathf.Cos (num), 0);
		num += step;
	}
}
