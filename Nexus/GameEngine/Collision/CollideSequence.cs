using System.Buffers;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class CollideSequence {

		// References
		private readonly LevelScene scene;
		private readonly CollideBroad broad;

		public ArrayPool<uint> pool;

		public CollideSequence(LevelScene scene ) {
			this.scene = scene;
			this.broad = new CollideBroad();

			// Build the Collision Pool
			this.pool = ArrayPool<uint>.Shared;
		}

		// 1. Get all game objects from the scene.
		// 2. Send to CollideBroad component to be sorted.
		public void RunCollisionSequence() {

			Dictionary<byte, Dictionary<uint, DynamicGameObject>> objects = this.scene.objects;

			// Run the Broad Sequence to build our array.
			this.broad.RunBroadSequence( 0, objects );
		}
	}
}
