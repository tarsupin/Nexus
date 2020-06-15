using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class TrailingKeys {

		private const byte TrackLength = 50;

		private readonly Atlas atlas;
		private readonly Character character;

		private byte keys;					// The number of keys trailing behind the character.
		private int[] frameTrackX;			// Tracks the last positions based on character movement.
		private int[] frameTrackY;          // Tracks the last positions based on character movement.
		private byte frameAt = 0;			// Tracks the specific position in the Frame Trackers.

		public TrailingKeys( Character character ) {
			this.atlas = Systems.mapper.atlas[(byte)AtlasGroup.Objects];
			this.character = character;
			this.keys = 0;

			// Frame Tracker
			this.frameTrackX = new int[TrailingKeys.TrackLength];
			this.frameTrackY = new int[TrailingKeys.TrackLength];
			this.ResetTrailingKeys();
		}

		public bool HasMaxKeys { get { return this.keys == 8; } }

		public void RunKeyTick() {
			if(this.keys == 0) { return; }

			// Track the updated position of the character.
			this.frameTrackX[this.frameAt] = character.posX;
			this.frameTrackY[this.frameAt] = character.posY;

			// Move the current frame forward by 1, and loop at TrailingKeys.NumberOfFrames.
			this.frameAt++;

			if(this.frameAt >= TrailingKeys.TrackLength) {
				this.frameAt = 0;
			}
		}

		public bool AddKey() {
			if(this.keys < 8) {
				this.keys++;
				if(this.keys == 1) {
					this.ResetTrailingKeys();
				}
				return true;
			}
			return false;
		}

		public bool RemoveKey() {
			if(this.keys > 0) {
				this.keys--;
				return true;
			}
			return false;
		}

		public void ResetTrailingKeys() {
			for(byte i = 0; i < TrailingKeys.TrackLength; i++) {
				this.frameTrackX[i] = this.character.posX;
				this.frameTrackY[i] = this.character.posY;
			}
		}

		// Draw Each Trailing Key
		public void Draw(int camX, int camY) {
			if(this.keys == 0) { return; }
			for(byte keyId = 0; keyId < this.keys; keyId++) {
				sbyte curPos = (sbyte)(this.frameAt - 10 - (keyId * 5));
				if(curPos < 0) { curPos += (sbyte) TrailingKeys.TrackLength; }
				int curX = this.frameTrackX[curPos];
				int curY = this.frameTrackY[curPos];
				this.atlas.Draw("Items/Key", curX - camX, curY + 17 - camY);
			}
		}
	}
}
