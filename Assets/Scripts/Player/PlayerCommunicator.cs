using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCommunicator : MonoBehaviour {
	public PlayerReticle reticle;
	public Pure_FPP_Controller fpsController;
	public PlayerTransformManager transformManager;
	public Transform player;

	public void GetInShip (Transform s) {
		reticle.GetInShip ();
		fpsController.GetInShip();
		transformManager.GetInShip(s);
	}

	public void LeaveShip () {
		reticle.LeaveShip ();
		fpsController.LeaveShip();
		transformManager.LeaveShip();
	}
}
