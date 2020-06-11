using Nexus.GameEngine;
using System.Collections.Generic;

namespace Nexus.Engine {

	// Add a QueueEvent for every RoomScene. It can be used to run events at a later time, generally for tiles that don't have RunTick() behaviors.
	// Some tiles will add single or repeating events to QueueEvent.
	//		- To add a QueueEvent, make sure the tile has .hasSetup set to true.
	//		- Then add SetupTile(RoomScene room, ushort gridX, ushort gridY) to the tile.
	//			- See "Cannon" or "ChomperFire" for an example.
	public class QueueEvent {

		RoomScene room;
		Dictionary<uint, List<short[]>> events;
		List<short[]>[] beatEvents;					// Tracks repeating events. Exactly four keys, one for each beat per second.

		public QueueEvent(RoomScene room) {
			this.room = room;
			this.events = new Dictionary<uint, List<short[]>>();
			this.beatEvents = new List<short[]>[4];
			this.beatEvents[0] = new List<short[]>();
			this.beatEvents[1] = new List<short[]>();
			this.beatEvents[2] = new List<short[]>();
			this.beatEvents[3] = new List<short[]>();
		}

		public void AddEvent( uint frame, byte tileId, short gridX, short gridY, short val1 = 0, short val2 = 0 ) {
			
			if(!this.events.ContainsKey(frame)) {
				this.events.Add(frame, new List<short[]>());
			}

			this.events[frame].Add(new short[5] { tileId, gridX, gridY, val1, val2 });
		}

		public void AddBeatEvent( byte tileId, short gridX, short gridY, byte beatMod ) {
			if(beatMod > 3) { return; }
			this.beatEvents[beatMod].Add(new short[3] { tileId, gridX, gridY });
		}

		public void RunEventSequence() {
			var timer = Systems.timer;
			this.RunEventsForThisFrame(timer.Frame);

			// Run Repeating Beat-Tick Events
			if(timer.IsBeatFrame) {
				this.RunEventsForBeatTicks(timer.beat4Modulus);
			}
		}

		private void RunEventsForThisFrame( uint frame ) {
			if(!this.events.ContainsKey(frame)) { return; }

			var eventList = this.events[frame];
			byte count = (byte)eventList.Count;

			// Remove all queue event data from the last frame.
			this.events.Remove(frame - 1);

			// Loop through every event being stored on this frame.
			for(byte i = 0; i < count; i++) {
				short[] eventData = eventList[i];
				this.RunEvent(eventData[0], eventData[1], eventData[2], eventData[3], eventData[4]);
			}
		}

		private void RunEventsForBeatTicks( byte beatMod ) {
			List<short[]> beatList = this.beatEvents[beatMod];
			if(beatList == null) { return; }
			byte count = (byte)beatList.Count;

			// Loop through every event being stored on this frame.
			for(byte i = 0; i < count; i++) {
				short[] eventData = beatList[i];
				
				// If the Beat Event returns false, we can dispose of the event from this beat loop.
				if(!this.RunBeatEvent(eventData[0], eventData[1], eventData[2])) {
					beatList.RemoveAt(i);
					count--;
					i--;
				}
			}
		}

		private void RunEvent( short tileId, short gridX, short gridY, short val1 = 0, short val2 = 0 ) {
			var tileDict = Systems.mapper.TileDict;
			TileObject tile = tileDict[(byte) tileId];
			tile.TriggerEvent(this.room, (ushort) gridX, (ushort) gridY, val1, val2);
		}

		// Return FALSE if the Trigger Event wants to dispose of the event.
		private bool RunBeatEvent( short tileId, short gridX, short gridY ) {
			var tileDict = Systems.mapper.TileDict;
			TileObject tile = tileDict[(byte) tileId];
			return tile.TriggerEvent(this.room, (ushort) gridX, (ushort) gridY);
		}
	}
}
