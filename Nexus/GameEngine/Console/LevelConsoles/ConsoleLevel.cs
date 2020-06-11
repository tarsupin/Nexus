using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.GameEngine {

	public static class ConsoleLevel {

		public static void LevelChange() {
			string currentIns = ConsoleTrack.GetArgAsString();

			ConsoleTrack.possibleTabs = "Example: `level QCALQOD16`";
			ConsoleTrack.helpText = "The level ID of the level to load.";

			if(ConsoleTrack.activate) {
				SceneTransition.ToLevel("", currentIns);
			}
		}

		public static void ConsoleTeleport() {
			string arg = ConsoleTrack.GetArgAsString();

			if(arg == "coords") {
				ConsoleLevel.ConsoleTeleportCoords();
				return;
			}

			ConsoleTrack.possibleTabs = "Options: coords, # #";
			ConsoleTrack.helpText = "The grid square (e.g. \"10, 10\") to teleport to. Or `move coords` for exact precision.";
			
			int x = ConsoleTrack.GetArgAsInt(false) * (byte) TilemapEnum.TileWidth;
			int y = ConsoleTrack.GetArgAsInt() * (byte) TilemapEnum.TileHeight;

			if(ConsoleTrack.activate && x > 0 && y > 0) {
				Character.Teleport(ConsoleTrack.character, x, y);
			}
		}

		public static void ConsoleTeleportCoords() {
			ConsoleTrack.possibleTabs = "Example: `move coords 5000 450`";
			ConsoleTrack.helpText = "The exact coordinates (e.g. \"5000, 450\") to teleport to.";
			
			int x = ConsoleTrack.GetArgAsInt();
			int y = ConsoleTrack.GetArgAsInt();

			if(ConsoleTrack.activate && x > 0 && y > 0) {
				Character.Teleport(ConsoleTrack.character, x, y);
			}
		}
	}
}
