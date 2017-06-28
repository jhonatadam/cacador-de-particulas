using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTrigger : MonoBehaviour {
	public GameObject scene;

	public void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
//			gameObject.GetComponent<Test> ().StartScene ();
			//scene = Resources.Load("Prefabs/Cutscenes/CutsceneTeste") as GameObject;
			scene = Instantiate (scene) as GameObject;
			//GameObject scene = Instantiate (o) as GameObject;
			scene.GetComponent<Cutscene> ().StartScene ();
		}

	}

	private void DeleteScene() {

		if (scene != null) {
			Destroy (scene);


			Destroy (this.gameObject);
		}
	}

	private void OnEnable() {
		EventsManager.onCutsceneEnd += DeleteScene;
	}

	private void OnDisable() {
		EventsManager.onCutsceneEnd -= DeleteScene;
	}
}
