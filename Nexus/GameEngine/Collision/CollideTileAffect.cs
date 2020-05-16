using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class CollideTileAffect {

		public static void CollideUp( DynamicObject obj, int posY ) {
			obj.physics.touch.TouchUp();
			obj.physics.StopY();
			CollideTileAffect.AlignDown(obj, posY);
		}

		public static void CollideDown( DynamicObject obj, int posY ) {
			obj.physics.touch.TouchDown();
			obj.physics.StopY();
			CollideTileAffect.AlignUp(obj, posY);
		}

		public static void CollideLeft( DynamicObject obj, int posX ) {
			obj.physics.touch.TouchLeft();
			obj.physics.StopX();
			CollideTileAffect.AlignRight(obj, posX);
		}

		public static void CollideRight( DynamicObject obj, int posX ) {
			obj.physics.touch.TouchRight();
			obj.physics.StopX();
			CollideTileAffect.AlignLeft(obj, posX);
		}

		public static void AlignUp( DynamicObject obj, int posY ) {
			obj.physics.MoveToPosY(posY);
		}

		public static void AlignDown( DynamicObject obj, int posY ) {
			obj.physics.MoveToPosY(posY + (byte)TilemapEnum.TileHeight);
		}

		public static void AlignLeft( DynamicObject obj, int posX ) {
			obj.physics.MoveToPosX(posX);
		}

		public static void AlignRight( DynamicObject obj, int posX) {
			obj.physics.MoveToPosX(posX + (byte)TilemapEnum.TileWidth);
		}
	}
}
