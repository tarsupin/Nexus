using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public enum BugSubType : byte {
		Bug,
	}

	public class Bug : EnemyLand {

		public Bug(LevelScene scene, byte subType, FVector pos, object[] paramList) : base(scene, subType, pos, paramList) {
			this.Meta = Systems.mapper.MetaList[MetaGroup.EnemyLand];

			// Movement
			this.speed = FInt.Create(1.0);

			// Physics, Collisions, etc.
			this.physics = new Physics(this);
			this.physics.SetGravity(FInt.Create(0.5));
			this.physics.velocity.X = (FInt)(0 - this.speed);

			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(4, 2, -2);
		}

		private void AssignSubType( byte subType ) {
			if(subType == (byte) BugSubType.Bug) {
				this.animate = new Animate(this, "Bug/");
				this.OnDirectionChange();
			}
		}

		public override void OnDirectionChange() {
			this.animate.SetAnimation("Bug/" + (this.FaceRight ? "Right" : "Left"), AnimCycleMap.Cycle2, 15);
		}
	}
}
