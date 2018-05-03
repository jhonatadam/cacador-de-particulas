using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZangoBattleStarter : MonoBehaviour {

	public GameObject camera1;
	public GameObject camera2;
	public GameObject zango;
	public GameObject escotilha;
	public GameObject portaEnd;
    private AudioManager audioManager;

	// Use this for initialization
	void Start () {
        audioManager = AudioManager.instance;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
            if (!audioManager.IsPlaying("Zango")) {
                audioManager.PlaySound("Zango");
            }
			escotilha.SetActive (true);
			portaEnd.SetActive (true);
			camera1.SetActive (false);
			camera2.SetActive (true);
			camera2.GetComponent<Animator> ().Play ("CameraFadeOut");
			zango.SetActive (true);
			Destroy(gameObject.GetComponent<Collider2D>());
		}
	}
}
