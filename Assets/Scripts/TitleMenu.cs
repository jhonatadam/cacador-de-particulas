﻿using System.Collections;
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
