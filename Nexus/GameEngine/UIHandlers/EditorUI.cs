using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.GameEngine {

	public class EditorUI {

		private readonly EditorScene scene;
		private readonly UtilityBar utilityBar;
		private readonly EditorScroller scroller;
		public readonly GridOverlay gridUI;
		public readonly UIStatusText noticeText;
		public readonly UIStatusText alertText;
		public readonly ContextMenu contextMenu;
		public readonly ParamMenu moveParamMenu;
		public readonly ParamMenu actParamMenu;

		public static byte currentSlotGroup; // Tracks which wheel menu is currently selected (relevant for the Utility Bar).
		
		public EditorUI( EditorScene scene ) {
			this.scene = scene;

			// UI Components
			this.gridUI = new GridOverlay(null, (byte)TilemapEnum.TileWidth, (byte)TilemapEnum.TileHeight);
			this.utilityBar = new UtilityBar(null, (byte)TilemapEnum.TileWidth, (short) (Systems.screen.windowHeight - (byte)TilemapEnum.TileHeight));
			this.scroller = new EditorScroller(null, (short)(Systems.screen.windowWidth - (byte)TilemapEnum.TileWidth), 0);

			// Alert Texts
			this.noticeText = new UIStatusText(null, (short)Systems.screen.windowHalfWidth, 5);
			this.alertText = new UIStatusText(null, (short)Systems.screen.windowHalfWidth, Systems.screen.windowHalfHeight);

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
			this.contextMenu.SetMenuOption((byte) SlotGroup.Collectables, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Health/Pack1", "Collectables");
			this.contextMenu.SetMenuOption((byte) SlotGroup.Decor, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Decor/Grass2", "Decor");
			this.contextMenu.SetMenuOption((byte) SlotGroup.Prompts, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Prompt/DPad/Right", "Prompts");
			this.contextMenu.SetMenuOption((byte) SlotGroup.Fixtures, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Cannon/UpRight", "Fixtures");
			this.contextMenu.SetMenuOption((byte) SlotGroup.Items, Systems.mapper.atlas[(byte)AtlasGroup.Objects], "Shell/Red/Spin1", "Items");
			this.contextMenu.SetMenuOption((byte) SlotGroup.Scripting, Systems.mapper.atlas[(byte)AtlasGroup.Objects], "Cluster/Basic", "Scripting");

			// Param Menus - Wand Tool
			this.moveParamMenu = new ParamMenu(null);
			this.actParamMenu = new ParamMenu(null, true);
		}

		public void RunTick() {
			UIComponent.ComponentWithFocus = null;
			this.utilityBar.RunTick();
			this.scroller.RunTick();
			this.contextMenu.RunTick();
			this.moveParamMenu.RunTick();
			this.actParamMenu.RunTick();
		}

		public void Draw() {

			// Draw Editor UI Components
			this.gridUI.DrawGridOverlay(Systems.camera.posX, Systems.camera.posY, this.scene.CurrentRoom.xCount, this.scene.CurrentRoom.yCount);
			this.DrawCurrentGridSquare();
			this.utilityBar.Draw();
			this.scroller.Draw();
			this.contextMenu.Draw();
			this.moveParamMenu.Draw();
			this.actParamMenu.Draw();

			// Alert Text
			this.alertText.DrawAlertFrame();
			if(Cursor.MouseY > 75) { this.noticeText.DrawAlertFrame(); }

			// Coordinate Tracker
			Systems.fonts.counter.Draw((Cursor.TileGridX + 1) + ", " + (Cursor.TileGridY + 1), 12, 5, Color.White);

			// Room Counter (Which Room)
			Systems.fonts.counter.Draw("Room #" + (this.scene.curRoomID + 1).ToString(), Systems.screen.windowWidth - (byte)TilemapEnum.TileWidth - 184, 5, Color.White);
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
