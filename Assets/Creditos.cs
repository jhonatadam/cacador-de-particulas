using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Creditos : MonoBehaviour {
	public float delay = 5;
	private float time = 0;
	private int current = 0;
	private float lastChange = -5;
	private AudioManager audioManager;
	// Use this for initialization
	void Start () {
		audioManager = AudioManager.instance;
		audioManager.PlaySound ("Creditos");
		foreach (Transform child in transform) {
			child.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time - lastChange > delay && current < transform.childCount - 1) {
			
			foreach (Transform child in transform) {
				if (child == transform.GetChild (current)) {
					child.gameObject.GetComponent<SpriteRenderer> ().enabled = true;
				} else {
					child.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
				}
			}
			current++;
			lastChange = time;
		}
		if (time > (transform.childCount + 2) * delay) {
			if (GameObject.Find("TempData") && GameObject.Find("TempData").GetComponent<TempData>().finalDoJogo) {
				#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
				#else
				Application.Quit();
				#endif
			} else {
				SceneManager.LoadScene ("MenuTitle");
			}

		}
	}
}
