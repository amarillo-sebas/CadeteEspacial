using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour {
	[Header("Dependencies")]
	public Transform npcSpawns;
	public Transform enemySpawns;
	public AsteroidManager asteroidManager;
	private List<Transform> _npcSpawns = new List<Transform>();
	private List<Transform> _enemySpawns = new List<Transform>();
	private List<EntityType> _npcsList = new List<EntityType>();

	[Space(5f)]
	[Header("Variables")]
	public Transform player;
	public float levelSize;
	public int numberOfNpcShips;
	public int numberOfEnemyShips;

	[Space(5f)]
	[Header("NPC Ships")]
	public GameObject npcShip;

	[Space(5f)]
	[Header("Enemy Ships")]
	public GameObject enemyShip;

	[Space(5f)]
	[Header("Lists")]
	public List<NPCShipTransformManager> npcShips = new List<NPCShipTransformManager>();
	public List<EnemyShipTransformManager> enemyShips = new List<EnemyShipTransformManager>();

	void Start () {
		player = GameObject.FindWithTag("Player").transform;

		_npcsList.Add(EntityType.Lopez);
		_npcsList.Add(EntityType.Quispe);
		_npcsList.Add(EntityType.Durflors);

		for (int i = 0; i < npcSpawns.childCount; i++) {
			_npcSpawns.Add(npcSpawns.GetChild(i));
			SpawnNPC(_npcSpawns[i]);
		}
		for (int j = 0; j < enemySpawns.childCount; j++) {
			_enemySpawns.Add(enemySpawns.GetChild(j));
			SpawnEnemy(_enemySpawns[j]);
		}
	}

	void SpawnNPC (Transform t) {
		int r = Random.Range(0, _npcsList.Count);
		EntityType pilotName = _npcsList[r];
		_npcsList.RemoveAt(r);

		NPCShipTransformManager npc = Instantiate(npcShip, t.position, t.rotation).GetComponent<NPCShipTransformManager>();
		npc.entityID.entityType = pilotName;
		npc.npcManager = this;
		npc.asteroidManager = asteroidManager;
		npc.hp.npcManager = this;
		npc.playerCommunicator.npcManager = this;
		npcShips.Add(npc);
	}
	void SpawnEnemy (Transform t) {
		EnemyShipTransformManager enemy = Instantiate(enemyShip, t.position, t.rotation).GetComponent<EnemyShipTransformManager>();
		enemy.npcManager = this;
		enemy.asteroidManager = asteroidManager;
		enemy.hp.npcManager = this;
		enemyShips.Add(enemy);
	}

	public void RemoveThisNPC (NPCShipTransformManager npc) {
		_npcsList.Add(npc.entityID.entityType);
		npcShips.Remove(npc);
	}
	public void RemoveThisEnemy (EnemyShipTransformManager enemy) {
		enemyShips.Remove(enemy);
	}
}
