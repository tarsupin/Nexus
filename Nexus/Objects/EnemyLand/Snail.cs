using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using Newtonsoft.Json.Linq;

namespace Nexus.Objects {

	public enum SnailSubType : byte {
		Snail,
	}

	public class Snail : EnemyLand {

		public Snail(LevelScene scene, byte subType, FVector pos, JObject paramList) : base(scene, subType, pos, paramList) {
			this.Meta = Systems.mapper.MetaList[MetaGroup.EnemyLand];

			// Movement
			this.speed = FInt.Create(1.0);

			// Physics, Collisions, etc.
			this.physics = new Physics(this);
			this.physics.SetGravity(FInt.Create(0.5));
			this.physics.velocity.X = (FInt)(0 - this.speed);

			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(10, 4, -4);
		}

		private void AssignSubType( byte subType ) {
			if(subType == (byte) SnailSubType.Snail) {
				this.animate = new Animate(this, "Snail/");
				this.OnDirectionChange();
			}
		}

		public override void OnDirectionChange() {
			this.animate.SetAnimation("Snail/" + (this.FaceRight ? "Right" : "Left"), AnimCycleMap.Cycle3Reverse, 15);
		}

		public override bool GetJumpedOn(Character character, sbyte bounceStrength = 0) {
			if(this.status.action is DeathEnemyAction) { return false; }
			character.BounceUp(this, bounceStrength);
			Systems.sounds.splat1.Play();
			// TODO: Snail Disappears; create shell in it's place.
			return this.Die(DeathResult.Special);
		}

	}
}
