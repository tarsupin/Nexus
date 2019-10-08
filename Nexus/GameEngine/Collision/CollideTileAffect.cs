using Nexus.Engine;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class CollideTileAffect {

		public static void CollideUp( DynamicGameObject obj, int posY ) {
			obj.physics.touch.TouchUp();
			CollideTileAffect.AlignDown(obj, posY);
			obj.physics.StopY();
		}

		public static void CollideDown( DynamicGameObject obj, int posY ) {
			obj.physics.touch.TouchDown();
			CollideTileAffect.AlignUp(obj, posY);
			obj.physics.StopY();
		}

		public static void CollideLeft( DynamicGameObject obj, int posX ) {
			obj.physics.touch.TouchLeft();
			CollideTileAffect.AlignRight(obj, posX);
			obj.physics.StopY();
		}

		public static void CollideRight( DynamicGameObject obj, int posX ) {
			obj.physics.touch.TouchRight();
			CollideTileAffect.AlignLeft(obj, posX);
			obj.physics.StopY();
		}

		public static void AlignUp( DynamicGameObject obj, int posY ) {
			obj.physics.MoveToPosY((FInt) (posY - obj.bounds.Bottom - 1));	// -1 is to account for True Bounds Height
		}

		public static void AlignDown( DynamicGameObject obj, int posY ) {
			obj.physics.MoveToPosY((FInt)(posY + (byte) TilemapEnum.TileHeight));
		}

		public static void AlignLeft( DynamicGameObject obj, int posX ) {
			obj.physics.MoveToPosX((FInt) (posX - obj.bounds.Right - 1));   // -1 is to account for True Bounds Width
		}

		public static void AlignRight( DynamicGameObject obj, int posX) {
			obj.physics.MoveToPosX((FInt)(posX + (byte) TilemapEnum.TileHeight));
		}
	}
}
