using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

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
			InitLevel();
		} else if (gameManager != this) {
			Destroy(gameObject);
		}
	}

	void InitLevel () {
		GameObject g = GameObject.FindWithTag("PlayerSpawn");
		if (g) {
			_playerSpawn = g.transform;
			_player = Instantiate(playerPrefab, _playerSpawn.position, _playerSpawn.rotation).transform;
		}

		ToggleVR(vrActive);
	}
	
	public void ToggleVR (bool b) {
		vrActive = b;
		XRSettings.enabled = vrActive;
		if (_fpsPlayerInput) _fpsPlayerInput.ToggleVR(b);
		toggleGVR.ToggleVR(b);
	}
}
