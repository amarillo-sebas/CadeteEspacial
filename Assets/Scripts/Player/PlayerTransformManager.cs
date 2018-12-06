﻿using System.Collections;
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

	public void GetInShip () {
		if (!_ship) _ship = FindObjectOfType(typeof(PlayerShipTransformManager)) as PlayerShipTransformManager;
		shipTransform = _ship.transform;
		_ship.PlayerGetsIn(fpsInput, transform.GetChild(0));
		_insideShip = true;
		reticle.GetInShip();
	}
	public void GetInShip (Transform ship) {
		
	}
	public void LeaveShip () {
		shipTransform = null;
		_ship.PlayerGetsOut();
		_insideShip = false;
		reticle.LeaveShip();
	}

	void Update () {
		if (shipTransform && _insideShip) transform.position = shipTransform.position;
	}
}
