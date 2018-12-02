using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Video;

public class VideoAudioManager : AudioManager {
	public AudioSource audioSource;
	public VideoPlayer videoPlayer;
	public AudioClip noise;
	//public VideoClip noiseVideo;
	public GameObject objectivePointer;

	[Space(5f)]
	[Header("Mission clips")]
	public AudioClip[] missionStart;
	public AudioClip[] missionEnd;
	public AudioClip[] missionProgress_25;
	public AudioClip[] missionProgress_50;
	public AudioClip[] missionProgress_75;
	public AudioClip[] giveObjectivePointer;

	private bool played_75;
	private bool played_50;
	private bool played_25;
	private bool played_End;

	[Space(5f)]
	[Header("Mission clips")]
	public VideoClip spaceCommander;

	public void MissionStart () {
		StartCoroutine(PlayClipWithNoise(missionStart));
	}
	public void Mission_25 () {
		if (!played_25) {
			StartCoroutine(PlayClipWithNoise(missionProgress_25));
			played_25 = true;
		}
	}
	public void Mission_50 () {
		if (!played_50) {
			StartCoroutine(PlayClipWithNoise(missionProgress_50));
			played_50 = true;
		}
	}
	public void Mission_75 () {
		if (!played_75) {
			StartCoroutine(PlayClipWithNoise(missionProgress_75));
			played_75 = true;
		}
	}
	public void MissionEnd () {
		if (!played_End) {
			StartCoroutine(PlayClipWithNoise(missionEnd));
			played_End = true;
		}
	}
	public void ActivatePointer () {
		StartCoroutine(PlayClipWithNoise(giveObjectivePointer));
		objectivePointer.SetActive(true);
	}

	IEnumerator PlayClipWithNoise (AudioClip[] ac) {
		//videoPlayer.Stop();
		audioSource.Stop();

		PlayClipAlways(audioSource, noise);
		videoPlayer.clip = spaceCommander;
		videoPlayer.Play();
		yield return new WaitForSeconds(audioSource.clip.length);
		PlayClipAlways(audioSource, ac);
		yield return new WaitForSeconds(audioSource.clip.length);
		videoPlayer.frame = 0;
		PlayClipAlways(audioSource, noise);
		yield return new WaitForSeconds(audioSource.clip.length);
		videoPlayer.Stop();
	}

	void Start () {
		StartCoroutine(MissionStartWait());
	}
	IEnumerator MissionStartWait () {
		yield return new WaitForSeconds(3f);
		MissionStart();
	}
}
