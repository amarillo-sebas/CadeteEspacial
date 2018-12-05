using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformManager : MonoBehaviour {
	public Transform shipTransform;
	
	public void GetInShip (Transform ship) {
		shipTransform = ship;
	}
	public void LeaveShip () {
		shipTransform = null;
	}

	void Update () {
		if (shipTransform) transform.position = shipTransform.position;
	}
}
