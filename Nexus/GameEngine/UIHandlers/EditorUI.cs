using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class EditorUI {

		private readonly EditorScene scene;

		// TODO CLEANUP: Remove if unneeded.
		//private readonly Atlas atlas;
		//private readonly ushort bottomRow;

		private readonly GridOverlay gridUI;

		public EditorUI( EditorScene scene ) {
			this.scene = scene;
			//this.atlas = Systems.mapper.atlas[(byte) AtlasGroup.Tiles];
			//this.bottomRow = (ushort) (Systems.screen.windowHeight - (byte) TilemapEnum.TileHeight);

			this.gridUI = new GridOverlay(null);
		}

		public void Draw() {

			// Draw Grid UI
			this.gridUI.Draw(-Systems.camera.posX % (byte)TilemapEnum.TileWidth, -Systems.camera.posY % (byte)TilemapEnum.TileHeight);

			// Coordinate Tracker
			Systems.fonts.counter.Draw(this.scene.MouseGridX + ", " + this.scene.MouseGridY, 10, 10, Color.White);

			// Room Counter (Which Room)
			Systems.fonts.counter.Draw("Room #" + this.scene.roomNum.ToString(), Systems.screen.windowWidth - 184, 10, Color.White);
		}
	}
}
