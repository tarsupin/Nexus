using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public enum ProjectileEarthSubType : byte {
		Earth
	}

	public class ProjectileEarth : Projectile {

		private uint DeathSequence;			// The frame # that the death sequence ends (or 0 if not in death sequence).

		public ProjectileEarth(LevelScene scene, byte subType, FVector pos, FVector velocity) : base(scene, subType, pos, velocity, "Earth") {
			this.AssignSubType(subType);
			this.CollisionType = ProjectileCollisionType.Special;
			this.SafelyJumpOnTop = true;
			this.Damage = DamageStrength.Lethal;
			this.DeathSequence = 0;
			this.physics.SetGravity(FInt.Create(0.8));

			// TODO PHYSICS
			// TODO RENDERING:
			//this.physics.update = ballMovement;
			//this.render = this.renderBallRotation;
		}

		public override void RunTick() {

			// If the death sequence is active, test for when it ends.
			if(this.DeathSequence > 0 && Systems.timer.frame > this.DeathSequence) {
				this.DeathSequence = 0;
				this.Disable();
				return;
			}

			base.RunTick();
		}

		public override void Destroy( DirCardinal dir = DirCardinal.Center, GameObject obj = null ) {
			
			// Can't be destroyed if already being destroyed:
			if(this.DeathSequence > 0) { return; }

			// If no object was sent, it's colliding with an enemy or other object.
			if(obj == null) { return; }

			// Only collide with ground and blocks; not platforms.
			Arch objArch = obj.Meta.Archetype;

			if(objArch == Arch.Ground || objArch == Arch.Block || objArch == Arch.ToggleBlock) {

				// The projectile collided; bounce against the ground. Set it's death sequence values:
				this.DeathSequence = Systems.timer.frame + 10;
				this.physics.velocity.Y = FInt.Create(-4);

				// TODO SOUND:
				// this.scene.soundList.shellThud.play(0.4); // Quiet Thud

				this.Damage = DamageStrength.Trivial;
			}
		}

		private void AssignSubType(byte subType) {
			if(subType == (byte) ProjectileEarthSubType.Earth) {
				this.SetSpriteName("Projectiles/Earth1");
			}
		}
	}
}
