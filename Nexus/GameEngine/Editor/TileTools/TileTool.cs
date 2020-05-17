using Nexus.Engine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class EditorPlaceholder {
		public byte tileId;
		public byte objectId;
		public byte subType;
		public LayerEnum layerEnum;
	}

	public class TileTool {

		public List<EditorPlaceholder[]> placeholders;
		public byte slotGroup = 0;		// Each tile tool has its own slot group metadata. Doesn't change.
		public byte index = 0;
		public byte subIndex = 0;
		public byte[] subIndexSaves = new byte[10];

		public static Dictionary<byte, TileTool> tileToolMap = new Dictionary<byte, TileTool>() {
			{ (byte) SlotGroup.Ground, new TileToolGround() },
			{ (byte) SlotGroup.Blocks, new TileToolBlocks() },
			{ (byte) SlotGroup.ColorToggles, new TileToolColorToggles() },
			{ (byte) SlotGroup.Platforms, new TileToolPlatforms() },
			{ (byte) SlotGroup.EnemiesLand, new TileToolEnemyLand() },
			{ (byte) SlotGroup.EnemiesFly, new TileToolEnemyFly() },
			{ (byte) SlotGroup.Interactives, new TileToolInteractives() },
			{ (byte) SlotGroup.Upgrades, new TileToolUpgrades() },
			{ (byte) SlotGroup.Collectables, new TileToolCollectables() },
			{ (byte) SlotGroup.Decor, new TileToolDecor() },
			{ (byte) SlotGroup.Prompts, new TileToolPrompts() },
			{ (byte) SlotGroup.Gadgets, new TileToolGadgets() },
			{ (byte) SlotGroup.Scripting, new TileToolScripting() },
		};

		public TileTool() {
			this.placeholders = new List<EditorPlaceholder[]>();
		}

		public EditorPlaceholder CurrentPlaceholder {
			get {
				return this.placeholders[this.index][this.subIndex];
			}
		}

		public void SetIndex(byte index) {
			if(this.placeholders.Count <= index) { index = 0; } // Index must be <= the number of placeholders available:
			this.index = index;
			this.subIndex = this.subIndexSaves[this.index];
		}

		public void SetSubIndex(byte subIndex) {
			EditorPlaceholder[] pData = this.placeholders[this.index];
			if(subIndex >= (byte)pData.Length) { subIndex = 0; } // SubIndex must be within valid range.
			this.subIndex = subIndex;
			this.subIndexSaves[this.index] = subIndex;
		}

		public void CycleSubIndex( sbyte dir ) {
			if(dir == 0) { return; }

			EditorPlaceholder[] pData = placeholders[this.index];
			byte phSubLen = (byte)pData.Length;

			// Cycle the SubIndex LEFT (by -1)
			if(dir == -1) {
				this.SetSubIndex(this.subIndex == 0 ? (byte)(phSubLen - 1) : (byte) (this.subIndex - 1));
			}

			// Cycle the SubIndex Right (by +1)
			else if(dir == 1) {
				this.SetSubIndex(this.subIndex >= phSubLen ? (byte) 0 : (byte)(this.subIndex + 1));
			}
		}

		public static TileTool GetTileToolFromTileData(byte[] tileData, bool isObject = false) {
			if(tileData == null) { return null; }

			// If we're retrieving a object, verify that it exists in the Object Dictionary (otherwise it's invalid).
			if(isObject) {
				Dictionary<byte, System.Type> objDict = Systems.mapper.ObjectTypeDict;
				if(!objDict.ContainsKey(tileData[0])) { return null; }
			}
			
			// If we're retrieving a tile, verify that it exists in the Tile Dictionary (otherwise it's invalid).
			else {
				Dictionary<byte, TileObject> tileDict = Systems.mapper.TileDict;
				if(!tileDict.ContainsKey(tileData[0])) { return null; }
			}

			// Loop through every TileTool in an effort to locate a match for the tile data.
			for(byte slotGroupNum = 1; slotGroupNum < 13; slotGroupNum++ ) {
				List<EditorPlaceholder[]> placeholders = TileTool.tileToolMap[(byte)slotGroupNum].placeholders;

				// Loop through each placeholders to see if a tileData ID match is found.
				byte phLen = (byte) placeholders.Count;

				for(byte i = 0; i < phLen; i++) {
					EditorPlaceholder[] pData = placeholders[i];

					byte phSubLen = (byte) pData.Length;
					for(byte s = 0; s < phSubLen; s++) {
						EditorPlaceholder ph = pData[s];

						// Check if the placeholder matches the tileData correctly:
						if(isObject) {
							if(tileData[0] != ph.objectId || tileData[1] != ph.subType) { continue; }
						} else {
							if(tileData[0] != ph.tileId || tileData[1] != ph.subType) { continue; }
						}

						// If the tileData[0] ID & SubType matches with the TileTool placeholder, we've found a match.
						TileTool clonedTool = TileTool.tileToolMap[(byte)slotGroupNum];

						// Set the default values for the tool.
						clonedTool.index = i;
						clonedTool.subIndex = s;
						clonedTool.subIndexSaves[i] = s;

						return clonedTool;
					}
				}
			}

			return null;
		} 
	}
}
