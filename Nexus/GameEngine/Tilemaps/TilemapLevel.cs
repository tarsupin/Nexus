using Nexus.Gameplay;
using System;

namespace Nexus.GameEngine {

	public class TilemapLevel {

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

			ushort fullXCount = (ushort)(xCount + (byte)TilemapEnum.WorldGapLeft + (byte)TilemapEnum.WorldGapRight);
			ushort fullYCount = (ushort)(yCount + (byte)TilemapEnum.WorldGapUp + (byte)TilemapEnum.WorldGapDown);

			// Create Empty Tilemap Data
			this.tiles = new byte[fullYCount, fullXCount][];
		}

		public byte[] GetTileDataAtGrid(ushort gridX, ushort gridY) {
			return this.tiles[gridY, gridX];
		}

		public byte GetMainSubType(ushort gridX, ushort gridY) { return this.tiles[gridY, gridX][1]; }
		public byte GetBGSubType(ushort gridX, ushort gridY) { return this.tiles[gridY, gridX][3]; }
		public byte GetFGSubType(ushort gridX, ushort gridY) { return this.tiles[gridY, gridX][5]; }

		// For performance reasons, it is up to the user to avoid exceeding the grid's X,Y limits.
		public void SetMainTile(ushort gridX, ushort gridY, byte id = 0, byte subType = 0) {

			if(this.tiles[gridY, gridX] == null) {
				this.tiles[gridY, gridX] = new byte[] { id, subType, 0, 0, 0, 0 };
			} else {
				this.tiles[gridY, gridX][0] = id;
				this.tiles[gridY, gridX][1] = subType;
			}
		}

		public void SetBGTile(ushort gridX, ushort gridY, byte bgId = 0, byte bgSubType = 0) {

			if(this.tiles[gridY, gridX] == null) {
				this.tiles[gridY, gridX] = new byte[] { 0, 0, bgId, bgSubType, 0, 0 };
			} else {
				this.tiles[gridY, gridX][2] = bgId;
				this.tiles[gridY, gridX][3] = bgSubType;
			}
		}

		public void SetFGTile(ushort gridX, ushort gridY, byte fgId = 0, byte fgSubType = 0) {
			
			if(this.tiles[gridY, gridX] == null) {
				this.tiles[gridY, gridX] = new byte[] { 0, 0, 0, 0, fgId, fgSubType };
			} else {
				this.tiles[gridY, gridX][4] = fgId;
				this.tiles[gridY, gridX][5] = fgSubType;
			}
		}

		public void SetTileSubType(ushort gridX, ushort gridY, byte subType = 0) {
			this.tiles[gridY, gridX][1] = subType;
		}

		// For performance reasons, it is up to the user to avoid exceeding the grid's X,Y limits.
		public void RemoveTile(ushort gridX, ushort gridY) {
			this.tiles[gridY, gridX] = null;
		}

		private void MaybeRemoveTile(ushort gridX, ushort gridY) {
			var x = this.tiles[gridY, gridX];

			// If every index (each layer) is empty, remove the tile:
			if(x[0] == 0 && x[2] == 0 && x[4] == 0) {
				this.RemoveTile(gridX, gridY);
			}
		}

		// Clear the Main Layer
		public void ClearMainLayer(ushort gridX, ushort gridY) {
			this.tiles[gridY, gridX][0] = 0;
			this.tiles[gridY, gridX][1] = 0;
			this.MaybeRemoveTile(gridX, gridY);
		}

		public void ClearBGLayer(ushort gridX, ushort gridY) {
			this.tiles[gridY, gridX][2] = 0;
			this.tiles[gridY, gridX][3] = 0;
			this.MaybeRemoveTile(gridX, gridY);
		}

		public void ClearFGLayer(ushort gridX, ushort gridY) {
			this.tiles[gridY, gridX][4] = 0;
			this.tiles[gridY, gridX][5] = 0;
			this.MaybeRemoveTile(gridX, gridY);
		}

		// Grid Square Positions
		public static ushort GridX(int posX) { return (ushort)Math.Floor((double)(posX / (ushort)TilemapEnum.TileWidth)); }
		public static ushort GridY(int posY) { return (ushort)Math.Floor((double)(posY / (ushort)TilemapEnum.TileHeight)); }
	}
}
