using UnityEngine;
using System.Collections;

public class Pure_FPP_Camera : MonoBehaviour {
	
	[Space(10)]
	[Tooltip("The Transform, which should rotate horizontally with mouse movement - in FPS it should be the Character Controller")]
	public Transform horizontalTransform;
	[Tooltip("The Transform, which should rotate vertically with mouse movement - in FPS it should be the FPS camera itself")]
	public Transform verticalTransform;
	[Tooltip("How far can the view be moved vertically up and down?")]
	public float MaxVerticalAngle = 90;
	[Tooltip("How far can the view be moved vertically up and down?")]
	public float MinVerticalAngle = -90;
	
	private Vector2 _lookVector;
	private bool _rotateCamera = false;
	public float rotationSpeed;
	public float rotationDamping;
	private float _desiredRotationX;
	private float _desiredRotationY;

	public FPSPlayerDebug playerDebug;

	void OnEnable () {
		_desiredRotationY = horizontalTransform.eulerAngles.y;
		_desiredRotationX = verticalTransform.eulerAngles.x;
	}

	void Update () {
		if (!TheGameManager.gameManager.vrActive) {
			if (_rotateCamera) {
				_desiredRotationY += rotationSpeed * _lookVector.x * Time.deltaTime;
				_desiredRotationX += rotationSpeed * -_lookVector.y * Time.deltaTime;

				_desiredRotationX = Mathf.Clamp(_desiredRotationX, MinVerticalAngle, MaxVerticalAngle);
			}

				Quaternion rotY = Quaternion.Euler(horizontalTransform.eulerAngles.x, _desiredRotationY, horizontalTransform.eulerAngles.z);
				Quaternion rotX = Quaternion.Euler(_desiredRotationX, horizontalTransform.eulerAngles.y, horizontalTransform.eulerAngles.z);

				rotY = Quaternion.Lerp(horizontalTransform.rotation, rotY, Time.deltaTime * rotationDamping);
				rotX = Quaternion.Lerp(verticalTransform.rotation, rotX, Time.deltaTime * rotationDamping);

				horizontalTransform.rotation = rotY;
				verticalTransform.rotation = rotX;
			//}
		}
	}

	public void GetLookVector (Vector2 v) {
		_lookVector = v;
		_rotateCamera = true;

	}
	public void StopRotation () {
		_rotateCamera = false;
	}

	public void ToggleVR (bool b) {
		if (!b) {
			_desiredRotationY = horizontalTransform.eulerAngles.y;
			_desiredRotationX = verticalTransform.eulerAngles.x;
		}
	}
}