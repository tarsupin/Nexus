using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

namespace Nexus.Gameplay {

	public class LevelFormat {

		[JsonProperty("id")]
		public string id { get; set; }

		[JsonProperty("rooms")]
		public Dictionary<string, RoomFormat> room { get; set; }
	}

	public class RoomFormat {

		[JsonProperty("bgLayer")]
		public Dictionary<string, Dictionary<string, ArrayList>> bgLayer { get; set; }

		[JsonProperty("mainLayer")]
		public Dictionary<string, Dictionary<string, ArrayList>> mainLayer { get; set; }

		[JsonProperty("cosmeticLayer")]
		public Dictionary<string, Dictionary<string, ArrayList>> cosmeticLayer { get; set; }
	}

}
