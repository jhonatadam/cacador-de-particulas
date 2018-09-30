using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Logo : MonoBehaviour {
	private GradientColorChanger gcc;
	private AudioManager audioManager;
	// Use this for initialization
	void Start () {
		audioManager = AudioManager.instance;
		audioManager.PlaySound ("Logo");
		gcc = GetComponent<GradientColorChanger> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > gcc.endChange || Input.GetButtonDown("Jump")) {
            audioManager.StopSound("Logo");
			SceneManager.LoadScene ("MenuTitle");
		}
	}
}
