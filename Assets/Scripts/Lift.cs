using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour {
	[Space(5f)]
	[Header("Dependencies")]
	public Animator animator;

	[Space(5f)]
	[Header("Variables")]
	public float liftResetTime;
	public bool liftIsMoving;
	private Coroutine resetCoroutine;

	void OnTriggerEnter (Collider c) {
		if (c.tag == "Player" && !liftIsMoving) {
			resetCoroutine = StartCoroutine(LiftReset(liftResetTime));
			MoveLift();
		}
	}

	void OnTriggerExit (Collider c) {
		if (c.tag == "Player") {
			if (resetCoroutine != null && !liftIsMoving) StopCoroutine(resetCoroutine);
		}
	}

	void MoveLift () {
		animator.SetTrigger("Move");
		liftIsMoving = true;
		StartCoroutine(LiftMovingTimer(4.5f));
	}

	IEnumerator LiftReset (float t) {
		yield return new WaitForSeconds(t);
		MoveLift();
		resetCoroutine = null;
	}

	IEnumerator LiftMovingTimer (float t) {
		yield return new WaitForSeconds(t);
		liftIsMoving = false;
	}
}
