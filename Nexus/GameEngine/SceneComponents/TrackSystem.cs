using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Engine {

	public class Track {
		public readonly TrackSystem trackSys;
		public readonly byte trackId = 0;

		// Position
		public readonly int posX;
		public readonly int posY;

		// Arrival + Destination
		public byte toId = 0;
		public readonly short duration;		// How long it will take to arrive at the next location.
		public readonly short delay;		// How long the track pauses at this location.
		public readonly bool beginsFall;	// TRUE means it will begin to fall on arrival.

		// Calculated Values
		public int distance;				// Distance, as measured between current track and destination track.
		public float rotation;				// Rotation between current track and destination track.
		public FInt speed;					// Speed that the track moves to it's destination.

		public Track(TrackSystem trackSys, byte trackId, short gridX, short gridY, byte toId, short duration, short delay, bool beginsFall) {
			this.trackSys = trackSys;
			this.trackId = trackId;

			// Position
			this.posX = gridX * (byte)TilemapEnum.TileWidth;
			this.posY = gridY * (byte)TilemapEnum.TileHeight;

			// Arrival + Destination
			this.toId = toId;
			this.duration = duration;
			this.delay = delay;
			this.beginsFall = beginsFall;
		}

		public Track NextTrack { get { return this.trackSys.GetTrack(this.toId); } }
	}
	
	public class Cluster {
		public readonly byte clusterId = 0;
		public readonly GameObject actor;

		public Cluster( byte clusterId, GameObject actor ) {
			this.clusterId = clusterId;
			this.actor = actor;
		}
	}

	// Add a TrackSystem class for every RoomScene. It will handle all of the tracked flight mechanics.
	public class TrackSystem {

		public const byte MaxTracks = 100;
		public const byte MaxClusters = 20;

		private Dictionary<byte, Track> tracks;
		private Dictionary<byte, Cluster> clusters;
		private Dictionary<GameObject, byte> prepTracks;		// Tracks a list of GameObjects with their intended track destination IDs.
		private Dictionary<GameObject, byte> prepClusters;		// Tracks a list of GameObjects with their intended cluster destination IDs.

		public TrackSystem() {
			this.ResetTrackSystem();
		}

		public Track GetTrack(byte trackId) { return this.tracks.ContainsKey(trackId) ? this.tracks[trackId] : null; }

		public void AddTrack( byte trackId, short gridX, short gridY, byte toId, short duration, short delay, bool beginsFall ) {

			// Must have a valid Track ID.
			if(trackId < 1 || trackId > TrackSystem.MaxTracks) { return; }

			this.tracks[trackId] = new Track(this, trackId, gridX, gridY, toId, duration, delay, beginsFall);
		}

		public void AddCluster( byte clusterId, GameObject actor ) {

			// Must have a valid Cluster ID assigned.
			if(clusterId < 1 || clusterId > TrackSystem.MaxClusters) { return; }

			// Must have Flight Behavior to qualify as a cluster.
			if((actor as dynamic).behavior == null || (actor as dynamic).behavior is FlightBehavior == false) { return; }

			// Create the Cluster
			this.clusters[clusterId] = new Cluster(clusterId, actor);
		}

		public void AssignToClusterId( byte clusterId, GameObject actor ) {

			// Must have a valid Cluster Destination ID.
			if(clusterId < 1 || clusterId > TrackSystem.MaxClusters) { return; }

			// We need to track this actor during the GameObject creation phase.
			// After all GameObjects have been created on the level, we'll build these in the BuildClusterLinks method.
			this.prepClusters[actor] = clusterId;
		}
		
		public void AssignToTrackId( byte trackId, GameObject actor ) {

			// Must have a valid Cluster Destination ID.
			if(trackId < 1 || trackId > TrackSystem.MaxTracks) { return; }

			// We need to track this actor during the GameObject creation phase.
			// After all GameObjects have been created on the level, we'll build these in the BuildClusterLinks method.
			this.prepTracks[actor] = trackId;
		}

		public void SetupTrackSystem() {
			this.BuildTrackCalculations();
			this.BuildClusterLinks();
			this.BuildTrackLinks();
		}

		// Runs after all GameObjects have been created in a room. It will complete the creation of all tracks, including their calculated fields.
		private void BuildTrackCalculations() {

			// Loop through every track.
			foreach(var trackMatch in this.tracks) {
				Track track = trackMatch.Value;

				// Identify Destination Track (if applicable).
				if(track.toId != 0) {

					// If unavailable, nullify this track's behavior.
					if(!this.tracks.ContainsKey(track.toId)) {
						track.toId = 0;
						continue;
					}

					Track destTrack = this.tracks[track.toId];

					// Calculate distance, rotation, and speed of the track's movement.
					track.distance = TrigCalc.GetDistance(track.posX, track.posY, destTrack.posX, destTrack.posY);
					track.rotation = Radians.GetRadiansBetweenCoords(track.posX, track.posY, destTrack.posX, destTrack.posY);
					track.speed = FInt.Create((float)(track.distance / track.duration));
				}
			}
		}
		
		// Runs after all GameObjects have been created in a room. It will attach each GameObject to its designated cluster.
		private void BuildClusterLinks() {

			// Loop through every track.
			foreach(var actorMatch in this.prepClusters) {
				GameObject actor = actorMatch.Key;
				byte destId = actorMatch.Value;

				// Must have Flight Behavior to qualify for connecting to a cluster.
				if((actor as dynamic).behavior == null || (actor as dynamic).behavior is FlightBehavior == false) { return; }
				
				// Connect the actor to their designated cluster.
				if(this.clusters.ContainsKey(destId)) {
					(actor as dynamic).behavior.cluster = this.clusters[destId];
				}
			}
		}

		// Runs after all GameObjects have been created in a room. It will attach each GameObject to its designated track.
		private void BuildTrackLinks() {

			// Loop through every track.
			foreach(var actorMatch in this.prepTracks) {
				GameObject actor = actorMatch.Key;
				byte destId = actorMatch.Value;

				// Must have Flight Behavior to qualify for connecting to a cluster.
				if((actor as dynamic).behavior == null || (actor as dynamic).behavior is FlightTrack == false) { return; }
				
				// Connect the actor to their designated cluster.
				if(this.tracks.ContainsKey(destId)) {
					(actor as dynamic).behavior.nextTrack = this.tracks[destId];
				}
			}
		}

		private void ResetTrackSystem() {
			this.tracks = new Dictionary<byte, Track>();
			this.clusters = new Dictionary<byte, Cluster>();
			this.prepTracks = new Dictionary<GameObject, byte>();
			this.prepClusters = new Dictionary<GameObject, byte>();
		}
	}
}
