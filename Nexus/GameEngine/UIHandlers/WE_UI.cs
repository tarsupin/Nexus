using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class WE_UI {

		private readonly WEScene scene;
		public Atlas atlas;
		private readonly ushort bottomRow;
		private readonly WorldContent worldContent;

		private readonly GridOverlay gridUI;
		private readonly WEUtilityBar utilityBar;
		private readonly WEScroller scroller;
		public readonly WEMenu weMenu;

		public static byte curWESlotGroup; // Tracks which wheel menu is currently selected (relevant for the Utility Bar).

		private string helperTitle = "";
		private string helperText = "";

		public WE_UI( WEScene scene ) {
			this.scene = scene;
			this.atlas = Systems.mapper.atlas[(byte) AtlasGroup.World];
			this.bottomRow = (ushort)(Systems.screen.windowHeight - (byte)WorldmapEnum.TileHeight);
			this.worldContent = this.scene.worldContent;

			// UI Components
			this.gridUI = new GridOverlay(null, 45, 28, (byte) WorldmapEnum.TileWidth, (byte) WorldmapEnum.TileHeight);
			this.utilityBar = new WEUtilityBar(null, this.scene, (byte)WorldmapEnum.TileWidth, (short)(Systems.screen.windowHeight - (byte)WorldmapEnum.TileHeight));
			this.scroller = new WEScroller(null, this.scene, (short)(Systems.screen.windowWidth - (byte)WorldmapEnum.TileWidth), 0);

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

		public void SetHelperText(string title, string text) {
			this.helperTitle = title;
			this.helperText = text;
		}

		public void Draw() {

			int offsetX = -Systems.camera.posX % (byte)WorldmapEnum.TileWidth;
			int offsetY = -Systems.camera.posY % (byte)WorldmapEnum.TileHeight;

			// Draw Editor UI Components
			this.gridUI.Draw(offsetX, offsetY);

			// Disability visibility of certain UI components if the console is visible.
			if(!Systems.worldEditConsole.visible) {
				this.DrawCurrentGridSquare();
				this.utilityBar.Draw();
				this.scroller.Draw();
				this.weMenu.Draw();
			}

			// Helper Text
			if(Cursor.MouseY > 75 && this.helperTitle.Length > 0) {
				Vector2 measureTitle = Systems.fonts.baseText.font.MeasureString(this.helperTitle);
				Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle((ushort)Systems.screen.windowHalfWidth - ((ushort)measureTitle.X / 2) - 2, 5 - 2, (int) (measureTitle.X + 4), (int) (measureTitle.Y + 4)), Color.DarkSlateGray);
				Systems.fonts.baseText.Draw(this.helperTitle, (ushort)Systems.screen.windowHalfWidth - ((ushort)measureTitle.X / 2), 5, Color.White);

				if(this.helperText.Length > 0) {
					Vector2 measureStr = Systems.fonts.console.font.MeasureString(this.helperText);
					Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle((ushort)Systems.screen.windowHalfWidth - ((ushort)measureStr.X / 2) - 2, 30 - 2, (int) measureStr.X + 4, (int) measureStr.Y + 4), Color.DarkSlateGray);
					Systems.fonts.console.Draw(this.helperText, (ushort)Systems.screen.windowHalfWidth - ((ushort)measureStr.X / 2), 30, Color.White);
				}
			}

			// Coordinate Tracker
			Systems.fonts.counter.Draw(Cursor.MiniGridX + ", " + Cursor.MiniGridY, 12, 5, Color.White);

			// Zone Counter (Which Zone)
			Systems.fonts.counter.Draw("Zone #" + this.scene.campaign.zoneId.ToString(), Systems.screen.windowWidth - (byte)WorldmapEnum.TileWidth - 184, 5, Color.White);
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
