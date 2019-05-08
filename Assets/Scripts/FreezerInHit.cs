using System.Collections;
using UnityEngine;

public class FreezerInHit : MonoBehaviour {
    public static FreezerInHit instance;
    [Range(0f, 8f)]
	public float duration = 1f;

	bool _isFronzen = false;
	float _pendingFreezeDuration = 0f;


	// Update is called once per frame

	void Awake(){
        if (instance != null) {
            if (instance != this) {
                Destroy(this.gameObject);
            }
        } else {
            instance = this;
            DontDestroyOnLoad(this);
        }
	}
	
	void Update () {
		if(_pendingFreezeDuration > 0 && !_isFronzen){
			StartCoroutine(DoFreeze());
		}
	}

	public void Freeze(){
		_pendingFreezeDuration = duration;
	}

	IEnumerator DoFreeze(){
		_isFronzen = true;
		var original = Time.timeScale;
		Time.timeScale = 0f;

		yield return new WaitForSecondsRealtime(duration);

		Time.timeScale = original;
		_pendingFreezeDuration = 0f;
		_isFronzen = false;
	}
}
