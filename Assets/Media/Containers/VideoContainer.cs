using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "VideoContainer", menuName = "Containers/VideoContainer")]
public class VideoContainer : ScriptableObject {
	[Header("Space Commander")]
	public VideoClip spaceCommander;

	[Space(5f)]
	[Header("Lopez")]
	public VideoClip lopez;

	[Space(5f)]
	[Header("Quispe")]
	public VideoClip quispe;

	[Space(5f)]
	[Header("Durflors")]
	public VideoClip durflors;
}
