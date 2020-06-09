using Nexus.GameEngine;
using Nexus.Gameplay;
using System.Collections.Generic;

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

		public FlightBehavior( GameObject actor, Dictionary<string, short> paramList) : base(actor) {
			
			if(paramList == null) { paramList = new Dictionary<string, short>(); }

			this.reverse = paramList.ContainsKey("reverse") ? paramList["reverse"] == 1 : false;

			this.duration = paramList.ContainsKey("duration") ? (ushort) paramList["duration"] : (ushort) FlightDefaults.MoveFlightDuration;
			this.offset = paramList.ContainsKey("offset") ? (ushort) paramList["offset"] : (ushort) 0;

			// Positions
			this.startX = actor.posX;
			this.startY = actor.posY;
			this.endX = this.startX + (paramList.ContainsKey("x") ? paramList["x"] * (byte) TilemapEnum.TileWidth : 0);
			this.endY = this.startY + (paramList.ContainsKey("y") ? paramList["y"] * (byte) TilemapEnum.TileHeight : 0);

			// Clusters
			this.actAsClusterId = paramList.ContainsKey("clusterId") ? (byte) paramList["clusterId"] : (byte) 0;
			this.clusterLinkId = paramList.ContainsKey("toCluster") ? (byte) paramList["toCluster"] : (byte) 0;

			// If the object is a cluster, or is attached to a parent cluster, it must be tracked through the full level.
			if(this.actAsClusterId > 0 || this.clusterLinkId > 0) {
				actor.SetActivity(Activity.ForceActive);
			}
		}

		public static FlightBehavior AssignFlightMotion( GameObject actor, Dictionary<string, short> paramList ) {
			byte type = paramList != null && paramList.ContainsKey("fly") ? (byte) paramList["fly"] : (byte) FlightMovement.Axis;

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
