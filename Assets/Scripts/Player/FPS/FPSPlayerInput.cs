using UnityEngine;
using System.Collections;

	public class FPSPlayerInput : MonoBehaviour {
		[Header("Dependencies")]
		public FPSPlayerDebug playerDebug;
		public Pure_FPP_Camera playerCamera;
		public Pure_FPP_Controller playerController;
		public PlayerTransformManager playerTransformManager;
		public GvrReticlePointer pointer;

		[Space(5f)]
		[Header("Variables")]
		public bool trigger = false;

		Vector2 touchDirection;
		private Vector2 touchOrigin = -Vector2.one; //Used to store location of screen touch origin for mobile controls.
		
		void Start () {
			_directionTouch = int.MaxValue;
			_triggerTouch = int.MaxValue;
		}

		private int _directionTouch;
		private int _triggerTouch;

		private void Update () {
			if (!TheGameManager.gameManager.vrActive) {
				if (Input.touchCount > 0) {
					foreach (Touch touch in Input.touches) {

						switch (touch.phase) {
							case TouchPhase.Began:
								if (touch.position.x < Screen.width / 2) {
									_directionTouch = touch.fingerId;
									touchOrigin = touch.position;
								} else if (touch.position.x > Screen.width / 2) {
									_triggerTouch = touch.fingerId;
									trigger = true;
								}
							break;
							case TouchPhase.Moved:
								if (touch.fingerId == _directionTouch) {
									touchDirection = touch.position - touchOrigin;
									touchDirection = new Vector2(Mathf.Clamp(touchDirection.x, -300f, 300f) / 300f, Mathf.Clamp(touchDirection.y, -300f, 300f) / 300f);
									playerCamera.GetLookVector(touchDirection);
								}
							break;
							case TouchPhase.Ended:
								if (touch.fingerId == _directionTouch) {
									playerCamera.StopRotation();
									touchOrigin.x = -1;
									touchDirection = Vector2.zero;
									_directionTouch = int.MaxValue;
								} else if (touch.fingerId == _triggerTouch) {
									trigger = false;
									_triggerTouch = int.MaxValue;
								}
							break;
						}
					}
				}
			}

			else {
				if (GvrControllerInput.ClickButton || Input.touchCount > 0) {
					trigger = true;
				} else trigger = false;
			}
		}

		public void ToggleVR (bool b) {
			playerCamera.ToggleVR(b);
			playerController.ToggleVR(b);
		}
	}