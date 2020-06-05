using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Objects {

	public enum SawSubType : byte {
		Small,
		Large,
		LethalSmall,
		LethalLarge,
	}

	public class Saw : EnemyFlight {

		public DamageStrength Damage;

		public Saw(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.Saw].meta;

			// Physics, Collisions, etc.
			this.physics = new Physics(this);

			// Assign Flight Behavior
			this.behavior = FlightBehavior.AssignFlightMotion(this, paramList);

			this.Damage = DamageStrength.Standard;

			this.AssignSubType(subType);
		}

		private void AssignSubType( byte subType ) {
			if(subType == (byte)SawSubType.Small) {
				this.SpriteName = "Saw/Small";
				this.AssignBoundsByAtlas(6, 6, -6, -6);
			} else if(subType == (byte) SawSubType.Large) {
				this.SpriteName = "Saw/Large";
				this.AssignBoundsByAtlas(12, 12, -12, -12);
			} else if(subType == (byte) SawSubType.LethalSmall) {
				this.Damage = DamageStrength.Lethal;
				this.SpriteName = "Saw/Lethal";
				this.AssignBoundsByAtlas(6, 6, -6, -6);
			} else if(subType == (byte) SawSubType.LethalLarge) {
				this.Damage = DamageStrength.Lethal;
				this.SpriteName = "Saw/LethalLarge";
				this.AssignBoundsByAtlas(12, 12, -12, -12);
			}
		}

		public override bool RunCharacterImpact(Character character) {
			character.wounds.ReceiveWoundDamage(this.Damage);
			return true;
		}
	}
}
