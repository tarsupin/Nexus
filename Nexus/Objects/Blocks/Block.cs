using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Block : StaticGameObject {

		public Block(Scene scene, byte subType, FVector pos, object[] paramList = null) : base(scene, subType, pos, paramList) {
			this.Meta = scene.mapper.MetaList[MetaGroup.Block];

			// TODO HIGH PRIORITY: Collisions for Blocks
			// collision solid = Solidity.All
			// ungrippable = false;
		}

		// TODO HIGH PRIORITY: Damage Above Block
		public void DamageAbove() {

			//// Destroy land enemies above this brick:
			//let enemiesFound: any = findObjectsTouchingArea(this.scene.activeObjects[LoadOrder.Enemy], this.pos.x + 16, this.pos.y - 4, 16, 4);
		
			//for(let i in enemiesFound) {
			//	let enemy: Enemy = enemiesFound[i];

			//	if(enemy.isLandCreature) {
			//		enemy.dieKnockout();
			//	}
			//}
		}
	}
}
