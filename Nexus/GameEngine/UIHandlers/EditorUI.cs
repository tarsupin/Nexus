using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class EditorUI {

		private readonly EditorScene scene;
		private readonly GridOverlay gridUI;
		private readonly UtilityBar utilityBar;
		private readonly EditorScroller scroller;
		public readonly ContextMenu contextMenu;

		public static byte currentSlotGroup; // Tracks which wheel menu is currently selected (relevant for the Utility Bar).

		public EditorUI( EditorScene scene ) {
			this.scene = scene;
			this.gridUI = new GridOverlay(null);
			this.utilityBar = new UtilityBar(null, (byte)TilemapEnum.TileWidth * 2, (short) (Systems.screen.windowHeight - (byte)TilemapEnum.TileHeight));
			this.scroller = new EditorScroller(null);

			// Wheel Menu
			this.contextMenu = new ContextMenu(null, (short)(Systems.screen.windowWidth * 0.5f), (short)(Systems.screen.windowHeight * 0.5f));

			this.contextMenu.SetMenuOption((byte) SlotGroup.Blocks, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Brick/Brown", "Blocks");
			this.contextMenu.SetMenuOption((byte) SlotGroup.Platforms, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Platform/Fixed/S", "Platforms");
			this.contextMenu.SetMenuOption((byte) SlotGroup.Interactives, Systems.mapper.atlas[(byte)AtlasGroup.Objects], "NPC/MasterNinja", "Interactives");
			this.contextMenu.SetMenuOption((byte) SlotGroup.EnemiesLand, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Chomper/Grass/Chomp2", "Enemies");
			this.contextMenu.SetMenuOption((byte) SlotGroup.EnemiesFly, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Chomper/Grass/Chomp2", "Flying");
			this.contextMenu.SetMenuOption((byte) SlotGroup.Ground, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Grass/S", "Ground");
			this.contextMenu.SetMenuOption((byte) SlotGroup.Upgrades, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "SuitCollect/WhiteWizard", "Upgrades");
			this.contextMenu.SetMenuOption((byte) SlotGroup.Collectables, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Goodie/Heart", "Collectables");
			this.contextMenu.SetMenuOption((byte) SlotGroup.Decor, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Decor/Grass2", "Decor");
			this.contextMenu.SetMenuOption((byte) SlotGroup.Gadgets, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Cannon/Diagonal", "Gadgets");
			this.contextMenu.SetMenuOption((byte) SlotGroup.Scripting, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "HiddenObject/Cluster", "Scripting");
		}

		public void RunTick() {
			UIComponent.ComponentWithFocus = null;
			this.utilityBar.RunTick();
			this.scroller.RunTick();
			this.contextMenu.RunTick();
		}

		public void Draw() {

			int offsetX = -Systems.camera.posX % (byte)TilemapEnum.TileWidth;
			int offsetY = -Systems.camera.posY % (byte)TilemapEnum.TileHeight;

			// Draw Editor UI Components
			this.gridUI.Draw(offsetX, offsetY);

			// Draw Currently Slotted Item & Highlighted Grid Square (if not overlapping a UI component)
			TileTool tool = EditorTools.tileTool;

			if(UIComponent.ComponentWithFocus == null && tool != null) {
				EditorPlaceholder ph = tool.CurrentPlaceholder;

				if(Systems.mapper.TileDict.ContainsKey(ph.tileId)) {
					TileGameObject tgo = Systems.mapper.TileDict[ph.tileId];
					tgo.Draw(null, ph.subType, Cursor.MouseGridX * (byte)TilemapEnum.TileWidth - Systems.camera.posX, Cursor.MouseGridY * (byte)TilemapEnum.TileHeight - Systems.camera.posY);
				}

				Systems.spriteBatch.Draw(Systems.tex2dDarkRed, new Rectangle(Cursor.MouseGridX * (byte)TilemapEnum.TileWidth - Systems.camera.posX, Cursor.MouseGridY * (byte)TilemapEnum.TileHeight - Systems.camera.posY, (byte)TilemapEnum.TileWidth, (byte)TilemapEnum.TileHeight), Color.White * 0.25f);
			}

			this.utilityBar.Draw();
			this.scroller.Draw();
			this.contextMenu.Draw();

			// Coordinate Tracker
			Systems.fonts.counter.Draw(Cursor.MouseGridX + ", " + Cursor.MouseGridY, (byte) TilemapEnum.TileWidth + 12, 5, Color.White);

			// Room Counter (Which Room)
			Systems.fonts.counter.Draw("Room #" + this.scene.roomNum.ToString(), Systems.screen.windowWidth - 184, 5, Color.White);
		}
	}
}
