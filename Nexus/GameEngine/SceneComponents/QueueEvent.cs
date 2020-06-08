﻿using Nexus.GameEngine;
using System.Collections.Generic;

namespace Nexus.Engine {

	// Add a QueueEvent for every RoomScene. It can be used to run events at a later time, generally for tiles that don't have RunTick() behaviors.
	// When a Room gets generated, some tiles will add repeating events (beatEvents) to QueueEvent. They will trigger on their respective beats.
	public class QueueEvent {

		Dictionary<uint, List<short[]>> events;
		List<short[]>[] beatEvents;					// Tracks repeating events. Exactly four keys, one for each beat per second.

		public QueueEvent() {
			this.events = new Dictionary<uint, List<short[]>>();
			this.beatEvents = new List<short[]>[4];
		}

		public void AddEvent( uint frame, byte tileId, short gridX, short gridY, short val1 = 0, short val2 = 0 ) {
			
			if(!this.events.ContainsKey(frame)) {
				this.events.Add(frame, new List<short[]>());
			}

			this.events[frame].Add(new short[5] { tileId, gridX, gridY, val1, val2 });
		}

		public void AddBeatEvent( byte beatMod, byte tileId, short gridX, short gridY ) {
			this.beatEvents[beatMod].Add(new short[3] { tileId, gridX, gridY } );
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

		private void RunEventsForBeatTicks( uint beatMod ) {
			List<short[]> beatList = this.beatEvents[beatMod];
			if(beatList == null) { return; }
			byte count = (byte)beatList.Count;

			// Loop through every event being stored on this frame.
			for(byte i = 0; i < count; i++) {
				short[] eventData = beatList[i];
				this.RunEvent(eventData[0], eventData[1], eventData[2], 0, 0);
			}
		}

		private void RunEvent( short tileId, short gridX, short gridY, short val1 = 0, short val2 = 0 ) {
			var tileDict = Systems.mapper.TileDict;
			TileObject tile = tileDict[(byte) tileId];
			tile.TriggerEvent(gridX, gridY, val1, val2);
		}
	}
}
