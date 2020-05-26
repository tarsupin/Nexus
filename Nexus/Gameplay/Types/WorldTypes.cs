using System;
using System.Collections.Generic;

namespace Nexus.Gameplay {

	public enum WorldmapEnum : ushort {

		// Grid Sizes
		TileWidth = 32,
		TileHeight = 32,

		// Tilemap Limits
		MinWidth = 46,
		MinHeight = 27,
	}

	public enum OTerrain {
		Grass = 1,
		Desert = 2,
		Snow = 3,
		WaterDeep = 4,
		Water = 5,
		Mud = 6,
		Dirt = 7,
		Cobble = 8,
		Road = 9,
		Ice = 10,
		GrassDeep = 11,
	}

	public class RandTypeCur {
		public OTerrain type;
		public string c;

		public RandTypeCur(OTerrain type, string c) {
			this.type = type;
			this.c = c;
		}
	}

	// NOTE: Don't change these. They're saved in World Data.
	public enum NodeType : byte {
		LevelStrict = 1,
		LevelCasual = 2,
		TravelPoint = 5,
		TravelMove = 6,
		Warp = 10,
		WarpAuto = 11,
	}

	public interface ZoneTiles {
		Dictionary<byte, ZoneTilesY> gridY { get; set; }
	}

	public interface ZoneTilesY {
		Dictionary<byte, ZoneTilesX> gridX { get; set; }
	}

	public interface ZoneTilesX {
		OTerrain t { get; set; }
		string f { get; set; }
		OTerrain l { get; set; }
		string lf { get; set; }
		string o { get; set; }
		ushort n { get; set; }
	}
	
}
