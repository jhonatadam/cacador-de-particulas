using UnityEngine;
using System.Collections;

public class Paralax : MonoBehaviour {

	public Player player;
	public CameraController camera;
	public float offset;
	private float real_offset;

	[HideInInspector]
	public Camera mainCamera;

	void Start () {
		try {
			// Buscando referência da Camera.
			camera = GameObject.Find ("MainCamera").GetComponent<CameraController> ();
			mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
		} catch {
			Debug.Log ("não encontrou o objeto Camera");
			camera = null;
		}
	}

	void Update () {
		if (IsInCamera ()) {
			real_offset = offset;
		} else {
			real_offset = 0;
		}

		transform.Translate (new Vector3((real_offset * camera.GetPreviousPositionDifference ()).x, 0, 0));
	}

	public bool IsInCamera() {
		if (mainCamera != null) {
			Vector3 screenPoint = mainCamera.WorldToViewportPoint (transform.position);
			bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
			return onScreen;
		} else {
			return false;
		}

	}
}
