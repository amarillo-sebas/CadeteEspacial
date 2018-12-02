using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShot : MonoBehaviour {
	public float speed;
	public float lifeSpan;
	public float collisionDistance = 2f;
	public LayerMask shotLayers;
	public float explosionForce;
	public GameObject hitFX;

	void Start () {
		Destroy(gameObject, lifeSpan);
	}
	
	void Update () {
		transform.position += transform.forward * Time.deltaTime * speed;
	
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward, out hit, collisionDistance, shotLayers)) {
			
			if (hit.transform.tag == "Asteroids") {
				hit.transform.GetComponent<Asteroid>().Damage(explosionForce);
			}

			Instantiate(hitFX, transform.position + transform.forward * 0.9f, Quaternion.identity);
			Destroy (gameObject);
		}
	}
}
