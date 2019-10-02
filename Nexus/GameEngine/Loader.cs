using Newtonsoft.Json;
using Nexus.Gameplay;
using System.IO;

namespace Nexus.GameEngine {

	public class Loader {

		public static string LoadJsonString( string filePath ) {
			using(StreamReader r = new StreamReader(filePath)) {
				return r.ReadToEnd();
			}
		}

		public static LevelFormat LoadLevelData(string levelPath) {
			using(StreamReader r = new StreamReader(levelPath)) {
				string json = r.ReadToEnd();
				return JsonConvert.DeserializeObject<LevelFormat>(json);
			}
		}
	}
}
