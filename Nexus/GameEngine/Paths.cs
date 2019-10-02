
namespace Nexus.GameEngine {

	public static class Paths {

		// TODO HIGH PRIORITY: Must change this path to an allowed path, like FileLocal.
		// TODO HIGH PRIORITY: Must change this path to an allowed path, like FileLocal.
		private static string levels = "D:/Web/nodeTesla/ryu-assets/levels/";

		public static string GetLevelPath( string levelId ) {
			string folder = levelId.Substring(0, 2);
			return Paths.levels + folder + "/" + levelId + ".json";
		}
	}
}
