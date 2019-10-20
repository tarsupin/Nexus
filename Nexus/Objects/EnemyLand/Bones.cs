using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public enum BonesSubType : byte {
		Bones,
	}

	public class Bones : EnemyLand {

		public Bones(LevelScene scene, byte subType, FVector pos, object[] paramList) : base(scene, subType, pos, paramList) {
			this.Meta = Systems.mapper.MetaList[MetaGroup.EnemyLand];

			// Movement
			this.speed = FInt.Create(0.8);

			// Physics, Collisions, etc.
			this.physics = new Physics(this);
			this.physics.SetGravity(FInt.Create(0.5));
			this.physics.velocity.X = (FInt)(0 - this.speed);

			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(8, 4, -4);
		}

		private void AssignSubType( byte subType ) {
			if(subType == (byte) BonesSubType.Bones) {
				this.animate = new Animate(this, "Bones/");
				this.OnDirectionChange();
			}
		}

		public override void OnDirectionChange() {
			this.animate.SetAnimation("Bones/" + (this.FaceRight ? "Right" : "Left"), AnimCycleMap.Cycle3Reverse, 15);
		}

		public override bool GetJumpedOn( Character character, sbyte bounceStrength = 0 ) {
			this.ReceiveWound();
			return true;
		}

		public override bool ReceiveWound() {
			Systems.sounds.crack.Play();
			this.Destroy(); // Can automatically destroy Bones, since it just disappears while bones are left behind.
							//this.Die(DeathResult.Special);

			// Particle Effect (bones exploding)
			ExplodeEmitter.BoxExplosion("Particles/Bone", this.posX + 24, this.posY + 24);

			return true;
		}
	}
}
