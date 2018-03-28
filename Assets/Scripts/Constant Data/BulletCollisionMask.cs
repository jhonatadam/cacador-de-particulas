using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionMask {

	private static string[] data = new string[] {"Player", "Ground", "Door"};

	public static List<string> collisionMask = new List<string> (data);

	public static bool canCollide(string tag) {
		return collisionMask.Contains (tag);
	}

}
