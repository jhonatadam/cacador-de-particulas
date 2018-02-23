using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {

	public ContactCheck cc;
	public string nextSceneName;

	public SceneDataManager sdm;

	void Start () {
		
	}

	void Update () {
		if (cc.getIsInContact ()) {
			sdm.UpdatePlayerData ();
			sdm.UpdateSceneData ();
			sdm.UpdateSoundtrack ();

			SceneManager.LoadScene (nextSceneName);
		}
	}
}
