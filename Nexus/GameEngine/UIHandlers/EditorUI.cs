using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.GameEngine {

	public class EditorUI {

		private readonly EditorScene scene;
		private readonly GridOverlay gridUI;
		private readonly UtilityBar utilityBar;
		private readonly EditorScroller scroller;
		public readonly ContextMenu contextMenu;
		public readonly ParamMenu paramMenu;

		public static byte currentSlotGroup; // Tracks which wheel menu is currently selected (relevant for the Utility Bar).
		
		private string helperTitle = "";
		private string helperText = "";

		public EditorUI( EditorScene scene ) {
			this.scene = scene;

			// UI Components
			this.gridUI = new GridOverlay(null);
			this.utilityBar = new UtilityBar(null, (byte)TilemapEnum.TileWidth, (short) (Systems.screen.windowHeight - (byte)TilemapEnum.TileHeight));
			this.scroller = new EditorScroller(null, (short)(Systems.screen.windowWidth - (byte)TilemapEnum.TileWidth), 0);

			// Tab Menu - TileTool Listings
			this.contextMenu = new ContextMenu(null, (short)(Systems.screen.windowWidth * 0.5f), (short)(Systems.screen.windowHeight * 0.5f));

			this.contextMenu.SetMenuOption((byte) SlotGroup.Ground, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Grass/S", "Ground");
			this.contextMenu.SetMenuOption((byte) SlotGroup.Blocks, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Brick/Brown", "Blocks");
			this.contextMenu.SetMenuOption((byte) SlotGroup.ColorToggles, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "ToggleOn/BoxBR", "Toggles");
			this.contextMenu.SetMenuOption((byte) SlotGroup.Platforms, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Platform/Fixed/S", "Platforms");
			this.contextMenu.SetMenuOption((byte) SlotGroup.EnemiesLand, Systems.mapper.atlas[(byte)AtlasGroup.Objects], "Shroom/Red/Left2", "Enemies");
			this.contextMenu.SetMenuOption((byte) SlotGroup.EnemiesFly, Systems.mapper.atlas[(byte)AtlasGroup.Objects], "Buzz/Left2", "Flying");
			this.contextMenu.SetMenuOption((byte) SlotGroup.Interactives, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "NPC/MasterNinja", "Interactives");
			this.contextMenu.SetMenuOption((byte) SlotGroup.Upgrades, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "SuitCollect/WhiteWizard", "Upgrades");
			this.contextMenu.SetMenuOption((byte) SlotGroup.Collectables, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Goodie/Heart", "Collectables");
			this.contextMenu.SetMenuOption((byte) SlotGroup.Decor, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Decor/Grass2", "Decor");
			this.contextMenu.SetMenuOption((byte) SlotGroup.Prompts, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Prompt/DPad/Right", "Prompts");
			this.contextMenu.SetMenuOption((byte) SlotGroup.Gadgets, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Cannon/Diagonal", "Gadgets");
			this.contextMenu.SetMenuOption((byte) SlotGroup.Scripting, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "HiddenObject/Cluster", "Scripting");

			// Param Menu - Wand Tool
			this.paramMenu = new ParamMenu(null);
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
			this.paramMenu.RunTick();
		}

		public void Draw() {

			int offsetX = -Systems.camera.posX % (byte)TilemapEnum.TileWidth;
			int offsetY = -Systems.camera.posY % (byte)TilemapEnum.TileHeight;

			// Draw Editor UI Components
			this.gridUI.Draw(offsetX, offsetY);

			// Disability visibility of certain UI components if the console is visible.
			if(!Systems.editorConsole.visible) {
				this.DrawCurrentGridSquare();
				this.utilityBar.Draw();
				this.scroller.Draw();
				this.contextMenu.Draw();
				this.paramMenu.Draw();
			}

			// Helper Text
			if(Cursor.MouseY > 75 && this.helperTitle.Length > 0) {
				Vector2 measureTitle = Systems.fonts.baseText.font.MeasureString(this.helperTitle);
				Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle((ushort)Systems.screen.windowHalfWidth - ((ushort)measureTitle.X / 2) - 2, 5 - 2, (int)(measureTitle.X + 4), (int)(measureTitle.Y + 4)), Color.DarkSlateGray);
				Systems.fonts.baseText.Draw(this.helperTitle, (ushort) Systems.screen.windowHalfWidth - ((ushort) measureTitle.X / 2), 5, Color.White);

				if(this.helperText.Length > 0) {
					Vector2 measureStr = Systems.fonts.console.font.MeasureString(this.helperText);
					Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle((ushort)Systems.screen.windowHalfWidth - ((ushort)measureStr.X / 2) - 2, 30 - 2, (int)measureStr.X + 4, (int)measureStr.Y + 4), Color.DarkSlateGray);
					Systems.fonts.console.Draw(this.helperText, (ushort)Systems.screen.windowHalfWidth - ((ushort)measureStr.X / 2), 30, Color.White);
				}
			}

			// Coordinate Tracker
			Systems.fonts.counter.Draw(Cursor.TileGridX + ", " + Cursor.TileGridY, 12, 5, Color.White);

			// Room Counter (Which Room)
			Systems.fonts.counter.Draw("Room #" + this.scene.roomNum.ToString(), Systems.screen.windowWidth - (byte)TilemapEnum.TileWidth - 184, 5, Color.White);
		}

		public void DrawCurrentGridSquare() {

			// Draw Currently Slotted Item & Highlighted Grid Square (if not overlapping a UI component)
			if(UIComponent.ComponentWithFocus == null) {

				// Draw Temporary Function Tool (if active)
				if(EditorTools.tempTool != null) {
					EditorTools.tempTool.DrawFuncTool();
				}

				// Draw Function Tool (if active)
				else if(EditorTools.funcTool != null) {
					EditorTools.funcTool.DrawFuncTool();
				}

				// Draw AutoTile Tool (if active)
				else if(EditorTools.autoTool.IsActive) {
					EditorTools.autoTool.DrawAutoTiles();
				}

				// Draw Tile Tool (if active)
				else if(EditorTools.tileTool != null) {
					EditorPlaceholder ph = EditorTools.tileTool.CurrentPlaceholder;

					// Draw Tile
					if(ph.tileId > 0) {
						if(Systems.mapper.TileDict.ContainsKey(ph.tileId)) {
							TileObject tgo = Systems.mapper.TileDict[ph.tileId];
							tgo.Draw(null, ph.subType, Cursor.TileGridX * (byte)TilemapEnum.TileWidth - Systems.camera.posX, Cursor.TileGridY * (byte)TilemapEnum.TileHeight - Systems.camera.posY);
						}
					}

					// Draw Object
					else if(ph.objectId > 0) {
						ShadowTile.Draw(ph.objectId, ph.subType, null, Cursor.TileGridX * (byte)TilemapEnum.TileWidth - Systems.camera.posX, Cursor.TileGridY * (byte)TilemapEnum.TileHeight - Systems.camera.posY);
					}

					Systems.spriteBatch.Draw(Systems.tex2dDarkRed, new Rectangle(Cursor.TileGridX * (byte)TilemapEnum.TileWidth - Systems.camera.posX, Cursor.TileGridY * (byte)TilemapEnum.TileHeight - Systems.camera.posY, (byte)TilemapEnum.TileWidth, (byte)TilemapEnum.TileHeight), Color.White * 0.25f);
				}
			}
		}
	}
}
