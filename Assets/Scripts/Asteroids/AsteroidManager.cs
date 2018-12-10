using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour {
	public VideoAudioManager playerMissionAudio;
	public NPCManager npcManager;

	public float levelSize;
	public GameObject[] asteroidPrefabs;
	public int asteroidNumber;
	private int _asteroidCounter;

	public List<Asteroid> asteroids;

	public float minSize;
	public float maxSize;

	public int asteroidsToGivePointer;
	public float pointerTimeCount;

	public List<int> asteroidsToSpawnEnemies = new List<int>();

	void Start () {
		_asteroidCounter = asteroidNumber * 9;
		asteroids = new List<Asteroid>();
		for (int i = 0; i < asteroidNumber; i++) {
			GameObject a = Instantiate(asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)], Random.insideUnitSphere * levelSize, Random.rotation);
			a.transform.localScale *= Random.Range(minSize, maxSize);
			a.GetComponent<Asteroid>().asteroidManager = this;
		}
	}
	
	public void AddThisAsteroid (Asteroid a) {
		asteroids.Add(a);
	}
	public void RemoveThisAsteroid (Asteroid a) {
		asteroids.Remove(a);

		CountAsteroids();
	}

	public void CountAsteroids () {
		_asteroidCounter--;

		int totalAsteroids = asteroidNumber * 9;
		int fewAsteroids = Mathf.RoundToInt(totalAsteroids / 4);
		int halfAsteroids = fewAsteroids * 2;
		int mostAsteroids = fewAsteroids * 3;
		if (_asteroidCounter == mostAsteroids) playerMissionAudio.Mission_25();
		if (_asteroidCounter == halfAsteroids) playerMissionAudio.Mission_50();
		if (_asteroidCounter == fewAsteroids) playerMissionAudio.Mission_75();
		if (_asteroidCounter == asteroidsToGivePointer) StartCoroutine(ActivatePointer());
		if (asteroids.Count <= 0) playerMissionAudio.MissionEnd();

		SpawnEnemies();
	}

	IEnumerator ActivatePointer () {
		yield return new WaitForSeconds(pointerTimeCount);
		if (asteroids.Count > 0) playerMissionAudio.ActivatePointer();
	}

	void SpawnEnemies () {
		if (asteroidsToSpawnEnemies.Count > 0) if ((asteroidNumber * 9) - _asteroidCounter == asteroidsToSpawnEnemies[0]) {
			npcManager.SpawnNPCs(EntityType.EnemyShip, 6);
			asteroidsToSpawnEnemies.RemoveAt(0);
		}
	}
}
