using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

namespace Nexus.Gameplay {

	public class WorldFormat {

		[JsonProperty("id")]
		public string id { get; set; }

		[JsonProperty("name")]
		public string name { get; set; }
		
		[JsonProperty("description")]
		public string description { get; set; }
		
		[JsonProperty("username")]
		public string username { get; set; }
		
		[JsonProperty("version")]
		public ushort version { get; set; }
		
		[JsonProperty("zones")]
		public Dictionary<string, WorldZoneFormat> zones { get; set; }
	}

}
