using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReticle : MonoBehaviour {
	[Header("Dependencies")]
	public Transform cameraTransform;
	public FPSPlayerDebug playerDebug;
	public ReticleSkin reticle;

	[Space(5f)]
	[Header("Variables")]
	public float reticleDistance;
	public LayerMask interactableLayers;

	private bool lookingAtSomething = false;
	private bool previousFrameLooking = false;
	
	void Update () {
		RaycastHit hit;
		if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, reticleDistance, interactableLayers)) {
			lookingAtSomething = true;
		} else {
			lookingAtSomething = false;
		}


		if (lookingAtSomething != previousFrameLooking) {
			previousFrameLooking = lookingAtSomething;

			if (lookingAtSomething) {
				playerDebug.Log(hit.transform.name);
				reticle.Gaze_In();
			} else {
				playerDebug.Log("");
				reticle.Gaze_Out();
			}
		}
	}
}
