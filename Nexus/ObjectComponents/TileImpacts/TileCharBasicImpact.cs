﻿using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public static class TileCharBasicImpact {

		// Allows the character to interact with walls, such as for grabbing and sliding.
		public static bool RunWallImpact(Character character, DirCardinal dir, DirCardinal facing = DirCardinal.None) {

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
				TileCharBasicImpact.RunUpwardImpact(character, dir);
			}

			else if(dir == DirCardinal.Down) {
				TileCharBasicImpact.RunDownwardImpact(character, dir);
			}

			return true;
		}

		public static bool RunUpwardImpact(Character character, DirCardinal dir) {
			if(dir != DirCardinal.Up) { return false; }

			// End any action that ends upward:
			Action action = character.status.action;

			if(action is JumpAction || action is WallJumpAction) {
				character.status.action.EndAction(character);
			}

			return true;
		}

		public static bool RunDownwardImpact(Character character, DirCardinal dir) {
			if(dir != DirCardinal.Down) { return false; }

			// End any action that ends upward:
			Action action = character.status.action;

			if(action is SlamAction) {
				SlamAction.CauseSlam();
			}

			return true;
		}
	}
}
