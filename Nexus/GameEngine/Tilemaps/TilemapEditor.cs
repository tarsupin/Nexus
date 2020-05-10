using Nexus.Gameplay;
using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class TilemapEditor : TilemapBool {

		private LevelContent levelContent;
		private RoomFormat roomData;
		private string roomNum;

		// Width and Height of the Tilemap:
		public int Width { get; protected set; }
		public int Height { get; protected set; }
		public ushort XCount { get; protected set; }
		public ushort YCount { get; protected set; }

		public TilemapEditor(LevelContent levelContent, string roomNum, ushort xCount, ushort yCount) {

			// Sizing
			this.XCount = xCount;
			this.YCount = yCount;
			this.Width = xCount * (byte)TilemapEnum.TileWidth;
			this.Height = yCount * (byte)TilemapEnum.TileHeight;

			// Data
			this.levelContent = levelContent;
			this.roomNum = roomNum;
			this.roomData = this.levelContent.data.rooms[this.roomNum];
		}

		public byte[] GetTileDataAtGrid(ushort gridX, ushort gridY) {
			return new byte[] { 0 };
			//ushort gridX, ushort gridY = this.GetgridX, gridY(gridX, gridY);
			//this.tiles.TryGetValue(gridX, gridY, out byte[] val);
			//return val;
		}

		// TODO: FIX THESE
		public byte GetMainSubType(ushort gridX, ushort gridY) { return 1; }
		public byte GetBGSubType(ushort gridX, ushort gridY) { return 1; }
		public byte GetFGSubType(ushort gridX, ushort gridY) { return 1; }

		// For performance reasons, it is up to the user to avoid exceeding the grid's X,Y limits.
		public void AddTileAtGrid(ushort gridX, ushort gridY, byte id = 0, byte subType = 0, byte fgId = 0, byte fgSubType = 0) {
			this.SetTile(gridX, gridY, id, subType, fgId, fgSubType);
		}

		// For performance reasons, it is up to the user to avoid exceeding the grid's X,Y limits.
		public void SetTile(ushort gridX, ushort gridY, byte id = 0, byte subType = 0, byte bgId = 0, byte bgSubType = 0, byte fgId = 0, byte fgSubType = 0) {

			//if(!this.tiles.ContainsKey(gridX, gridY)) {
			//	this.tiles[gridX, gridY] = new byte[4] { id, subType, fgId, fgSubType };

			//} else {
			//	if(id > 0) { this.tiles[gridX, gridY][0] = id; this.tiles[gridX, gridY][1] = subType; }
			//	if(fgId > 0) { this.tiles[gridX, gridY][2] = fgId; this.tiles[gridX, gridY][3] = fgSubType; }
			//}
		}
		
		public void SetTileSubType(ushort gridX, ushort gridY, byte subType = 0) {
			//if(!this.tiles.ContainsKey(gridX, gridY)) { return; }
			//this.tiles[gridX, gridY][1] = subType;
		}

		public void RemoveTile(ushort gridX, ushort gridY) {
			//this.tiles.Remove(gridX, gridY);
		}

		public void ClearMainLayer(ushort gridX, ushort gridY) {
			//this.tiles[gridX, gridY][0] = 0;
			//this.tiles[gridX, gridY][1] = 0;
		}

		public void ClearBGLayer(ushort gridX, ushort gridY) {
			//this.tiles[gridX, gridY][0] = 0;
			//this.tiles[gridX, gridY][1] = 0;
		}

		public void ClearFGLayer(ushort gridX, ushort gridY) {
			//this.tiles[gridX, gridY][0] = 0;
			//this.tiles[gridX, gridY][1] = 0;
		}

		// Grid Square Positions
		public static ushort GridX(int posX) { return (ushort)Math.Floor((double)(posX / (byte)TilemapEnum.TileWidth)); }
		public static ushort GridY(int posY) { return (ushort)Math.Floor((double)(posY / (byte)TilemapEnum.TileHeight)); }
	}
}
