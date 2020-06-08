using Nexus.Objects;

namespace Nexus.GameEngine {

	public class HitCompareProjectile : IHitCompare {

		public bool RunImpact( DynamicObject projectile, DynamicObject obj ) {

			// Specific Impact Types
			if(obj is Projectile) { return false; }

			Projectile proj = (Projectile) projectile;

			// TODO: Right now, nothing happening here.

			// Standard Collision
			return false;
		}
	}
}
