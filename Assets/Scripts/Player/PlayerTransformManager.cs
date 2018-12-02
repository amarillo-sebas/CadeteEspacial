using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformManager : MonoBehaviour {
	public Transform shipTransform;
	
	void Start () {
		//Screen.SetResolution(1280, 720, true);
	}

	void Update () {
		transform.position = shipTransform.position;
	}
}
