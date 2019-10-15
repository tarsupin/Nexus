using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public static class TileCharWallImpact {

		// A Standard Tile Impact just triggers the collision methods that are commonly overridden (collideLeft(), collideRight(), etc).
		public static bool RunImpact(Character character, bool moveRight, DirCardinal facing = DirCardinal.Center) {

			if(moveRight) {
				if(facing == DirCardinal.Center || facing == DirCardinal.Left) {
					ActionMap.WallGrab.StartAction(character, DirCardinal.Right);
				}

			} else {
				if(facing == DirCardinal.Center || facing == DirCardinal.Right) {
					ActionMap.WallGrab.StartAction(character, DirCardinal.Left);
				}
			}

			return true;
		}
	}
}
