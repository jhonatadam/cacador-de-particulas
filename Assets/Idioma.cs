using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idioma : MonoBehaviour
{
    private IdiomaEnum language;
    public static Idioma instance;
    // Start is called before the first frame update
    void Awake() {
        if (instance != null) {
            if (instance != this) {
                Destroy(this.gameObject);
            }
        } else {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetIdioma(IdiomaEnum l) {
        language = l;
    }
    public IdiomaEnum GetIdioma() {
        return language;
    }
}
