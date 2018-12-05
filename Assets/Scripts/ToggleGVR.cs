using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGVR : MonoBehaviour {
	public GameObject[] GVRDependencies;

	public void ToggleVR (bool b) {
		for (int i = 0; i < GVRDependencies.Length; i++) {
			GVRDependencies[i].SetActive(b);
		}
	}
}
