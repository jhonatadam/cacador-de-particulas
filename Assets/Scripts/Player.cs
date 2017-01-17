using UnityEngine;
using System;
using System.IO;
using System.Collections;

[Serializable]
public class PlayerInfo
{
	public float health;
	public float energy;

	public bool[] doors;

	public bool isFacingRight;
	public Vector3 position;

	private string fileName = "PlayerInfo.json";
	private string filePath = "Assets/Resources/JSONData/";


	public void Save () {

		#if UNITY_STANDALONE
		filePath = "Particles_Data/Resources/";
		#endif

		#if UNITY_EDITOR
		filePath = "Assets/Resources/JSONData/";
		#endif

		string jsonInfo = JsonUtility.ToJson (this);

		using (FileStream fs = new FileStream (filePath + fileName, FileMode.Create)) {
			using (StreamWriter sw = new StreamWriter (fs)) {
				sw.Write (jsonInfo);
			}
		}

		#if UNITY_EDITOR
		UnityEditor.AssetDatabase.Refresh ();
		#endif

	}

	public void Load () {
		
		#if UNITY_STANDALONE
		filePath = "Particles_Data/Resources/";
		#endif

		#if UNITY_EDITOR
		filePath = "Assets/Resources/JSONData/";
		#endif

		string jsonInfo;//= JsonUtility.ToJson (this);

		using (FileStream fs = new FileStream (filePath + fileName, FileMode.Open)) {
			using (StreamReader sr = new StreamReader (fs)) {
				jsonInfo = sr.ReadToEnd();
				JsonUtility.FromJsonOverwrite(jsonInfo, this);
			}
		}

		#if UNITY_EDITOR
		UnityEditor.AssetDatabase.Refresh ();
		#endif
	
	}

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
		info.Load ();

		rb2d = GetComponent<Rigidbody2D> ();
		previousPosition = transform.position;
	}

	void OnDestroy () {
		info.Save ();
		print ("saved");
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
