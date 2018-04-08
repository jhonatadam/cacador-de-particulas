using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTitle : MonoBehaviour {
	private MenuTitleAni mta;
	public string firstSceneName;
	private AudioManager audioManager;
	public GameObject loadingScreen;
	// Use this for initialization
	void Start () {
		audioManager = AudioManager.instance;
		audioManager.PlaySound ("Titulo");
		mta = GetComponent<MenuTitleAni> ();
		DontDestroyOnLoad (loadingScreen);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Jump")) {
			if (mta.GetOption () == 1) {
				NovoJogo ();
				return;
			}
			if (mta.GetOption () == 3) {
				Sair ();
				return;
			}
		}
	}
	public void NovoJogo() {
		loadingScreen.SetActive (true);
		audioManager.StopSound ("Titulo");

		GameObject tempData = GameObject.Find ("TempData");
		DontDestroyOnLoad (tempData);
		SceneManager.LoadSceneAsync (firstSceneName);
	}
	public void Creditos(){

	}
	public void Sair() {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}

}
