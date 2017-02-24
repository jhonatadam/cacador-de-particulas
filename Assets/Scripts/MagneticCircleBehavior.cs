using UnityEngine;
using System.Collections;

public class MagneticCircleBehavior : MonoBehaviour {

	// quanto maior, menor a variação
	public float scaleVariation = 1.0f;

	private Vector3 initialScale;

	void Start () {
		initialScale = transform.localScale;
	}

	// Update is called once per frame
	void FixedUpdate () {
		transform.localScale = new Vector3(
			initialScale.x + Random.value / scaleVariation,
			initialScale.y + Random.value / scaleVariation,
			initialScale.z
		);
	}
}