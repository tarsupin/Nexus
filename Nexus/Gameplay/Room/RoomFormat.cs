using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

namespace Nexus.Gameplay {

	public class RoomFormat {

		[JsonProperty("music")]
		public short music { get; set; }        // The Music Track ID. (MusicTrack in Maps/MusicAssets.cs)

		[JsonProperty("bg")]
		public Dictionary<string, Dictionary<string, ArrayList>> bg { get; set; }
		
		[JsonProperty("main")]
		public Dictionary<string, Dictionary<string, ArrayList>> main { get; set; }

		[JsonProperty("obj")]
		public Dictionary<string, Dictionary<string, ArrayList>> obj { get; set; }

		[JsonProperty("fg")]
		public Dictionary<string, Dictionary<string, ArrayList>> fg { get; set; }
	}
}
