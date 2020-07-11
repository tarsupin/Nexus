
namespace Nexus.GameEngine {

	public static class ServerCommands {

		// -------------------- //
		// -- Login Commands -- //
		// -------------------- //

		public static bool IsLoggedIn() {
			return false;
		}

		public static void LoginRequest(string username, string passHash) {

		}

		public static void LoginResponse(object response) {

		}

		private static void _SaveLoginData() {

		}


		// -------------------- //
		// -- Level Commands -- //
		// -------------------- //
		
		public static bool LevelExistsLocally(string levelId) {
			return false;
		}

		public static void LevelRequest(string levelId) {

		}

		public static void LevelResponse(object response) {

		}

		public static void LevelPublishRequest(string levelId) {

		}

		public static void LevelPublishResponse(object response) {

		}

		// -------------------- //
		// -- World Commands -- //
		// -------------------- //

		public static bool WorldExistsLocally(string worldId) {
			return false;
		}

		public static void WorldRequest(string worldId) {

		}

		public static void WorldResponse(object response) {

		}

		public static void WorldPublishRequest(string worldId) {

		}

		public static void WorldPublishResponse(object response) {

		}
	}
}
