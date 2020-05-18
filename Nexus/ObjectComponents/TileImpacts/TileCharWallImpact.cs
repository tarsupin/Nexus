using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public static class TileCharBasicImpact {

		public static bool RunImpact(Character character, DirCardinal dir, DirCardinal facing = DirCardinal.Center) {

			if(dir == DirCardinal.Right) {
				if(facing == DirCardinal.Center || facing == DirCardinal.Left) {
					ActionMap.WallGrab.StartAction(character, DirCardinal.Right);
				}

			}
			
			else if(dir == DirCardinal.Left) {
				if(facing == DirCardinal.Center || facing == DirCardinal.Right) {
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
