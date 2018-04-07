using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortSparkle : MonoBehaviour {
	private ParticleSystem ps;
	private float life;
	private AudioManager audioManager;
	// Use this for initialization
	void Start () {
		audioManager = AudioManager.instance;
		audioManager.PlaySound ("Sparkle");
		ps = GetComponent<ParticleSystem> ();
		life = ps.main.duration + ps.main.startLifetime.constantMax;
		Destroy (gameObject, life);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
