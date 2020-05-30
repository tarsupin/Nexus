using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nexus.Gameplay {

	public class WorldZoneFormat {

		// byte[] { Base Terrain, Top Terrain, Top Layer, Cover, Cover Layer, Object, Node ID }
		//		Base Terrain: Lowest terrain layer, a 'b1' value. Assigned to OTerrain. Set to 0 if top overlaps it fully.
		//		Top Terrain: the layer above the base, when applicable. Combined with Top Layer, such as for paths (e7, s3, etc).
		//		Top Layer: The layer that applies to the top terrain.
		//		Cover: Terrain cover, like MountainGray, FieldHay, TreesPalm, etc. Combines with Layer.
		//		Cover Layer: Last layer aspect, such as s4, s6, etc.
		//		Object: An object that is layered above the others.
		//		Node ID: Links to a node.

		[JsonProperty("tiles")]
		public byte[][][] tiles { get; set; }

		//[JsonProperty("nodes")]
		//public WorldNodeFormat[][] nodes { get; set; }
	}
}
