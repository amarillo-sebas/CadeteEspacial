using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCShipHealth : Health {
	public NPCShipPlayerCommunicator playerCommunicator;

	public override void Damage (int d, EntityID entityID) {
		base.Damage(d, entityID);

		switch (entityID.entityType) {
			case EntityType.Player: playerCommunicator.FriendlyFire();
			break;
			case EntityType.NPCShip: playerCommunicator.NPCFire();
			break;
			case EntityType.EnemyShip: playerCommunicator.EnemyFire();
			break;
		}
	}

	public override void Die () {
		playerCommunicator.Die();
		base.Die();
	}
}
