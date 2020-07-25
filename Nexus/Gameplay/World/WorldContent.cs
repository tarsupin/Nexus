using Newtonsoft.Json;
using Nexus.Engine;
using Nexus.ObjectComponents;
using System;
using System.Collections.Generic;
using System.IO;

namespace Nexus.Gameplay {

	public class WorldContent {

		// World Data
		public string worldId;          // World ID (e.g. "Astaria")
		public WorldFormat data;		// World Data

		public WorldContent() {

			// Make sure the Worlds directory exists.
			Systems.filesLocal.MakeDirectory("Worlds");
		}

		public static bool WorldExists(string worldId) {

			// Verify the presence of a World ID.
			if(worldId.Length == 0) { return false; }
			worldId = worldId.ToUpper();

			string localPath = WorldContent.GetLocalWorldPath(worldId);

			// Make sure the world exists:
			return Systems.filesLocal.FileExists(localPath);
		}

		public bool LoadWorldData(string worldId = "") {
			worldId = worldId.ToUpper();

			// Update the World ID, or use existing World ID if applicable.
			if(WorldContent.WorldExists(worldId)) { this.worldId = worldId; }
			else { return false; }

			string localPath = WorldContent.GetLocalWorldPath(this.worldId);
			string json = Systems.filesLocal.ReadFile(localPath);

			// If there is no JSON content, end the attempt to load world:
			if(json == "") { return false; }

			// Load the Data
			this.data = JsonConvert.DeserializeObject<WorldFormat>(json);

			return true;
		}

		public bool LoadWorldData(WorldFormat worldData) {
			this.data = worldData;
			this.worldId = worldData.id.ToUpper();
			return true;
		}

		public static string GetLocalWorldPath(string worldId) {
			worldId = worldId.ToUpper();
			return "Worlds/" + worldId.Substring(0, 2) + "/" + worldId + ".json";
		}

		public static WorldFormat BuildEmptyWorld(string worldId) {
			WorldZoneFormat zone = WorldContent.BuildEmptyZone();

			WorldFormat world = new WorldFormat {
				id = worldId.ToUpper(),
				account = "",
				mode = (byte) HardcoreMode.SoftCore,
				title = "Unnamed World",
				description = "",
				lives = 30,
				version = 0,
				music = 0,
				zones = new List<WorldZoneFormat>() { zone },
				//start = new StartNodeFormat {
				//	character = 0,
				//	zoneId = 0,
				//	x = 0,
				//	y = 0,
				//},
			};

			return world;
		}

		public static WorldZoneFormat BuildEmptyZone() {

			WorldZoneFormat zone = new WorldZoneFormat() {
				tiles = new byte[1][][],
				nodes = new Dictionary<string, string>(),
			};

			zone.tiles[0] = new byte[1][];
			zone.tiles[0][0] = new byte[6] { 20, 0, 0, 0, 0, 0 };

			return zone;
		}

		// Assign World Data
		public void SetAccount(string account) { this.data.account = account; }
		public void SetMode(byte mode) { this.data.mode = mode; }
		public void SetName(string name) { this.data.title = name; }
		public void SetDescription(string desc) { this.data.description = desc; }
		public void SetLives(short lives) { this.data.lives = lives; }
		public void SetMusicTrack(byte track) { this.data.music = (byte)track; } // MusicTrack enum

		public void SaveWorld() {

			// Can only save a world state if the world ID is assigned correctly.
			if(this.worldId == null || this.worldId.Length == 0) { return; }
			this.worldId = this.worldId.ToUpper();

			// Determine the Destination Path and Destination Level ID
			string localFile = WorldContent.GetLocalWorldPath(this.worldId);

			// Make sure the directory exists:
			string localDir = Path.GetDirectoryName(localFile);
			Systems.filesLocal.MakeDirectory(localDir, true);

			// Save State
			string json = JsonConvert.SerializeObject(this.data);
			Systems.filesLocal.WriteFile(localFile, json);
		}

		public WorldZoneFormat GetWorldZone(byte zoneId) {
			if(this.data.zones.Count <= zoneId) { return null; }
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

			if(newWidth < (byte) WorldmapEnum.MinWidth) { newWidth = (byte) WorldmapEnum.MinWidth; }
			else if(newWidth > (byte) WorldmapEnum.MaxWidth) { newWidth = (byte) WorldmapEnum.MaxWidth; }

			// Loop through Y Data
			for(byte y = 0; y < height; y++) {

				var newYData = zone.tiles[y];
				Array.Resize<byte[]>(ref newYData, newWidth);
				zone.tiles[y] = newYData;

				// If New Width is lower:
				if(newWidth < width) { continue; }

				// Loop through X Data
				for(byte x = width; x < newWidth; x++) {
					zone.tiles[y][x] = new byte[] { 20, 0, 0, 0, 0, 0, 0 };
				}
			}

			return newWidth;
		}

