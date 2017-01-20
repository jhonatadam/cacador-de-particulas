using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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

	private string playerFileName = "Player.json";
	private string sceneFileName = "";
	private string soundtrackFileName = "Soundtrack.json";
	private string filePath = null;

	// caminhos onde o arquivo deve ser salvo
	private string standaloneSaveFilePath = "Particles_Data/Resources/Save/";
	private string editorSaveFilePath = "Assets/Resources/JSONData/Save/";

	private string standaloneTempFilePath = "Particles_Data/Resources/Temp/";
	private string editorTempFilePath = "Assets/Resources/JSONData/Temp/";

	void Start () {
		// definindo nome do arquivo da cena
		sceneFileName = sceneName + ".json";

		// selecionando caminho dos arquivos
		#if UNITY_STANDALONE
		filePath = standaloneTempFilePath;
		#endif

		#if UNITY_EDITOR
		filePath = editorTempFilePath;
		#endif

		// criar paste de temporários se nao existir
		if (!Directory.Exists (filePath)) {
			Directory.CreateDirectory (filePath);
		}

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


		// carregando cena
		string sceneJson = LoadJsonFile (filePath + sceneFileName);

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

		// carregando trilha sonora
		string stJson = LoadJsonFile (filePath + soundtrackFileName);

		if (!stJson.Equals ("")) {
			JsonUtility.FromJsonOverwrite (stJson, soundtrack);

			music.time = soundtrack.currentTime + 0.1f + Time.deltaTime;
		}

		music.Play (0);

	}

	void OnApplicationQuit () {
		// definindo caminho dos arquivos
		#if UNITY_STANDALONE
		filePath = standaloneTempFilePath;
		#endif

		#if UNITY_EDITOR
		filePath = editorTempFilePath;
		#endif

		// deletando diretorio de arquivos
		// temporários
		Directory.Delete (filePath, true);
	}

	void OnDestroy () {

		// definindo caminho dos arquivos
		#if UNITY_STANDALONE
		filePath = standaloneTempFilePath;
		#endif

		#if UNITY_EDITOR
		filePath = editorTempFilePath;
		#endif

		if (Directory.Exists (filePath)) {
			// salvando dados do player
			string playerJson = JsonUtility.ToJson (playerData);
			SaveJsonFile (filePath + playerFileName, playerJson);

			// salvando dados da cena
			string sceneJson = JsonUtility.ToJson (sceneData);
			SaveJsonFile (filePath + sceneFileName, sceneJson);

			// salvando trilha sonora
			string stJson = JsonUtility.ToJson (soundtrack);
			SaveJsonFile (filePath + soundtrackFileName, stJson);
		}
		
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
		soundtrack.currentTime = music.time;
	}

}
