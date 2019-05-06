using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageVerificator : MonoBehaviour {
    public bool verificator;
    // Start is called before the first frame update
    void Awake(){
        verificator = OptionMenu.English;
        DontDestroyOnLoad(this.gameObject);
    }
}
