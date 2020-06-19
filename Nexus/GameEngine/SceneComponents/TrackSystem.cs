using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.Engine {

	public class Track {
		public readonly byte trackId = 0;

		// Position
		public readonly int posX;
		public readonly int posY;

		// Arrival + Destination
		public readonly byte toId = 0;
		public readonly short duration;		// How long it will take to arrive at the next location.
		public readonly short delay;		// How long the track pauses at this location.
		public readonly bool beginsFall;	// TRUE means it will begin to fall on arrival.

		public Track(byte trackId, short gridX, short gridY, byte toId, short duration, short delay, bool beginsFall) {
			this.trackId = trackId;
			this.posX = gridX * (byte)TilemapEnum.TileWidth;
			this.posY = gridY * (byte)TilemapEnum.TileHeight;
			this.toId = toId;
			this.duration = duration;
			this.delay = delay;
			this.beginsFall = beginsFall;
		}
	}

	// Add a TrackSystem class for every RoomScene. It will handle all of the tracked flight mechanics.
	public class TrackSystem {

		Dictionary<byte, Track> tracks;

		public TrackSystem() {
			this.ResetTrackSystem();
		}

		public void AddTrack( byte trackId, short gridX, short gridY, byte toId, short duration, short delay, bool beginsFall ) {

			// Ignore any Tracks listed as ID of 0.
			if(trackId == 0) { return; }

			this.tracks[trackId] = new Track(trackId, gridX, gridY, toId, duration, delay, beginsFall);
		}

		public void ResetTrackSystem() {
			this.tracks = new Dictionary<byte, Track>();
		}
	}
}
