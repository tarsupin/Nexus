using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nexus.Gameplay {

	public class PlanetInfo {

		public static string[] Planets = new string[] {

			// Grassy
			"Planets/Grassland",	// ID 0
			"Planets/Jungle",		// ID 1
			"Planets/Swamp",		// ID 2

			// Water, Island
			"Planets/Ocean",		// ID 3
			"Planets/Water",		// ID 4
			"Planets/Blue",			// ID 5

			// Desert
			"Planets/Desert",		// ID 6
			"Planets/Brown",		// ID 7
			"Planets/Diag",			// ID 8
			"Planets/Orange",		// ID 9
			"Planets/Yellow",		// ID 10

			// Snow
			"Planets/Snow",			// ID 11
			"Planets/Ice",			// ID 12

			// Toxic
			"Planets/Toxic",		// ID 13
			"Planets/ToxicLight",	// ID 14
			"Planets/Gas",			// ID 15

			// Moons
			"Planets/MoonBrown",	// ID 16
			"Planets/MoonDark",		// ID 17
			"Planets/MoonGreen",	// ID 18
			"Planets/MoonOrange",	// ID 19
			"Planets/MoonRed",		// ID 20
		};
		
		public enum PlanetID : byte {

			// Grassy
			Grassland = 0,
			Jungle = 1,
			Swamp = 2,

			// Water
			Ocean = 3,
			Water = 4,
			Blue = 5,

			// Desert
			Desert = 6,
			Brown = 7,
			Diag= 8,
			Orange = 9,
			Yellow = 10,

			// Snow
			Snow = 11,
			Ice = 12,

			// Toxic
			Toxic = 13,
			ToxicLight = 14,
			Gas = 15,

			// Moons
			MoonBrown = 16,
			MoonDark = 17,
			MoonGreen = 18,
			MoonOrange = 19,
			MoonRed = 20,
		}
	}

	public class WorldListFormat {

		[JsonProperty("planets")]
		public List<PlanetFormat> planets { get; set; }
	}

	public class PlanetFormat {

		[JsonProperty("title")]
		public string title { get; set; }

		[JsonProperty("worldID")]
		public string worldID { get; set; }

		// PlanetInfo.PlanetID
		[JsonProperty("planetID")]
		public byte planetID { get; set; }
		
		// GameplayTypes.DifficultyMode
		[JsonProperty("difficulty")]
		public byte difficulty { get; set; }

		// GameplayTypes.HardcoreMode
		[JsonProperty("hardcore")]
		public byte hardcore { get; set; }

		// Character Icon: HeadSubType, SuitSubType, HatSubType, ...
		[JsonProperty("icon")]
		public byte[] icon { get; set; }

		// PlanetInfo.PlanetID
		[JsonProperty("moons")]
		public List<byte> moons { get; set; }
	}
}
