using Microsoft.Xna.Framework.Input;
using Nexus.Engine;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class WEFuncToolEraser : WEFuncTool {

		public WEFuncToolEraser() : base() {
			this.spriteName = "Small/Eraser";
			this.title = "Eraser";
			this.description = "Erases tiles.";
		}

		public override void RunTick(WEScene scene) {
			if(UIComponent.ComponentWithFocus != null) { return; }

			// Left Mouse Button Down (Delete Current Tile)
			if(Cursor.mouseState.LeftButton == ButtonState.Pressed) {
				WorldZoneFormat zone = scene.currentZone;
				byte gridX = (byte)Cursor.MiniGridX;
				byte gridY = (byte)Cursor.MiniGridY;

				byte[] wtData = scene.worldContent.GetWorldTileData(zone, gridX, gridY);

				// Delete Tile Top if the base is not water.
				if(wtData[0] != (byte) OTerrain.Water) {
					scene.worldContent.DeleteTileTop(zone, gridX, gridY);
				}

				// Delete Tile Cover
				scene.worldContent.DeleteTileCover(zone, gridX, gridY);

				// Handle Special Deletes for Nodes
				if(scene.DeleteNodeIfPresent(gridX, gridY)) { return; }

				// Delete Objects (unless node was already deleted)
				scene.worldContent.DeleteTileObject(zone, gridX, gridY);
			}

			// Right Mouse Button Clicked (Clone Current Tile)
			else if(Cursor.RightMouseState == Cursor.MouseDownState.Clicked) {
				scene.CloneTile((byte) Cursor.MiniGridX, (byte) Cursor.MiniGridY);
			}
		}
	}
}
