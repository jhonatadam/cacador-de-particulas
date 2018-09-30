using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CineMachineFollow : MonoBehaviour {
    private Cinemachine.CinemachineVirtualCamera cmv;
    public GameObject player;
    private bool foundplayer;
    private bool finished;
	// Use this for initialization
	void Start () {
        cmv = GetComponent<Cinemachine.CinemachineVirtualCamera>();
        player = GameObject.Find("Player");
        if (player != null) {
            foundplayer = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!foundplayer) {
            player = GameObject.Find("Player");
            if(player != null) {
                foundplayer = true;
            }
        }
        if (foundplayer && !finished) {
            cmv.Follow = player.transform;
            finished = true;
        }
	}
}
