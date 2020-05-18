
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class CollideAffect {

		public static void AlignLeft( DynamicObject obj, DynamicObject obj2 ) {
			obj.physics.result.X = FInt.Create(obj2.posX + obj2.bounds.Left - obj.bounds.Right);
		}

		public static void AlignRight( DynamicObject obj, DynamicObject obj2 ) {
			obj.physics.result.X = FInt.Create(obj2.posX + obj2.bounds.Right - obj.bounds.Left);
		}
		
		public static void AlignUp( DynamicObject obj, DynamicObject obj2 ) {
			obj.physics.result.Y = FInt.Create(obj2.posY + obj2.bounds.Top - obj.bounds.Bottom);
		}

		public static void AlignDown( DynamicObject obj, DynamicObject obj2 ) {
			obj.physics.result.Y = FInt.Create(obj2.posY + obj2.bounds.Bottom - obj.bounds.Top);
		}
	}
}
