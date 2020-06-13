using Nexus.GameEngine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// Rings are associated with Fewer Balls
	// Necklaces are associated with More Balls
	// Purple Jewelry is associated with Purple Regenerating Magi Balls. Others are elemental; non-regenerative.

	public class MagiShield {

		private const byte MaximumBalls = 8;

		private readonly GameObject actor;
		public string IconTexture { get; protected set; }

		public ProjectileMagi[] balls;		// Tracks the magi-balls within the shield.

		public MagiShield(GameObject actor) {
			this.actor = actor;
			this.balls = new ProjectileMagi[MagiShield.MaximumBalls];
		}

		public void SetIconTexture( string iconTexture ) {
			this.IconTexture = iconTexture;
		}

		public void CheckShieldEnd() {
			for(byte i = 0; i < MagiShield.MaximumBalls; i++) {
				if(this.balls[i] is ProjectileMagi && this.balls[i].isAlive) { return; }
			}
			
			// Clear the Icon Texture
			this.SetIconTexture(null);
		}

		public void DestroyShield(byte startIndex) {
			for(byte i = startIndex; i < MagiShield.MaximumBalls; i++) {
				if(this.balls[i] is ProjectileMagi) {
					if(this.balls[i].isAlive == true) { this.balls[i].DestroyFinal(); }
				}
			}
		}
		
		public void SetShield(byte subType, byte ballCount, byte radius, short regenFrames = 0) {

			// Clear Existing Shield
			this.DestroyShield(ballCount);

			for(byte ballNum = 0; ballNum < ballCount; ballNum++) {
				ProjectileMagi projectile = this.balls[ballNum];

				if(projectile != null) {
					projectile.ResetMagiBall(this, this.actor, subType, ballCount, ballNum, radius, regenFrames);
				}
				
				else {
					this.balls[ballNum] = ProjectileMagi.Create(this, this.actor, subType, ballCount, ballNum, radius, regenFrames);
				}
			}
		}
	}
}
