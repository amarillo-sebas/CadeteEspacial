using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipTransformManager : MonoBehaviour {
	[Header("Dependencies")]
	public Transform playerTransform;
	public Rigidbody rb;
	private AsteroidManager asteroidManager;
	public MessageDisplayer messages;
	public EntityID entityID;
	public FPSPlayerInput fpsPlayerInput;

	[Space(5f)]
	[Header("Movement")]
	public bool alwaysMove;
	public float rotationSpeed;
	public float thrustForce;
	private float _timeCount;
	public bool _canMove = false;

	[Space(5f)]
	[Header("Firing")]
	private bool _isShooting;
	public float fireRate;
	private float _fireTimeCounter = 0f;
	public Transform[] shotEmitters;
	public GameObject shotPrefab;
	public int damage;
	public LayerMask shotLayers;
	
	void Start () {
		asteroidManager = FindObjectOfType(typeof(AsteroidManager)) as AsteroidManager;
		StartCoroutine(WaitForAsteroids(5f));
	}

	void Update () {
		if (_canMove) {
			//if (TheGameManager.gameManager.vrActive) {
				if (alwaysMove) {
					rb.AddForce(transform.forward * thrustForce);
					if (fpsPlayerInput.trigger) {
						_isShooting = true;
					} else {
						_isShooting = false;
					}
				}
				else {
					if (fpsPlayerInput.trigger) {
						if (!alwaysMove) rb.AddForce(transform.forward * thrustForce);
					}
				}
			/*} else {
				if (fpsPlayerInput) if (fpsPlayerInput.trigger) {
					_isShooting = true;
				} else {
					_isShooting = false;
				}
			}*/

			if (_isShooting) {
				if (Time.time > _fireTimeCounter) {
					_fireTimeCounter = Time.time + fireRate;
					Shoot();
				}
			}
		}
	}

	void FixedUpdate () {
		if (_canMove) {
			float t = Quaternion.Angle(playerTransform.rotation, transform.rotation);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, playerTransform.rotation, Time.deltaTime * rotationSpeed * t);
		}
	}

	void Shoot () {
		for (int i = 0; i < shotEmitters.Length; i++) {
			GameObject shot = Instantiate(shotPrefab, shotEmitters[i].position, shotEmitters[i].rotation);
			Shot shotScript = shot.GetComponent<Shot>();
			shotScript.damage = damage;
			shotScript.shotLayers = shotLayers;
			shotScript.entityID = entityID;
		}
	}

	void OnTriggerEnter (Collider c) {
		if (_warmUp) {
			if (c.tag == "Base") {
				if (asteroidManager.asteroids.Count <= 0) {
					_canMove = false;
					messages.TypeMessage("BIEN CUÑAO\nSALVASTE LA BASE");
				}
			}
		}
	}

	private bool _warmUp = false;
	IEnumerator WaitForAsteroids (float t) {
		yield return new WaitForSeconds(t);
		_warmUp = true;
	}

	public void PlayerGetsIn (FPSPlayerInput pi) {
		fpsPlayerInput = pi;
		playerTransform = pi.transform;
		StartUpSequence();
	}
	public void PlayerGetsOut () {
		_canMove = false;
		fpsPlayerInput = null;
		playerTransform = null;
		rb.isKinematic = true;
	}

	public void StartUpSequence () {
		StartCoroutine(StartUpCountdown(5f));
	}
	public void StopShipSequence () {
		StartCoroutine(StopShipCountdown(2f));
	}
	IEnumerator StartUpCountdown (float t) {
		yield return new WaitForSeconds(t);
		_canMove = true;
		rb.isKinematic = false;
	}
	IEnumerator StopShipCountdown (float t) {
		yield return new WaitForSeconds(t);
	}
}
