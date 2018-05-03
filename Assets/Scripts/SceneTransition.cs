using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {

	public ContactCheck cc;
	public string nextSceneName;

	public SceneDataManager sdm;
	private AudioManager audioManager;
	public bool useCustomPosition = false;
	public Vector2 customPosition;
	void Start () {
		audioManager = AudioManager.instance;		
	}

	void Update () {
		if (cc.getIsInContact ()) {
			if (useCustomPosition) {
				sdm.player.transform.position = new Vector3 (customPosition.x, customPosition.y, sdm.player.transform.position.z);
			}
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
            if(nextSceneName == "Scenes/Level1.7") {
                audioManager.StopSound("Fly");
            }
            if (nextSceneName == "Scenes/Level2.0") {
                if(!audioManager.IsPlaying("Cutscene Track")) {
                    audioManager.PlaySound("Cutscene Track");
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
