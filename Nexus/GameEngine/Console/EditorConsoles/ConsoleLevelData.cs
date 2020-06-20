using Nexus.Engine;
using System;

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
					((EditorScene)Systems.scene).editorUI.alertText.SetNotice("Unable to Rename Level", "Level Name must be 72 characters or less.", 240);
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
	}
}
