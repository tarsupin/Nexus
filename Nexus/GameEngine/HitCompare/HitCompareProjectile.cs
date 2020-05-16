using Nexus.Objects;

namespace Nexus.GameEngine {

	public class HitCompareProjectile : IHitCompare {

		public virtual bool RunImpact( DynamicObject projectile, DynamicObject obj ) {

			// TODO: ALL OF THIS HAS TO BE BUILT. NEED DYNAMIC OBJECTS IN PLACE FIRST.
			// TODO: ALL OF THIS HAS TO BE BUILT. NEED DYNAMIC OBJECTS IN PLACE FIRST.

			// Specific Impact Types
			if(obj is Projectile) { return false; }

			Projectile proj = (Projectile)projectile;

			// TODO: Right now, nothing happening here.

			// Standard Collision
			return false;
		}
	}
}
