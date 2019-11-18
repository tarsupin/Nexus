using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public static class ConsoleLevel {

		public static void CheatCodeLevel() {
			string currentIns = ConsoleTrack.NextArg();

			ConsoleTrack.PrepareTabLookup(levelCodes, currentIns, "Debug options that relate to levels.");

			if(levelCodes.ContainsKey(currentIns)) {
				levelCodes[currentIns].Invoke();
				return;
			}
		}

		public static readonly Dictionary<string, System.Action> levelCodes = new Dictionary<string, System.Action>() {
			{ "editor", ConsoleLevel.EditorScene },
			{ "teleport", ConsoleLevel.CheatCodeTeleport },
			{ "teleport-coords", ConsoleLevel.CheatCodeTeleportCoords },
			{ "load", ConsoleLevel.LevelChange },
		};

		public static void CheatCodeTeleport() {
			ConsoleTrack.possibleTabs = "Example: `level teleport 10 10`";
			ConsoleTrack.helpText = "The grid square (e.g. \"10, 10\") to teleport to.";
			
			int x = ConsoleTrack.NextInt() * (byte) TilemapEnum.TileWidth;
			int y = ConsoleTrack.NextInt() * (byte) TilemapEnum.TileHeight;

			if(ConsoleTrack.activate) {
				Character.Teleport(ConsoleTrack.character, x, y);
			}
		}

		public static void CheatCodeTeleportCoords() {
			ConsoleTrack.possibleTabs = "Example: `level teleport-coords 5000 450`";
			ConsoleTrack.helpText = "The exact coordinates (e.g. \"5000, 450\") to teleport to.";
			
			int x = ConsoleTrack.NextInt();
			int y = ConsoleTrack.NextInt();

			if(ConsoleTrack.activate) {
				Character.Teleport(ConsoleTrack.character, x, y);
			}
		}

		public static void LevelChange() {
			string currentIns = ConsoleTrack.NextArg();

			ConsoleTrack.possibleTabs = "Example: `level load QCALQOD16`";
			ConsoleTrack.helpText = "The level ID of the level to load.";

			if(ConsoleTrack.activate) {
				SceneTransition.ToLevel("", currentIns);
			}
		}

		public static void EditorScene() {
			string currentIns = ConsoleTrack.NextArg();

			ConsoleTrack.possibleTabs = "Example: `level editor 10`";
			ConsoleTrack.helpText = "Load the level editor for one of your levels.";

			if(ConsoleTrack.activate) {

				// Prepare the current level as the default option.
				string levelId = currentIns == "" ? Systems.handler.levelContent.levelId : currentIns;

				// Transition to an Editor Scene
				SceneTransition.ToLevel("", currentIns, false, true);
			}
		}

	}
}
