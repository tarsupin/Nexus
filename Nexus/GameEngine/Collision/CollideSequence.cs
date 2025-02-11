﻿using System.Buffers;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class CollideSequence {

		// References
		private readonly RoomScene room;
		private readonly CollideBroad broad;

		public ArrayPool<int> pool;

		public CollideSequence(RoomScene room) {
			this.room = room;
			this.broad = new CollideBroad();

			// Build the Collision Pool
			//this.pool = ArrayPool<int>.Shared;
		}

		// 1. Get all game objects from the room.
		// 2. Send to CollideBroad component to be sorted.
		public void RunCollisionSequence() {

			Dictionary<byte, Dictionary<int, GameObject>> objects = this.room.objects;

			// Run the Broad Sequence to build our array.
			this.broad.RunBroadSequence( 0, objects );
		}
	}
}
