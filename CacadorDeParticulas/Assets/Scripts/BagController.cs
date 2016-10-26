using UnityEngine;
using System.Collections;

public class BagController : MonoBehaviour {

	public AnimUpdaterController updater;

	void FixedUpdate() {
		updater.UpdateAnim (gameObject);
	}
		
}
