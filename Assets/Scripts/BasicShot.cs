using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShot : Shot {
	public float speed;
	public float lifeSpan;
	public float collisionDistance = 2f;
	public float explosionForce;
	public GameObject hitFX;

	void Start () {
		Destroy(gameObject, lifeSpan);
	}
	
	void Update () {
		transform.position += transform.forward * Time.deltaTime * speed;
	
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward, out hit, collisionDistance, shotLayers)) {
			
			Rigidbody rb;
			rb = hit.transform.GetComponent<Rigidbody>();

			if (hit.transform.tag == "Asteroids") {
				hit.transform.GetComponent<Asteroid>().Damage(explosionForce, damage, entityID);
				if (rb) rb.AddForce(transform.forward * explosionForce * 2f);
			} else {
				Health hp = hit.transform.GetComponent<Health>();
				if (hp) hp.Damage(damage, entityID);
				if (rb) rb.AddForce(transform.forward * explosionForce * 10f);
			}

			if (rb) rb.AddTorque(Random.insideUnitSphere * 5f);

			Instantiate(hitFX, transform.position + transform.forward * 0.9f, Quaternion.identity);
			Destroy (gameObject);
		}
	}
}
