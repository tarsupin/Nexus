using Nexus.Engine;
using Nexus.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nexus.GameEngine {

	public class TilemapLevel {

		// Tile Array
		private byte[,][] tiles;        // ID, SubType, Background ID, Background SubType, Foreground ID, Foreground SubType

		// Param Dictionary
		private readonly Dictionary<string, short> emptyParam;
		private Dictionary<int, Dictionary<string, short>> paramList;       // Key is the Coords.MapToInt(x, y)

		// Width and Height of the Tilemap:
		public int Width { get; protected set; }
		public int Height { get; protected set; }
		public short XCount { get; protected set; }
		public short YCount { get; protected set; }

		public TilemapLevel(short xCount, short yCount) {

			// Sizing
			this.XCount = xCount;
			this.YCount = yCount;
			this.Width = xCount * (byte)TilemapEnum.TileWidth;
			this.Height = yCount * (byte)TilemapEnum.TileHeight;

			short fullXCount = (short)(xCount + (byte)TilemapEnum.WorldGapLeft + (byte)TilemapEnum.WorldGapRight);
			short fullYCount = (short)(yCount + (byte)TilemapEnum.WorldGapUp + (byte)TilemapEnum.WorldGapDown);

			// Create Empty Tilemap Data
			this.tiles = new byte[fullYCount, fullXCount][];
			this.paramList = new Dictionary<int, Dictionary<string, short>>();
			this.emptyParam = new Dictionary<string, short>();
		}

		public byte[] GetTileDataAtGrid(short gridX, short gridY) {
			return this.tiles[gridY, gridX];
		}

		public byte GetMainSubType(short gridX, short gridY) { return this.tiles[gridY, gridX][1]; }
		public byte GetBGSubType(short gridX, short gridY) { return this.tiles[gridY, gridX][3]; }
		public byte GetFGSubType(short gridX, short gridY) { return this.tiles[gridY, gridX][5]; }

		// Setting Tile Data
		// For performance reasons, it is up to the user to avoid exceeding the grid's X,Y limits.
		public void SetMainTile(short gridX, short gridY, byte id = 0, byte subType = 0) {

			if(this.tiles[gridY, gridX] == null) {
				this.tiles[gridY, gridX] = new byte[] { id, subType, 0, 0, 0, 0 };
			} else {
				this.tiles[gridY, gridX][0] = id;
				this.tiles[gridY, gridX][1] = subType;
			}
		}

		public void SetBGTile(short gridX, short gridY, byte bgId = 0, byte bgSubType = 0) {

			if(this.tiles[gridY, gridX] == null) {
				this.tiles[gridY, gridX] = new byte[] { 0, 0, bgId, bgSubType, 0, 0 };
			} else {
				this.tiles[gridY, gridX][2] = bgId;
				this.tiles[gridY, gridX][3] = bgSubType;
			}
		}

		public void SetFGTile(short gridX, short gridY, byte fgId = 0, byte fgSubType = 0) {
			
			if(this.tiles[gridY, gridX] == null) {
				this.tiles[gridY, gridX] = new byte[] { 0, 0, 0, 0, fgId, fgSubType };
			} else {
				this.tiles[gridY, gridX][4] = fgId;
				this.tiles[gridY, gridX][5] = fgSubType;
			}
		}

		public void SetTileSubType(short gridX, short gridY, byte subType = 0) {
			this.tiles[gridY, gridX][1] = subType;
		}

		// Param Tracking
		public Dictionary<string, short> GetParamList(short gridX, short gridY) {
			int coords = Coords.MapToInt(gridX, gridY);
			return this.paramList.ContainsKey(coords) ? this.paramList[coords] : this.emptyParam;
		}

		public void SetParamList(short gridX, short gridY, Dictionary<string, short> paramList) {
			this.paramList[Coords.MapToInt(gridX, gridY)] = paramList;
		}

		public void SetParam(short gridX, short gridY, string paramKey, short paramVal) {
			int coordVal = Coords.MapToInt(gridX, gridY);
			if(!this.paramList.ContainsKey(coordVal)) {
				this.paramList.Add(coordVal, new Dictionary<string, short>());
			}
			this.paramList[coordVal][paramKey] = paramVal;
		}

		public void ClearParams(short gridX, short gridY) {
			this.paramList.Remove(Coords.MapToInt(gridX, gridY));
		}

		// Removing Tiles
		// For performance reasons, it is up to the user to avoid exceeding the grid's X,Y limits.
		public void RemoveTile(short gridX, short gridY) {
			this.tiles[gridY, gridX] = null;
		}

		private void MaybeRemoveTile(short gridX, short gridY) {
			var x = this.tiles[gridY, gridX];

			// If every index (each layer) is empty, remove the tile:
			if(x[0] == 0 && x[2] == 0 && x[4] == 0) {
				this.RemoveTile(gridX, gridY);
			}
		}

		// Area Of Effect Check: Search for specific Tile IDs (Main Layer) within an area.
		public List<(byte tileId, short gridX, short gridY)> GetTilesByMainIDsWithinArea(byte[] tileIds, short startX, short startY, short endX, short endY) {
			List<(byte tileId, short gridX, short gridY)> gridList = new List<(byte tileId, short gridX, short gridY)>();
			for(var y = startY; y <= endY; y++) {
				for(var x = startX; x <= endX; x++) {
					byte[] tileData = this.tiles[y, x];
					if(tileData == null) { continue; }
					if(Array.Exists(tileIds, element => element == tileData[0])) {
						gridList.Add((tileData[0], x, y));
					}
				}
			}
			return gridList;
		}

		// Specific SubType in Area Of Effect Test: Search for specific Tile and SubType IDs (Main Layer) within an area.
		public List<(short gridX, short gridY)> ScanMainTilesForSubType(byte tileId, byte subType, short startX, short startY, short endX, short endY) {
			List<(short gridX, short gridY)> gridList = new List<(short gridX, short gridY)>();
			for(var y = startY; y <= endY; y++) {
				for(var x = startX; x <= endX; x++) {
					byte[] tileData = this.tiles[y, x];
					if(tileData == null) { continue; }
					if(tileData[0] == tileId && tileData[1] == subType) {
						gridList.Add((x, y));
					}
				}
			}
			return gridList;
		}

		// Clear the Main Layer
		public void ClearMainLayer(short gridX, short gridY) {
			this.tiles[gridY, gridX][0] = 0;
			this.tiles[gridY, gridX][1] = 0;
			this.MaybeRemoveTile(gridX, gridY);
		}

		public void ClearBGLayer(short gridX, short gridY) {
			this.tiles[gridY, gridX][2] = 0;
			this.tiles[gridY, gridX][3] = 0;
			this.MaybeRemoveTile(gridX, gridY);
		}

		public void ClearFGLayer(short gridX, short gridY) {
			this.tiles[gridY, gridX][4] = 0;
			this.tiles[gridY, gridX][5] = 0;
			this.MaybeRemoveTile(gridX, gridY);
		}

		// Grid Square Positions
		public static short GridX(int posX) { return (short)Math.Floor((double)(posX / (short)TilemapEnum.TileWidth)); }
		public static short GridY(int posY) { return (short)Math.Floor((double)(posY / (short)TilemapEnum.TileHeight)); }
	}
}
