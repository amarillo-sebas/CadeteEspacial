using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
[CreateAssetMenu(fileName = "AudioContainer", menuName = "Containers/AudioContainer")]
public class AudioContainer : ScriptableObject {
	[Header("Space Commander")]
	public AudioClip[] sc_MissionStart;
	public AudioClip[] sc_MissionEnd;
	public AudioClip[] sc_MissionProgress_25;
	public AudioClip[] sc_MissionProgress_50;
	public AudioClip[] sc_MissionProgress_75;
	public AudioClip[] sc_GiveMissionPointer;

	[Space(5f)]
	[Header("Pilots")]
	/*[SerializeField]
	public PilotLines[] pilotLines;*/

	[Space(5f)]
	[Header("Lopez")]
	public AudioClip[] l_FriendlyFire;
	public AudioClip[] l_EnemyAttack;
	public AudioClip[] l_Asteroid;
	public AudioClip[] l_EnemyKilled;
	public AudioClip[] l_Death;

	[Space(5f)]
	[Header("Quispe")]
	public AudioClip[] q_FriendlyFire;
	public AudioClip[] q_EnemyAttack;
	public AudioClip[] q_Asteroid;
	public AudioClip[] q_EnemyKilled;
	public AudioClip[] q_Death;

	[Space(5f)]
	[Header("Durflors")]
	public AudioClip[] d_FriendlyFire;
	public AudioClip[] d_EnemyAttack;
	public AudioClip[] d_Asteroid;
	public AudioClip[] d_EnemyKilled;
	public AudioClip[] d_Death;

	/*public void GetPilotLine (EntityType entityType) {
		switch (entityType) {
			case EntityType.Quispe:
			break;
			case EntityType.Lopez:
			break;
			case EntityType.Durflors:
			break;
		}
	}*/
}
