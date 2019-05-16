using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IdiomaScene : MonoBehaviour
{
    private int selected;
    private GameObject flare;
    private GameObject idioma;
    // Start is called before the first frame update
    void Start()
    {
        flare = transform.Find("Flare").gameObject;
        idioma = GameObject.Find("Idioma");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Vertical") > 0) {
            if(selected == 1) {
                flare.transform.Translate(new Vector3(0, 2.5f, 0));
                selected = 0;
            }
        }
        if (Input.GetAxis("Vertical") < 0) {
            if (selected == 0) {
                flare.transform.Translate(new Vector3(0, -2.5f, 0));
                selected = 1;
            }
        }
        if(Input.GetButtonDown("Jump") || Input.GetKeyDown("return")) {
            if(selected == 0) {
                idioma.GetComponent<Idioma>().SetIdioma(IdiomaEnum.Portugues);
            } else if(selected == 1) {
                idioma.GetComponent<Idioma>().SetIdioma(IdiomaEnum.English);
            }
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
