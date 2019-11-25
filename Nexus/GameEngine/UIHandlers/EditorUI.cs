using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class EditorUI {

		private readonly EditorScene scene;
		private readonly GridOverlay gridUI;
		private readonly UtilityBar utilityBar;
		private readonly EditorScroller scroller;
		public readonly WheelMenu wheel;

		public static DirRotate wheelDirChosen; // Tracks which wheel menu is currently selected (relevant for the Utility Bar).

		public EditorUI( EditorScene scene ) {
			this.scene = scene;
			this.gridUI = new GridOverlay(null);
			this.utilityBar = new UtilityBar(null, (byte)TilemapEnum.TileWidth * 2, (short) (Systems.screen.windowHeight - (byte)TilemapEnum.TileHeight));
			this.scroller = new EditorScroller(null);

			// Wheel Menu
			this.wheel = new WheelMenu(null, (short)(Systems.screen.windowWidth * 0.5f), (short)(Systems.screen.windowHeight * 0.5f));

			this.wheel.SetMenuOption((byte)DirRotate.UpLeft, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Brick/Brown", "Blocks");
			this.wheel.SetMenuOption((byte)DirRotate.Up, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Platform/Fixed/S", "Platforms");
			this.wheel.SetMenuOption((byte)DirRotate.UpRight, Systems.mapper.atlas[(byte)AtlasGroup.Objects], "NPC/MasterNinja", "Interactives");
			this.wheel.SetMenuOption((byte)DirRotate.Left, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Chomper/Grass/Chomp2", "Enemies");
			this.wheel.SetMenuOption((byte)DirRotate.Center, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Grass/S", "Ground");
			this.wheel.SetMenuOption((byte)DirRotate.Right, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Goodie/Heart", "Collectables");
			this.wheel.SetMenuOption((byte)DirRotate.DownLeft, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Decor/Grass2", "Decor");
			this.wheel.SetMenuOption((byte)DirRotate.Down, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Cannon/Diagonal", "Gadgets");
			this.wheel.SetMenuOption((byte)DirRotate.DownRight, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "HiddenObject/Cluster", "Scripting");
		}

		public void RunTick() {
			UIComponent.ComponentWithFocus = null;
			this.utilityBar.RunTick();
			this.scroller.RunTick();
			this.wheel.RunTick();
		}

		public void Draw() {

			int offsetX = -Systems.camera.posX % (byte)TilemapEnum.TileWidth;
			int offsetY = -Systems.camera.posY % (byte)TilemapEnum.TileHeight;

			// Draw Editor UI Components
			this.gridUI.Draw(offsetX, offsetY);

			// Draw Highlighted Grid Square (if not overlapping a UI component)
			if(UIComponent.ComponentWithFocus == null) {
				Systems.spriteBatch.Draw(Systems.tex2dDarkRed, new Rectangle(Cursor.MouseGridX * (byte)TilemapEnum.TileWidth - Systems.camera.posX, Cursor.MouseGridY * (byte)TilemapEnum.TileHeight - Systems.camera.posY, (byte)TilemapEnum.TileWidth, (byte)TilemapEnum.TileHeight), Color.DarkRed * 0.5f);
			}

			this.utilityBar.Draw(EditorUI.wheelDirChosen);
			this.scroller.Draw();
			this.wheel.Draw();

			// Coordinate Tracker
			Systems.fonts.counter.Draw(Cursor.MouseGridX + ", " + Cursor.MouseGridY, (byte) TilemapEnum.TileWidth + 12, 5, Color.White);

			// Room Counter (Which Room)
			Systems.fonts.counter.Draw("Room #" + this.scene.roomNum.ToString(), Systems.screen.windowWidth - 184, 5, Color.White);
		}
	}
}
