using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;
using static Nexus.GameEngine.Scene;

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

				// If the wand clicked on a node, then we can attempt to assign a level.
				if(NodeData.IsObjectANode(wtData[5])) {
					UIHandler.SetMenu(UIHandler.worldEditConsole, true);
					UIHandler.worldEditConsole.Open();
					UIHandler.worldEditConsole.SetInstructionText("setLevel " + gridX.ToString() + " " + gridY.ToString() + " ");
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

	// Stores data related to a selected object and the wand param menu currently open.
	public static class WorldWandData {

	}
}
