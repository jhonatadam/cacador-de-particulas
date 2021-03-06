﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewPauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;
    public GameObject player;
    public GameObject pauseMenuUI;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(GameIsPaused){
                Resume();
            }else{
                Paused();
            }
        }

    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Paused(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu(){
        pauseMenuUI.SetActive(false);
        Destroy(player);
        Time.timeScale = 1f;
        SceneManager.LoadScene("NewMenuTitle", LoadSceneMode.Single);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
