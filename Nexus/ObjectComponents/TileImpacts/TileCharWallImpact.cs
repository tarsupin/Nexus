using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public static class TileCharBasicImpact {

		// A Standard Tile Impact just triggers the collision methods that are commonly overridden (collideLeft(), collideRight(), etc).
		public static bool RunImpact(Character character, DirCardinal dir, DirCardinal facing = DirCardinal.None) {

			if(dir == DirCardinal.Right) {
				if(facing == DirCardinal.None || facing == DirCardinal.Left) {
					ActionMap.WallGrab.StartAction(character, DirCardinal.Right);
				}

			}
			
			else if(dir == DirCardinal.Left) {
				if(facing == DirCardinal.None || facing == DirCardinal.Right) {
					ActionMap.WallGrab.StartAction(character, DirCardinal.Left);
				}
			}
			
			else if(dir == DirCardinal.Up) {

				// End any action that ends upward:
				Action action = character.status.action;

				if(action is JumpAction || action is WallJumpAction) {
					character.status.action.EndAction(character);
				}
			}

			return true;
		}
	}
}
