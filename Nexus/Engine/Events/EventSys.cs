using Nexus.GameEngine;
using System;
using System.Collections.Generic;

namespace Nexus.Engine {

	// Attach listeners in the Scene.StartScene() method. These listeners will wait for event feedback and run the action associated with it.
	public static class EventSys {

		// EventSys.AttachListener(GlobalScene.listeners, EventCategory.Form, EventType.Form_Submission, "myCode", GlobalScene.MyTest);
		// private static void MyTest(EventData data) {}
		public static void AttachListener(List<EventListen> listeners, EventCategory cat, EventType type, string code, Action<EventData> action) {
			listeners.Add(new EventListen(cat, type, code, action));
		}

		// EventSys.TriggerEvent(EventCategory.Form, EventType.Form_Submission, "myCode", EventData.Empty);
		public static void TriggerEvent(EventCategory cat, EventType type, string code, EventData data) {

			// Check if there are any scene listeners:
			Scene scene = Systems.scene;

			if(scene.listeners is List<EventListen>) {
				foreach(EventListen listen in scene.listeners) {

					// Make sure the listener matches:
					if(listen.cat != cat) { continue; }
					if(listen.type != type && listen.type != EventType.Undefined) { continue; }
					if(listen.code.Length > 0 && listen.code != code) { continue; }

					data.AssignTriggerData(cat, type, code);

					// Trigger the Event
					listen.action(data);
				}
			}

			// Check if there are any global listeners:
			foreach(EventListen listen in GlobalScene.listeners) {

				// Make sure the listener matches:
				if(listen.cat != cat) { continue; }
				if(listen.type != type && listen.type != EventType.Undefined) { continue; }
				if(listen.code.Length > 0 && listen.code != code) { continue; }

				data.AssignTriggerData(cat, type, code);

				// Trigger the Event
				listen.action(data);
			}
		}
	}

	public class EventListen {
		public EventCategory cat;			// The event category to track. Set to "Undefined" for any category.
		public EventType type;				// The event sub-category to track. Set to 0 ("Undefined") for any sub-category.
		public string code;					// The event key or code to distinguish it from others. Empty string to avoid narrowing results.
		public Action<EventData> action;	// The action to run when the listener is found. Accepts one parameter: EventData.

		public EventListen(EventCategory cat, EventType type, string code, Action<EventData> action) {
			this.cat = cat;
			this.type = type;
			this.code = code;
			this.action = action;
		}
	}

	// If additional data needs to be passed, can create a sub-class of EventData.
	public class EventData {

		public static EventData Empty = new EventData("", 0);
		
		// Custom-Sent Data
		public string str;
		public int num;

		// Triger Data (sends with every trigger)
		public EventCategory eventCategory;
		public EventType eventType;
		public string eventCode;

		public EventData(string str, int num) {
			this.str = str;
			this.num = num;
		}

		public void AssignTriggerData(EventCategory cat, EventType type, string code) {
			this.eventCategory = cat;
			this.eventType = type;
			this.eventCode = code;
		}
	}

	public class EventComponentData : EventData {
		public UIComponent comp;
		public EventComponentData(UIComponent comp, string str = "", int num = 0) : base(str, num) {
			this.comp = comp;
		}
	}
}
