using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class CollideAffect {

		// To Align Left, use hor -1. To Align Right, use hor 1.
		public static void AlignHorizontal( DynamicGameObject obj, GameObject obj2, sbyte hor ) {
			obj.physics.MoveToPosX(obj.pos.X - CollideDetect.GetOverlapX(obj, obj2, hor ));
		}

		// To Align Up, use vert -1. To Align Right, use vert 1.
		public static void AlignVertical( DynamicGameObject obj, GameObject obj2, sbyte vert ) {
			obj.physics.MoveToPosY(obj.pos.Y - CollideDetect.GetOverlapY(obj, obj2, vert ));
		}
	}
}
