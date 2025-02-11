﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nexus.Gameplay {

	public class LevelFormat {

		[JsonProperty("id")]
		public string id { get; set; }

		[JsonProperty("account")]
		public string account { get; set; }     // The Free Account

		[JsonProperty("title")]
		public string title { get; set; }
		
		[JsonProperty("description")]
		public string description { get; set; }

		[JsonProperty("gameClass")]
		public byte gameClass { get; set; }		// GameClassFlag enum
		
		[JsonProperty("timeLimit")]
		public short timeLimit { get; set; }	// In Seconds
		
		[JsonProperty("music")]
		public byte music { get; set; }        // The Music Track ID. (MusicTrack in Maps/MusicAssets.cs)

		// Character Icon: HeadSubType, SuitSubType, HatSubType, ...
		[JsonProperty("icon")]
		public byte[] icon { get; set; }

		[JsonProperty("rooms")]
		public List<RoomFormat> rooms { get; set; }
	}

	public class LevelListFormat {

		[JsonProperty("levels")]
		public List<short> levels { get; set; }
	}
}
