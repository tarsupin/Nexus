using Nexus.Engine;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	// This Scene runs on every frame, including UniFrames. It can handle global events, UI Events, and anything that can happen across scenes.
	public static class GlobalScene {

		// Global Event Listeners
		public static List<EventListen> listeners;

		public static void Setup() {
			//EventSys.AttachListener(GlobalScene.listeners, EventCategory.Form, (byte)FormEvents.Submission, id, MyAction);
		}

		public static void RunTick() {}
	}
}
