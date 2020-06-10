using Nexus.GameEngine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// Rings are associated with Inner Shield
	// Necklaces are associated with Outer Shield
	// Purple Jewelry is associated with Purple Regenerating Magi Balls. Others are elemental; non-regenerative.

	public class MagiShield {

		private const byte MaximumBalls = 8;

		private readonly GameObject actor;
		public ProjectileMagi[] innerBalls;		// Tracks the magi-balls within the inner shield.
		public ProjectileMagi[] outerBalls;		// Tracks the magi-balls within the outer shield.

		public MagiShield(GameObject actor) {
			this.actor = actor;
			this.innerBalls = new ProjectileMagi[MagiShield.MaximumBalls];
			this.outerBalls = new ProjectileMagi[MagiShield.MaximumBalls];
		}

		public void DestroyInnerShield() {
			for(byte i = 0; i < MagiShield.MaximumBalls; i++) {
				if(this.innerBalls[i] is ProjectileMagi) { this.innerBalls[i].DestroyForce(); }
			}
		}
		
		public void DestroyOuterShield() {
			for(byte i = 0; i < MagiShield.MaximumBalls; i++) {
				if(this.outerBalls[i] is ProjectileMagi) { this.outerBalls[i].DestroyForce(); }
			}
		}

		public void SetInnerShield( byte subType, byte ballCount, byte radius, short regenFrames = 0) {

			// Clear Existing Shield
			this.DestroyInnerShield();

			// Create First Layer of MagiBall Shield
			for(byte ballNum = 0; ballNum < ballCount; ballNum++) {

				// Find Open Projectile Slot (if available)
				ProjectileMagi projectile = ProjectileMagi.Create(this.actor, subType, ballCount, ballNum, radius, regenFrames);
				
				this.innerBalls[ballNum] = projectile;
			}
		}
		
		public void SetOuterShield( byte subType, byte ballCount, byte radius, short regenFrames = 0) {

			// Clear Existing Shield
			this.DestroyOuterShield();

			// Create First Layer of MagiBall Shield
			for(byte ballNum = 0; ballNum < ballCount; ballNum++) {

				// Find Open Projectile Slot (if available)
				ProjectileMagi projectile = ProjectileMagi.Create(this.actor, subType, ballCount, ballNum, radius, regenFrames);
				
				this.outerBalls[ballNum] = projectile;
			}
		}

	}
}
