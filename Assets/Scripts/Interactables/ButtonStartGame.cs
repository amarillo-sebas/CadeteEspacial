using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStartGame : Interactable {
	public override void Trigger_Up (PlayerCommunicator p) {
		base.Trigger_Up(p);
		TheGameManager.gameManager.LoadLevel("TestScene2");
	}
}
