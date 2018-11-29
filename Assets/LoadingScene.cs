using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour {
    // Use this for initialization
    public float wait = 0.5f;
    private GameObject player;
    private float gravityscale;
    private AudioManager audioManager;
	void Start () {
        player = GameObject.Find("Player");

        if (player != null) {
            gravityscale = player.GetComponent<Rigidbody2D>().gravityScale;
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            player.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
        audioManager = AudioManager.instance;
        audioManager.StopAnySound();
    }
	
	// Update is called once per frame
	void Update () {
        wait -= Time.deltaTime;
        if (wait <= 0) {
            if (player != null) {
                player.GetComponent<Rigidbody2D>().gravityScale = gravityscale;
            }
            SceneManager.LoadScene(GameObject.Find("NextSceneName").GetComponent<NextSceneName>().nextSceneName);
        }
    }
}
