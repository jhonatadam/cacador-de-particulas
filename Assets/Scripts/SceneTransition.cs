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
				if (!audioManager.IsPlaying ("Majestic Aurora")) {
					audioManager.PlaySound ("Majestic Aurora");
				}
			}
			if (nextSceneName == "Scenes/Level1.5") {
				audioManager.StopSound ("Majestic Aurora");
				if (!audioManager.IsPlaying ("Fly")) {
					audioManager.PlaySound ("Fly");
				}
			}
		}
	}
	void OnDestroy(){
		cc = null;
		nextSceneName = null;
		sdm = null;
	}
}
