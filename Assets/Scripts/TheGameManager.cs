using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class TheGameManager : MonoBehaviour {
	public static TheGameManager gameManager = null;

	[Header("Dependencies")]
	public NPCManager npcManager;
	public AsteroidManager asteroidManager;
	public ToggleGVR toggleGVR;

	[Space(5f)]
	[Header("Prefabs")]
	public GameObject playerPrefab;
	public GameObject playerShipPrefab;
	private Transform _player;
	private Transform _playerShipPrefab;

	[Space(5f)]
	[Header("Variables")]
	private FPSPlayerInput _fpsPlayerInput;
	private Transform _playerSpawn;
	public bool vrActive = false;

	void Awake () {
		if (gameManager == null) {
			DontDestroyOnLoad(gameObject);
			gameManager = this;
		} else if (gameManager != this) {
			Destroy(gameObject);
		}
	}

	void OnEnable () {
		SceneManager.sceneLoaded += OnSceneLoaded;
	}
	void OnDisable () {
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	void Start () {
		ToggleVR(vrActive);
	}

	void OnSceneLoaded (Scene scene, LoadSceneMode mode) {
		InitLevel();
	}

	void InitLevel () {
		GameObject g = GameObject.FindWithTag("PlayerSpawn");
		if (g) {
			_playerSpawn = g.transform;
			_player = Instantiate(playerPrefab, _playerSpawn.position, _playerSpawn.rotation).transform;
			if (SceneManager.GetActiveScene().name != "BaseMenu") _player.GetComponent<Pure_FPP_Controller>().GetInShip();
		}

		npcManager = FindObjectOfType(typeof(NPCManager)) as NPCManager;
		asteroidManager = FindObjectOfType(typeof(AsteroidManager)) as AsteroidManager;
	}
	
	public void ToggleVR (bool b) {
		vrActive = b;
		XRSettings.enabled = vrActive;
		if (_fpsPlayerInput) _fpsPlayerInput.ToggleVR(b);
		toggleGVR.ToggleVR(b);
	}

	public void LoadLevel (string s) {
		SceneManager.LoadScene(s);
	}
}
