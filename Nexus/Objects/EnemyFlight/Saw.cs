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

		public float spinRate;          // The rate of rotation, if applicable.
		public float rotation;

		public DamageStrength Damage;

		public Saw(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.Saw].meta;

			// Physics, Collisions, etc.
			this.physics = new Physics(this);
			this.SetCollide(CollideEnum.NoTileCollide);

			// Assign Flight Behavior
			this.behavior = FlightBehavior.AssignFlightMotion(this, paramList);

			this.Damage = DamageStrength.Standard;

			this.AssignSubType(subType);
		}

		public override void RunTick() {
			if(this.behavior is Behavior) { this.behavior.RunTick(); }
			this.physics.RunPhysicsTick();
			this.rotation += 0.05f;
		}

		private void AssignSubType( byte subType ) {
			if(subType == (byte)SawSubType.Small) {
				this.SpriteName = "Saw/Small";
				this.AssignBoundsByAtlas(2, 2, -2, -2);
			} else if(subType == (byte) SawSubType.Large) {
				this.SpriteName = "Saw/Large";
				this.AssignBoundsByAtlas(2, 2, -2, -2);
			} else if(subType == (byte) SawSubType.LethalSmall) {
				this.Damage = DamageStrength.InstantKill;
				this.SpriteName = "Saw/Lethal";
				this.AssignBoundsByAtlas(2, 2, -2, -2);
			} else if(subType == (byte) SawSubType.LethalLarge) {
				this.Damage = DamageStrength.InstantKill;
				this.SpriteName = "Saw/LethalLarge";
				this.AssignBoundsByAtlas(2, 2, -2, -2);
			}
		}

		public override bool RunCharacterImpact(Character character) {
			character.wounds.ReceiveWoundDamage(this.Damage);
			return true;
		}

		public override void Draw(int camX, int camY) {
			this.Meta.Atlas.DrawAdvanced(this.SpriteName, this.posX - camX, this.posY - camY, null, this.rotation);
		}
	}
}
