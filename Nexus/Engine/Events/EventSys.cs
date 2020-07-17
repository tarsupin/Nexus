using Nexus.GameEngine;
using System;
using System.Collections.Generic;

namespace Nexus.Engine {

	// Attach listeners in the Scene.StartScene() method. These listeners will wait for event feedback and run the action associated with it.
	public static class EventSys {

		// EventSys.AttachListener(this.scene.listeners, EventCategory.Form, (byte) FormEvents.Submission, id, MyAction);
		public static void AttachListener(List<EventListen> listeners, EventCategory cat, byte subCat, string code, Action<EventData> action) {
			listeners.Add(new EventListen(cat, subCat, code, action));
		}

		public static void TriggerEvent(EventCategory cat, byte subCat, string code, EventData data) {

			// Check if there are any scene listeners:
			Scene scene = Systems.scene;

			foreach(EventListen listen in scene.listeners) {

				// Make sure the listener matches:
				if(listen.cat != cat) { continue; }
				if(listen.subCat != subCat && listen.subCat != 0) { continue; }
				if(listen.code.Length > 0 && listen.code != code) { continue; }

				// Trigger the Event
				listen.action(data);
			}
		}
	}

	public class EventListen {
		public EventCategory cat;			// The event category to track. Set to "Undefined" for any category.
		public byte subCat;					// The event sub-category to track. Set to 0 ("Undefined") for any sub-category.
		public string code;					// The event key or code to distinguish it from others. Empty string to avoid narrowing results.
		public Action<EventData> action;	// The action to run when the listener is found. Accepts one parameter: EventData.

		public EventListen(EventCategory cat, byte subCat, string code, Action<EventData> action) {
			this.cat = cat;
			this.subCat = subCat;
			this.code = code;
			this.action = action;
		}
	}

	// If additional data needs to be passed, can create a sub-class of EventData.
	public class EventData {
		public string str;
		public int num;
	}
}
