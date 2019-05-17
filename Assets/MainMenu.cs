using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    // Start is called before the first frame update
    public void Play(){
        SceneManager.LoadScene("Scenes/Cutscene");
    }

    public void Quit(){

        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
