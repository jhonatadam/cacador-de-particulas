using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneScene : MonoBehaviour {
    private UnityEngine.Video.VideoPlayer vp;
    public string nextSceneName;
    private bool hasStarted = false;
    // Use this for initialization
    void Start() {
        vp = GetComponent<UnityEngine.Video.VideoPlayer>();
    }

    // Update is called once per frame
    void Update() { 
        if(vp.isPlaying){
            hasStarted = true;
        }
        if (!vp.isPlaying && hasStarted)
        {
            SceneManager.LoadScene(nextSceneName);
        }
	}
}
