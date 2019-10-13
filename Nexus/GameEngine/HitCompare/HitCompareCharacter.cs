
namespace Nexus.GameEngine {

	public class HitCompareCharacter : IHitCompare {

		public virtual void RunImpact( DynamicGameObject character, DynamicGameObject obj ) {

			// TODO: ALL OF THIS HAS TO BE BUILT. NEED DYNAMIC OBJECTS IN PLACE FIRST.
			// TODO: ALL OF THIS HAS TO BE BUILT. NEED DYNAMIC OBJECTS IN PLACE FIRST.
			// TODO: ALL OF THIS HAS TO BE BUILT. NEED DYNAMIC OBJECTS IN PLACE FIRST.
			// TODO: ALL OF THIS HAS TO BE BUILT. NEED DYNAMIC OBJECTS IN PLACE FIRST.

			// If the entity is intangible, don't collide with the Character.
			//if(obj is Item && obj.status.intangible > obj.scene.timer.frame) { return; }

			// If the obj has a custom character collision, run it:
			//if(THE obj HAS collideWithCharacter() method) { return obj.CollideWithCharacter( character, obj ); }

			//if(obj is Item) {}
		}
	}
}
