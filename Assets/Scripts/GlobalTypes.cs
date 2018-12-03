using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Video;

public enum EntityType {
	Player,
	NPCShip,
	EnemyShip,
	Lopez,
	Quispe,
	Durflors
}

public struct MessageClip {
	public AudioClip[] ac;
	public VideoClip vc;
}