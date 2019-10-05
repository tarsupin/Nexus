using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

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

			// TODO: Basically everything in "Moosh"
			// TODO: Basically everything in "Moosh"
			// TODO: Basically everything in "Moosh"
		}

		private void AssignSubType( byte subType ) {
			if(subType == (byte) MooshSubType.Brown) {
				//this.behavior = new PrepareCharge(this, 4.5, 7, 30, 15);
				this.Texture = "Moosh/Brown/Left2";
			} else if(subType == (byte) MooshSubType.White) {
				//this.duration = 0;
				//this.update = this.repeatBounce;
				this.Texture = "Moosh/White/Left2";
			} else if(subType == (byte) MooshSubType.Purple) {
				//this.behavior = new PrepareCharge(this, 1, 9, 30, 15);
				this.Texture = "Moosh/Purple/Left2";
			}
		}

		// TODO: Change BYTE to Character
		public void GetJumpedOn( byte character ) {
			base.GetJumpedOn(character, 3);
		}
	}
}
