using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Creditos : MonoBehaviour {
    public float startTime = 0;
	public float delay = 5;
	private float time = 0;
	private int current = 0;
	private float lastChange = -5;
	private AudioManager audioManager;
    public GameObject logos;
    private bool logosEnabled = false;
    public GameObject background;
    public GameObject background2;

    // Use this for initialization
    void Start () {
		audioManager = AudioManager.instance;
		audioManager.PlaySound ("Creditos");
		foreach (Transform child in transform) {
			child.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		}
        //logos = GameObject.Find("Logos");
        //background = GameObject.Find("Background");
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
        if (time < startTime) {
            return;
        }
		if (time - lastChange > delay && current <= transform.childCount) {
            if (current < transform.childCount) {
                foreach (Transform child in transform) {
                    if (child == transform.GetChild(current)) {
                        child.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    } else {
                        child.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    }
                }
            }
			current++;
			lastChange = time;
		}
        if (current > transform.childCount && !logosEnabled) {
            background.GetComponent<GradientColorChanger>().enabled = true;
            background2.GetComponent<GradientColorChanger>().enabled = true;

            foreach (Transform child in logos.transform) {
                child.GetComponent<SpriteRenderer>().enabled = true;
            }
            logosEnabled = true;
        }
		if (time > (transform.childCount + 4) * delay) {
			if (GameObject.Find("TempData") != null && GameObject.Find("TempData").GetComponent<TempData>().finalDoJogo) {
				#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
				#else
				Application.Quit();
				#endif
			} else {
				audioManager.StopSound("Creditos");
				SceneManager.LoadScene ("NewMenuTitle");
			}

		}

		if(Input.GetButtonDown("Jump")){
			audioManager.StopSound("Creditos");
			SceneManager.LoadScene ("NewMenuTitle");
		}
	}
}
