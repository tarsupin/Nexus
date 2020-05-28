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

	public enum OTerrain : byte {
		Grass = 1,
		Desert = 2,
		Snow = 3,
		WaterShallow = 4,
		Water = 5,
		Mud = 6,
		Dirt = 7,
		Cobble = 8,
		Road = 9,
		Ice = 10,
		DirtDark = 11,
	}

	public enum OTerrainCat : byte {
		Trees = 1,
		Mountains = 2,
		Field = 3,
		Veg = 4,
		Water = 5,
		Field2 = 6,
	}

	public enum OLayer : byte {

		// Base Variations
		b1 = 1,
		b2 = 2,
		b3 = 3,
		b4 = 4,
		b5 = 5,
		b6 = 6,
		b7 = 7,
		b8 = 8,
		b9 = 9,
		b10 = 10,
			
		// Paths
		c2 = 20,
		c4 = 21,
		c5 = 22,
		c6 = 23,
		c8 = 24,
			
		e1 = 25,
		e2 = 26,
		e3 = 27,
		e4 = 28,
		e5 = 29,
		e6 = 30,
		e7 = 31,
		e8 = 32,
		e9 = 33,
		el = 34,
		er = 35,
			
		l1 = 36,
		l3 = 37,
		l7 = 38,
		l9 = 39,
			
		p1 = 40,
		p3 = 41,
		p7 = 42,
		p9 = 43,
		ph = 44,
		pv = 45,
			
		r1 = 46,
		r3 = 47,
		r7 = 48,
		r9 = 49,
			
		s = 50,
		s1 = 51,
		s2 = 52,
		s3 = 53,
		s4 = 54,
		s5 = 55,
		s6 = 56,
		s7 = 57,
		s8 = 58,
		s9 = 59,
			
		t2 = 60,
		t4 = 61,
		t6 = 63,
		t8 = 64,
			
		v1 = 65,
		v3 = 66,
		v7 = 67,
		v9 = 68,
	};

	public enum OTerrainObjects : byte {

		// Ground Objects
		Bones = 1,
		Cactus = 2,
		Stump = 3,
		Snowman1 = 4,
		Snowman2 = 5,

		Tree1 = 6,
		Tree2 = 7,
		Pit = 8,
		Dungeon = 9,

		// Nodes
		NodeStrict = 20,
		NodeCasual = 21,
		NodePoint = 22,
		NodeMove = 23,
		NodeWarp = 24,
		NodeWon = 25,
		NodeStart = 26,

		// Buildings = , Residence
		House1 = 30,
		House2 = 31,
		House3 = 32,
		House4 = 33,
		House5 = 34,
		House6 = 35,
		House7 = 36,
		House8 = 37,
		House9 = 38,
		House10 = 39,

		// Buildings = , Defense
		Castle1 = 40,
		Castle2 = 41,
		Castle3 = 42,
		Castle4 = 43,
		Castle5 = 44,

		Tower1 = 45,
		Tower2 = 46,
		Tower3 = 47,
		Tower4 = 48,

		// Large Buildings
		Town1 = 50,
		Town2 = 51,
		Town3 = 52,

		Stadium = 53,

		Pyramid1 = 55,
		Pyramid2 = 56,
		Pyramid3 = 57,

		// Buildings, Misc
		Tent = 60,

		// Stone Bridge
		StoneBridge2 = 70,
		StoneBridge4 = 71,
		StoneBridge6 = 72,
		StoneBridge8 = 73,
		StoneBridgeH = 74,
		StoneBridgeV = 75,

		// Wood Bridge
		WoodBridge2 = 81,
		WoodBridge4 = 82,
		WoodBridge6 = 83,
		WoodBridge8 = 84,
		WoodBridgeH = 85,
		WoodBridgeV = 86,
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
