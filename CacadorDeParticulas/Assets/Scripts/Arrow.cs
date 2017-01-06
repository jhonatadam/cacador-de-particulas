using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {
	
	public float speed;
	public float amplitude;
	public string direction;

	private float amplitudeSum = 0.0f;
	private Vector3 initialPosition;
	private SpriteRenderer sr;
	public SpriteRenderer keySr;

	void Start () {
		initialPosition = transform.position;
		sr = GetComponent<SpriteRenderer> ();
	}

	void FixedUpdate () {
		if (direction == "Up") {
			moveArrow ('Y', -1);
		} else if (direction == "Down") {
			moveArrow ('Y', 1);
		} else if (direction == "Right") {
			moveArrow ('X', 1);
		} else if (direction == "Left"){
			moveArrow ('X', -1);
		}

		amplitudeSum += speed;

		if (amplitudeSum > amplitude) {
			transform.position = initialPosition;
			amplitudeSum = 0.0f;
		}
			
		Color tempColor = new Color (sr.color.r, sr.color.g, sr.color.b, (1-amplitudeSum/amplitude));
		sr.color = tempColor;
		keySr.color = tempColor;
	}

	void moveArrow (char axis, int direct) {
		if (axis == 'X') {
			transform.Translate (new Vector3(direct * speed, 0, 0));
		} else if (axis == 'Y') {
			transform.Translate (new Vector3(0, direct * speed, 0));			
		}
	}
}
