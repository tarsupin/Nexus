using Nexus.Engine;
using Nexus.Gameplay;
using System;
using System.Collections.Generic;
using static Nexus.Gameplay.MusicAssets;

namespace Nexus.GameEngine {

	public static class ConsoleEditData {

		public static void SetTitle() {
			string text = Sanitize.Title(ConsoleTrack.instructionText.Substring(5).TrimStart());
			
			if(text.Length > 0) {
				ConsoleTrack.instructionText = "title " + text.Substring(0, Math.Min(text.Length, 24));
			}
			
			short remain = (short)(24 - text.Length);

			ConsoleTrack.possibleTabs = "Example: `title \"My Cool Level\"`";
			ConsoleTrack.helpText = "Provide a level title. " + remain.ToString() + " characters remaining.";

			// Activate the Instruction
			if(ConsoleTrack.activate) {

				// Prevent Rename if it exceeds name length.
				if(text.Length > 24) {
					((EditorScene)Systems.scene).editorUI.alertText.SetNotice("Unable to Rename Level", "Title must be 24 characters or less.", 240);
					return;
				}

				((EditorScene)Systems.scene).levelContent.SetTitle(text);
				((EditorScene)Systems.scene).editorUI.noticeText.SetNotice("New Level Title", "Title Set: \"" + text + "\"", 240);
			}
		}
		
		public static void SetDescription() {
			string text = Sanitize.Description(ConsoleTrack.instructionText.Substring(4).TrimStart());
			
			if(text.Length > 0) {
				ConsoleTrack.instructionText = "desc " + text.Substring(0, Math.Min(text.Length, 72));
			}
			
			short remain = (short)(72 - text.Length);

			ConsoleTrack.possibleTabs = "Example: `desc My Cool Level`";
			ConsoleTrack.helpText = "Provide a level description. " + remain.ToString() + " characters remaining.";

			// Activate the Instruction
			if(ConsoleTrack.activate) {

				// Prevent Rename if it exceeds name length.
				if(text.Length > 72) {
					((EditorScene)Systems.scene).editorUI.alertText.SetNotice("Unable to Set Description", "Level description must be 72 characters or less.", 240);
					return;
				}

				((EditorScene)Systems.scene).levelContent.SetDescription(text);
				((EditorScene)Systems.scene).editorUI.noticeText.SetNotice("New Level Description", "Description Set: \"" + text + "\"", 240);
			}
		}

		public static void SetTimeLimit() {
			ConsoleTrack.possibleTabs = "Example: `time 400`";
			ConsoleTrack.helpText = "Set the Time Limit (in seconds) for the level. Minimum 10, Maximum 500.";

			// Prepare Height
			short seconds = (short) ConsoleTrack.GetArgAsInt();

			// Activate the Instruction
			if(ConsoleTrack.activate) {

				// Prevent Rename if it exceeds name length.
				if(seconds < 10 || seconds > 500) {
					((EditorScene)Systems.scene).editorUI.alertText.SetNotice("Unable to Set Time Limit", "Time Limit must be between 10 and 500 seconds.", 240);
					return;
				}

				((EditorScene)Systems.scene).levelContent.SetTimeLimit(seconds);
				((EditorScene)Systems.scene).editorUI.noticeText.SetNotice("New Time Limit", "Time Limit set to " + seconds + ".", 240);
			}
		}

		public static void SetMusicTrack() {
			string currentIns = ConsoleTrack.GetArgAsString();

			// Update the tab lookup.
			ConsoleTrack.PrepareTabLookup(ConsoleEditData.TrackCategory, currentIns, "Choose a music category for the track to play.");

			if(!ConsoleEditData.TrackCategory.ContainsKey(currentIns)) { return; }

			EditorScene scene = (EditorScene)Systems.scene;

			// Remove Music From Level
			if(currentIns == "none") {
				scene.levelContent.SetMusicTrack(0);
				scene.editorUI.noticeText.SetNotice("Removed Music Track", "", 240);
				return;
			}

			Dictionary<string, object> trackCat = ConsoleEditData.TrackLookup[currentIns];

			string trackName = ConsoleTrack.GetArgAsString();
			ConsoleTrack.PrepareTabLookup(trackCat, trackName, "Set a music track for the level.");

			if(MusicAssets.TrackNames.ContainsKey(scene.levelContent.data.music)) {
				ConsoleTrack.helpText += "Currently: \"" + MusicAssets.TrackNames[scene.levelContent.data.music].Replace("Music/", "") + "\".";
			}

			// Activate the Instruction
			if(ConsoleTrack.activate) {

				if(trackCat.ContainsKey(trackName)) {
					byte track = (byte) trackCat[trackName];
					scene.levelContent.SetMusicTrack((byte) track);
					scene.editorUI.noticeText.SetNotice("Set Music Track", "Music Track set to " + MusicAssets.TrackNames[track].Replace("Music/", "") + ".", 240);
					return;
				}

				// Prevent Rename if it exceeds name length.
				scene.editorUI.alertText.SetNotice("Unable to Set Music Track", "Designated music track doesn't exist.", 240);
			}
		}

		public static Dictionary<string, object> TrackCategory = new Dictionary<string, object>() {
			{ "none", 0 },
			{ "peaceful", 1 },
			{ "theme", 2 },
			{ "journey", 3 },
			{ "intense", 4 },
			{ "challenge", 5 },
		};

		public static Dictionary<string, Dictionary<string, object>> TrackLookup = new Dictionary<string, Dictionary<string, object>>() {
			
			// Peaceful
			{ "peaceful", new Dictionary<string, object>() {
				{ "happy1", (byte) MusicTrack.Happy1 },
				{ "happy2", (byte) MusicTrack.Happy2 },
				{ "peaceful", (byte) MusicTrack.Peaceful },
				{ "pleasant1", (byte) MusicTrack.PleasantDay1 },
				{ "pleasant2", (byte) MusicTrack.PleasantDay2 },
			} },

			// Themes
			{ "theme", new Dictionary<string, object>() {
				{ "desert", (byte) MusicTrack.CasualDesert },
				{ "dungeon", (byte) MusicTrack.CasualDungeon },
				{ "forest", (byte) MusicTrack.CasualForest },
				{ "grassland", (byte) MusicTrack.CasualGrassland },
				{ "iceland", (byte) MusicTrack.CasualIceland },
			} },
			
			// Journey
			{ "journey", new Dictionary<string, object>() {
				{ "epic", (byte) MusicTrack.EpicTask },
				{ "quest", (byte) MusicTrack.Journey },
			} },
			
			// Intense
			{ "intense", new Dictionary<string, object>() {
				{ "intense1", (byte) MusicTrack.Intensity1 },
				{ "intense2", (byte) MusicTrack.Intensity2 },
				{ "intense3", (byte) MusicTrack.Intensity3 },
				{ "intense4", (byte) MusicTrack.Intensity4 },
			} },
			
			// Challenges
			{ "challenge", new Dictionary<string, object>() {
				{ "boss1", (byte) MusicTrack.BossFight1 },
				{ "boss2", (byte) MusicTrack.BossFight2 },
				{ "challenge1", (byte) MusicTrack.Challenge1 },
				{ "challenge2", (byte) MusicTrack.Challenge2 },
				{ "challenge3", (byte) MusicTrack.Challenge3 },
			} },
		};
	}
}
