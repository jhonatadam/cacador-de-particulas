using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundtrackController : MonoBehaviour {

	// Use this for initialization
	private AudioSource audio;
	private float newVolume;
	void Start () {
		audio = gameObject.GetComponent<AudioSource> ();
		newVolume = audio.volume;
	}
	
	// Update is called once per frame
	void Update () {
		LerpVolume (newVolume);
	}

	void LerpVolume(float volume){
		audio.volume = Vector2.Lerp (new Vector2 (audio.volume, 0), new Vector2 (volume, 0), Time.deltaTime*5).x;
	}
	public void FadeOut(){
		newVolume = 0;
	}
}
