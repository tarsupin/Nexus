using Nexus.Engine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class WEPlaceholder {
		public bool auto = false;
		public byte tBase = 0;
		public byte top = 0;
		public byte topLay = 0;
		public byte cover = 0;
		public byte coverLay = 0;
		public byte obj = 0;
		public byte tNodeId = 0;
	}

	public class WETileTool {

		public List<WEPlaceholder[]> placeholders;
		public byte slotGroup = 0;		// Each tile tool has its own slot group metadata. Doesn't change.
		public byte index = 0;
		public byte subIndex = 0;
		public byte[] subIndexSaves = new byte[10];

		public static Dictionary<byte, WETileTool> WorldTileToolMap = new Dictionary<byte, WETileTool>() {
			{ (byte) WorldSlotGroup.Standard, new WETileToolAuto() },
		};

		public WETileTool() {
			this.placeholders = new List<WEPlaceholder[]>();
		}

		public WEPlaceholder CurrentPlaceholder {
			get { return this.placeholders[this.index][this.subIndex]; }
		}

		public void SetIndex(byte index) {
			if(this.placeholders.Count <= index) { index = 0; } // Index must be <= the number of placeholders available:
			this.index = index;
			this.subIndex = this.subIndexSaves[this.index];
		}

		public void SetSubIndex(byte subIndex) {
			WEPlaceholder[] pData = this.placeholders[this.index];
			if(subIndex >= (byte)pData.Length) { subIndex = 0; } // SubIndex must be within valid range.
			this.subIndex = subIndex;
			this.subIndexSaves[this.index] = subIndex;
		}

		public void CycleSubIndex( sbyte dir ) {
			if(dir == 0) { return; }

			WEPlaceholder[] pData = placeholders[this.index];
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

		public static WETileTool GetWorldTileToolFromTileData(byte[] tileData) {
			Dictionary<byte, WETileTool> toolMap = WETileTool.WorldTileToolMap;

			// Standard 
			byte tBase = tileData[0];
			byte top = tileData[1];
			byte topLay = tileData[2];
			byte cover = tileData[3];
			byte coverLay = tileData[4];
			byte obj = tileData[5];
			byte nodeId = tileData[6];

			// Scan each entry in WorldTileToolMap.
			for(byte slotGroupNum = 0; slotGroupNum < 8; slotGroupNum++) {
				if(!toolMap.ContainsKey(slotGroupNum)) { continue; }
				List<WEPlaceholder[]> placeholders = toolMap[slotGroupNum].placeholders;

				// Loop through each placeholder to see if a tileData match is found.
				byte phLen = (byte) placeholders.Count;

				for(byte i = 0; i < phLen; i++) {
					WEPlaceholder[] pData = placeholders[i];

					byte phSubLen = (byte) pData.Length;
					for(byte s = 0; s < phSubLen; s++) {
						WEPlaceholder ph = pData[s];

						// If the item is an object:
						if(obj > 0) {
							if(ph.obj != obj) { continue; }
						}

						// If there is a tBase value provided:
						else if(tBase > 0) {
							if(ph.tBase != tBase) { continue; }
							if(ph.cover != cover) { continue; }

							// If there is a layer, apply it if it's a b# value.
							if(topLay > (byte) OLayer.b1 && cover == 0 && top == 0) {
								if(topLay != ph.topLay && top != ph.top) { continue; }
								if(ph.topLay != topLay && ph.top == 0) { continue; }
								if(ph.auto) { continue; }

								switch(ph.topLay) {
									case (byte)OLayer.b2:
									case (byte)OLayer.b3:
									case (byte)OLayer.b4:
									case (byte)OLayer.b5:
									case (byte)OLayer.b6:
									case (byte)OLayer.b7:
									case (byte)OLayer.b8:
									case (byte)OLayer.b9:
									case (byte)OLayer.b10:
										break;
									default:
										continue;
								}
							}
						}

						// Any other results caught:
						else {
							throw new System.Exception("Unable to locate a potential match in WorldTileTool.");
						}

						// If the tileData[0] ID & SubType matches with the WorldTileTool placeholder, we've found a match.
						WETileTool clonedTool = toolMap[(byte) slotGroupNum];

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
