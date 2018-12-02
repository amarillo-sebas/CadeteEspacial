using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour {
	public float destroyCountdown;

	void Start () {
		Destroy(gameObject, destroyCountdown);
	}
}
