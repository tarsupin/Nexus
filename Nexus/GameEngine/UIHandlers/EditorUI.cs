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
		
		private string helperTitle = "";
		private string helperText = "";

		public EditorUI( EditorScene scene ) {
			this.scene = scene;
			this.gridUI = new GridOverlay(null);
			this.utilityBar = new UtilityBar(null, (byte)TilemapEnum.TileWidth * 2, (short) (Systems.screen.windowHeight - (byte)TilemapEnum.TileHeight));
			this.scroller = new EditorScroller(null);

			// Wheel Menu
			this.contextMenu = new ContextMenu(null, (short)(Systems.screen.windowWidth * 0.5f), (short)(Systems.screen.windowHeight * 0.5f));

			this.contextMenu.SetMenuOption((byte) SlotGroup.Ground, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Grass/S", "Ground");
			this.contextMenu.SetMenuOption((byte) SlotGroup.Blocks, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Brick/Brown", "Blocks");
			this.contextMenu.SetMenuOption((byte) SlotGroup.ColorToggles, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "ToggleOn/BoxBR", "Toggles");
			this.contextMenu.SetMenuOption((byte) SlotGroup.Platforms, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Platform/Fixed/S", "Platforms");
			this.contextMenu.SetMenuOption((byte) SlotGroup.EnemiesLand, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Chomper/Grass/Chomp2", "Enemies");
			this.contextMenu.SetMenuOption((byte) SlotGroup.EnemiesFly, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Chomper/Grass/Chomp2", "Flying");
			this.contextMenu.SetMenuOption((byte) SlotGroup.Interactives, Systems.mapper.atlas[(byte)AtlasGroup.Objects], "NPC/MasterNinja", "Interactives");
			this.contextMenu.SetMenuOption((byte) SlotGroup.Upgrades, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "SuitCollect/WhiteWizard", "Upgrades");
			this.contextMenu.SetMenuOption((byte) SlotGroup.Collectables, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Goodie/Heart", "Collectables");
			this.contextMenu.SetMenuOption((byte) SlotGroup.Decor, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Decor/Grass2", "Decor");
			this.contextMenu.SetMenuOption((byte) SlotGroup.Prompts, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Prompt/DPad/Right", "Prompts");
			this.contextMenu.SetMenuOption((byte) SlotGroup.Gadgets, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Cannon/Diagonal", "Gadgets");
			this.contextMenu.SetMenuOption((byte) SlotGroup.Scripting, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "HiddenObject/Cluster", "Scripting");
		}

		public void SetHelperText( string title, string text ) {
			this.helperTitle = title;
			this.helperText = text;
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

			if(UIComponent.ComponentWithFocus == null) {

				// Draw Function Tool (if active)
				if(EditorTools.funcTool != null || EditorTools.tempTool != null) {
					string toolSprite = EditorTools.tempTool != null ? EditorTools.tempTool.spriteName : EditorTools.funcTool.spriteName;
					Atlas atlas = Systems.mapper.atlas[(byte)AtlasGroup.Tiles];
					atlas.Draw(toolSprite, Cursor.MouseGridX * (byte)TilemapEnum.TileWidth - Systems.camera.posX, Cursor.MouseGridY * (byte)TilemapEnum.TileHeight - Systems.camera.posY);
				}

				// Draw AutoTile Tool (if active)
				else if(EditorTools.autoTool.IsActive) {
					EditorTools.autoTool.DrawAutoTiles();
				}

				// Draw Tile Tool (if active)
				else if(EditorTools.tileTool != null) {
					EditorPlaceholder ph = EditorTools.tileTool.CurrentPlaceholder;

					if(Systems.mapper.TileDict.ContainsKey(ph.tileId)) {
						TileGameObject tgo = Systems.mapper.TileDict[ph.tileId];
						tgo.Draw(null, ph.subType, Cursor.MouseGridX * (byte)TilemapEnum.TileWidth - Systems.camera.posX, Cursor.MouseGridY * (byte)TilemapEnum.TileHeight - Systems.camera.posY);
					}

					Systems.spriteBatch.Draw(Systems.tex2dDarkRed, new Rectangle(Cursor.MouseGridX * (byte)TilemapEnum.TileWidth - Systems.camera.posX, Cursor.MouseGridY * (byte)TilemapEnum.TileHeight - Systems.camera.posY, (byte)TilemapEnum.TileWidth, (byte)TilemapEnum.TileHeight), Color.White * 0.25f);
				}
			}

			this.utilityBar.Draw();
			this.scroller.Draw();
			this.contextMenu.Draw();

			// Helper Text
			if(Cursor.MouseY > 75 && this.helperTitle.Length > 0) {
				Vector2 measureTitle = Systems.fonts.console.font.MeasureString(this.helperTitle);
				Systems.fonts.baseText.Draw(this.helperTitle, (ushort) Systems.screen.windowHalfWidth - ((ushort) measureTitle.X / 2), 5, Color.White);

				if(this.helperText.Length > 0) {
					Vector2 measureStr = Systems.fonts.console.font.MeasureString(this.helperText);
					Systems.fonts.console.Draw(this.helperText, (ushort)Systems.screen.windowHalfWidth - ((ushort)measureStr.X / 2), 30, Color.White);
				}
			}

			// Coordinate Tracker
			Systems.fonts.counter.Draw(Cursor.MouseGridX + ", " + Cursor.MouseGridY, (byte) TilemapEnum.TileWidth + 12, 5, Color.White);

			// Room Counter (Which Room)
			Systems.fonts.counter.Draw("Room #" + this.scene.roomNum.ToString(), Systems.screen.windowWidth - 184, 5, Color.White);
		}
	}
}
