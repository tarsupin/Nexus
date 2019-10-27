using Newtonsoft.Json.Linq;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.ObjectComponents {

	public enum FlightDefaults : short {
		MoveFlightDuration = 180,			// NOTE: this needs to be moved to ParamsMoveFlight.rules.duration.default or new system.
	}

	public class FlightBehavior : Behavior {

		// Motion Flags
		protected ushort duration;			// Duration the motion is intended to take, in frames (such as 120 frames between two points).
		protected ushort offset;			// The offset in the duration cycle (for timing purposes)
		protected bool reverse;				// `true` means the motion is backward (such as for moving platforms returning).
		
		protected int startX;				// Starting X position of a motion.
		protected int startY;               // Starting Y position of a motion.
		protected int endX;                 // End X position of a motion.
		protected int endY;                 // End Y position of a motion.

		// Cluster Motion
		protected byte actAsClusterId;      // Indicates that this object is a cluster. All child clusters will remain offset to it.
		protected byte clusterLinkId;		// Indicates a cluster to link to. Object will lock its offset position to the parent.

		public FlightBehavior( DynamicGameObject actor, JObject paramList) : base(actor) {

			this.reverse = paramList["reverse"] != null;

			this.duration = paramList["duration"] != null ? paramList["duration"].Value<ushort>() : (ushort) FlightDefaults.MoveFlightDuration;
			this.offset = paramList["offset"] != null ? paramList["offset"].Value<ushort>() : (ushort) 0;

			// Positions
			this.startX = actor.posX;
			this.startY = actor.posY;
			this.endX = this.startX + (paramList["x"] != null ? paramList["x"].Value<int>() * (byte) TilemapEnum.TileWidth : 0);
			this.endY = this.startY + (paramList["y"] != null ? paramList["y"].Value<int>() * (byte) TilemapEnum.TileHeight : 0);

			// Clusters
			this.actAsClusterId = paramList["clusterId"] != null ? paramList["clusterId"].Value<byte>() : (byte) 0;
			this.clusterLinkId = paramList["toCluster"] != null ? paramList["toCluster"].Value<byte>() : (byte) 0;

			// If the object is a cluster, or is attached to a parent cluster, it must be tracked through the full level.
			if(this.actAsClusterId > 0 || this.clusterLinkId > 0) {
				actor.SetActivity(Activity.ForceActive);
			}
		}

		public static FlightBehavior AssignFlightMotion( DynamicGameObject actor, JObject paramList ) {
			byte type = paramList["fly"] == null ? (byte) FlightMovement.Axis : paramList["fly"].Value<byte>();

			// TODO HIGH PRIORITY: UNCOMMENT BEHAVIORS BELOW:
			// TODO HIGH PRIORITY: UNCOMMENT BEHAVIORS BELOW:

			if(type == (byte) FlightMovement.Circle) {
				return new FlightCircle(actor, paramList);
			}

			if(type == (byte) FlightMovement.Quadratic) {
				return new FlightQuadratic(actor, paramList);
			}

			if(type == (byte) FlightMovement.To) {
				//return new FlightTo(actor, paramList);
			}

			if(type == (byte) FlightMovement.Track) {
				//return new FlightTrack(actor, paramList);
			}

			return new FlightAxis( actor, paramList );
		}
	}
}
