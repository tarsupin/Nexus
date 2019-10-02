
using Newtonsoft.Json;
using Nexus.Engine;
using System.Collections.Generic;

namespace Nexus.Gameplay {

	public class EditorRoomJson {
		public ushort camX;
		public ushort camY;
	}

	public class EditorJson {
		public Dictionary<string, Dictionary<byte, EditorRoomJson>> levels;
	}

	public class EditorState : EditorJson {

		// References
		private readonly GameHandler handler;

		public EditorState(GameHandler handler) {
			this.handler = handler;
		}

		// TODO HIGH PRIORITY: See EditorSystem.ts && WorldEditorSystem.ts
		// TODO HIGH PRIORITY: See EditorSystem.ts && WorldEditorSystem.ts

		public void SaveEditor() {

			EditorJson campaignJson = new EditorJson {
				levels = this.levels,
			};

			// Save State
			string json = JsonConvert.SerializeObject(campaignJson);
			this.handler.GameStateWrite("Editor", json);
		}

		public void LoadEditor() {
			string json = this.handler.GameStateRead("Editor");

			// If there is no JSON content, load an empty state:
			if(json == "") { return; }

			EditorJson editor = JsonConvert.DeserializeObject<EditorJson>(json);
			this.levels = editor.levels;
		}
	}
}
