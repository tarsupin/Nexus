using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class CollideTileAffect {

		public static void CollideUp( DynamicObject obj, int posY ) {
			obj.physics.touch.TouchUp();
			obj.physics.StopY();
			obj.physics.MoveToPosY(posY + (byte)TilemapEnum.TileHeight);
		}

		public static void CollideDown( DynamicObject obj, int posY ) {
			obj.physics.touch.TouchDown();
			obj.physics.StopY();
			obj.physics.MoveToPosY(posY);
		}

		public static void CollideLeft( DynamicObject obj, int posX ) {
			obj.physics.touch.TouchLeft();
			obj.physics.StopX();
			obj.physics.MoveToPosX(posX + (byte)TilemapEnum.TileWidth);
		}

		public static void CollideRight( DynamicObject obj, int posX ) {
			obj.physics.touch.TouchRight();
			obj.physics.StopX();
			obj.physics.MoveToPosX(posX);
		}
	}
}
