using Newtonsoft.Json;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Nexus.Scripts {

	public class LevelFormatOldV1 {

		[JsonProperty("id")]
		public string id { get; set; }

		[JsonProperty("account")]
		public string account { get; set; }     // The Free Account

		[JsonProperty("title")]
		public string title { get; set; }

		[JsonProperty("description")]
		public string description { get; set; }

		[JsonProperty("gameClass")]
		public byte gameClass { get; set; }     // GameClassFlag enum

		[JsonProperty("timeLimit")]
		public short timeLimit { get; set; }    // In Seconds

		[JsonProperty("music")]
		public byte music { get; set; }        // The Music Track ID. (MusicTrack in Maps/MusicAssets.cs)

		// Character Icon: HeadSubType, SuitSubType, HatSubType, ...
		[JsonProperty("icon")]
		public byte[] icon { get; set; }

		[JsonProperty("rooms")]
		public Dictionary<string, RoomFormat> oldRooms { get; set; }
	}

	class LevelConvertV2 : LevelConvert {

		public LevelConvertV2( string fromPath, string toPath ) : base( fromPath, toPath ) {
			System.Console.WriteLine("--------------------------------------");
			System.Console.WriteLine("----- LEVEL CONVERSION - Version 2.0");
			System.Console.WriteLine("--------------------------------------");
		}

		protected override void ProcessLevel(string levelId) {
			System.Console.WriteLine("Processing Level ID: " + levelId);

			// Load the Level Content
			// Update the Level ID, or use existing Level ID if applicable.
			string fullLevelPath = LevelContent.GetFullLevelPath(levelId);

			// Make sure the level exists:
			if(!File.Exists(fullLevelPath)) { return; }

			string json = File.ReadAllText(fullLevelPath);

			// If there is no JSON content, end the attempt to load level:
			if(json == "") { return; }

			// Load the Data
			LevelFormatOldV1 oldData = JsonConvert.DeserializeObject<LevelFormatOldV1>(json);

			// New Level Format
			LevelFormat newData = new LevelFormat();
			newData.account = oldData.account;
			newData.description = oldData.description;
			newData.gameClass = oldData.gameClass;
			newData.icon = oldData.icon;
			newData.id = oldData.id;
			newData.music = oldData.music;
			newData.rooms = new List<RoomFormat>();
			newData.timeLimit = oldData.timeLimit;
			newData.title = oldData.title;

			// Add the Rooms to New Format:
			foreach(KeyValuePair<string, RoomFormat> kvp in oldData.oldRooms) {
				RoomFormat room = kvp.Value;
				newData.rooms.Add(room);
			}

			// Assign the Level Content
			this.levelContent.data = newData;
			this.levelContent.levelId = levelId;
			this.levelContent.data.id = levelId;

			// Save the level content.
			this.levelContent.SaveLevel(this.deliveryPath, levelId);
		}
	}
}
