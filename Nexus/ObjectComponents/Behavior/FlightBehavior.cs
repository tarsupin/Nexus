using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.ObjectComponents {

	public enum FlightDefaults : short {
		MoveFlightDuration = 180,			// NOTE: this needs to be moved to ParamsMoveFlight.rules.duration.default or new system.
	}

	public class FlightBehavior : Behavior {
		protected Physics physics;			// Reference to the actor's physics.

		// Motion Flags
		protected short duration;			// Duration the motion is intended to take, in frames (such as 120 frames between two points).
		protected short offset;				// The offset in the duration cycle (for timing purposes)
		protected bool reverse;				// `true` means the motion is backward (such as for moving platforms returning).
		
		protected int startX;				// Starting X position of a motion.
		protected int startY;				// Starting Y position of a motion.
		protected int endX;					// End X position of a motion.
		protected int endY;                 // End Y position of a motion.

		// Cluster Motion
		public Cluster cluster;				// Indicates the cluster that this object connects to.

		public FlightBehavior( GameObject actor, Dictionary<string, short> paramList) : base(actor) {
			if(paramList == null) { paramList = new Dictionary<string, short>(); }
			this.physics = actor.physics;

			this.reverse = paramList.ContainsKey("reverse") ? paramList["reverse"] == 1 : false;

			this.duration = paramList.ContainsKey("duration") ? (short) paramList["duration"] : (short) FlightDefaults.MoveFlightDuration;
			this.offset = paramList.ContainsKey("durOffset") ? (short) paramList["durOffset"] : (short) 0;

			// Positions
			this.startX = actor.posX;
			this.startY = actor.posY;
			this.endX = this.startX + (paramList.ContainsKey("x") ? paramList["x"] * (byte) TilemapEnum.TileWidth : 0);
			this.endY = this.startY + (paramList.ContainsKey("y") ? paramList["y"] * (byte) TilemapEnum.TileHeight : 0);

			// Clusters
			byte clusterId = paramList.ContainsKey("clusterId") ? (byte) paramList["clusterId"] : (byte) 0;
			byte toClusterId = paramList.ContainsKey("toCluster") ? (byte) paramList["toCluster"] : (byte) 0;

			if(clusterId > 0) {
				actor.room.trackSys.AddCluster(clusterId, actor);
			}

			if(toClusterId > 0) {
				actor.room.trackSys.AssignToClusterId(toClusterId, actor);
			}
		}

		public static FlightBehavior AssignFlightMotion( GameObject actor, Dictionary<string, short> paramList ) {
			if(paramList == null) { paramList = new Dictionary<string, short>(); }
			byte type = paramList.ContainsKey("fly") ? (byte) paramList["fly"] : (byte) FlightMovement.Axis;

			if(type == (byte) FlightMovement.Circle) {
				return new FlightCircle(actor, paramList);
			}

			if(type == (byte) FlightMovement.Quadratic) {
				return new FlightQuadratic(actor, paramList);
			}

			if(type == (byte) FlightMovement.To) {
				return new FlightTo(actor, paramList);
			}

			if(type == (byte) FlightMovement.Track) {
				return new FlightTrack(actor, paramList);
			}

			return new FlightAxis( actor, paramList );
		}
	}
}
