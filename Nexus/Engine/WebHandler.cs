
using Newtonsoft.Json;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Engine {

	public static class WebHandler {

		// Track the last response message from a Web Handler request.
		public static string ResponseMessage = "";

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

		// ----------------- //
		// -- Web Helpers -- //
		// ----------------- //

		// PostJSON - Instructions:
		// var data = new { name = "Foo", val = "Bar" };
		// HttpResponseMessage response = await WebHandler.PostJSON(new Uri(GameValues.CreoAPI + "login"), data);
		// if(response == null || response.IsSuccessStatusCode == false) { return false; }
		// string contents = await response.Content.ReadAsStringAsync();
		// object json = JsonConvert.DeserializeObject(contents);
		public static async Task<HttpResponseMessage> PostJSON(Uri uri, object data) {
			try {
				string myContent = JsonConvert.SerializeObject(data);
				byte[] buffer = Encoding.UTF8.GetBytes(myContent);
				ByteArrayContent byteContent = new ByteArrayContent(buffer);
				byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
				return await Systems.httpClient.PostAsync(uri, byteContent);
			} catch (Exception ex) {
				return null;
			}
		}

		// cookie.Name, cookie.Value
		//public static IEnumerable<Cookie> GetAvailableCookies(string url) {
		//	return Systems.cookieContainer.GetCookies(new Uri(url)).Cast<Cookie>();
		//}

		private static IEnumerable<string> GetCookieStringsFromResponse(HttpResponseMessage response) {
			return response.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value;
		}

		// Dictionary<string, string> cookies = GetCookiesFromResponse(response);
		private static Dictionary<string, string> GetCookiesFromResponse(HttpResponseMessage response) {
			IEnumerable<string> cookieStrs = GetCookieStringsFromResponse(response);

			if(cookieStrs == null) return new Dictionary<string, string>();

			Dictionary<string, string> cookies = new Dictionary<string, string>();

			// Split the cookies:
			foreach(string cookieStr in cookieStrs) {
				string[] split = cookieStr.Split(';');
				string[] cData = split[0].Split('=');
				cookies.Add(cData[0], cData[1]);
			}

			return cookies;
		}

		// Set Cookies
		private static void AttachLoginCookiesToRequest(string url) {
			Uri serverUri = new Uri(url);

			// Prepare Cookies to Send
			Cookie userCookie = new Cookie("User", Systems.settings.login.User);
			Cookie tokenCookie = new Cookie("Token", Systems.settings.login.Token);

			// Set Cookies to Secure if we're sending over https.
			if(url.IndexOf("https://") > -1) {
				userCookie.Secure = true;
				tokenCookie.Secure = true;
			}

			Systems.cookieContainer.Add(serverUri, userCookie);
			Systems.cookieContainer.Add(serverUri, tokenCookie);
		}

		// -------------------- //
		// -- Login Commands -- //
		// -------------------- //

		public static bool IsLoggedIn() {
			if(Systems.settings.login.User.Length == 0) { return false; }
			if(Systems.settings.login.Token.Length == 0) { return false; }
			return true;
		}

		public class LoginFormat {
			[JsonProperty("success")]
			public bool success { get; set; }
			[JsonProperty("reason")]
			public string reason { get; set; }
		}

		public static async Task<bool> LoginRequest(string username, string password) {
			var data = new { username, password };

			// All checks have passed. Request the Level.
			try {

				// Run the Login Request
				Uri uri = new Uri(GameValues.CreoAPI + "login");
				HttpResponseMessage response = await WebHandler.PostJSON(uri, data);
				if(response == null || response.IsSuccessStatusCode == false) { return false; }
				
				// Extract the contents from the response.
				string contents = await response.Content.ReadAsStringAsync();
				LoginFormat json = JsonConvert.DeserializeObject<LoginFormat>(contents);

				if(json.success == false) {
					if(json.reason is string) {
						WebHandler.ResponseMessage = json.reason;
					}
					return false;
				}

				// Save Login Cookies
				Dictionary<string, string> cookies = GetCookiesFromResponse(response);
				WebHandler.SaveLoginCookies(cookies);

				// Verify the Cookies Were Sent
				return WebHandler.IsLoggedIn();

			} catch(Exception ex) {
				return false;
			}
		}

		private static void SaveLoginCookies(Dictionary<string, string> cookies) {
			if(!cookies.ContainsKey("User") || !cookies.ContainsKey("Token")) { return; }
			Systems.settings.login.User = cookies["User"];
			Systems.settings.login.Token = cookies["Token"];
			Systems.settings.login.SaveSettings();
		}

		// -------------------- //
		// -- Login Commands -- //
		// -------------------- //



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

				// Load Level Data
				LevelFormat levelData = JsonConvert.DeserializeObject<LevelFormat>(json);
				levelData.id = levelId;
				Systems.handler.levelContent.LoadLevelData(levelData);

				// Save Level
				Systems.handler.levelContent.SaveLevel();

				return true;

			} catch (Exception ex) {
				return false;
			}
		}

		public static async Task<string> LevelPublishRequestAsync(string levelId) {

			// Must be logged in:
			if(!WebHandler.IsLoggedIn()) { return "fail"; }

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

				// Attach Login Cookies
				WebHandler.AttachLoginCookiesToRequest(GameValues.CreoAPI);

				// Run the Level Post
				StringContent content = new StringContent(JsonConvert.SerializeObject(Systems.handler.levelContent.data), Encoding.UTF8, "application/json");
				var response = await Systems.httpClient.PostAsync(GameValues.CreoAPI + "level/" + levelNum, content);
				return await response.Content.ReadAsStringAsync();

			} catch(Exception ex) {
				return "fail";
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

				// Load World
				WorldFormat worldData = JsonConvert.DeserializeObject<WorldFormat>(json);
				worldData.id = worldId;
				Systems.handler.worldContent.LoadWorldData(worldData);

				// Save World
				Systems.handler.worldContent.SaveWorld();

				return true;

			} catch(Exception ex) {
				return false;
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

				// Attach Login Cookies
				WebHandler.AttachLoginCookiesToRequest(GameValues.CreoAPI);

				// Run the World Post
				StringContent content = new StringContent(JsonConvert.SerializeObject(Systems.handler.worldContent.data), Encoding.UTF8, "application/json");
				var response = await Systems.httpClient.PostAsync(GameValues.CreoAPI + "world/" + worldId, content);
				return await response.Content.ReadAsStringAsync();
			} catch(Exception ex) {
				return "fail";
			}
		}
	}
}
