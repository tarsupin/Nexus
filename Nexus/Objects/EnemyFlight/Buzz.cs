using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Objects {

	public enum BuzzSubType : byte {
		Buzz = 0
	}

	public class Buzz : EnemyFlight {

		public Buzz(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.Buzz].meta;

			// Physics, Collisions, etc.
			this.physics = new Physics(this);
			this.shellCollision = true;
			this.SetCollide(CollideEnum.NoTileCollide);

			// Assign Flight Behavior
			this.behavior = FlightBehavior.AssignFlightMotion(this, paramList);

			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(2, 2, -2, -2);
		}

		private void AssignSubType(byte subType) {
			this.animate = new Animate(this, "Buzz/");
			this.animate.SetAnimation("Buzz/" + (this.FaceRight ? "Right" : "Left"), AnimCycleMap.Cycle3BothWays, 12);
		}

		public override void OnDirectionChange() {
			this.animate.SetAnimation("Buzz/" + (this.FaceRight ? "Right" : "Left"), AnimCycleMap.Cycle3BothWays, 12);
		}
	}
}
