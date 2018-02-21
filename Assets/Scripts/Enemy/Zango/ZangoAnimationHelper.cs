using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZangoAnimationHelper : MonoBehaviour {

	private ZangoMeleeWeapon weapon;

	void Start() {
		weapon = GetComponentInParent<ZangoMeleeWeapon> ();
	}

	//chamada quando o macahado encosta no chao durante ataque 1
	public void stuckStart() {
		weapon.onStuckStart ();
	}

	//chamada quando o ataque rotatorio inicia
	public void startRotatingChronometer() {
		weapon.startRotatingChronometer ();
	}
}
