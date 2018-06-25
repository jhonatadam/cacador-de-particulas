using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundtrackPlayer : MonoBehaviour {
    private AudioManager audioManager;
    public string trackName;
	// Use this for initialization
	void Start () {
        audioManager = AudioManager.instance;
	}
	
	// Update is called once per frame
	void Update () {
        if (!audioManager.IsPlaying(trackName)) {
            audioManager.PlaySound(trackName);
        }
	}
}
