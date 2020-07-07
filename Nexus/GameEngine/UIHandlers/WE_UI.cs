using Microsoft.Xna.Framework;
using Nexus.Config;
using Nexus.Engine;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class WE_UI {

		private readonly WEScene scene;
		public Atlas atlas;

		private readonly GridOverlay gridUI;
		private readonly WEUtilityBar utilityBar;
		private readonly WEScroller scroller;
		public readonly AlertText noticeText;
		public readonly AlertText alertText;
		public readonly WEMenu weMenu;

		public static byte curWESlotGroup; // Tracks which wheel menu is currently selected (relevant for the Utility Bar).

		public WE_UI( WEScene scene ) {
			this.scene = scene;
			this.atlas = Systems.mapper.atlas[(byte) AtlasGroup.World];

			// UI Components
			this.gridUI = new GridOverlay(null, (byte) WorldmapEnum.TileWidth, (byte) WorldmapEnum.TileHeight);
			this.utilityBar = new WEUtilityBar(null, this.scene, (byte)WorldmapEnum.TileWidth, (short)(Systems.screen.windowHeight - (byte)WorldmapEnum.TileHeight));
			this.scroller = new WEScroller(null, this.scene, (short)(Systems.screen.windowWidth - (byte)WorldmapEnum.TileWidth), 0);

			// Notice + Alert Text
			this.noticeText = new AlertText(null, (short)Systems.screen.windowHalfWidth, 5);
			this.noticeText.SetColors(Color.White, Color.DarkSlateBlue);
			this.alertText = new AlertText(null, (short)Systems.screen.windowHalfWidth, Systems.screen.windowHalfHeight);
			this.alertText.SetColors(Color.White, Color.Red);

			// Tab Menu - WorldTileTool Listings
			this.weMenu = new WEMenu(null, (short)(Systems.screen.windowWidth * 0.5f), (short)(Systems.screen.windowHeight * 0.5f), 4, 2);

			this.weMenu.SetMenuOption((byte) 1, Systems.mapper.atlas[(byte)AtlasGroup.World], "Mud/b1", "Terrain");
			this.weMenu.SetMenuOption((byte) 2, Systems.mapper.atlas[(byte)AtlasGroup.World], "Desert/p7", "Detail");
			this.weMenu.SetMenuOption((byte) 3, Systems.mapper.atlas[(byte)AtlasGroup.World], "MountainBrown/s", "Coverage");
			this.weMenu.SetMenuOption((byte) 4, Systems.mapper.atlas[(byte)AtlasGroup.World], "Objects/Pyramid1", "Objects");
			this.weMenu.SetMenuOption((byte) 5, Systems.mapper.atlas[(byte)AtlasGroup.World], "Objects/NodeStrict", "Nodes");
			this.weMenu.SetMenuOption((byte) 6, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Icons/Move", "Resize");
		}

		public void RunTick() {
			UIComponent.ComponentWithFocus = null;
			this.utilityBar.RunTick();
			this.scroller.RunTick();
			this.weMenu.RunTick();
		}

		public void Draw() {

			// Draw Editor UI Components
			this.gridUI.DrawGridOverlay(Systems.camera.posX, Systems.camera.posY, this.scene.xCount, this.scene.yCount);
			this.DrawCurrentGridSquare();
			this.utilityBar.Draw();
			this.scroller.Draw();
			this.weMenu.Draw();

			// Alert Text
			this.alertText.Draw(Systems.timer.Frame);

			if(Cursor.MouseY > 75) {
				this.noticeText.Draw(Systems.timer.Frame);
			}

			// Coordinate Tracker
			Systems.fonts.counter.Draw(Cursor.MiniGridX + ", " + Cursor.MiniGridY, 12, 5, Color.White);

			// Zone Counter (Which Zone)
			Systems.fonts.counter.Draw("Zone #" + this.scene.campaign.zoneId.ToString(), Systems.screen.windowWidth - (byte)WorldmapEnum.TileWidth - 184, 5, Color.White);

			// Debug Render
			if(DebugConfig.Debug) {
				DebugConfig.DrawDebugNotes();
			}
		}

		public void DrawCurrentGridSquare() {

			// Draw Currently Slotted Item & Highlighted Grid Square (if not overlapping a UI component)
			if(UIComponent.ComponentWithFocus == null) {

				// Draw Temporary Function Tool (if active)
				if(WETools.WETempTool != null) {
					WETools.WETempTool.DrawWorldFuncTool();
				}

				// Draw Function Tool (if active)
				else if(WETools.WEFuncTool != null) {
					WETools.WEFuncTool.DrawWorldFuncTool();
				}

				// Draw Tile Tool (if active)
				else if(WETools.WETileTool != null) {
					WEPlaceholder ph = WETools.WETileTool.CurrentPlaceholder;

					// Draw Tile
					this.scene.DrawWorldTile(new byte[] { ph.tBase, ph.top, ph.topLay, ph.cover, ph.coverLay, ph.obj, 0 }, Cursor.MiniGridX * (byte)WorldmapEnum.TileWidth - Systems.camera.posX, Cursor.MiniGridY * (byte)WorldmapEnum.TileHeight - Systems.camera.posY);
				}
			}
		}

	}
}
