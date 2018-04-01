using System.Collections;
using UnityEngine;

public class DoorPanel : MonoBehaviour {


	public ContactCheck contactCheck;

	public SpriteRenderer ledSr;
	public SpriteRenderer lightSr;
	private SpriteRenderer ledPorta;
	private SpriteRenderer LightPorta;

	// cor fixa do painel (essa cor varia de intensidade)
	private Color panelColor;

	public Door door;

	// audio do painel
	public AudioClip lockedSound;
	public AudioClip unlockedSound;
	public AudioClip doorOpening;

	private AudioSource audioSource;

	private Player player;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent <AudioSource> ();
		ledPorta = GameObject.Find ("LedPorta").GetComponent<SpriteRenderer> ();
		LightPorta = GameObject.Find ("LightPorta").GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (player == null) {
			player = GameObject.Find ("Player").GetComponent<Player> ();
		}
		if (door.state == DoorState.Locked) {
			ledSr.color = new Color (1, 0, 0, 1);
			panelColor = new Color (1, 1, 1, 1);
		} else if (door.state == DoorState.Unlocked) {
			ledSr.color = new Color (0, 0.7f, 1, 1);
			panelColor = new Color (1, 1, 1, 1);
		} else {
			ledSr.color = new Color (0, 0.8f, 0, 1);
			panelColor = new Color (1, 1, 1, 1);
		}

		// sr.color = panelColor * VariationValue ();
		lightSr.color = LightColor ();
		ledPorta.color = ledSr.color;
		LightPorta.color = LightColor ();
	}

	public void Push () {
		if (contactCheck.getIsInContact ()) {
			if (door.state == DoorState.Locked) {
				audioSource.PlayOneShot (lockedSound);
			} else if (door.state == DoorState.Unlocked && (door.necessaryCard == CardEnum.None ? true : player.hasCard(door.necessaryCard))) {
				door.state = DoorState.Opened;
				audioSource.PlayOneShot (unlockedSound);
				audioSource.PlayOneShot (doorOpening);
			}
		}
	}

	private Color LightColor () {
		Color color = new Color ();

		color.r = ledSr.color.r;
		color.g = ledSr.color.g;
		color.b = ledSr.color.b;
		color.a = 0.20f;

		return color;
	}

	private Vector4 VariationValue () {
		float value = Mathf.Abs(Mathf.Sin (Time.time));
		return new Vector4 (value, value, value, 1);
	}

	private void OnEnable() {
		EventsManager.onInteract += Push;
	}

	private void OnDisable() {
		EventsManager.onInteract -= Push;
	}
}
