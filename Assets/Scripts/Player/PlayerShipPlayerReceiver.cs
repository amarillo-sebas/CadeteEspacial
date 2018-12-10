using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipPlayerReceiver : MonoBehaviour {
	public PlayerShipTransformManager transformManager;
	public PlayerBorderControl borderControl;

	public void PlayerGetsIn (Transform p) {
		transformManager.PlayerGetsIn(p.parent.GetComponent<FPSPlayerInput>(), p);
		borderControl.PlayerGetsIn(p);
	}
	public void PlayerLeaves () {
		transformManager.PlayerGetsOut();
		borderControl.PlayerGetsOut();
	}
}
