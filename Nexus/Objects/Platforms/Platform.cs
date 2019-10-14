using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

// Items are considered objects that Characters can pick up.
// DROP is a guaranteed action.

namespace Nexus.Objects {

	public class Platform : DynamicGameObject {

		public static readonly FInt MaxFallVelocity = FInt.Create(5);

		public Platform(LevelScene scene, byte subType, FVector pos, object[] paramList) : base(scene, subType, pos, paramList) {
			this.Meta = scene.mapper.MetaList[MetaGroup.Platform];
			this.AssignBoundsByAtlas();
			this.physics.SetGravity(FInt.Create(0));
		}

		public override void RunTick() {

			// Limit Velocity
			if(this.physics.velocity.Y > Platform.MaxFallVelocity) { this.physics.velocity.Y = Platform.MaxFallVelocity; }

			base.RunTick();
		}
	}
}
