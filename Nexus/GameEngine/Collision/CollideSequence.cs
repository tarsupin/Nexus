using System.Buffers;

namespace Nexus.GameEngine {

	public class CollideSequence {

		public ArrayPool<ushort> pool;

		public CollideSequence() {

			// Build the Collision Pool
			this.pool = ArrayPool<ushort>.Shared;

			short minLength = 100;
			ushort[] buffer = this.pool.Rent(minLength);

			this.pool.Return(buffer);
		}
	}
}
