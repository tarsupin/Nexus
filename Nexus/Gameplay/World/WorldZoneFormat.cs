using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nexus.Gameplay {

	public class WorldZoneFormat {

		[JsonProperty("tiles")]
		public Dictionary<string, WorldTileFormat[]> tiles { get; set; }

		//[JsonProperty("nodes")]
		//public Dictionary<string, WorldTileFormat[]> nodes { get; set; }
		// See "nodes" in {world}.json, where it has a string val, then dir, then a list of things.
	}
}
