using Newtonsoft.Json;
using Nexus.Engine;
using System;

namespace Nexus.Gameplay {

	public class WorldContent {

		// World Data
		public string worldId;          // World ID (e.g. "Astaria")
		public WorldFormat data;		// World Data

		public WorldContent() {

			// Make sure the Worlds directory exists.
			Systems.filesLocal.MakeDirectory("Worlds");
		}

		public bool LoadWorldData(string worldId = "") {

			// Update the World ID, or use existing World ID if applicable.
			if(worldId.Length > 0) { this.worldId = worldId; } else { return false; }

			string localPath = WorldContent.GetLocalWorldPath(this.worldId);

			// Make sure the world exists:
			if(!Systems.filesLocal.FileExists(localPath)) { return false; }

			string json = Systems.filesLocal.ReadFile(localPath);

			// If there is no JSON content, end the attempt to load world:
			if(json == "") { return false; }

			// Load the Data
			this.data = JsonConvert.DeserializeObject<WorldFormat>(json);

			return true;
		}

		public static string GetLocalWorldPath(string worldId) {
			return "Worlds/" + worldId.Substring(0, 2) + "/" + worldId + ".json";
		}

		private WorldFormat BuildWorldStruct() {
			WorldFormat worldStructure = new WorldFormat();
			return worldStructure;
		}

		public void SaveWorld() {

			// Can only save a world state if the world ID is assigned correctly.
			if(this.worldId.Length == 0) { return; }

			// TODO LOW PRIORITY: Verify that the worldId exists; not just that an ID is present.

			// Save State
			string json = JsonConvert.SerializeObject(this.data);
			Systems.filesLocal.WriteFile(WorldContent.GetLocalWorldPath(this.worldId), json);
		}

		public WorldZoneFormat GetWorldZone(byte zoneId) {
			if(this.data.zones.Length <= zoneId) { return null; }
			return this.data.zones[zoneId];
		}

		public byte GetHeightOfZone(WorldZoneFormat zone) {
			return (zone.tiles.Length > 0) ? (byte) zone.tiles.Length : (byte) 0;
		}

		public byte GetWidthOfZone(WorldZoneFormat zone) {
			return (zone.tiles.Length > 0) ? (byte) zone.tiles[0].Length : (byte) 0;
		}

		public byte SetZoneWidth(WorldZoneFormat zone, byte newWidth) {
			byte width = this.GetWidthOfZone(zone);
			byte height = this.GetHeightOfZone(zone);

			// Loop through Y Data
			for(byte y = 0; y < height; y++) {
				var yData = zone.tiles[y];

				// If New Width is lower:
				if(newWidth < width) {
					var newYData = zone.tiles[y];
					Array.Resize<byte[]>(ref newYData, newWidth);
					zone.tiles[y] = newYData;
					continue;
				}
				
				// Loop through X Data
				for(byte x = width; x < newWidth; x++) {
					yData[x] = new byte[] { 0, 0, 0, 0, 0, 0 };
				}
			}

			return newWidth;
		}

		public byte SetZoneHeight(WorldZoneFormat zone, byte newHeight) {
			byte width = this.GetWidthOfZone(zone);
			byte height = this.GetHeightOfZone(zone);
			
			// If New Height is lower:
			if(newHeight < height) {
				byte[][][] tiles = zone.tiles;
				Array.Resize<byte[][]>(ref tiles, newHeight);
				zone.tiles = tiles;
				return newHeight;
			}

			// Loop through Y Data
			for(byte y = height; y < newHeight; y++) {
				zone.tiles[y] = new byte[width][];
				
				// Loop through X Data
				for(byte x = 0; x < width; x++) {
					zone.tiles[y][x] = new byte[] { 0, 0, 0, 0, 0, 0 };
				}
			}

			return newHeight;
		}

		public byte[] GetWorldTileData(WorldZoneFormat zone, byte gridX, byte gridY) {
			var tiles = zone.tiles;
			if(gridY > tiles.Length) { return new byte[] { 0, 0, 0, 0, 0, 0 }; }
			if(gridX > tiles[gridY].Length) { return new byte[] { 0, 0, 0, 0, 0, 0 }; }
			return tiles[gridY][gridX];
		}

		public bool SetTileObject(WorldZoneFormat zone, byte gridX, byte gridY, byte obj = 0 ) {
			var tiles = zone.tiles;
			if(gridY > tiles.Length) { return false; }
			if(gridX > tiles[gridY].Length) { return false; }
			tiles[gridY][gridX][4] = obj;
			return true;
		}

		public bool SetTile(WorldZoneFormat zone, byte gridX, byte gridY, byte tBase = 0, byte tTop = 0, byte tCat = 0, byte tLay = 0, byte obj = 0, byte nodeId = 0 ) {
			var tiles = zone.tiles;
			if(gridY > tiles.Length) { return false; }
			if(gridX > tiles[gridY].Length) { return false; }
			tiles[gridY][gridX] = new byte[] { tBase, tTop, tCat, tLay, obj, nodeId };
			return true;
		}

		public bool DeleteTile(WorldZoneFormat zone, ushort gridX, ushort gridY) {
			var tiles = zone.tiles;
			if(gridY > tiles.Length) { return false; }
			if(gridX > tiles[gridY].Length) { return false; }
			tiles[gridY][gridX] = new byte[] { 0, 0, 0, 0, 0, 0 };
			return true;
		}
	}
}
