using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
	public int hp;
	public GameObject deathFX;

	public virtual void Damage (int d, EntityID entityID) {
		hp -= d;
		if (hp <= 0) {
			Die(entityID);
		}
	}

	public virtual void Die (EntityID entityID) {
		Instantiate(deathFX, transform.position, transform.rotation);
		Destroy(gameObject);
	}
}
