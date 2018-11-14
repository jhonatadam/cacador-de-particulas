using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Incubadora : MonoBehaviour {
	private GameObject player;
	private AudioManager audioManager;
	// Use this for initialization
	void Start () {
        if (GameObject.Find("Player") == null) {
            GameObject player = Resources.Load<GameObject>("Prefabs/Player");
            audioManager = AudioManager.instance;
            if (player != null) {
                audioManager.PlaySound("Tutorial");
                player = Instantiate(player);
                player.name = "Player";
                player.transform.position = transform.position;
                player.GetComponent<Player>().SetResetPT(player.transform.position);
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                DontDestroyOnLoad(player);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
