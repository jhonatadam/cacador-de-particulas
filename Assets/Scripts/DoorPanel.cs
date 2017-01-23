using System.Collections;
using UnityEngine;

public class DoorPanel : MonoBehaviour {


	public ContactCheck contactCheck;

	public SpriteRenderer ledSr;
	public SpriteRenderer lightSr;
	private SpriteRenderer sr;

	// cor fixa do painel (essa cor varia de intensidade)
	private Color panelColor;

	public Door door;

	// audio do painel
	public AudioClip lockedSound;
	public AudioClip unlockedSound;
	public AudioClip doorOpening;

	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
		audioSource = GetComponent <AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (door.state == DoorState.Locked) {
			ledSr.color = new Color (1, 0, 0, 1);
			panelColor = new Color (1, 0, 0, 1);
		} else if (door.state == DoorState.Unlocked) {
			ledSr.color = new Color (1, 0, 0, 1);
			panelColor = new Color (0, 1, 1, 1);
		} else {
			ledSr.color = new Color (0, 1, 0, 1);
			panelColor = new Color (0, 1, 1, 1);
		}

		if (contactCheck.getIsInContact () && Input.GetButtonDown ("Fire1")) {
			if (door.state == DoorState.Locked) {
				audioSource.PlayOneShot (lockedSound);
			} else if (door.state == DoorState.Unlocked) {
				door.state = DoorState.Opened;
				audioSource.PlayOneShot (unlockedSound);
				audioSource.PlayOneShot (doorOpening);
			}
		}

		sr.color = panelColor * VariationValue ();
		lightSr.color = LightColor ();
	}

	private Color LightColor () {
		Color color = new Color ();

		color.r = Mathf.Max(ledSr.color.r, sr.color.r);
		color.g = Mathf.Max(ledSr.color.g, sr.color.g);
		color.b = Mathf.Max(ledSr.color.b, sr.color.b);
		color.a = 0.20f;

		return color;
	}

	private Vector4 VariationValue () {
		float value = Mathf.Abs(Mathf.Sin (Time.time));
		return new Vector4 (value, value, value, 1);
	}
}
