using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroy : MonoBehaviour {
	private ParticleSystem[] ps;
	// Use this for initialization
	void Start () {
		ps = GetComponentsInChildren<ParticleSystem> ();
		print (ps.Length);
	}
	
	// Update is called once per frame
	void Update () {
		int count = 0;
		for (int i = 0; i < ps.Length; i++) {
			count += ps [i].particleCount;
		}
		if (count == 0) {
			Destroy (gameObject);
		}
	}
}
