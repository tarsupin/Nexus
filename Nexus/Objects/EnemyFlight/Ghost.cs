using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Objects {

	public enum GhostSubType : byte {
		Norm,
		Hide,
		Hat,
	}

	public class Ghost : EnemyFlight {

		public Ghost(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.Ghost].meta;

			// Physics, Collisions, etc.
			this.physics = new Physics(this);

			// Assign Flight Behavior
			this.behavior = FlightBehavior.AssignFlightMotion(this, paramList);

			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(2, 2, -2, -2);
		}

		private void AssignSubType( byte subType ) {
			if(subType == (byte)GhostSubType.Norm) {
				this.SpriteName = "Ghost/Norm/Left";
			} else if(subType == (byte)GhostSubType.Hide) {
				this.SpriteName = "Ghost/Hide/Left";
			} else if(subType == (byte)GhostSubType.Hat) {
				this.SpriteName = "Ghost/Hat/Left";
			}
		}

		//update( time: Timer ) {
		//	super.update( time );

		//	// Update facing based on speed.
		//	if(Math.abs(this.physics.velocity.x) > 0.15) {
		//		this.status.faceRight = this.physics.velocity.x > 0;

		//		if(this.status.faceRight) {
		//			this.setFrame("Ghost/" + this.subType + "/Right");
		//		} else {
		//			this.setFrame("Ghost/" + this.subType + "/Left");
		//		}
		//	}
		//}

		//getJumpedOn( character: Character, bounceStrength: number = 0 ): void {
		//	if(this.status.inDeathSequence) { return; }
		//	if(this.subType === "Hat") {
		//		character.bounceUp( this, bounceStrength );
		//	} else {
		//		character.wound();
		//	}
		//}

		public override bool RunCharacterImpact(Character character) {
			character.wounds.ReceiveWoundDamage(DamageStrength.Standard);
			return true;
		}
	}
}
