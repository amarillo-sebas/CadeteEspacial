using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Video;

public class VideoAudioManager : AudioManager {
	[Header("Dependencies")]
	public AudioSource audioSource;
	public VideoPlayer videoPlayer;
	public AudioClip noise;
	public GameObject objectivePointer;
	public VideoContainer videoContainer;
	public AudioContainer audioContainer;

	private bool played_75;
	private bool played_50;
	private bool played_25;
	private bool played_End;

	private List<MessageClip> _messageQueue = new List<MessageClip>();
	private bool _isPlaying = false;

	public void PlayVideoAudio (AudioClip[] ac, VideoClip vc) {
		if (!_isPlaying) {
			StartCoroutine(PlayClipWithNoise(ac, vc));
		} else {
			MessageClip messageClip = new MessageClip();
			messageClip.ac = ac;
			messageClip.vc = vc;
			_messageQueue.Add(messageClip);
		}
	}

	public void MissionStart () {
		PlayVideoAudio(audioContainer.sc_MissionStart, videoContainer.spaceCommander);
	}
	public void Mission_25 () {
		if (!played_25) {
			PlayVideoAudio(audioContainer.sc_MissionProgress_25, videoContainer.spaceCommander);
			played_25 = true;
		}
	}
	public void Mission_50 () {
		if (!played_50) {
			PlayVideoAudio(audioContainer.sc_MissionProgress_50, videoContainer.spaceCommander);
			played_50 = true;
		}
	}
	public void Mission_75 () {
		if (!played_75) {
			PlayVideoAudio(audioContainer.sc_MissionProgress_75, videoContainer.spaceCommander);
			played_75 = true;
		}
	}
	public void MissionEnd () {
		if (!played_End) {
			PlayVideoAudio(audioContainer.sc_MissionEnd, videoContainer.spaceCommander);
			played_End = true;
		}
	}
	public void ActivatePointer () {
		PlayVideoAudio(audioContainer.sc_GiveMissionPointer, videoContainer.spaceCommander);
		objectivePointer.SetActive(true);
	}

	IEnumerator PlayClipWithNoise (AudioClip[] ac, VideoClip vc) {
		_isPlaying =  true;

		if (ac != null) {
			audioSource.Stop();
			PlayClipAlways(audioSource, noise);
		}

		if (vc != null) {
			videoPlayer.clip = vc;
			videoPlayer.Play();
		}
		yield return new WaitForSeconds(audioSource.clip.length);

		if (ac != null) PlayClipAlways(audioSource, ac);
		yield return new WaitForSeconds(audioSource.clip.length);

		if (vc != null) videoPlayer.frame = 0;
		if (ac != null) PlayClipAlways(audioSource, noise);
		yield return new WaitForSeconds(audioSource.clip.length);

		if (vc != null) videoPlayer.Stop();

		if (_messageQueue.Count > 0) {
			StartCoroutine(PlayClipWithNoise(_messageQueue[0].ac, _messageQueue[0].vc));
			_messageQueue.RemoveAt(0);
		} else {
			_isPlaying = false;
		}
	}

	void Start () {
		StartCoroutine(MissionStartWait());
	}
	IEnumerator MissionStartWait () {
		yield return new WaitForSeconds(3f);
		MissionStart();
	}
}
