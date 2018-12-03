using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipHealth : Health {
	public EnemyShipTransformManager transformManager;
	public NPCManager npcManager;

	public override void Die (EntityID entityID) {
		npcManager.RemoveThisEnemy(transformManager);
		
		NPCShipTransformManager npc = LookForKiller(entityID);
		if (npc) npc.playerCommunicator.EnemyKilled();

		/*switch (entityID.entityType) {
			case EntityType.Lopez:
				LookForKiller(entityID.entityType).playerCommunicator.EnemyKilled();
			break;
		}*/

		base.Die(entityID);
	}

	NPCShipTransformManager LookForKiller (EntityID entityID) {
		for (int i = 0; i < npcManager.npcShips.Count; i++) {
			if(npcManager.npcShips[i].entityID.entityType == entityID.entityType) {
				return npcManager.npcShips[i];
			}
		}
		return null;
	}
}
