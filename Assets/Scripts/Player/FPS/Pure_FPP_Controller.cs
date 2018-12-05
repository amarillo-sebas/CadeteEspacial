using UnityEngine;
using System.Collections;

public class Pure_FPP_Controller : MonoBehaviour {

	//Landing sound tylko jak controller dłużej spadał :<
	//jest przerwa po skoku w walk soundzie

	[Space(10)]
	[Tooltip("The Character Controller to operate EFPS. IT's required to work.")]
	private CharacterController Controller; 
	[Tooltip("FPS Camera's transform. Used at crouching height change.")]
	public Camera CharacterCamera; // it's position is used. It shouldn't be moved by animations or other type of external transform movement.
	[Tooltip("Gravity force power.")]
	public float Gravity = 1f;

	[Space(10)]
	[Tooltip("The primary speed of this character.")]
	public float MoveSpeed = 6;

	private float  InputVert; //Input value (-1.0 - 1.0)
	private float  InputHor;	//same as above
	private int JumpsAvailable;
	private bool JumpBlock;		
	private bool SlopeJumpBlock = false;
	private float SlopeAngle;
	private float tempJumpSpeed;
	private float jt; //jump timer
	private RaycastHit SlopeCheck;
	private RaycastHit CrouchHeadCheck;
	private Vector3 CamPos; //camera starting position
	private Vector3 tempCamPos; //cam position
	private float ControllerStartingHeight; //staring height of Controller
	private Vector3 CameraVector; //a "dirty" value to store temporary camera position;
	private bool CrouchingBlocked;
	private int RollMultiJumpSound;
	private int RollJumpSound;
	private bool tempGrounded; //was grounded in the previous frame?
	private int RollWalkSound;
	private int RollRunSound;
	private int RollCrouchSound;
	private float ft; //falling timer
	private RaycastHit HeadHitCheck; //this checks if during a jump the player has hit something with his head.


	private float TheX;	//Calculated movement vector variables applied to  Character Controller
	private float TheZ;
	private float TheY;
	private Vector3 TheVector; //Movement vector, applied to the Character Controller
	private bool HalfSpeed; // this is used to prevent diagonal acceleration bug (faster diagonal movement than normal)

	private bool _insideShip = false;
	public FPSPlayerInput fpsPlayerInput;

	private float acceleration;
	[Range(0f, 0.1f)]
	public float accelerationValue;
	private float accelerationFactor;
	/*private Coroutine accelerate = null;
	private Coroutine decelerate = null;*/

	private bool _canCalculateAcceleration = true;
	private bool _canCalculateDeceleration = false;

	void Start() {
		Controller = GetComponent<CharacterController> ();
		if (CharacterCamera != null) {
			CamPos = CharacterCamera.transform.localPosition;
		}
	}

	private bool _justReleasedTrigger = false;
	void FixedUpdate() {
		if (!_insideShip && !fpsPlayerInput.pointer.pointerLooking) {

			if (fpsPlayerInput.trigger) {
				if (!_justReleasedTrigger) {
					_justReleasedTrigger = true;
					_canCalculateAcceleration = true;
					_canCalculateDeceleration = false;
					accelerationFactor = 0f;
				}

				if (_canCalculateAcceleration) {
					accelerationFactor += Time.fixedDeltaTime + accelerationValue;
					if (SineAccelerator.Accelerate(out acceleration, accelerationFactor)) {
						_canCalculateAcceleration = false;
						_canCalculateDeceleration = true;
					}
				} else accelerationFactor = 0f;

				Vector3 tV;
				tV = CharacterCamera.transform.forward;
				tV.y = 0f;
				tV = tV.normalized;
				tV = tV * MoveSpeed * Time.fixedDeltaTime;

				tV *= acceleration;

				TheVector = tV;
			} else {
				if (_justReleasedTrigger) {
					_justReleasedTrigger = false;
					_canCalculateAcceleration = false;
					_canCalculateDeceleration = true;
					accelerationFactor = 0f;
				}

				if (_canCalculateDeceleration) {
					accelerationFactor += Time.fixedDeltaTime;
					if (SineAccelerator.Decelerate(out acceleration, accelerationFactor)) {
						_canCalculateAcceleration = true;
						_canCalculateDeceleration = false;
					}
				} else accelerationFactor = 0f;

				TheVector = TheVector * acceleration;
			}
			
			if (!Controller.isGrounded) {
				TheVector.y = -Gravity * Time.fixedDeltaTime;
			}

			Controller.Move(TheVector);
			//fpsPlayerInput.playerDebug.Log(TheVector.magnitude);
		}
	}

	public void ToggleVR (bool b) {

	}

	public void GetInShip () {
		_insideShip = true;
	}
	public void LeaveShip () {
		_insideShip = false;
	}
}