		public byte SetZoneHeight(WorldZoneFormat zone, byte newHeight) {
			byte width = this.GetWidthOfZone(zone);
			byte height = this.GetHeightOfZone(zone);

			if(newHeight < (byte)WorldmapEnum.MinHeight) { newHeight = (byte)WorldmapEnum.MinHeight; }
			else if(newHeight > (byte)WorldmapEnum.MaxHeight) { newHeight = (byte)WorldmapEnum.MaxHeight; }

			byte[][][] tiles = zone.tiles;
			Array.Resize<byte[][]>(ref tiles, newHeight);
			zone.tiles = tiles;

			// If New Height is lower:
			if(newHeight < height) {
				return newHeight;
			}

			// Loop through Y Data
			for(byte y = height; y < newHeight; y++) {
				zone.tiles[y] = new byte[width][];
				
				// Loop through X Data
				for(byte x = 0; x < width; x++) {
					zone.tiles[y][x] = new byte[] { 20, 0, 0, 0, 0, 0, 0 };
				}
			}

			return newHeight;
		}

		// byte[]:  { Base, Top, Top Layer, Cover, Cover Layer, Object }
		// example: { Dirt, Grass, s4, MountainBrown, s2, NodeCasual }
		public byte[] GetWorldTileData(WorldZoneFormat zone, byte gridX, byte gridY) {
			var tiles = zone.tiles;
			if(gridY >= tiles.Length) { return new byte[] { 0, 0, 0, 0, 0, 0, 0 }; }
			if(gridX >= tiles[gridY].Length) { return new byte[] { 0, 0, 0, 0, 0, 0, 0 }; }
			return tiles[gridY][gridX];
		}

		public bool SetStartData(byte zoneId, byte gridX, byte gridY, byte character = (byte) HeadSubType.RyuHead) {
			this.data.start = new StartNodeFormat() {
				x = gridX,
				y = gridY,
				zoneId = zoneId,
				character = character,
			};
			return true;
		}

		public bool SetTile(WorldZoneFormat zone, byte gridX, byte gridY, byte tBase = 0, byte top = 0, byte topLay = 0, byte cover = 0, byte coverLay = 0, byte obj = 0, byte nodeId = 0) {
			var tiles = zone.tiles;
			if(gridY >= tiles.Length) { return false; }
			if(gridX >= tiles[gridY].Length) { return false; }
			tiles[gridY][gridX] = new byte[] { tBase, top, topLay, cover, coverLay, obj, nodeId };
			return true;
		}

		public bool SetTileBase(WorldZoneFormat zone, byte gridX, byte gridY, byte tBase = 0) {
			var tiles = zone.tiles;
			if(gridY >= tiles.Length) { return false; }
			if(gridX >= tiles[gridY].Length) { return false; }
			tiles[gridY][gridX][0] = tBase;
			return true;
		}
		
		public bool SetTileTop(WorldZoneFormat zone, byte gridX, byte gridY, byte top = 0, byte topLay = 0) {
			var tiles = zone.tiles;
			if(gridY >= tiles.Length) { return false; }
			if(gridX >= tiles[gridY].Length) { return false; }
			tiles[gridY][gridX][1] = top;
			tiles[gridY][gridX][2] = topLay;
			return true;
		}

		public bool SetTileCover(WorldZoneFormat zone, byte gridX, byte gridY, byte cover = 0, byte coverLay = 0) {
			var tiles = zone.tiles;
			if(gridY >= tiles.Length) { return false; }
			if(gridX >= tiles[gridY].Length) { return false; }
			tiles[gridY][gridX][3] = cover;
			tiles[gridY][gridX][4] = coverLay;
			return true;
		}

		public bool SetTileObject(WorldZoneFormat zone, byte gridX, byte gridY, byte obj = 0 ) {
			var tiles = zone.tiles;
			if(gridY >= tiles.Length) { return false; }
			if(gridX >= tiles[gridY].Length) { return false; }
			tiles[gridY][gridX][5] = obj;
			return true;
		}

		public bool DeleteTileTop(WorldZoneFormat zone, byte gridX, byte gridY) {
			return this.SetTileTop(zone, gridX, gridY);
		}

		public bool DeleteTileCover(WorldZoneFormat zone, byte gridX, byte gridY) {
			return this.SetTileCover(zone, gridX, gridY);
		}
		
		public bool DeleteTileObject(WorldZoneFormat zone, byte gridX, byte gridY) {
			return this.SetTileObject(zone, gridX, gridY);
		}

		public bool DeleteTile(WorldZoneFormat zone, byte gridX, byte gridY) {
			var tiles = zone.tiles;
			if(gridY > tiles.Length) { return false; }
			if(gridX > tiles[gridY].Length) { return false; }
			tiles[gridY][gridX] = new byte[] { 0, 0, 0, 0, 0, 0, 0 };
			return true;
		}
	}
}
