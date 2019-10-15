using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public enum MooshSubType : byte {
		Brown,
		Purple,
		White,
	}

	public class Moosh : EnemyLand {

		public Moosh(LevelScene scene, byte subType, FVector pos, object[] paramList) : base(scene, subType, pos, paramList) {
			this.Meta = scene.mapper.MetaList[MetaGroup.EnemyLand];
			this.AssignSubType(subType);

			// Movement
			this.speed = FInt.Create(1.4);

			// Physics, Collisions, etc.
			this.AssignBoundsByAtlas(4, 4, -4);
			this.physics = new Physics(this);
			this.physics.SetGravity(FInt.Create(0.7));
			this.physics.velocity.X = (FInt)(0-this.speed);
			this.impact = new Impact(this);


			// TODO: Basically everything in "Moosh"
			// TODO: Basically everything in "Moosh"
			// TODO: Basically everything in "Moosh"
		}

		private void AssignSubType( byte subType ) {
			if(subType == (byte) MooshSubType.Brown) {
				//this.behavior = new PrepareCharge(this, 4.5, 7, 30, 15);
				this.SpriteName = "Moosh/Brown/Left2";
			} else if(subType == (byte) MooshSubType.White) {
				//this.duration = 0;
				//this.update = this.repeatBounce;
				this.SpriteName = "Moosh/White/Left2";
			} else if(subType == (byte) MooshSubType.Purple) {
				//this.behavior = new PrepareCharge(this, 1, 9, 30, 15);
				this.SpriteName = "Moosh/Purple/Left2";
			}
		}

		// TODO: Change BYTE to Character
		public void GetJumpedOn( byte character ) {
			base.GetJumpedOn(character, 3);
		}
	}
}
