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

		[JsonProperty("main")]
		public Dictionary<string, Dictionary<string, ArrayList>> main { get; set; }

		[JsonProperty("obj")]
		public Dictionary<string, Dictionary<string, ArrayList>> obj { get; set; }

		[JsonProperty("fg")]
		public Dictionary<string, Dictionary<string, ArrayList>> fg { get; set; }

		[JsonProperty("width")]
		public ushort width { get; set; }

		[JsonProperty("height")]
		public ushort height { get; set; }
	}

}
