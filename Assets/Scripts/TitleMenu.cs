using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour {

	public string firstSceneName;

	public Button continuar;
	public GameObject loadingImg;
	private AudioManager audioManager;
	public Slider slider;
	// Use this for initialization
	void Start () {
		audioManager = AudioManager.instance;
		audioManager.PlaySound ("Titulo");
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
			player.transform.position = new Vector3 (-16.5f, -13f, player.transform.position.z);
			DontDestroyOnLoad (player);
		}

		audioManager.StopSound ("Titulo");


		GameObject tempData = GameObject.Find ("TempData");
		DontDestroyOnLoad (tempData);

		StartCoroutine(LoadAsynchronously (firstSceneName));

	}

	IEnumerator LoadAsynchronously(string sceneName){
		
		AsyncOperation operation = SceneManager.LoadSceneAsync (sceneName);
		while (!operation.isDone) {
			float progress = Mathf.Clamp01 (operation.progress / 0.9f);
			slider.value = progress;
			yield return null;
		}
	}

	public void Sair() {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}

}
