using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroy : MonoBehaviour {
	private ParticleSystem[] ps;
	public bool loop;
	private bool ended = false;
	private Collider2D col;
	private bool hasCollider = false;
	// Use this for initialization
	void Start () {
		ps = GetComponentsInChildren<ParticleSystem> ();
		col = GetComponent<Collider2D> ();
		if (Resources.ReferenceEquals (col, null)) {
			hasCollider = false;
		} else {
			hasCollider = true;
		}
		//print (ps.Length);
	}
	
	// Update is called once per frame
	void Update () {
		if (loop && ended) {
			int count = 0;
			for (int i = 0; i < ps.Length; i++) {
				count += ps [i].particleCount;
			}
			if (count == 0 && ps[0].time > 0.1f) {
				Destroy (gameObject);

			}
		}else if(!loop){
			int count = 0;
			if (hasCollider) {
				col.enabled = false;
				hasCollider = false;
			}
			for (int i = 0; i < ps.Length; i++) {
				count += ps [i].particleCount;
			}
			if (count == 0 && ps[0].time > 0.1f) {
				Destroy (gameObject);
			}
		}
	}
	public void End(){
		if (hasCollider) {
			col.enabled = false;
			hasCollider = false;
		}
		for (int i = 0; i < ps.Length; i++) {
			ps [i].Stop ();
		}
		ended = true;
	}
}
