
namespace Nexus.GameEngine {

	public class CollideAffect {

		public static void AlignLeft( DynamicGameObject obj, GameObject obj2 ) {
			obj.physics.MoveToPosX(obj2.posX + obj2.bounds.Left - obj.bounds.Right);
		}

		public static void AlignRight( DynamicGameObject obj, GameObject obj2 ) {
			obj.physics.MoveToPosX(obj2.posX + obj2.bounds.Right - obj.bounds.Left);
		}
		
		public static void AlignUp( DynamicGameObject obj, GameObject obj2 ) {
			obj.physics.MoveToPosY(obj2.posY + obj2.bounds.Top - obj.bounds.Bottom);
		}

		public static void AlignDown( DynamicGameObject obj, GameObject obj2 ) {
			obj.physics.MoveToPosY(obj2.posY + obj2.bounds.Bottom - obj.bounds.Top);
		}
	}
}
