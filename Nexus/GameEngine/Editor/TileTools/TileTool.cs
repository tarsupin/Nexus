using Nexus.Engine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class EditorPlaceholder {
		public byte tileId;
		public byte subType;
	}

	public class TileTool {

		public List<EditorPlaceholder[]> placeholders;
		public byte slotGroup = 0;		// Each tile tool has its own slot group metadata. Doesn't change.
		public int index = 0;
		public int subIndex = 0;

		public static Dictionary<byte, TileTool> tileToolMap = new Dictionary<byte, TileTool>() {
			{ (byte) SlotGroup.Ground, new TileToolGround() },
			{ (byte) SlotGroup.Blocks, new TileToolBlocks() },
			{ (byte) SlotGroup.Platforms, new TileToolPlatforms() },
			{ (byte) SlotGroup.Interactives, new TileToolInteractives() },
			{ (byte) SlotGroup.EnemiesLand, new TileToolEnemyLand() },
			{ (byte) SlotGroup.EnemiesFly, new TileToolEnemyFly() },
			{ (byte) SlotGroup.Upgrades, new TileToolUpgrades() },
			{ (byte) SlotGroup.Collectables, new TileToolCollectables() },
			{ (byte) SlotGroup.Decor, new TileToolDecor() },
			{ (byte) SlotGroup.Gadgets, new TileToolGadgets() },
			{ (byte) SlotGroup.Scripting, new TileToolScripting() },
		};

		public TileTool() {
			this.placeholders = new List<EditorPlaceholder[]>();
		}

		public static TileTool GetTileToolFromTileData(byte[] tileData) {
			if(tileData == null) { return null; }

			Dictionary<byte, TileGameObject> tileDict = Systems.mapper.TileDict;

			// If the Tile Dictionary does not contain the tileData[0] ID, it means this is not a valid tile.
			if(!tileDict.ContainsKey(tileData[0])) {
				return null;
			}

			// Loop through every TileTool in an effort to locate a match for the tile data.
			for(byte slotGroupNum = 1; slotGroupNum < 10; slotGroupNum++ ) {
				List<EditorPlaceholder[]> placeholders = TileTool.tileToolMap[(byte)slotGroupNum].placeholders;

				// Loop through each placeholders to see if a tileData ID match is found.
				byte phLen = (byte) placeholders.Count;

				for(byte i = 0; i < phLen; i++) {
					EditorPlaceholder[] pData = placeholders[i];

					byte phSubLen = (byte) pData.Length;
					for(byte s = 0; s < phSubLen; s++) {
						EditorPlaceholder ph = pData[s];

						// If the tileData[0] ID & SubType matches with the TileTool placeholder, we've found a match.
						if(tileData[0] == ph.tileId && tileData[1] == ph.subType) {
							TileTool clonedTool = TileTool.tileToolMap[(byte)slotGroupNum];

							// Set the default values for the tool.
							clonedTool.index = i;
							clonedTool.subIndex = s;

							return clonedTool;
						}
					}
				}
			}

			return null;
		} 
	}
}
