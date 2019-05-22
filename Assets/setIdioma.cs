using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class setIdioma : MonoBehaviour
{
    private GameObject idioma;
    // Start is called before the first frame update
    void Start()
    {
        idioma = GameObject.Find("IdiomaData");
    }

    public void Portuguese(){
        print(idioma.GetComponent<Idioma>());
        idioma.GetComponent<Idioma>().SetIdioma(IdiomaEnum.Portugues);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void English(){
        print(idioma.GetComponent<Idioma>());
        idioma.GetComponent<Idioma>().SetIdioma(IdiomaEnum.English);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
  
}
