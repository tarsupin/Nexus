using Newtonsoft.Json;

namespace Nexus.Gameplay {

	public class WorldTileFormat {

		[JsonProperty("t")]
		public byte tile { get; set; }

		[JsonProperty("f")]
		public string tileFrame { get; set; }

		[JsonProperty("l")]
		public byte layer { get; set; }

		[JsonProperty("lf")]
		public string layerFrame { get; set; }

		[JsonProperty("o")]
		public byte obj { get; set; }

		[JsonProperty("n")]
		public ushort nodeId { get; set; }
	}

}
