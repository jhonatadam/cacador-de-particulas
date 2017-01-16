using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {

	public ContactCheck cc;
	public string nextSceneName;

	void Start () {
		
	}

	void Update () {
		if (cc.getIsInContact ()) {
			//Scene nextScene = SceneManager.GetSceneByName (nextSceneName);
			//if (!nextScene.IsValid () || !nextScene.isLoaded) {
			//	Application.LoadLevelAdditive (nextSceneName);
			//}

			SceneManager.LoadScene (nextSceneName);
			//gameObject.SetActive (false);
		}
	}
}
