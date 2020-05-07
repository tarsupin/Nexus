using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nexus.Gameplay {

	public class LevelFormat {

		[JsonProperty("id")]
		public string id { get; set; }

		[JsonProperty("rooms")]
		public Dictionary<string, RoomFormat> rooms { get; set; }
	}
}
