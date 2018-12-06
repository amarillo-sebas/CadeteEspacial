using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStartGame : Interactable {
	public override void Trigger_Up () {
		base.Trigger_Up();
		TheGameManager.gameManager.LoadLevel("TestScene2");
	}
}
