using Nexus.Engine;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	// This Scene runs on every frame, including UniFrames. It can handle global events, UI Events, and anything that can happen across scenes.
	public static class GlobalScene {

		// Global Event Listeners
		public static List<EventListen> listeners = new List<EventListen>();

		public static void Setup() {

			// Activate Confirm Box
			//EventSys.AttachListener(GlobalScene.listeners, EventCategory.Confirm, EventType.Undefined, "", GlobalScene.RunConfirmBox);
		}

		private static void RunConfirmBox(EventData data) {
			EventComponentData cData = (EventComponentData) data;
		}

		public static void RunTick() {}
	}
}
