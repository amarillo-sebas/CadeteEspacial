using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Video;

public class NPCShipPlayerCommunicator : MonoBehaviour {
	[Header("Dependencies")]
	public VideoAudioManager playerCommunication;
	public NPCManager npcManager;
	public VideoContainer videoContainer;
	public AudioContainer audioContainer;

	[Space(5f)]
	[Header("Variables")]
	public EntityID entityID;
	public float speechCooldown = 5f;
	private bool _onCooldown;

	void Start () {
		StartCoroutine(WaitForPlayer());
	}

	IEnumerator WaitForPlayer () {
		yield return null;
		if (npcManager) playerCommunication = npcManager.player.GetComponent<VideoAudioManager>();
		else StartCoroutine(WaitForPlayer());
	}

	public void FriendlyFire () {
		AudioClip[] ac = null;
		VideoClip vc = null;

		switch (entityID.entityType) {
			case EntityType.Lopez:
				ac = audioContainer.l_FriendlyFire;
				vc = videoContainer.lopez;
			break;
			case EntityType.Quispe:
				ac = audioContainer.q_FriendlyFire;
				vc = videoContainer.quispe;
			break;
			case EntityType.Durflors:
				ac = audioContainer.d_FriendlyFire;
				vc = videoContainer.durflors;
			break;
		}

		CommunicateWithPlayer(ac, vc);
	}

	public void Die () {
		AudioClip[] ac = null;
		VideoClip vc = null;

		switch (entityID.entityType) {
			case EntityType.Lopez:
				ac = audioContainer.l_Death;
				vc = videoContainer.lopez;
			break;
			case EntityType.Quispe:
				ac = audioContainer.q_Death;
				vc = videoContainer.quispe;
			break;
			case EntityType.Durflors:
				ac = audioContainer.d_Death;
				vc = videoContainer.durflors;
			break;
		}

		CommunicateWithPlayer(ac, vc, true);
	}

	public void DestroyedAsteroid () {
		AudioClip[] ac = null;
		VideoClip vc = null;

		switch (entityID.entityType) {
			case EntityType.Lopez:
				ac = audioContainer.l_Asteroid;
				vc = videoContainer.lopez;
			break;
			case EntityType.Quispe:
				ac = audioContainer.q_Asteroid;
				vc = videoContainer.quispe;
			break;
			case EntityType.Durflors:
				ac = audioContainer.d_Asteroid;
				vc = videoContainer.durflors;
			break;
		}

		CommunicateWithPlayer(ac, vc);
	}

	public void EnemyKilled () {
		AudioClip[] ac = null;
		VideoClip vc = null;

		switch (entityID.entityType) {
			case EntityType.Lopez:
				ac = audioContainer.l_EnemyKilled;
				vc = videoContainer.lopez;
			break;
			case EntityType.Quispe:
				ac = audioContainer.q_EnemyKilled;
				vc = videoContainer.quispe;
			break;
			case EntityType.Durflors:
				ac = audioContainer.d_EnemyKilled;
				vc = videoContainer.durflors;
			break;
		}

		CommunicateWithPlayer(ac, vc);
	}

	public void EnemyFire () {
		AudioClip[] ac = null;
		VideoClip vc = null;

		switch (entityID.entityType) {
			case EntityType.Lopez:
				ac = audioContainer.l_EnemyAttack;
				vc = videoContainer.lopez;
			break;
			case EntityType.Quispe:
				ac = audioContainer.q_EnemyAttack;
				vc = videoContainer.quispe;
			break;
			case EntityType.Durflors:
				ac = audioContainer.d_EnemyAttack;
				vc = videoContainer.durflors;
			break;
		}

		CommunicateWithPlayer(ac, vc);
	}

	public void NPCFire () {
		
	}

	public void CommunicateWithPlayer (AudioClip[] ac, VideoClip vc, bool playAlways = false) {
		if (!_onCooldown || playAlways) {
			if (playerCommunication) playerCommunication.PlayVideoAudio(ac, vc);
			StartCoroutine(Cooldown());
		}
	}

	IEnumerator Cooldown () {
		_onCooldown = true;
		yield return new WaitForSeconds(speechCooldown);
		_onCooldown = false;
	}
}
