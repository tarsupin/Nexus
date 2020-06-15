using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class Platform : GameObject {

		public static readonly FInt MaxFallVelocity = FInt.Create(5);

		public Platform(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {

			// Physics + Collisions
			this.physics = new Physics(this);
			this.physics.SetGravity(FInt.Create(0));
			this.SetActivity(Activity.NoTileCollide);
		}

		public virtual void ActivatePlatform() {}

		public override void RunTick() {

			// Limit Velocity
			if(this.physics.velocity.Y > Platform.MaxFallVelocity) { this.physics.velocity.Y = Platform.MaxFallVelocity; }

			base.RunTick();
		}
	}
}
