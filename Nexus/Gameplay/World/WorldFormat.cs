using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

namespace Nexus.Gameplay {

	public class WorldFormat {

		[JsonProperty("id")]
		public string id { get; set; }

		[JsonProperty("rooms")]
		public Dictionary<string, RoomFormat> room { get; set; }
	}

}
