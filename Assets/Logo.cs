using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Logo : MonoBehaviour {
	private GradientColorChanger gcc;
	// Use this for initialization
	void Start () {
		gcc = GetComponent<GradientColorChanger> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > gcc.endChange) {
			SceneManager.LoadScene ("MenuTitle");
		}
	}
}
