using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityID : MonoBehaviour {
	public EntityType entityType;
	public NPCShipPlayerCommunicator playerCommunicator;

	public void CommunicateAsteroidDestruction () {
		if (playerCommunicator) playerCommunicator.DestroyedAsteroid();
	}
}
