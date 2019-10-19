using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public enum ShroomSubType : byte {
		Black,
		Purple,
		Red,
	}

	public class Shroom : EnemyLand {

		public Shroom(LevelScene scene, byte subType, FVector pos, object[] paramList) : base(scene, subType, pos, paramList) {
			this.Meta = Systems.mapper.MetaList[MetaGroup.EnemyLand];
			this.AssignSubType(subType);

			// Movement
			this.speed = FInt.Create(1.40);

			// Physics, Collisions, etc.
			this.AssignBoundsByAtlas(4, 6, -6);
			this.physics = new Physics(this);
			this.physics.SetGravity(FInt.Create(0.35));
			this.physics.velocity.X = (FInt)(0-this.speed);
			this.impact = new Impact(this);


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

		public void GetJumpedOn( Character character ) {
			base.GetJumpedOn(character, 6);
		}
	}
}
