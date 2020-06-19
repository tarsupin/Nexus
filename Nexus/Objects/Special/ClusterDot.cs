using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class ClusterDot : GameObject {

		public enum ClusterDotSubType : byte {
			Basic = 0,
			Char = 1,
			Screen = 2,
		}

		public Behavior behavior;

		public ClusterDot(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.ClusterDot].meta;

			// Physics, Collisions, etc.
			this.physics = new Physics(this);
			this.SetActivity(Activity.NoCollide);

			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(16, 16, -16, -16);

			// Add Behavior to Clusters
			// Note: Must be after Atlas Bounds (it depends on it).
			if(subType == (byte)ClusterDotSubType.Basic) {
				this.behavior = FlightBehavior.AssignFlightMotion(this, paramList);
			}

			else if(subType == (byte)ClusterDotSubType.Char) {
				// TODO: Make Character Cluster Behavior
				this.behavior = FlightBehavior.AssignFlightMotion(this, paramList);
			}
			
			else if(subType == (byte)ClusterDotSubType.Screen) {
				// TODO: Make Screen Cluster Behavior
				this.behavior = FlightBehavior.AssignFlightMotion(this, paramList);
			}
		}

		public override void RunTick() {
			this.behavior.RunTick();
			this.physics.RunPhysicsTick();
		}

		private void AssignSubType( byte subType ) {
			if(subType == (byte)ClusterDotSubType.Basic) {
				this.SpriteName = "HiddenObject/Cluster";
			} else if(subType == (byte)ClusterDotSubType.Char) {
				this.SpriteName = "HiddenObject/ClusterChar";
			} else if(subType == (byte)ClusterDotSubType.Screen) {
				this.SpriteName = "HiddenObject/ClusterScreen";
			}
		}

		// Clusters are invisible. Do not render them.
		public override void Draw(int camX, int camY) {}
	}
}
