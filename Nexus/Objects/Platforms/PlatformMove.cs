using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class PlatformMove : Platform {

		public bool triggeredByTouch = false;
		public int startTime = 0;
		public Behavior behavior;

		public PlatformMove(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.PlatformMove].meta;
			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(0, 0, 0, 0);

			// Assign Flight Behavior
			this.behavior = FlightBehavior.AssignFlightMotion(this, paramList);
		}

		public override void ActivatePlatform() {

			// Activate Move-To Behavior (i.e. it goes to a specific location over a given time),
			if(this.behavior is FlightTo) {
				((FlightTo)this.behavior).BeginMovement();
			}
		}

		public override void RunTick() {

			// Flight Behavior
			this.behavior.RunTick();

			// Standard Physics
			this.physics.RunPhysicsTick();

			// Animations, if applicable.
			if(this.animate is Animate) {
				this.animate.RunAnimationTick(Systems.timer);
			}
		}

		private void AssignSubType(byte subType) {
			if(subType == (byte)PlatformSubTypes.W2) {
				this.SpriteName = "Platform/Move/W2";
			} else {
				this.SpriteName = "Platform/Move/W1";
			}
		}
	}
}
