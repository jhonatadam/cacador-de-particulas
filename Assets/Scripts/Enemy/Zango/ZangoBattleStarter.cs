using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZangoBattleStarter : MonoBehaviour {

	public GameObject camera1;
	public GameObject camera2;
	public GameObject zango;
	public GameObject escotilha;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			escotilha.SetActive (true);
			camera1.SetActive (false);
			camera2.SetActive (true);
			camera2.GetComponent<Animator> ().Play ("CameraFadeOut");
			zango.SetActive (true);
			Destroy(gameObject.GetComponent<Collider2D>());
		}
	}
}
