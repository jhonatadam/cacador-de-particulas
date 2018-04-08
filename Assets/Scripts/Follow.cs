using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {
	private GameObject target;
	public string targetName;
	public Vector2 offset;
	private bool foundTarget = false;
	public bool active = true;
	// Use this for initialization
	void Start () {
		target = GameObject.Find (targetName);
		if (Resources.ReferenceEquals (target, null)) {
			active = false;
		}
		if (!Resources.ReferenceEquals (target, null)) {
			foundTarget = true;
			active = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!foundTarget) {
			target = GameObject.Find (targetName);
			if (!Resources.ReferenceEquals (target, null)) {
				foundTarget = true;
				active = true;
			}
		}
		if (active) {
			transform.position = new Vector3 (target.transform.position.x + offset.x, target.transform.position.y - offset.y, transform.position.z);
		}
	}
}
