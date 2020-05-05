using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLanding : MonoBehaviour {
	private PlayerShipTransformManager playerShip;
	private bool _movingShip;
	private float _t;

	private Transform playerShipTransform;
	private Vector3 playerShipPosition;
	private Quaternion playerShipRotation;
	private Vector3 landingPosition;

	private Pure_FPP_Camera playerCamera;

	public float landingTime;
	public AnimationCurve easingCurve;

	void Update () {
		if (_movingShip) {
			playerShipTransform.position = Vector3.Lerp(playerShipPosition, landingPosition, easingCurve.Evaluate(_t));
			playerShipTransform.rotation = Quaternion.Lerp(playerShipRotation, transform.rotation, easingCurve.Evaluate(_t));

			//if (playerCamera) playerCamera.LookAtThis(playerShipTransform.rotation);
			playerCamera.ForceLookAt(playerShipTransform.rotation);

			_t += Time.deltaTime / landingTime;
			if (_t >= 1f) {
				_movingShip = false;
				_t = 0f;
				playerShipPosition = Vector3.zero;
				playerShipRotation = Quaternion.identity;
				playerCamera.takeControlAway = false;
				playerCamera.LookAtThis(playerShipTransform.rotation);
				playerShip.playerTransform.parent.GetComponent<PlayerCommunicator>().LeaveShip();
				playerShip.PlayerGetsOut();

				playerCamera = null;
				playerShip = null;
			}
		}
	}
	
	void OnTriggerEnter (Collider c) {
		if (c.tag == "Player") {
			playerShip = c.GetComponent<PlayerShipTransformManager>();
			if (playerShip) if (playerShip.canMove) {
				playerShip.StopShipSequence();
				_movingShip = true;
				playerShipTransform = playerShip.transform;
				playerShipPosition = c.transform.position;
				playerShipRotation = c.transform.rotation;

				playerCamera = playerShip.playerTransform.GetComponent<Pure_FPP_Camera>();
				playerCamera.takeControlAway = true;

				landingPosition = transform.position - playerShip.landingPivot.localPosition;
			}
		}
	}
}
