using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nexus.Gameplay {

	public class WorldFormat {

		[JsonProperty("id")]
		public string id { get; set; }

		[JsonProperty("account")]
		public string account { get; set; }

		[JsonProperty("mode")]
		public byte mode { get; set; }

		[JsonProperty("name")]
		public string name { get; set; }
		
		[JsonProperty("description")]
		public string description { get; set; }

		[JsonProperty("lives")]
		public short lives { get; set; }
		
		[JsonProperty("version")]
		public short version { get; set; }
		
		[JsonProperty("music")]
		public byte music { get; set; }
		
		[JsonProperty("zones")]
		public List<WorldZoneFormat> zones { get; set; }

		[JsonProperty("start")]
		public StartNodeFormat start { get; set; }
	}

	public class WorldZoneFormat {

		// byte[] { Base Terrain, Top Terrain, Top Layer, Cover, Cover Layer, Object }
		//		Base Terrain: Lowest terrain layer, a 'b1' value. Assigned to OTerrain. Set to 0 if top overlaps it fully.
		//		Top Terrain: the layer above the base, when applicable. Combined with Top Layer, such as for paths (e7, s3, etc).
		//		Top Layer: The layer that applies to the top terrain.
		//		Cover: Terrain cover, like MountainGray, FieldHay, TreesPalm, etc. Combines with Layer.
		//		Cover Layer: Last layer aspect, such as s4, s6, etc.
		//		Object: An object that is layered above the others.

		[JsonProperty("tiles")]
		public byte[][][] tiles { get; set; }

		// Stores a dictionary of CoordinateIDs (as strings) mapped to Level IDs.
		// e.g. "nodes":{"1664":"TUTORIAL_1","1676":"TUTORIAL_2","1977":"TUTORIAL_3","2162":"TUTORIAL_4","2610":"TUTORIAL_5"}
		// Warps get set as "_warp#" with the # representing their link ID (e.g. "_warp1", "_warp7");
		[JsonProperty("nodes")]
		public Dictionary<string, string> nodes { get; set; }
	}

	public class StartNodeFormat {

		[JsonProperty("character")]
		public byte character { get; set; } // HeadSubType

		[JsonProperty("zoneId")]
		public byte zoneId { get; set; }

		[JsonProperty("x")]
		public byte x { get; set; }

		[JsonProperty("y")]
		public byte y { get; set; }
	}
}
