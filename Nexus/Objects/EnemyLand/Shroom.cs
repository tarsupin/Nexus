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
			this.Meta = scene.mapper.MetaList[MetaGroup.EnemyLand];
			this.AssignSubType(subType);

			// Movement
			this.speed = FInt.FromParts(1, 400);

			// Physics, Collisions, etc.
			this.physics = new Physics(this);
			this.physics.SetGravity(FInt.FromParts(0, 350));
			this.physics.velocity.X = (FInt)(0-this.speed);

			this.bounds.Left += 6;
			this.bounds.Right -= 6;
			this.bounds.Top += 4;

			// TODO: Basically everything in "Shroom"
			// TODO: Basically everything in "Shroom"
			// TODO: Basically everything in "Shroom"
		}

		private void AssignSubType( byte subType ) {
			if(subType == (byte) ShroomSubType.Black) {
				this.Texture = "Shroom/Black/Left2";
			} else if(subType == (byte) ShroomSubType.Red) {
				this.Texture = "Shroom/Red/Left2";
			} else if(subType == (byte) ShroomSubType.Purple) {
				//this.behavior = new PrepareCharge(this, 1, 9, 30, 15);
				this.Texture = "Shroom/Purple/Left2";
			}
		}

		// TODO: Change BYTE to Character
		public void GetJumpedOn( byte character ) {
			base.GetJumpedOn(character, 6);
		}
	}
}
