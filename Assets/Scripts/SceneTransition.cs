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
			SceneManager.LoadScene (nextSceneName);
		}
	}
}
