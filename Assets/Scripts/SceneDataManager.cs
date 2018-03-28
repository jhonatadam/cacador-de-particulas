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

	private TempData tempData;

	public GameObject camera;

	public AudioSource music;
	
	public Elevator[] elevators;

	public Door[] doors;

	public Dialogue[] dialogues;

	public MapManager mapManager;

	//Dados do player
	private PlayerData playerData = new PlayerData();

	// Dados da cena
	private SceneData sceneData = new SceneData();

	// Dados da trilha sonora
	private Soundtrack soundtrack = new Soundtrack ();

	private string playerName = "Player.json";

	void Start () {
		player = GameObject.Find ("Player");
		tempData = GameObject.Find ("TempData").GetComponent<TempData> ();
		mapManager = GameObject.Find ("Map").GetComponent<MapManager>();

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

		//======================================================================\\
		//=============== ATUALIZANDO AS INFORMAÇÕES DA CENA ===================\\
		//======================================================================\\

		foreach (Door door in doors) {
			door.state = tempData.doorState [door.id];
		}

		foreach (Elevator e in elevators) {
			// pegando o andar que o elevador deve estar
			int i = tempData.elevatorFloors [e.id];

			e.currentFloor = i;
			e.nextFloor = i;
			e.transform.position = 
				new Vector3 (e.transform.position.x, e.floorsPosition[i], e.transform.position.z);
		}

		//Informando as áreas descobertas do mapa
		mapManager.discoveredMapBlockers = tempData.discoveredMapBlockers;
		mapManager.updateMap ();

		//diálogos
		foreach (Dialogue dialogue in dialogues) {
			dialogue.over = tempData.dialoguesOver [dialogue.id];
		}

		//======================================================================\\
		//======================================================================\\
			
	}

	void OnApplicationQuit () {
		PlayerPrefs.DeleteAll ();
	}

	void OnDestroy () {
		// salvando informações da cena (estado dos elevadores
		// e das porta)
		//string sceneJson = JsonUtility.ToJson (sceneData);
		//PlayerPrefs.SetString (sceneName, sceneJson);
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
		foreach (Elevator elevator in elevators) {
			tempData.elevatorFloors [elevator.id] = elevator.currentFloor;
		}

		// portas
		foreach (Door door in doors) {
			tempData.doorState [door.id] = door.state; 
		}

		//Áreas do mapa
		tempData.discoveredMapBlockers = mapManager.discoveredMapBlockers;

		//diálogos
		foreach (Dialogue dialogue in dialogues) {
			tempData.dialoguesOver [dialogue.id] = dialogue.over; 
		}
	}

	public void UpdateSoundtrack () {
		//soundtrack.currentTime = music.time;
	}

}
