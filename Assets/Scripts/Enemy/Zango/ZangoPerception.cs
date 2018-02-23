using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZangoPerception {

	private Transform playerPos;

	public void setPlayerPos(Transform playerPos) {
		this.playerPos = playerPos;
	}

	public Transform getPlayerPos() {
		return this.playerPos;
	}
}
