using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class EditorUI {

		private readonly EditorScene scene;
		private readonly GridOverlay gridUI;
		private readonly UtilityBar utilityBar;
		private readonly EditorScroller scroller;

		public EditorUI( EditorScene scene ) {
			this.scene = scene;
			this.gridUI = new GridOverlay(null);
			this.utilityBar = new UtilityBar(null);
			this.scroller = new EditorScroller(null);
		}

		public void Draw() {

			int offsetX = -Systems.camera.posX % (byte)TilemapEnum.TileWidth;
			int offsetY = -Systems.camera.posY % (byte)TilemapEnum.TileHeight;

			// Draw Editor UI Components
			this.gridUI.Draw(offsetX, offsetY);
			this.utilityBar.Draw((byte) TilemapEnum.TileWidth * 2, Systems.screen.windowHeight - (byte) TilemapEnum.TileHeight);
			this.scroller.Draw();

			// Draw Highlighted Tile
			Systems.spriteBatch.Draw(Systems.tex2dDarkRed, new Rectangle(EditorCursor.MouseGridX * (byte)TilemapEnum.TileWidth - Systems.camera.posX, EditorCursor.MouseGridY * (byte)TilemapEnum.TileHeight - Systems.camera.posY, (byte)TilemapEnum.TileWidth, (byte)TilemapEnum.TileHeight), Color.DarkRed * 0.5f);

			// Coordinate Tracker
			Systems.fonts.counter.Draw(EditorCursor.MouseGridX + ", " + EditorCursor.MouseGridY, (byte) TilemapEnum.TileWidth + 12, 5, Color.White);

			// Room Counter (Which Room)
			Systems.fonts.counter.Draw("Room #" + this.scene.roomNum.ToString(), Systems.screen.windowWidth - 184, 5, Color.White);
		}
	}
}
