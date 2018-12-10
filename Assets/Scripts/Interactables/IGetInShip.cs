using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGetInShip : Interactable {
	[Header("Dependencies")]
	public PlayerShipPlayerReceiver shipReceiver;
	public Animator anim;
	public Renderer renderer;
	public Transform shipSeatTransform;

	[Space(5f)]
	[Header("Variables")]
	public Material Gaze_InMat;
	public Material Gaze_OutMat;

	public override void Trigger_Down (PlayerCommunicator p) {
		base.Trigger_Down(p);
		if (anim) anim.SetBool("pressed", true);
	}

	public override void Trigger_Up (PlayerCommunicator p) {
		base.Trigger_Up(p);
		p.GetInShip(shipSeatTransform);
		if (anim) anim.SetBool("pressed", false);
		shipReceiver.PlayerGetsIn(p.player);
	}

	public override void Gaze_In (PlayerCommunicator p) {
		base.Gaze_In(p);
		renderer.material = Gaze_InMat;
	}

	public override void Gaze_Out (PlayerCommunicator p) {
		base.Gaze_Out(p);
		renderer.material = Gaze_OutMat;
	}
}
