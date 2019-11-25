using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class UtilityBar : UIComponent {

		private enum UtilityBarEnum : byte {
			BarTiles = 26,
		}

		public static Dictionary<byte, TileTool> slots = new Dictionary<byte, TileTool>() {
			{ (byte) SlotGroup.Blocks, new TileToolBlocks() },
			{ (byte) SlotGroup.Platforms, new TileToolPlatforms() },
			{ (byte) SlotGroup.Interactives, new TileToolInteractives() },
			{ (byte) SlotGroup.EnemiesLand, new TileToolEnemyLand() },
			{ (byte) SlotGroup.EnemiesFly, new TileToolEnemyFly() },
			{ (byte) SlotGroup.Ground, new TileToolGround() },
			{ (byte) SlotGroup.Upgrades, new TileToolUpgrades() },
			{ (byte) SlotGroup.Collectables, new TileToolCollectables() },
			{ (byte) SlotGroup.Decor, new TileToolDecor() },
			{ (byte) SlotGroup.Gadgets, new TileToolGadgets() },
			{ (byte) SlotGroup.Scripting, new TileToolScripting() },
		};

		public UtilityBar( UIComponent parent, short posX, short posY ) : base(parent) {
			this.x = posX;
			this.y = posY;
			this.width = ((byte) TilemapEnum.TileWidth + 2) * (byte) UtilityBarEnum.BarTiles;
			this.height = (byte) TilemapEnum.TileHeight;
		}

		public void RunTick() {
			if(this.IsMouseOver()) { UIComponent.ComponentWithFocus = this; }
		}

		public void Draw( byte menuOpt ) {
			byte tileWidth = (byte) TilemapEnum.TileWidth + 2;

			// Draw Utility Bar Background
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.x, this.y - 2, this.width, this.height + 2), Color.DarkSlateGray);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.x + 2, this.y, this.width - 2, this.height), Color.White);

			// Tile Outlines
			for(byte i = 0; i <= (byte) UtilityBarEnum.BarTiles; i++) {
				Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.x + i * tileWidth, this.y, 2, this.height), Color.DarkSlateGray);
			}

			// Tile Icons
			if(UtilityBar.slots.ContainsKey((byte) menuOpt)) {
				Atlas atlas = Systems.mapper.atlas[(byte)AtlasGroup.Tiles];
				List<EditorPlaceholder[]> placeholders = UtilityBar.slots[(byte)menuOpt].placeholders;
				Dictionary<byte, TileGameObject> tileMap = Systems.mapper.TileMap;

				for(byte i = 0; i < 10; i++) {
					if(placeholders.Count <= i) { continue; }
					EditorPlaceholder ph = placeholders[i][0];
					byte tileId = ph.tileId;
					if(!tileMap.ContainsKey(tileId)) { continue; }

					// Draw Tile (with correct subtype)
					tileMap[tileId].Draw(null, ph.subType, this.x + i * tileWidth + 2, this.y);

					// Draw Keybind Text
					Systems.fonts.baseText.Draw((i + 1).ToString(), this.x + i * tileWidth + 4, this.y + this.height - 18, Color.DarkOrange);
				}
			}

			// Hovering Visual
			if(UIComponent.ComponentWithFocus is UtilityBar) {
				short mx = (short) Snap.GridFloor(tileWidth, Cursor.MouseX - this.x);

				Systems.spriteBatch.Draw(Systems.tex2dDarkRed, new Rectangle(this.x + mx * tileWidth, this.y, tileWidth, this.height), Color.White * 0.5f);
			}
		}
	}
}
