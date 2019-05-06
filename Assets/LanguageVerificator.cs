using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageVerificator : MonoBehaviour {
    public static bool verificator;
    // Start is called before the first frame update
    void Update(){
        verificator = OptionMenu.English;
        Debug.Log(verificator);
        DontDestroyOnLoad(this.gameObject);
    }
}
