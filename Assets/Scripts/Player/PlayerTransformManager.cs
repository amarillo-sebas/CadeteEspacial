using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformManager : MonoBehaviour {
	public Transform shipTransform;
	private PlayerShipTransformManager _ship;
	public FPSPlayerInput fpsInput;
	public PlayerReticle reticle;

	private bool _insideShip;
	
	void Start () {
		_ship = FindObjectOfType(typeof(PlayerShipTransformManager)) as PlayerShipTransformManager;
		//shipTransform = _ship.transform;
	}

	public void GetInShip (Transform ship) {
		transform.rotation = ship.rotation;
		GetComponentInChildren<Pure_FPP_Camera>().LookAtThis(transform.rotation);
		shipTransform = ship;
		_insideShip = true;
		reticle.GetInShip();
	}

	public void LeaveShip () {
		shipTransform = null;
		//_ship.PlayerGetsOut();
		_insideShip = false;
		reticle.LeaveShip();
	}

	void Update () {
		if (shipTransform && _insideShip) transform.position = shipTransform.position;//why am I not parenting the player to the ship?
	}

	public void MakePlayerForceLookAt (Quaternion r) {
		GetComponentInChildren<Pure_FPP_Camera>().ForceLookAt(r);
	}
}
