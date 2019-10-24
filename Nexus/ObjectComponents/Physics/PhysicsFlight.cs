using Newtonsoft.Json.Linq;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.ObjectComponents {

	public enum PhysicsDefaults : short {
		MoveFlightDuration = 180,			// NOTE: this needs to be moved to ParamsMoveFlight.rules.duration.default or new system.
	}

	public class PhysicsFlight : Physics {

		// Motion Flags
		protected ushort duration;			// Duration the motion is intended to take, in frames (such as 120 frames between two points).
		protected ushort durationOffset;	// the offset in the duration cycle (for timing purposes)
		protected bool reverse;				// `true` means the motion is backward (such as for moving platforms returning).
		
		protected int startX;				// Starting X position of a motion.
		protected int startY;               // Starting Y position of a motion.
		protected int midX;                 // Middle X position of a motion, such as for curves (quadratic motion)
		protected int midY;                 // Middle Y position of a motion, such as for curves (quadratic motion).
		protected int endX;                 // End X position of a motion.
		protected int endY;                 // End Y position of a motion.

		// Cluster Motion
		protected byte actAsClusterId;      // Indicates that this object is a cluster. All child clusters will remain offset to it.
		protected byte clusterLinkId;		// Indicates a cluster to link to. Object will lock its offset position to the parent.

		public PhysicsFlight( DynamicGameObject objRef, JObject paramList) : base(objRef) {

			// TODO CLEANUP: REMOVE
			System.Console.WriteLine(paramList.ToString());

			this.reverse = paramList["reverse"] != null;

			this.duration = paramList["duration"] != null ? ushort.Parse(paramList["duration"].ToString()) : (ushort) PhysicsDefaults.MoveFlightDuration;
			this.durationOffset = paramList["durationOffset"] != null ? ushort.Parse(paramList["durationOffset"].ToString()) : (ushort) 0;

			// Positions
			this.startX = objRef.posX;
			this.startY = objRef.posY;
			this.midX = this.startX + (paramList["midX"] != null ? int.Parse(paramList["midX"].ToString()) * (byte) TilemapEnum.TileWidth : 0);
			this.midY = this.startY + (paramList["midY"] != null ? int.Parse(paramList["midY"].ToString()) * (byte) TilemapEnum.TileHeight : 0);
			this.endX = this.startX + (paramList["x"] != null ? int.Parse(paramList["x"].ToString()) * (byte) TilemapEnum.TileWidth : 0);
			this.endY = this.startY + (paramList["y"] != null ? int.Parse(paramList["y"].ToString()) * (byte) TilemapEnum.TileHeight : 0);

			// Clusters
			this.actAsClusterId = paramList["clusterId"] != null ? byte.Parse(paramList["clusterId"].ToString()) : (byte) 0;
			this.clusterLinkId = paramList["clusterLinkId"] != null ? byte.Parse(paramList["clusterLinkId"].ToString()) : (byte) 0;

			// If the object is a cluster, or is attached to a parent cluster, it must be tracked through the full level.
			if(this.actAsClusterId > 0 || this.clusterLinkId > 0) {
				objRef.SetActivity(Activity.ForceActive);
			}
		}

		public new void RunTick() {
			this.TrackPhysicsTick();
		}
	}
}
