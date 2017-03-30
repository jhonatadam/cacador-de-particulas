using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour {

	public string firstSceneName;

	public Button continuar;
	public GameObject loadingImg;

	// Use this for initialization
	void Start () {
		continuar.interactable = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void NovoJogo() {
		loadingImg.SetActive (true);

		// carregando player
		GameObject player = Resources.Load<GameObject>("Prefabs/Player");

		if (player != null) {
			player = Instantiate (player);
			player.name = "Player";
			DontDestroyOnLoad (player);
		}

		// mantendo instancia de soundtrack na cena carregada
		GameObject soundtrack = GameObject.Find ("Soundtrack");
		DontDestroyOnLoad (soundtrack);

		GameObject tempData = GameObject.Find ("TempData");
		DontDestroyOnLoad (tempData);

		SceneManager.LoadScene (firstSceneName);
	}

	public void Sair() {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}

}
