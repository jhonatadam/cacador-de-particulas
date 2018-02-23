using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour {
	private float originalY;
	public float secondY;
	public float startTime;
	public float speed = 0.2f;
	public bool original = true;
	public float length;
	public float covered;
	public float fracJourney;
	// Use this for initialization
	void Start () {
		originalY = transform.localPosition.y;
		//length = Mathf.Round(Mathf.Abs (originalY - secondY));
	}
	
	// Update is called once per frame
	void Update () {
		covered = Senoid (speed, length, length);
		fracJourney = covered / (length*2);
		transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp (originalY, secondY, fracJourney), transform.localPosition.z);

	}
	float Senoid(float frequency, float amplitude, float offset){
		return Mathf.Sin ((Time.time - startTime) * frequency) * amplitude + offset;

	}

}
