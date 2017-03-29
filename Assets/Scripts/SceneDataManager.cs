using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData {

	public string currentScene;

	public float health;
	public float energy;

	// Permissao das portas
	public bool[] doors;

	public bool isFacingRight;
	public Vector3 position;
	public Vector2 velocity;

}

[Serializable]
public class SceneData {

	// ELEVADOR------------------------------------
	// conjunto de andares atuais dos elevadores
	// (o elevador 0 desse vetor é correspondente
	// ao elevador 0 do vetor de elevadores do 
	// SceneDataManager)
	public int[] elevatorCurrentFloor;

	// PORTA---------------------------------------
	// conjunto de estado das portas
	// (mapeado da mesma forma que os elevadores)
	public DoorState[] doorState;

}

[Serializable]
public class Soundtrack {
	// tempo atual
	public float currentTime;
}

public class SceneDataManager : MonoBehaviour {

	public string sceneName;

	public GameObject player;

	public GameObject camera;

	public AudioSource music;
	
	public Elevator[] elevators;

	public Door[] doors;

	//Dados do player
	private PlayerData playerData = new PlayerData();

	// Dados da cena
	private SceneData sceneData = new SceneData();

	// Dados da trilha sonora
	private Soundtrack soundtrack = new Soundtrack ();

	private string playerName = "Player.json";

	void Start () {
		player = GameObject.Find ("Player");


		// definindo nome do arquivo da cena
		sceneName = sceneName + ".json";

		/*
		// carregando player
		string playerJson = LoadJsonFile (filePath + playerFileName);

		if (!playerJson.Equals("")) {
			JsonUtility.FromJsonOverwrite(playerJson, playerData);

			player.transform.position = playerData.position;
			player.GetComponent <Rigidbody2D> ().velocity = playerData.velocity;
			player.GetComponent<SpriteRenderer> ().flipX = !playerData.isFacingRight;

			camera.transform.position = 
				new Vector3 (playerData.position.x, playerData.position.y, camera.transform.position.z);

		}
		*/


		// carregando cena
		if (PlayerPrefs.HasKey (sceneName)) {
			
			string sceneJson = PlayerPrefs.GetString (sceneName);

			if (!sceneJson.Equals ("")) {
				JsonUtility.FromJsonOverwrite (sceneJson, sceneData);

				// carregando elevadores
				for (int i = 0; i < elevators.Length; i++) {
					elevators [i].currentFloor = sceneData.elevatorCurrentFloor [i];
					elevators [i].nextFloor = sceneData.elevatorCurrentFloor [i];

					elevators [i].transform.position = new Vector3 (
						elevators [i].transform.position.x, 
						elevators [i].floorsPosition [elevators [i].currentFloor],
						elevators [i].transform.position.z
					);
				}

				// carregando portas
				for (int i = 0; i < doors.Length; i++) {
					doors [i].state = sceneData.doorState [i]; 
				}
			}
		}
			
	}

	void OnApplicationQuit () {
		PlayerPrefs.DeleteAll ();
	}

	void OnDestroy () {
		// salvando informações da cena (estado dos elevadores
		// e das porta)
		string sceneJson = JsonUtility.ToJson (sceneData);
		PlayerPrefs.SetString (sceneName, sceneJson);
	}

	public void Save () {

	}

	private string LoadJsonFile (string file) {
		string json = "";

		try {
			using (FileStream fs = new FileStream (file, FileMode.Open)) {
				using (StreamReader sr = new StreamReader (fs)) {
					json = sr.ReadToEnd();
				}
			}
		} catch {
		}

		return json;
	}

	private void SaveJsonFile (string file, string json) {
		using (FileStream fs = new FileStream (file, FileMode.Create)) {
			using (StreamWriter sw = new StreamWriter (fs)) {
				sw.Write (json);
			}
		}
	}

	public void UpdatePlayerData () {
		playerData.position = player.transform.position;
		playerData.velocity = player.GetComponent <Rigidbody2D> ().velocity;
		playerData.isFacingRight = !player.GetComponent<SpriteRenderer> ().flipX;
	}

	public void UpdateSceneData () {
		// elevadores
		sceneData.elevatorCurrentFloor = new int[elevators.Length];
		for (int i = 0; i < elevators.Length; i++) {
			sceneData.elevatorCurrentFloor [i] = elevators [i].currentFloor;
		}

		// portas
		sceneData.doorState = new DoorState[doors.Length];
		for (int i = 0; i < doors.Length; i++) {
			sceneData.doorState [i] = doors [i].state; 
		}
	}

	public void UpdateSoundtrack () {
		//soundtrack.currentTime = music.time;
	}

}
