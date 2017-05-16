using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoShow : MonoBehaviour {



	void Start() {
		Time.timeScale = 0f;
	}

	public void closeScreen() {	
		Destroy (gameObject);
	}

	void OnDestroy() {
		Time.timeScale = 1f;
	}
}
