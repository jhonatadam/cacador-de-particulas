using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class PlayerInfo
{
	public float health;
	public float energy;
	public bool[] doors;
	public int sceneId;
	public Vector3 scenePosition;
}

public class Player : MonoBehaviour {

	PlayerInfo info = new PlayerInfo();

	public float speed;
	public float jumpForce;

	public Vector3 previousPosition;

	public GroundCheck groundCheck;
	public PlayerAnimUpdater animUpdater;

	private Rigidbody2D rb2d;

	private bool updateOn = true;

	void Start () {
		print (JsonUtility.ToJson(info));


		rb2d = GetComponent<Rigidbody2D> ();
		previousPosition = transform.position;
	}

	void OnDestroy () {
		print ("Salvando estado do player");
	}

	void Update () {
		if (updateOn) {
			if (groundCheck.isGrounded ()) {
				if (Input.GetKeyDown (KeyCode.Space)) {
					rb2d.AddForce (new Vector2 (0, jumpForce));
				}
			}		
		}
	}

	void FixedUpdate() {
		if (updateOn) {
			float horizontalMoviment = Input.GetAxis ("Horizontal");
			UpdatePlayerVelocity (horizontalMoviment);
			animUpdater.UpdateAnim (gameObject);
		}


	}

	void LateUpdate() {
		previousPosition = transform.position;
	}

	private void UpdatePlayerVelocity (float horizontalMovement) {
		rb2d.velocity = new Vector2 (horizontalMovement * speed, rb2d.velocity.y);
	}

	public Vector3 GetPreviousPositionDifference () {
		return transform.position - previousPosition;
	}
		
	public void SetUpdateOn(bool value) {
		updateOn = value;
		rb2d.velocity = new Vector2 (0, 0);
	}

	public bool GetUpdateOn() {
		return updateOn;
	}

}
