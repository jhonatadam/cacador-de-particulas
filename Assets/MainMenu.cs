using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    private AudioManager audioManager;
    private GameObject flare;
    private int selected;
    // Start is called before the first frame update
    void Start() {
        selected = 0;
        audioManager = AudioManager.instance;
        audioManager.PlaySound("Titulo");
        flare = GameObject.Find("Flare");
    }
    public void Play(){
        GameObject tempData = GameObject.Find("TempData");
        DontDestroyOnLoad(tempData);
        audioManager.StopSound("Titulo");
        SceneManager.LoadScene("Scenes/Cutscene");
    }

    public void Quit(){

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
	        Application.Quit();
        #endif
    }
    public void Creditos() {
        audioManager.StopSound("Titulo");
        SceneManager.LoadScene("Creditos");
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown("up")) {
        //     if (selected > 0) {
        //         flare.transform.Translate(new Vector3(0, 1.65f, 0));
        //         selected --;
        //     }
        // }
        // if (Input.GetKeyDown("down")) {
        //     if (selected < 2) {
        //         flare.transform.Translate(new Vector3(0, -1.65f, 0));
        //         selected ++;
        //     }
        // }
        // if (Input.GetButtonDown("Jump") || Input.GetKeyDown("return")) {
            
        //     if (selected == 0) {
        //         Play();
        //         return;
        //     } else if (selected == 1) {
        //         Creditos();
        //         return;
        //     } else if (selected == 2) {
        //         Quit();
        //         return;
        //     }
        // }
    }
}
