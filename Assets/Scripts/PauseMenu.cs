using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	private GameObject player;

	public GameObject bgImage;
	public GameObject menu;
	private bool paused = false;
	//Flag que impede de o jogo ser pausado
	private bool locked = false;

	public GameObject totalMap;
	private bool totalMapPaused = false;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");

		menu.SetActive (false);
		bgImage.SetActive (false);

		totalMap.SetActive (false);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowPauseMenu() {
		if (!locked && !totalMapPaused) {
			if (!paused) {
				Pause ();
			} else {
				UnPause ();
			}
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
		Destroy (player);
		SceneManager.LoadScene ("MenuTitle");
	}

	public void Sair() {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}

	private void LockPause() {
		locked = true;
	}

	private void UnlockPause() {
		locked = false;
	}

	//=================================================================
	//==================== Funções do TotalMap ========================
	//=================================================================

	public void TotalMapShow() {
		if (!locked && !paused) {
			if (!totalMapPaused) {
				ShowMap ();
			} else {
				HideMap ();
			}
		}
	}

	private void ShowMap() {
		totalMap.SetActive (true);
		Time.timeScale = 0f;
		totalMapPaused = true;
	}

	private void HideMap() {
		totalMap.SetActive (false);
		Time.timeScale = 1f;
		totalMapPaused = false;
	}

	private void OnEnable() {
		EventsManager.onStartBtn += ShowPauseMenu;
		EventsManager.onTotalMapBtn += TotalMapShow;
		EventsManager.onScreenShown += LockPause;
		EventsManager.onScreenDismissed += UnlockPause;
	}

	private void OnDisable() {
		EventsManager.onStartBtn -= ShowPauseMenu;
		EventsManager.onTotalMapBtn -= TotalMapShow;
		EventsManager.onScreenShown -= LockPause;
		EventsManager.onScreenDismissed -= UnlockPause;
	}
}
