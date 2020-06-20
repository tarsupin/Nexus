
using Nexus.Config;
using Nexus.Engine;
using System;

namespace Nexus.GameEngine {

	//				{ "title", ConsoleLevelData.SetTitle},
	//				{ "desc", ConsoleLevelData.SetDescription },
	//				{ "music", ConsoleLevelData.SetMusic },
	//				{ "time", ConsoleLevelData.SetTimeLimit },
	//				{ "game-class", ConsoleLevelData.SetGameClass },

	public static class ConsoleEditData {

		public static void SetTitle() {
			string text = Sanitize.Title(ConsoleTrack.instructionText.Substring(5).TrimStart());
			
			if(text.Length > 0) {
				ConsoleTrack.instructionText = "title " + text.Substring(0, Math.Min(text.Length, 24));
			}
			
			short remain = (short)(24 - text.Length);

			ConsoleTrack.possibleTabs = "Example: `title \"My Cool Level\"`";
			ConsoleTrack.helpText = "Provide a level name. " + remain.ToString() + " characters remaining.";

			// Activate the Instruction
			if(ConsoleTrack.activate) {

				// Prevent Rename if it exceeds name length.
				if(text.Length > 24) {
					((EditorRoomScene)Systems.scene).scene.editorUI.alertText.SetNotice("Unable to Rename Level", "Level Name must be 24 characters or less.", 240);
					return;
				}

				((EditorRoomScene)Systems.scene).levelContent.SetTitle(text);
			}
		}
		
		public static void SetDescription() {
			string text = Sanitize.Description(ConsoleTrack.instructionText.Substring(11).TrimStart());
			
			if(text.Length > 0) {
				ConsoleTrack.instructionText = "description " + text.Substring(0, Math.Min(text.Length, 72));
			}
			
			short remain = (short)(72 - text.Length);

			ConsoleTrack.possibleTabs = "Example: `description My Cool Level`";
			ConsoleTrack.helpText = "Provide a level description. " + remain.ToString() + " characters remaining.";

			// Activate the Instruction
			if(ConsoleTrack.activate) {

				// Prevent Rename if it exceeds name length.
				if(text.Length > 72) {
					((EditorRoomScene)Systems.scene).scene.editorUI.alertText.SetNotice("Unable to Rename Level", "Level Name must be 72 characters or less.", 240);
					return;
				}

				((EditorRoomScene)Systems.scene).levelContent.SetDescription(text);
			}
		}

	}
}
