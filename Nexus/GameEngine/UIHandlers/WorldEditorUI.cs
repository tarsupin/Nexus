using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class WorldEditorUI {

		private readonly WorldEditorScene scene;
		public Atlas atlas;
		private readonly ushort bottomRow;
		private readonly WorldContent worldContent;

		private readonly GridOverlay gridUI;
		private readonly WorldUtilityBar utilityBar;
		private readonly WorldEditorScroller scroller;
		public readonly ContextMenu contextMenu;

		public static byte currentSlotGroup; // Tracks which wheel menu is currently selected (relevant for the Utility Bar).

		private string helperTitle = "";
		private string helperText = "";

		public WorldEditorUI( WorldEditorScene scene ) {
			this.scene = scene;
			this.atlas = Systems.mapper.atlas[(byte) AtlasGroup.World];
			this.bottomRow = (ushort)(Systems.screen.windowHeight - (byte)WorldmapEnum.TileHeight);
			this.worldContent = this.scene.worldContent;

			// UI Components
			this.gridUI = new GridOverlay(null, 45, 26, (byte) WorldmapEnum.TileWidth, (byte) WorldmapEnum.TileHeight);
			this.utilityBar = new WorldUtilityBar(null, (byte)WorldmapEnum.TileWidth, (short)(Systems.screen.windowHeight - (byte)WorldmapEnum.TileHeight));
			this.scroller = new WorldEditorScroller(null, (short)(Systems.screen.windowWidth - (byte)WorldmapEnum.TileWidth), 0);

			// Tab Menu - WorldTileTool Listings
			this.contextMenu = new ContextMenu(null, (short)(Systems.screen.windowWidth * 0.5f), (short)(Systems.screen.windowHeight * 0.5f));

			this.contextMenu.SetMenuOption((byte)SlotGroup.Ground, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Grass/S", "Ground");
			this.contextMenu.SetMenuOption((byte)SlotGroup.Blocks, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Brick/Brown", "Blocks");
			this.contextMenu.SetMenuOption((byte)SlotGroup.ColorToggles, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "ToggleOn/BoxBR", "Toggles");
			this.contextMenu.SetMenuOption((byte)SlotGroup.Platforms, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Platform/Fixed/S", "Platforms");
			this.contextMenu.SetMenuOption((byte)SlotGroup.EnemiesLand, Systems.mapper.atlas[(byte)AtlasGroup.Objects], "Shroom/Red/Left2", "Enemies");
			this.contextMenu.SetMenuOption((byte)SlotGroup.EnemiesFly, Systems.mapper.atlas[(byte)AtlasGroup.Objects], "Buzz/Left2", "Flying");
			this.contextMenu.SetMenuOption((byte)SlotGroup.Interactives, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "NPC/MasterNinja", "Interactives");
			this.contextMenu.SetMenuOption((byte)SlotGroup.Upgrades, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "SuitCollect/WhiteWizard", "Upgrades");
			this.contextMenu.SetMenuOption((byte)SlotGroup.Collectables, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Goodie/Heart", "Collectables");
			this.contextMenu.SetMenuOption((byte)SlotGroup.Decor, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Decor/Grass2", "Decor");
			this.contextMenu.SetMenuOption((byte)SlotGroup.Prompts, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Prompt/DPad/Right", "Prompts");
			this.contextMenu.SetMenuOption((byte)SlotGroup.Gadgets, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Cannon/Diagonal", "Gadgets");
			this.contextMenu.SetMenuOption((byte)SlotGroup.Scripting, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "HiddenObject/Cluster", "Scripting");
		}

		public void RunTick() {
			UIComponent.ComponentWithFocus = null;
			this.utilityBar.RunTick();
			this.scroller.RunTick();
			this.contextMenu.RunTick();
		}

		public void SetHelperText(string title, string text) {
			this.helperTitle = title;
			this.helperText = text;
		}

		public void Draw() {

			int offsetX = -Systems.camera.posX % (byte)WorldmapEnum.TileWidth;
			int offsetY = -Systems.camera.posY % (byte)WorldmapEnum.TileHeight;

			// Draw Editor UI Components
			this.gridUI.Draw(offsetX, offsetY);

			// Draw Currently Slotted Item & Highlighted Grid Square (if not overlapping a UI component)

			if(UIComponent.ComponentWithFocus == null) {

				// Draw Temporary Function Tool (if active)
				if(WorldEditorTools.WorldTempTool != null) {
					WorldEditorTools.WorldTempTool.DrawWorldFuncTool();
				}

				// Draw Function Tool (if active)
				else if(WorldEditorTools.WorldFuncTool != null) {
					WorldEditorTools.WorldFuncTool.DrawWorldFuncTool();
				}

				// Draw AutoTile Tool (if active)
				else if(WorldEditorTools.autoTool.IsActive) {
					WorldEditorTools.autoTool.DrawAutoTiles();
				}

				// Draw Tile Tool (if active)
				else if(WorldEditorTools.WorldTileTool != null) {
					WEPlaceholder ph = WorldEditorTools.WorldTileTool.CurrentPlaceholder;

					// Draw Tile
					this.scene.DrawWorldTile(new byte[] { ph.tBase, ph.tTop, ph.tCat, ph.tLayer, ph.tObj, 0 }, (ushort) (Cursor.MiniGridX * (byte)WorldmapEnum.TileWidth - Systems.camera.posX), (ushort) (Cursor.MiniGridY * (byte)WorldmapEnum.TileHeight - Systems.camera.posY));
				}
			}

			this.utilityBar.Draw();
			this.scroller.Draw();
			this.contextMenu.Draw();

			// Helper Text
			if(Cursor.MouseY > 75 && this.helperTitle.Length > 0) {
				Vector2 measureTitle = Systems.fonts.console.font.MeasureString(this.helperTitle);
				Systems.fonts.baseText.Draw(this.helperTitle, (ushort)Systems.screen.windowHalfWidth - ((ushort)measureTitle.X / 2), 5, Color.White);

				if(this.helperText.Length > 0) {
					Vector2 measureStr = Systems.fonts.console.font.MeasureString(this.helperText);
					Systems.fonts.console.Draw(this.helperText, (ushort)Systems.screen.windowHalfWidth - ((ushort)measureStr.X / 2), 30, Color.White);
				}
			}

			// Coordinate Tracker
			Systems.fonts.counter.Draw(Cursor.MiniGridX + ", " + Cursor.MiniGridY, 12, 5, Color.White);

			// Zone Counter (Which Zone)
			Systems.fonts.counter.Draw("Zone #" + this.scene.campaign.zoneId.ToString(), Systems.screen.windowWidth - (byte)WorldmapEnum.TileWidth - 184, 5, Color.White);
		}
	}
}
