using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoShow : MonoBehaviour {



	void Start() {
		EventsManager.ShowScreen ();
		Time.timeScale = 0f;
	}

	public void closeScreen() {	
		EventsManager.DismissScreen ();
		Time.timeScale = 1f;
	}

	void OnDestroy() {
		
	}
}
