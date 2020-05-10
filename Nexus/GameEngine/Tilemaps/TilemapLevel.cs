using Nexus.Gameplay;
using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class TilemapLevel : TilemapBool {

		// Tile Array
		private byte[,][] tiles;      // ID, SubType, Background ID, Background SubType, Foreground ID, Foreground SubType

		// Width and Height of the Tilemap:
		public int Width { get; protected set; }
		public int Height { get; protected set; }
		public ushort XCount { get; protected set; }
		public ushort YCount { get; protected set; }

		public TilemapLevel(ushort xCount, ushort yCount) {

			// Sizing
			this.XCount = xCount;
			this.YCount = yCount;
			this.Width = xCount * (byte)TilemapEnum.TileWidth;
			this.Height = yCount * (byte)TilemapEnum.TileHeight;

			// Data
			this.tiles = new byte[this.YCount + 1, this.XCount + 1][];
		}

		public byte[] GetTileDataAtGrid(ushort gridX, ushort gridY) {
			return this.tiles[gridY, gridX];
		}

		public byte GetMainSubType(ushort gridX, ushort gridY) { return this.tiles[gridY, gridX][1]; }
		public byte GetBGSubType(ushort gridX, ushort gridY) { return this.tiles[gridY, gridX][3]; }
		public byte GetFGSubType(ushort gridX, ushort gridY) { return this.tiles[gridY, gridX][5]; }

		// For performance reasons, it is up to the user to avoid exceeding the grid's X,Y limits.
		public void SetTile(ushort gridX, ushort gridY, byte id = 0, byte subType = 0, byte bgId = 0, byte bgSubType = 0, byte fgId = 0, byte fgSubType = 0) {
			
			if(this.tiles[gridY, gridX] == null) {
				this.tiles[gridY, gridX] = new byte[] { id, subType, bgId, bgSubType, fgId, fgSubType };
			}
			
			if(id > 0) { this.tiles[gridY, gridX][0] = id; this.tiles[gridY, gridX][1] = subType; }
			if(bgId > 0) { this.tiles[gridY, gridX][2] = bgId; this.tiles[gridY, gridX][3] = bgSubType; }
			if(fgId > 0) { this.tiles[gridY, gridX][4] = fgId; this.tiles[gridY, gridX][5] = fgSubType; }
		}

		public void SetTileSubType(ushort gridX, ushort gridY, byte subType = 0) {
			this.tiles[gridY, gridX][1] = subType;
		}

		// For performance reasons, it is up to the user to avoid exceeding the grid's X,Y limits.
		public void RemoveTile(ushort gridX, ushort gridY) {
			var x = this.tiles[gridY, gridX];
			x[0] = 0;
			x[1] = 0;
			x[2] = 0;
			x[3] = 0;
			x[4] = 0;
			x[5] = 0;
		}

		// Clear the Main Layer
		public void ClearMainLayer(ushort gridX, ushort gridY) {
			var x = this.tiles[gridY, gridX];
			x[0] = 0;
			x[1] = 0;
		}

		public void ClearBGLayer(ushort gridX, ushort gridY) {
			var x = this.tiles[gridY, gridX];
			x[2] = 0;
			x[3] = 0;
		}

		public void ClearFGLayer(ushort gridX, ushort gridY) {
			var x = this.tiles[gridY, gridX];
			x[4] = 0;
			x[5] = 0;
		}

		// Grid Square Positions
		public static ushort GridX(int posX) { return (ushort)Math.Floor((double)(posX / (ushort)TilemapEnum.TileWidth)); }
		public static ushort GridY(int posY) { return (ushort)Math.Floor((double)(posY / (ushort)TilemapEnum.TileHeight)); }
	}
}
