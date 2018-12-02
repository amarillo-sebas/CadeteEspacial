using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {
	public int hp = 3;
	public float initialTorque;
	public float initialForce;

	public float size;

	private Rigidbody _rb;

	public GameObject[] asteroidPieces;

	public AsteroidManager asteroidManager;

	public GameObject explosionFX;

	void Start () {
		asteroidManager.AddThisAsteroid(this);
		if (initialTorque > 0 && initialForce > 0) {
			_rb = GetComponent<Rigidbody>();
			Vector3 randomVector = Random.insideUnitSphere.normalized;
			float randomMultiplier = Random.Range(0f, 1f);
			_rb.AddTorque(randomVector * initialTorque * randomMultiplier);
			_rb.AddForce(randomVector * initialForce * randomMultiplier);
		}
	}

	public void Activate (float force) {
		GetComponent<Rigidbody>().AddForce(Random.insideUnitSphere.normalized * force);
	}
	
	public void Damage (float force) {
		hp--;
		if (hp == 0) {
			Explode(force);
		}
	}

	public void Explode (float force) {
		if (asteroidPieces.Length > 0) {
			for (int i = 0; i < asteroidPieces.Length; i++) {
				GameObject go = Instantiate(asteroidPieces[i], transform.position, transform.rotation);
				go.transform.localScale = transform.localScale;
				Rigidbody[] rbs = go.GetComponentsInChildren<Rigidbody>();
				if (!_rb) _rb = GetComponent<Rigidbody>();
				for (int j = 0; j < rbs.Length; j++) {
					rbs[j].angularVelocity = _rb.angularVelocity;
				}
				
				Asteroid a = go.GetComponent<Asteroid>();
				a.Activate(force);
				a.asteroidManager = asteroidManager;
			}
		}
		asteroidManager.RemoveThisAsteroid(this);
		GameObject fx = Instantiate(explosionFX, transform.position, Quaternion.identity);
		fx.GetComponent<FX_Size>().SetSize(transform.localScale.x, size);
		Destroy(gameObject);
	}
	public void ExplodeDestroy () {
		asteroidManager.RemoveThisAsteroid(this);
		Instantiate(explosionFX, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
