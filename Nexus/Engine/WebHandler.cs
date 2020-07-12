
using Newtonsoft.Json;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Engine {

	public static class WebHandler {

		public static string LaunchURL(string url) {

			// Attempt to Start the URL Process
			try {
				System.Diagnostics.Process.Start(url);
			}
			
			// If there is no browser that opens:
			catch (System.ComponentModel.Win32Exception exception) {
				if(exception.ErrorCode == -2147467259) {
					return exception.Message;
				}
			}

			// Other Issue
			catch(System.Exception other) {
				return other.Message;
			}

			return "";
		}

		// -------------------- //
		// -- Login Commands -- //
		// -------------------- //

		public static bool IsLoggedIn() {
			return false;
		}

		public static void LoginRequest(string username, string passHash) {
			using(WebClient wc = new WebClient()) {
				var json = wc.DownloadString(GameValues.CreoAPI + "login");
			}
		}

		public static void LoginResponse(object response) {

		}

		private static void _SaveLoginData() {

		}

		// -------------------- //
		// -- Level Commands -- //
		// -------------------- //

		public static async Task<bool> LevelRequest(string levelId) {

			// Make sure the level doesn't already exist locally. If it does, there's no need to call the online API.
			if(LevelContent.LevelExists(levelId)) {
				Systems.handler.levelContent.LoadLevelData(levelId);
				return true;
			}

			// All checks have passed. Request the Level.
			try {
				string json = await Systems.httpClient.GetStringAsync(GameValues.CreoAPI + "level/" + levelId);

				// If the retrieval fails:
				if(json.IndexOf("success:\"fail") > -1) {
					return false;
				}

				LevelFormat levelData = JsonConvert.DeserializeObject<LevelFormat>(json);
				Systems.handler.levelContent.LoadLevelData(levelData);
				return true;

			} catch (Exception ex) {
				throw ex;
			}
		}

		public static async Task<string> LevelPublishRequestAsync(string levelId) {

			// Make sure we have the level loaded:
			if(Systems.handler.levelContent == null || Systems.handler.levelContent.levelId != levelId) {
				return "fail";
			}

			// Make sure the level is owned, i.e. it has the "__#" format.
			if(levelId.IndexOf("__") != 0) { return "fail"; }

			string testId = levelId.Replace("__", "");

			bool success = byte.TryParse(testId, out byte levelNum);

			if(!success) { return "fail"; }
			if(levelNum > GameValues.MaxLevelsAllowedPerUser) { return "fail"; }

			// All checks have passed. Attempt to publish the level.
			try {
				StringContent content = new StringContent(JsonConvert.SerializeObject(Systems.handler.levelContent.data), Encoding.UTF8, "application/json");
				var response = await Systems.httpClient.PostAsync(GameValues.CreoAPI + "level/" + levelId, content);
				return await response.Content.ReadAsStringAsync();
			} catch(Exception ex) {
				throw ex;
			}
		}

		// -------------------- //
		// -- World Commands -- //
		// -------------------- //

		public static async Task<bool> WorldRequest(string worldId) {

			// Make sure the world doesn't already exist locally. If it does, there's no need to call the online API.
			if(WorldContent.WorldExists(worldId)) {
				Systems.handler.worldContent.LoadWorldData(worldId);
				return true;
			}

			try {
				string json = await Systems.httpClient.GetStringAsync(GameValues.CreoAPI + "world/" + worldId);

				// If the retrieval fails:
				if(json.IndexOf("success:\"fail") > -1) {
					return false;
				}

				WorldFormat worldData = JsonConvert.DeserializeObject<WorldFormat>(json);
				Systems.handler.worldContent.LoadWorldData(worldData);
				return true;

			} catch(Exception ex) {
				throw ex;
			}
		}

		public static async Task<string> WorldPublishRequest(string worldId) {

			// Make sure the world is owned, i.e. it has the "__#" format.
			if(worldId != "__World") { return "fail"; }

			// Make sure we have the world loaded:
			if(Systems.handler.worldContent == null || Systems.handler.worldContent.worldId != worldId) {
				return "fail";
			}

			// All checks have passed. Attempt to publish the world.
			try {
				StringContent content = new StringContent(JsonConvert.SerializeObject(Systems.handler.worldContent.data), Encoding.UTF8, "application/json");
				var response = await Systems.httpClient.PostAsync(GameValues.CreoAPI + "world/" + worldId, content);
				return await response.Content.ReadAsStringAsync();
			} catch(Exception ex) {
				throw ex;
			}
		}
	}
}
