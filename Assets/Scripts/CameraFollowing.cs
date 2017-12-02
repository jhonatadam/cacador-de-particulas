using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour {
	public GameObject aim;
	// Use this for initialization
	void Start () {
		aim = GameObject.Find ("Anti-Kaon");
	}
	
	// Update is called once per frame
	void Update () {
		if (aim != null) {
			transform.position = new Vector3(aim.transform.position.x, aim.transform.position.y, transform.position.z);
		}
	}
}
