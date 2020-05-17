using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Objects {

	public enum ShroomSubType : byte {
		Black,
		Purple,
		Red,
	}

	public class Shroom : EnemyLand {

		public Shroom(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.Shroom].meta;
			this.AssignSubType(subType);

			// Movement
			this.speed = FInt.Create(1.40);

			// Physics, Collisions, etc.
			this.AssignBoundsByAtlas(4, 6, -6);
			this.physics = new Physics(this);
			this.physics.SetGravity(FInt.Create(0.35));
			this.physics.velocity.X = (FInt)(0-this.speed);


			// TODO: Basically everything in "Shroom"
			// TODO: Basically everything in "Shroom"
			// TODO: Basically everything in "Shroom"
		}

		private void AssignSubType( byte subType ) {
			if(subType == (byte) ShroomSubType.Black) {
				this.SetSpriteName("Shroom/Black/Left2");
			} else if(subType == (byte) ShroomSubType.Red) {
				this.SetSpriteName("Shroom/Red/Left2");
			} else if(subType == (byte) ShroomSubType.Purple) {
				//this.behavior = new PrepareCharge(this, 1, 9, 30, 15);
				this.SetSpriteName("Shroom/Purple/Left2");
			}
		}

		public override bool GetJumpedOn(Character character, sbyte bounceStrength = 6) {
			return base.GetJumpedOn(character, 6);
		}
	}
}
