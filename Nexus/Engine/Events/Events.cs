using System.Collections.Generic;

namespace Nexus.Engine {

	public static class EventSys {

		public static EventList generic;	// Locally Defined Events, Scene-Specific Events
		public static EventList form;		// Buttons, Forms, UI Events
		public static EventList external;	// Web, Other Programs
		public static EventList system;		// System-Exclusive Events (cannot edit)

		public static void ClearEvents() {
			byte mod = (byte)(Systems.timer.UniFrame % 10);

			// Clear Event Groups
			if(mod == 0) { EventSys.system.PurgeEventList(); }
			else if(mod == 2) { EventSys.external.PurgeEventList(); }
			else if(mod == 4) { EventSys.generic.PurgeEventList(); }
			else if(mod == 6) { EventSys.form.PurgeEventList(); }
		}
	}
	
	public class EventList {
		public List<EventData> events;

		// Loop through the event list; clear any that are outdated.
		public void PurgeEventList() {
			for(short i = (short)(this.events.Count); i > 0; i--) {
				EventData ev = this.events[i];
				if(ev.uniFrame < Systems.timer.UniFrame) {
					this.events.RemoveAt(i);
				}
			}
		}

		// Returns the first available event based on the tag watched, and processes it (clears it from the event list).
		// Use this if the event tag is designed for one-off instances that won't overlap.
		public EventData ProcessFirstEvent(string tag) {
			return this.GetFirstEvent(tag, true);
		}

		// Returns the first available event based on the tag watched.
		// Use this if it is impossible (or unnecessary) to have multiple returns from this tag.
		public EventData GetFirstEvent(string tag, bool process = false) {

			for(short i = (short)(this.events.Count); i > 0; i--) {
				EventData ev = this.events[i];

				// Match the Event
				if(ev.uniFrame == Systems.timer.UniFrame && ev.tag == tag) {

					// Process the event (if applicable)
					if(process) {
						this.events.RemoveAt(i);
					}

					return ev;
				}
			}

			return null;
		}

		// Returns a list of all events that match the tag watched.
		// Use this if it is possible to have multiple returns from the tag, such as for generic tags like "Message."
		public List<EventData> GetListOfEvents(string tag) {
			List<EventData> list = new List<EventData>();

			for(short i = (short)(this.events.Count); i > 0; i--) {
				EventData ev = this.events[i];

				// Locate Matching Tags
				if(ev.uniFrame == Systems.timer.UniFrame && ev.tag == tag) {
					list.Add(ev);
				}
			}

			return null;
		}
	}

	public class EventData {
		public string tag;				// An event ID or category to track the events by. Can be generic (e.g. "Message") or specific ("Button_14")
		public int uniFrame;			// The UniFrame that this event is designed to trigger on. Set to [Current UniFrame + 1] to ensure it gets detected.
		public string instruction;		// An advanced instruction to include with the event.
		public int value;				// A tracker, since many events might have a simple enum or numeric value to pass.
	}
}
