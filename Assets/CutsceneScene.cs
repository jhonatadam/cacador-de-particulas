using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneScene : MonoBehaviour {
    private UnityEngine.Video.VideoPlayer vp;
    public string nextSceneName;
	// Use this for initialization
	void Start () {
        vp = GetComponent<UnityEngine.Video.VideoPlayer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!vp.isPlaying)
        {
            SceneManager.LoadScene(nextSceneName);
        }
	}
}
