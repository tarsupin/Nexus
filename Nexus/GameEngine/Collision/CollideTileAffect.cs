using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class CollideTileAffect {

		public static void CollideUp( DynamicGameObject obj, int posY ) {
			obj.physics.touch.TouchUp();
			obj.physics.StopY();
			CollideTileAffect.AlignDown(obj, posY);
		}

		public static void CollideDown( DynamicGameObject obj, int posY ) {
			obj.physics.touch.TouchDown();
			obj.physics.StopY();
			CollideTileAffect.AlignUp(obj, posY);
		}

		public static void CollideLeft( DynamicGameObject obj, int posX ) {
			obj.physics.touch.TouchLeft();
			obj.physics.StopX();
			CollideTileAffect.AlignRight(obj, posX);
		}

		public static void CollideRight( DynamicGameObject obj, int posX ) {
			obj.physics.touch.TouchRight();
			obj.physics.StopX();
			CollideTileAffect.AlignLeft(obj, posX);
		}

		public static void AlignUp( DynamicGameObject obj, int posY ) {
			obj.physics.MoveToPosY(posY);
		}

		public static void AlignDown( DynamicGameObject obj, int posY ) {
			obj.physics.MoveToPosY(posY + (byte)TilemapEnum.TileHeight);
		}

		public static void AlignLeft( DynamicGameObject obj, int posX ) {
			obj.physics.MoveToPosX(posX);
		}

		public static void AlignRight( DynamicGameObject obj, int posX) {
			obj.physics.MoveToPosX(posX + (byte)TilemapEnum.TileWidth);
		}
	}
}
