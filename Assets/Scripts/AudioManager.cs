using UnityEngine;

[System.Serializable]
public class Sound{
	public string name;
	public AudioClip clip;

	[Range(0.0f, 1.0f)]
	public float volume = 0.7f;
	[Range(0.5f, 1.5f)]
	public float pitch = 1f;

	[Range(0.0f, 0.5f)]
	public float randomVolume = 0.1f;
	[Range(0.0f, 0.5f)]
	public float randomPitch = 0.1f;

	public bool loop = false;

	private AudioSource source;
	public bool isTrack = false;
	public void SetSource(AudioSource _source){
		source = _source;
		source.volume = volume;
		source.clip = clip;
		source.loop = loop;
	}
	public void Play(){
		source.volume = volume * (1 + Random.Range(-randomVolume/2f, randomVolume/2f));
		source.pitch = pitch * (1 + Random.Range(-randomPitch/2f, randomPitch/2f));
		source.Play ();
	}
	public void Stop(){
		source.Stop ();
	}
	public AudioSource GetSource(){
		return source;
	}
}

public class AudioManager : MonoBehaviour {
	public static AudioManager instance;

	[SerializeField]
	Sound[] sounds;
	private string isPlaying;
	void Awake(){
		if (instance != null) {
			if (instance != this) {
				Destroy (this.gameObject);
			}
		} else {
			instance = this;
			DontDestroyOnLoad (this);
		}
		for (int i = 0; i < sounds.Length; i++) {
			GameObject _go = new GameObject ("Sound_"+i+"_"+sounds[i].name);
			_go.transform.SetParent (this.transform);
			sounds[i].SetSource(_go.AddComponent<AudioSource> ());
		}
	}
	public void PlaySound(string _name){
		
		for (int i = 0; i < sounds.Length; i++) {
			if (sounds [i].name == _name) {
				if (sounds [i].isTrack) {
					isPlaying = _name;
				}
				sounds [i].Play ();
				return;
			}
		}
		//não achou nenhum som
		Debug.Log("não existe som chamado " + _name + " na lista. Confira o som no AudioManager ou adicione.");
	}
	public void StopSound(string _name){
		for (int i = 0; i < sounds.Length; i++) {
			if (sounds [i].name == _name) {
				if (sounds [i].isTrack && isPlaying == _name) {
					isPlaying = "";
				}
				sounds [i].Stop();
				return;
			}
		}
		//não achou nenhum som
		Debug.Log("não existe som chamado " + _name + " na lista. Confira o som no AudioManager ou adicione.");
	}
	public bool IsPlaying(string _name){
		return _name == isPlaying;
	}
}
