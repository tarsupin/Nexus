using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class WEFuncToolWand : WEFuncTool {

		public WEFuncToolWand() : base() {
			this.spriteName = "Icons/Small/Wand";
			this.title = "Wand";
			this.description = "An advanced tool that allows you to modify properties on game objects.";
		}

		public override void RunTick(WEScene scene) {
			if(UIComponent.ComponentWithFocus != null) { return; }

			// Left Mouse Button
			if(Cursor.LeftMouseState == Cursor.MouseDownState.Clicked) {
				WorldZoneFormat zone = scene.currentZone;
				byte gridX = (byte)Cursor.MiniGridX;
				byte gridY = (byte)Cursor.MiniGridY;

				byte[] wtData = scene.worldContent.GetWorldTileData(zone, gridX, gridY);

				// If the wand clicked on a warp, then we can attempt to assign a warp link ID.
				if(NodeData.IsObjectAWarp(wtData[5])) {
					UIHandler.SetMenu(UIHandler.worldEditConsole, true);
					UIHandler.worldEditConsole.Open();
					UIHandler.worldEditConsole.SendCommand("setWarp " + gridX.ToString() + " " + gridY.ToString() + " ", false);
					ChatConsole.SendMessage("--------------------", Color.White);
					ChatConsole.SendMessage("Assign a Link ID to this Warp Node. Must be a number between 1 and 20. Warps that share the same ID will link to each other. ", Color.Red);
					ChatConsole.SendMessage("--------------------", Color.White);
					ChatConsole.SendMessage("Example: setWarp " + gridX.ToString() + " " + gridY.ToString() + " 1", Color.Green);
					ChatConsole.SendMessage("--------------------", Color.White);
				}

				// If the wand clicked on a node, then we can attempt to assign a level.
				if(NodeData.IsObjectANode(wtData[5], false, false, true)) {
					UIHandler.SetMenu(UIHandler.worldEditConsole, true);
					UIHandler.worldEditConsole.Open();
					UIHandler.worldEditConsole.SendCommand("setLevel " + gridX.ToString() + " " + gridY.ToString() + " ", false);
					ChatConsole.SendMessage("--------------------", Color.White);
					ChatConsole.SendMessage("Assign a Level ID to this Node. It can be any valid level, including official levels or levels created by other players. The original author will be credited with the level design.", Color.Red);
					ChatConsole.SendMessage("--------------------", Color.White);
					ChatConsole.SendMessage("Example: setLevel " + gridX.ToString() + " " + gridY.ToString() + " TUTORIAL_1", Color.Green);
					ChatConsole.SendMessage("--------------------", Color.White);
				}
			}

			// Right Mouse Button (Clone Current Tile)
			else if(Cursor.RightMouseState == Cursor.MouseDownState.Clicked) {
				scene.CloneTile((byte) Cursor.MiniGridX, (byte) Cursor.MiniGridY);
			}
		}
	}
}
