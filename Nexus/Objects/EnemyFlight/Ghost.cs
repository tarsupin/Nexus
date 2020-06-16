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
		Slimer,
	}

	public class Ghost : EnemyFlight {

		public Ghost(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.Ghost].meta;

			// Physics, Collisions, etc.
			this.physics = new Physics(this);
			this.SetActivity(Activity.NoTileCollide);

			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(2, 2, -2, -2);

			// Assign Flight-Chase Behavior (must be after atlas bounds, since it depends on it)
			this.behavior = new FlightChase(this, paramList);

			if(this.subType == (byte)GhostSubType.Hat) {
				((FlightChase)this.behavior).SetStallMinimum(2);
			}
		}

		private void AssignSubType( byte subType ) {
			if(subType == (byte)GhostSubType.Norm) {
				this.SpriteName = "Ghost/Norm/Left";
			} else if(subType == (byte)GhostSubType.Hide) {
				this.SpriteName = "Ghost/Hide/Left";
			} else if(subType == (byte)GhostSubType.Hat) {
				this.SpriteName = "Ghost/Hat/Left";
			} else if(subType == (byte)GhostSubType.Slimer) {
				this.SpriteName = "Ghost/Slimer/Left";
			}
		}

		public override bool RunCharacterImpact(Character character) {
			DirCardinal dir = CollideDetect.GetDirectionOfCollision(character, this);

			if(dir == DirCardinal.Down && this.subType == (byte)GhostSubType.Hat) {
				ActionMap.Jump.StartAction(character, 2, 0, 4, true);
			} else {
				if(this.subType != (byte)GhostSubType.Hide) {
					if(this.subType == (byte)GhostSubType.Hat && this.posY - character.posY > 25) { return false; }
					character.wounds.ReceiveWoundDamage(DamageStrength.Standard);
				}
			}

			if(this.subType == (byte) GhostSubType.Slimer) {
				return Impact.StandardImpact(character, this, dir);
			}

			return false;
		}

		public override bool RunProjectileImpact(Projectile projectile) { return false; }
	}
}
