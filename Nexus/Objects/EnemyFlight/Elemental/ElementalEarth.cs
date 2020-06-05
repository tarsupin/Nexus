using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.Objects {

	public enum ElementalEarthSubType : byte { Normal };

	public class ElementalEarth : Elemental {

		public ElementalEarth(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.ElementalEarth].meta;
			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(2, 4, -4, -12);
		}

		private void AssignSubType(byte subType) {
			this.SpriteName = "Elemental/Earth/Left";
		}

		public override void OnDirectionChange() {
			this.SpriteName = "Elemental/Earth/" + (this.FaceRight ? "Right" : "Left");
		}

		//attack( time: Timer ): boolean {
		//	if(!super.attack( time )) { return false; }

		//	let velX = 0;
		//	let velY = this.att.speed;

		//	// Create the projectile
		//	var projectile = Projectile.fire( this.scene, ProjectileEarth, "Earth", this.pos.x + 12, this.pos.y + 12, velX, velY, "EnemyEarth" );

		//	// Set the projectile to rotate
		//	projectile.render = projectile.renderBallRotation;

		//	this.scene.soundList.flame.addToSoundPool(); // Track Sound			// TODO: Alter to a rock dropping sound.
		//	return true;
		//}
	}
}
