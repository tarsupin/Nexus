using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Objects {

	public enum BonesSubType : byte {
		Bones,
	}

	public class Bones : EnemyLand {

		public Bones(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.Bones].meta;

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
			this.physics.velocity.X = this.speed * (this.FaceRight ? 1 : -1);
			this.animate.SetAnimation("Bones/" + (this.FaceRight ? "Right" : "Left"), AnimCycleMap.Cycle3BothWays, 15);
		}

		public override bool GetJumpedOn( Character character, sbyte bounceStrength = 0 ) {
			this.ReceiveWound();
			return true;
		}

		public override bool ReceiveWound() {
			this.room.PlaySound(Systems.sounds.crack, 1f, this.posX + 16, this.posY + 16);
			this.Destroy(); // Can automatically destroy Bones, since it just disappears while bones are left behind.
			//this.Die(DeathResult.Special);

			// Particle Effect (bones exploding)
			ExplodeEmitter.BoxExplosion(this.room, "Particles/Bone", this.posX + (byte)TilemapEnum.HalfWidth, this.posY + (byte)TilemapEnum.HalfHeight);

			return true;
		}
	}
}
