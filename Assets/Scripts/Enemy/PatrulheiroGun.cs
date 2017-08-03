using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrulheiroGun : EnemyGun {

	// Update is called once per frame
	void Update () {
		if (behavior.isSeeingThePlayer) {
			Aim ();
			Shoot ();
		}
	}

	public override void Aim () {
		if (behavior.isFacingRight) {
			shootingDirection = Vector2.right;
		} else {
			shootingDirection = Vector2.left;
		}
	}
}
