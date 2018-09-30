using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundtrackController : MonoBehaviour {
    public string trackname;
    private AudioManager audioManager;
	// Use this for initialization
	void Start () {
        audioManager = AudioManager.instance;
	}
	
	// Update is called once per frame
	void Update () {
        if (!audioManager.IsPlaying(trackname)) {
            audioManager.PlaySound(trackname);
        }
    }

}
