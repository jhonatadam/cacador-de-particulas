using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour {
	private GradientColorChanger gcc;
	// Use this for initialization
	void Start () {
		gcc = GetComponent<GradientColorChanger> ();
	}
	
	// Update is called once per frame
	void Update () {	
		if (gcc.IsDone ()) {
			gameObject.SetActive (false);
		}
	}
}
