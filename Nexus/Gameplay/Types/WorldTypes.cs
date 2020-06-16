
namespace Nexus.Gameplay {

	public enum WorldmapEnum : short {

		// Grid Sizes
		TileWidth = 32,
		TileHeight = 32,

		// Tilemap Limits
		MinWidth = 46,
		MinHeight = 27,

		MaxWidth = 120,			// <= 200, avoid crossing byte threshold for range modifiers.
		MaxHeight = 120,		// <= 200, avoid crossing byte threshold for range modifiers.
	}

	public enum WorldTileStack : byte {
		Base = 0,		// Means that we're only dealing with the base tiles.
		Top = 1,		// Means that we're only dealing with the top tiles (top, topLayer).
		Cover = 2,		// Means that we're only dealing with the cover tiles (cover, coverLayer).
	}

	// Auto-Map Sequences
	// Identifies surrounding tiles relative to what was placed, and how that affects what layer should be used.
	// 0 = Surrounding Terrain, 1 = Placed Tile
	// For example: map_1000 means the top is the same as the tile placed, but everything else surrounding it is ____ (some other terrain type).
	// String Order is: Top, Left, Right, Bottom
	public enum AutoMapSequence : byte {
		map_1001,	// pv
		map_0110,	// ph
		
		map_1010,	// s1
		map_1110,	// s2
		map_1100,	// s3
		map_1011,	// s4
		map_1111,	// s5
		map_1101,	// s6
		map_0011,	// s7
		map_0111,	// s8
		map_0101,	// s9

		map_0000,	// c5
		map_1000,	// c2
		map_0010,	// c4
		map_0100,	// c6
		map_0001,	// c8
	}

	public enum OTerrain : byte {
		Grass = 1,
		Desert = 2,
		Snow = 3,

		Mud = 6,
		Dirt = 7,
		Cobble = 8,
		Road = 9,
		Ice = 10,
		DirtDark = 11,

		Water = 20,
		WaterShallow = 21,

		FieldGrass = 25,
		FieldHay = 26,
		FieldSnow = 27,

		MountainBrown = 30,
		MountainGray = 31,
		MountainIce = 32,

		TreesPine = 35,
		TreesPineSnow = 36,
		TreesOak = 37,
		TreesOakSnow = 38,
		TreesPalm = 39,
	}

	public enum OLayer : byte {

		// Base
		b1 = 1,
		
		// Ends
		e2 = 20,
		e4 = 21,
		e5 = 22,
		e6 = 23,
		e8 = 24,

		// Corners
		c1 = 25,
		c2 = 26,
		c3 = 27,
		c4 = 28,
		c5 = 29,
		c6 = 30,
		c7 = 31,
		c8 = 32,
		c9 = 33,
		cl = 34,
		cr = 35,
		
		l1 = 36,
		l3 = 37,
		l7 = 38,
		l9 = 39,

		// Paths
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
		
		// Standard
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

		// Wide (Water)
		w1 = 70,
		w3 = 71,
		w7 = 72,
		w9 = 73,
	};

	public enum OTerrainObjects : byte {

		// Ground Vegetation
		Brush = 1,
		Rose2 = 2,
		Rose3 = 3,
		Lily2 = 4,
		Lily3 = 5,
		Blur = 6,
		Stump = 7,
		Cactus = 8,
		CactusSmall = 9,
		CactusBig = 10,
		Tree1 = 11,
		Tree2 = 12,

		// Ground Objects
		DirtSpot = 15,
		Dune = 16,
		Drift = 17,
		Snowman1 = 18,
		Snowman2 = 19,
		Bones = 20,
		Pit = 21,
		Dungeon = 22,
		Tent = 23,

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

		// Dots
		Dot_All = 60,
		Dot_ULR = 61,
		Dot_ULD = 62,
		Dot_URD = 63,
		Dot_LRD = 64,
		Dot_UL = 65,
		Dot_UR = 66,
		Dot_UD = 67,
		Dot_LR = 68,
		Dot_LD = 69,
		Dot_RD = 70,

		// Nodes
		NodePoint = 71,
		NodeMove = 72,

		NodeStrict = 75,
		NodeCasual = 76,
		NodeWarp = 77,
		NodeWon = 78,

		// Stone Bridge
		StoneBridge2 = 100,
		StoneBridge4 = 101,
		StoneBridge6 = 102,
		StoneBridge8 = 103,
		StoneBridgeH = 104,
		StoneBridgeV = 105,

		// Wood Bridge
		WoodBridge2 = 111,
		WoodBridge4 = 112,
		WoodBridge6 = 113,
		WoodBridge8 = 114,
		WoodBridgeH = 115,
		WoodBridgeV = 116,

		// Characters (Reserve 200-230)
		StartRyu = 200,
		StartPoo = 201,
		StartCarl = 202,
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
}
