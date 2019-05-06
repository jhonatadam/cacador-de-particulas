using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenu : MonoBehaviour {
    public static bool English = false;

    // Start is called before the first frame update
    public void PortuguesLanguage(){
        English = false;
        Debug.Log(English);
    }

    public void EnglishLanguage(){
        English = true;
        Debug.Log(English);
    }
}
