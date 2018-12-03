using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCShipTransformManager : MonoBehaviour {
	[Header("Dependencies")]
	public AsteroidManager asteroidManager;
	public Transform player;
	public EntityID entityID;

	[Space(5f)]
	[Header("Movement")]
	public Transform target;
	public float raycastRate;
	public float raycastDistance;
	public LayerMask avoidLayers;
	public float thrustForce;
	public float rotationSpeed;
	private bool _canMove = true;
	private Rigidbody _rb;
	private Vector3 _destinationVector;
	private bool _hasDestinationVector = false;
	public float avoidanceRadius;
	public float randomTorqueAmount;
	public float randomTorqueRate;

	[Space(5f)]
	[Header("Shooting")]
	public float fireRate;
	public Transform[] shotEmitters;
	public GameObject shotPrefab;
	private bool _isShooting;
	public float shootingAngle;
	private float _fireTimeCounter;
	public int damage;
	public LayerMask shotLayers;

	IEnumerator Start () {
		_rb = GetComponent<Rigidbody>();
		_fireTimeCounter = Random.Range(5f, 10f);
		yield return new WaitForSeconds(0.5f);
		PickNewTarget();
		StartCoroutine(CastRay(raycastRate));
		player = GameObject.FindWithTag("Player").transform;
	}
	
	void Update () {
		if (_canMove) {
			_rb.AddForce(transform.forward * thrustForce);
		}

		if (target) {
			if (!_hasDestinationVector) {
				Vector3 targetDir = target.position - transform.position;
				float step = rotationSpeed * Time.deltaTime;
				Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0f);
				transform.rotation = Quaternion.LookRotation(newDir);
			}
		} else {
			PickNewTarget();
		}

		if (_hasDestinationVector) {
			float step = rotationSpeed * Time.deltaTime;
			Vector3 targetDir = _destinationVector - transform.position;
			Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0f);
			transform.rotation = Quaternion.LookRotation(newDir);
			//Debug.Log(Vector3.Distance(transform.position, _destinationVector));

			if (Vector3.Distance(transform.position, _destinationVector) < 3f) {
				_hasDestinationVector = false;
			}
		}

		AddRandomTorque();

		if (asteroidManager.asteroids.Count > 0) {
			float angle = Vector3.Angle(transform.forward, target.position - transform.position);
			if (angle <= shootingAngle && angle >= -shootingAngle) {
				if (Time.time > _fireTimeCounter) {
					_fireTimeCounter = Time.time + fireRate;
					Shoot();
				}
			}
		}
	}

	private float _randomTorqueCounter;
	void AddRandomTorque () {
		if (Time.time > _randomTorqueCounter) {
			_randomTorqueCounter = Time.time + randomTorqueRate;
			_rb.AddTorque(Random.insideUnitCircle * randomTorqueAmount);
		}
	}

	void PickNewTarget () {
		if (asteroidManager.asteroids.Count > 0) {
			int a = Random.Range(0, asteroidManager.asteroids.Count);
			target = asteroidManager.asteroids[a].transform;
		}
		else {
			target = player;
		}
	}

	void Shoot () {
		bool clearToShoot = true;
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward, out hit, 10f)) {
			if (hit.transform != target) {
				clearToShoot = false;
			}
		}

		if (clearToShoot) {
			for (int i = 0; i < shotEmitters.Length; i++) {
				GameObject shot = Instantiate(shotPrefab, shotEmitters[i].position, shotEmitters[i].rotation);
				Shot shotScript = shot.GetComponent<Shot>();
				shotScript.damage = damage;
				shotScript.shotLayers = shotLayers;
				shotScript.entityID = entityID;
			}
		}
	}

	IEnumerator CastRay (float t) {
		yield return new WaitForSeconds(t);

		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance, avoidLayers)) {
			Vector3 randomPosition = Random.insideUnitCircle.normalized;
			Vector3 _destinationVector = transform.position + randomPosition * avoidanceRadius;
			_hasDestinationVector = true;
			yield return new WaitForSeconds(3f);
		}

		//Debug.DrawRay(transform.position, transform.forward * raycastDistance, Color.red, raycastRate, true);

		StartCoroutine(CastRay(t));
	}
}
