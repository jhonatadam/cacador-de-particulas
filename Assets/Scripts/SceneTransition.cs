using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {

	public ContactCheck cc;
	public string nextSceneName;

	public SceneDataManager sdm;
	private AudioManager audioManager;
	void Start () {
		audioManager = AudioManager.instance;		
	}

	void Update () {
		if (cc.getIsInContact ()) {
			sdm.UpdatePlayerData ();
			sdm.UpdateSceneData ();
			SceneManager.LoadScene (nextSceneName);
			if (nextSceneName == "Scenes/Level1.1") {
				audioManager.StopSound ("Tutorial");
			}
		}
	}
	void OnDestroy(){
		cc = null;
		nextSceneName = null;
		sdm = null;
	}
}
