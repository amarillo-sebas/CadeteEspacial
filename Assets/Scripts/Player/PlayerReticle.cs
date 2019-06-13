﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReticle : MonoBehaviour {
	[Header("Dependencies")]
	public PlayerCommunicator player;
	public Transform cameraTransform;
	public FPSPlayerDebug playerDebug;
	public ReticleSkin reticle;
	public FPSPlayerInput fpsPlayerInput;
	public Pure_FPP_Controller fpsController;
	public PlayerTransformManager transformManager;

	[Space(5f)]
	[Header("Variables")]
	public float reticleDistance;
	public LayerMask interactableLayers;

	private bool lookingAtSomething = false;
	private bool previousFrameLooking = false;
	private bool triggerState = false;
	private bool triggerBuffer = true;

	private Interactable _tempGazeInteractable = null;
	private Interactable _tempTriggerInteractable = null;
	public bool gazing = false;
	void Update () {
		RaycastHit hit;
		Interactable interactable = null;

		if (!fpsController.moving) {
			if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, reticleDistance, interactableLayers)) {
				lookingAtSomething = true;
				interactable = hit.collider.transform.GetComponent<Interactable>();
			} else {
				lookingAtSomething = false;
			}
		}


		if (lookingAtSomething != previousFrameLooking) {
			previousFrameLooking = lookingAtSomething;

			if (lookingAtSomething) {
				reticle.Gaze_In();
				if (interactable) {
					interactable.Gaze_In(player);
					_tempGazeInteractable = interactable;
					gazing = true;
				}
			} else {
				reticle.Gaze_Out();
				if (_tempGazeInteractable) {
					_tempGazeInteractable.Gaze_Out(player);
					_tempGazeInteractable = null;
					gazing = false;
				}
			}
		}

		if (fpsPlayerInput) {
			if (fpsPlayerInput.trigger) {
				if (interactable) interactable.Trigger_Hold(player);
			} else playerDebug.Log("");
		}
	}

	public void Trigger_Down () {
		if (_tempGazeInteractable) {
			_tempTriggerInteractable = _tempGazeInteractable;
			_tempTriggerInteractable.Trigger_Down(player);
		}
	}
	public void Trigger_Up () {
		if (_tempTriggerInteractable) {
			_tempTriggerInteractable.Trigger_Up(player);
			_tempTriggerInteractable = null;
		}
	}

	public void GetInShip () {
		reticle.gameObject.SetActive(false);
	}
	public void LeaveShip () {
		reticle.gameObject.SetActive(true);
		reticle.Gaze_Out();
	}
}
