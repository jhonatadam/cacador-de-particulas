using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTitleAni : MonoBehaviour {
	private int option = 0;
	private GameObject opt1r; //opcao 1 red
	private GameObject opt1b; //opcao 1 blue
	private ParticleSystem opt1Sparkle;
	private GameObject opt2r;
	private GameObject opt2b;
	private ParticleSystem opt2Sparkle;
	private GameObject opt3r;
	private GameObject opt3b;
	private ParticleSystem opt3Sparkle;
	private GameObject opt4r;
	private GameObject opt4b;
	private ParticleSystem opt4Sparkle;
	private float time = 0;
	public float startEffects = 4;
	// Use this for initialization
	void Start () {
		opt1r = GameObject.Find ("opt 1 red");
		opt1b = GameObject.Find ("opt 1 blue");
		opt1Sparkle = GameObject.Find ("opt 1 sparkles").GetComponent<ParticleSystem> ();

		opt2r = GameObject.Find ("opt 2 red");
		opt2b = GameObject.Find ("opt 2 blue");
		opt2Sparkle = GameObject.Find ("opt 2 sparkles").GetComponent<ParticleSystem> ();

		opt3r = GameObject.Find ("opt 3 red");
		opt3b = GameObject.Find ("opt 3 blue");
		opt3Sparkle = GameObject.Find ("opt 3 sparkles").GetComponent<ParticleSystem> ();

		opt4r = GameObject.Find ("opt 4 red");
		opt4b = GameObject.Find ("opt 4 blue");
		opt4Sparkle = GameObject.Find ("opt 4 sparkles").GetComponent<ParticleSystem> ();

		opt1r.GetComponent<SpriteRenderer> ().enabled = false;
		opt1b.GetComponent<SpriteRenderer> ().enabled = false;
		opt1Sparkle.Stop ();
		opt2r.GetComponent<SpriteRenderer> ().enabled = false;
		opt2b.GetComponent<SpriteRenderer> ().enabled = false;
		opt2Sparkle.Stop ();
		opt3r.GetComponent<SpriteRenderer> ().enabled = false;
		opt3b.GetComponent<SpriteRenderer> ().enabled = false;
		opt3Sparkle.Stop ();
		opt4r.GetComponent<SpriteRenderer> ().enabled = false;
		opt4b.GetComponent<SpriteRenderer> ().enabled = false;
		opt4Sparkle.Stop ();

	}
	
	// Update is called once per frame
	void Update () {
		if (time < startEffects) {
			time += Time.deltaTime;
			if (time >= startEffects) {
				opt1r.GetComponent<SpriteRenderer> ().enabled = true;
				opt1b.GetComponent<SpriteRenderer> ().enabled = true;
				opt1Sparkle.Play ();
			}
		}
		if (Input.GetKeyDown ("down")) {
			option++;
			if (option > 3){
				option = 0;
			}
			SelectOption (option);
			return;
		}
		if (Input.GetKeyDown ("up")) {
			option--;
			if (option < 0){
				option = 3;
			}
			SelectOption (option);
			return;
		}
	}

	void SelectOption(int opt){
		if (opt == 0) {
			opt1r.GetComponent<SpriteRenderer> ().enabled = true;
			opt1b.GetComponent<SpriteRenderer> ().enabled = true;
			opt1Sparkle.Play ();
			opt2r.GetComponent<SpriteRenderer> ().enabled = false;
			opt2b.GetComponent<SpriteRenderer> ().enabled = false;
			opt2Sparkle.Stop ();
			opt3r.GetComponent<SpriteRenderer> ().enabled = false;
			opt3b.GetComponent<SpriteRenderer> ().enabled = false;
			opt3Sparkle.Stop ();
			opt4r.GetComponent<SpriteRenderer> ().enabled = false;
			opt4b.GetComponent<SpriteRenderer> ().enabled = false;
			opt4Sparkle.Stop ();
			return;
		}
		if (opt == 1) {
			opt2r.GetComponent<SpriteRenderer> ().enabled = true;
			opt2b.GetComponent<SpriteRenderer> ().enabled = true;
			opt2Sparkle.Play ();
			opt1r.GetComponent<SpriteRenderer> ().enabled = false;
			opt1b.GetComponent<SpriteRenderer> ().enabled = false;
			opt1Sparkle.Stop ();
			opt3r.GetComponent<SpriteRenderer> ().enabled = false;
			opt3b.GetComponent<SpriteRenderer> ().enabled = false;
			opt3Sparkle.Stop ();
			opt4r.GetComponent<SpriteRenderer> ().enabled = false;
			opt4b.GetComponent<SpriteRenderer> ().enabled = false;
			opt4Sparkle.Stop ();
			return;
		}
		if (opt == 2) {
			opt3r.GetComponent<SpriteRenderer> ().enabled = true;
			opt3b.GetComponent<SpriteRenderer> ().enabled = true;
			opt3Sparkle.Play ();
			opt2r.GetComponent<SpriteRenderer> ().enabled = false;
			opt2b.GetComponent<SpriteRenderer> ().enabled = false;
			opt2Sparkle.Stop ();
			opt1r.GetComponent<SpriteRenderer> ().enabled = false;
			opt1b.GetComponent<SpriteRenderer> ().enabled = false;
			opt1Sparkle.Stop ();
			opt4r.GetComponent<SpriteRenderer> ().enabled = false;
			opt4b.GetComponent<SpriteRenderer> ().enabled = false;
			opt4Sparkle.Stop ();
			return;
		}
		if (opt == 3) {
			opt4r.GetComponent<SpriteRenderer> ().enabled = true;
			opt4b.GetComponent<SpriteRenderer> ().enabled = true;
			opt4Sparkle.Play ();
			opt2r.GetComponent<SpriteRenderer> ().enabled = false;
			opt2b.GetComponent<SpriteRenderer> ().enabled = false;
			opt2Sparkle.Stop ();
			opt1r.GetComponent<SpriteRenderer> ().enabled = false;
			opt1b.GetComponent<SpriteRenderer> ().enabled = false;
			opt1Sparkle.Stop ();
			opt3r.GetComponent<SpriteRenderer> ().enabled = false;
			opt3b.GetComponent<SpriteRenderer> ().enabled = false;
			opt3Sparkle.Stop ();
			return;
		}
	}
	public int GetOption(){
		return option;
	}
}
