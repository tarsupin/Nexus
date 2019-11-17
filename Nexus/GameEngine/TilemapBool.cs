using Nexus.Gameplay;
using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class TilemapBool {

		RoomScene room;

		// Tile Data: Dictionaries of data that matches to the gridID (gridY*xCount + gridX)
		public Dictionary<uint, TileGameObject> ids;
		public Dictionary<uint, byte> subTypes;

		// Width and Height of the Tilemap:
		public int Width { get; protected set; }
		public int Height { get; protected set; }
		public ushort XCount { get; protected set; }
		public ushort YCount { get; protected set; }

		public TilemapBool( RoomScene room, ushort xCount, ushort yCount ) {
			this.room = room;

			// Sizing
			this.XCount = xCount;
			this.YCount = yCount;
			this.Width = xCount * (byte) TilemapEnum.TileWidth;
			this.Height = yCount * (byte) TilemapEnum.TileHeight;

			// Data
			this.ids = new Dictionary<uint, TileGameObject>();
			this.subTypes = new Dictionary<uint, byte>();
		}

		public TileGameObject GetTileAtGridID(uint gridId) {
			this.ids.TryGetValue(gridId, out TileGameObject val);
			return val;
		}
		
		public TileGameObject GetTileAtGrid(ushort gridX, ushort gridY) {
			uint gridId = this.GetGridID(gridX, gridY);
			this.ids.TryGetValue(gridId, out TileGameObject val);
			return val;
		}

		public byte GetSubTypeAtGridID(uint gridId) {
			this.subTypes.TryGetValue(gridId, out byte val);
			return val;
		}
		
		public byte GetSubTypeAtGrid(ushort gridX, ushort gridY) {
			uint gridId = this.GetGridID(gridX, gridY);
			this.subTypes.TryGetValue(gridId, out byte val);
			return val;
		}

		// For performance reasons, it is up to the user to avoid exceeding the grid's X,Y limits.
		public void AddTile( ushort gridX, ushort gridY, byte classId, byte subTypeId ) {
			uint gridId = this.GetGridID(gridX, gridY);
			this.ids[gridId] = this.room.tileObjects[classId];
			this.subTypes[gridId] = subTypeId;
		}

		// For performance reasons, it is up to the user to avoid exceeding the grid's X,Y limits.
		public void RemoveTileByGrid( ushort gridX, ushort gridY ) {
			uint gridId = this.GetGridID(gridX, gridY);
			this.RemoveTile(gridId);
		}

		public void RemoveTile( uint gridId ) {
			this.ids.Remove(gridId);
			this.subTypes.Remove(gridId);
		}

		public uint GetGridID( ushort gridX, ushort gridY ) {
			return (uint) gridY * this.XCount + gridX;
		}

		// Grid Square Positions
		public static ushort GridX(int posX) { return (ushort) Math.Floor((double)(posX / (byte)TilemapEnum.TileWidth)); }
		public static ushort GridY(int posY) { return (ushort) Math.Floor((double)(posY / (byte)TilemapEnum.TileHeight)); }
	}
}
