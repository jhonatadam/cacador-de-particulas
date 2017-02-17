using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public GameObject bgImage;
	public GameObject menu;
	private bool paused = false;

	// Use this for initialization
	void Start () {
		menu.SetActive (false);
		bgImage.SetActive (false);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowPauseMenu() {
		if (!paused) {
			Pause ();
		} else {
			UnPause ();
		}

	}

	private void Pause() {
		menu.SetActive (true);
		bgImage.SetActive (true);
		Time.timeScale = 0;
		paused = true;
	}

	private void UnPause() {
		menu.SetActive (false);
		bgImage.SetActive (false);
		Time.timeScale = 1;
		paused = false;
	}

	public void Continue() {
		UnPause ();
	}

	public void BackToMenu() {
		SceneManager.LoadScene ("TestMenuTitle");
	}

	public void Sair() {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
}
