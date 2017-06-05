using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTrigger : MonoBehaviour {
	private GameObject scene;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
//			gameObject.GetComponent<Test> ().StartScene ();
			Object o = Resources.Load("Prefabs/Cutscenes/CutsceneTeste");
			GameObject scene = Instantiate (o) as GameObject;
			scene.GetComponent<Test> ().StartScene ();
		}

	}

	private void DeleteScene() {
		print ("entrou mas ta nulo");
		if (scene != null) {
			Destroy (scene);
			print ("era pra ter deletado");
		}
	}

	private void OnEnable() {
		EventsManager.onCutsceneEnd += DeleteScene;
	}

	private void OnDisable() {
		EventsManager.onCutsceneEnd -= DeleteScene;
	}
}
