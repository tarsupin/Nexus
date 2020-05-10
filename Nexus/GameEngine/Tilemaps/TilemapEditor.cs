using Nexus.Gameplay;
using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class TilemapEditor : TilemapBool {

		// Tile Data: Dictionaries of data that matches to the gridID (gridY*xCount + gridX)
		private Dictionary<uint, byte[]> tiles;      // ID, SubType, Foreground ID, Foreground SubType

		// Width and Height of the Tilemap:
		public int Width { get; protected set; }
		public int Height { get; protected set; }
		public ushort XCount { get; protected set; }
		public ushort YCount { get; protected set; }

		public TilemapEditor(ushort xCount, ushort yCount) {

			// Sizing
			this.XCount = xCount;
			this.YCount = yCount;
			this.Width = xCount * (byte)TilemapEnum.TileWidth;
			this.Height = yCount * (byte)TilemapEnum.TileHeight;

			// Data
			this.tiles = new Dictionary<uint, byte[]>();
		}

		public byte[] GetTileDataAtGridID(uint gridId) {
			this.tiles.TryGetValue(gridId, out byte[] val);
			return val;
		}

		public byte[] GetTileDataAtGrid(ushort gridX, ushort gridY) {
			uint gridId = this.GetGridID(gridX, gridY);
			this.tiles.TryGetValue(gridId, out byte[] val);
			return val;
		}

		// For performance reasons, it is up to the user to avoid exceeding the grid's X,Y limits.
		public void AddTileAtGrid(ushort gridX, ushort gridY, byte id = 0, byte subType = 0, byte fgId = 0, byte fgSubType = 0) {
			uint gridId = this.GetGridID(gridX, gridY);
			this.SetTile(gridId, id, subType, fgId, fgSubType);
		}

		// For performance reasons, it is up to the user to avoid exceeding the grid's X,Y limits.
		public void SetTile(uint gridId, byte id = 0, byte subType = 0, byte fgId = 0, byte fgSubType = 0) {

			if(!this.tiles.ContainsKey(gridId)) {
				this.tiles[gridId] = new byte[4] { id, subType, fgId, fgSubType };

			} else {
				if(id > 0) { this.tiles[gridId][0] = id; this.tiles[gridId][1] = subType; }
				if(fgId > 0) { this.tiles[gridId][2] = fgId; this.tiles[gridId][3] = fgSubType; }
			}
		}
		
		public void SetTileSubType(uint gridId, byte subType = 0) {
			if(!this.tiles.ContainsKey(gridId)) { return; }
			this.tiles[gridId][1] = subType;
		}

		// For performance reasons, it is up to the user to avoid exceeding the grid's X,Y limits.
		public void RemoveTileByGrid(ushort gridX, ushort gridY) {
			uint gridId = this.GetGridID(gridX, gridY);
			this.RemoveTile(gridId);
		}

		public void RemoveTile(uint gridId) {
			this.tiles.Remove(gridId);
		}

		// Clear the Main Layer
		public void ClearMainLayer(uint gridId) {
			this.tiles[gridId][0] = 0;
			this.tiles[gridId][1] = 0;
		}

		public uint GetGridID(ushort gridX, ushort gridY) {
			return (uint)gridY * this.XCount + gridX;
		}

		// Grid Square Positions
		public static ushort GridX(int posX) { return (ushort)Math.Floor((double)(posX / (byte)TilemapEnum.TileWidth)); }
		public static ushort GridY(int posY) { return (ushort)Math.Floor((double)(posY / (byte)TilemapEnum.TileHeight)); }
	}
}
