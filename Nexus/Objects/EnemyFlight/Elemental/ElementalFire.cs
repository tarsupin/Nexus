using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.Objects {

	public enum ElementalFireSubType : byte { Normal };

	public class ElementalFire : Elemental {

		public ElementalFire(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.ElementalFire].meta;
			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(2, 4, -4, -12);
		}

		private void AssignSubType(byte subType) {
			this.SpriteName = "Elemental/Fire/Left";
		}

		public override void OnDirectionChange() {
			this.SpriteName = "Elemental/Fire/" + (this.FaceRight ? "Right" : "Left");
		}

		//attack( time: Timer ): boolean {
		//	if(!super.attack( time )) { return false; }

		//	// Create the projectile
		//	let projectile = Projectile.fire( this.scene, ProjectileBall, "Fire", this.pos.x + 12, this.pos.y + 12, this.att.x, 0, "EnemyFire" );

		//	// Set the projectile to rotate
		//	projectile.render = projectile.renderBallRotation;

		//	this.scene.soundList.flame.addToSoundPool(); // Track Sound
		//	return true;
		//}
	}
}
