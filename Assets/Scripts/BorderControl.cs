using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderControl : MonoBehaviour {
	public Asteroid asteroidScript;
	public float timeToKill = 10f;
	private float timeCounter;
	private bool _count;

	void Start () {
		asteroidScript = GetComponent<Asteroid>();
	}
	
	void OnCollisionEnter (Collision c) {
		if (c.gameObject.tag == "LevelBorder") {
			_count = true;
			timeCounter = Time.time + timeToKill;
		}
	}
	void OnCollisionExit (Collision c) {
		if (c.gameObject.tag == "LevelBorder") {
			_count = false;
		}
	}

	void Update () {
		if (_count) {
			if (Time.time > timeCounter) {
				asteroidScript.ExplodeDestroy();
			}
		}
	}
}
