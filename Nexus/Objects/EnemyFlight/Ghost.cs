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
			this.SetActivity(Activity.NoTileCollide);

			// Assign Flight Behavior
			this.behavior = new FlightChase(this, paramList);

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

		public override bool RunCharacterImpact(Character character) {
			DirCardinal dir = CollideDetect.GetDirectionOfCollision(character, this);

			if(dir == DirCardinal.Down && this.subType == (byte)GhostSubType.Hat) {
				ActionMap.Jump.StartAction(character, 0, 0, 4);
			} else {
				character.wounds.ReceiveWoundDamage(DamageStrength.Standard);
			}

			return Impact.StandardImpact(character, this, dir);
		}
	}
}
