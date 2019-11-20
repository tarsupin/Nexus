using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class EditorUI {

		private readonly EditorScene scene;
		private readonly GridOverlay gridUI;

		public EditorUI( EditorScene scene ) {
			this.scene = scene;
			this.gridUI = new GridOverlay(null);
		}

		public void Draw() {

			int offsetX = -Systems.camera.posX % (byte)TilemapEnum.TileWidth;
			int offsetY = -Systems.camera.posY % (byte)TilemapEnum.TileHeight;

			// Draw Grid UI
			this.gridUI.Draw(offsetX, offsetY);

			// Draw Highlighted Tile
			Systems.spriteBatch.Draw(Systems.tex2dDarkRed, new Rectangle(EditorCursor.MouseGridX * (byte)TilemapEnum.TileWidth - Systems.camera.posX, EditorCursor.MouseGridY * (byte)TilemapEnum.TileHeight - Systems.camera.posY, (byte)TilemapEnum.TileWidth, (byte)TilemapEnum.TileHeight), Color.DarkRed * 0.5f);

			// Coordinate Tracker
			Systems.fonts.counter.Draw(EditorCursor.MouseGridX + ", " + EditorCursor.MouseGridY, 10, 10, Color.White);

			// Room Counter (Which Room)
			Systems.fonts.counter.Draw("Room #" + this.scene.roomNum.ToString(), Systems.screen.windowWidth - 184, 10, Color.White);
		}
	}
}
