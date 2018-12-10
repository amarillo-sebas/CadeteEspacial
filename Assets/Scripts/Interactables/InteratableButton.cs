using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteratableButton : Interactable {
	public Material triggerDownMat;
	public Material triggerUpMat;

	public override void Trigger_Down (PlayerCommunicator p) {
		base.Trigger_Down(p);
		GetComponent<Renderer>().material = triggerDownMat;
	}

	public override void Trigger_Up (PlayerCommunicator p) {
		base.Trigger_Up(p);
		GetComponent<Renderer>().material = triggerUpMat;
		TheGameManager.gameManager.ToggleVR(!TheGameManager.gameManager.vrActive);
	}

	public override void Trigger_Hold (PlayerCommunicator p) {
		base.Trigger_Hold(p);
	}

	public override void Gaze_In (PlayerCommunicator p) {
		base.Gaze_In(p);
		transform.localScale *= 1.1f;
	}

	public override void Gaze_Out (PlayerCommunicator p) {
		base.Gaze_Out(p);
		transform.localScale *= 0.9f;
	}
}
