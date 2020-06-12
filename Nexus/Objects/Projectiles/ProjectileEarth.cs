﻿using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public enum ProjectileEarthSubType : byte {
		Earth
	}

	public class ProjectileEarth : Projectile {

		private uint DeathSequence;         // The frame # that the death sequence ends (or 0 if not in death sequence).

		public ProjectileEarth() : base(null, 0, FVector.Create(0, 0), FVector.Create(0, 0)) { }

		public static ProjectileEarth Create(RoomScene room, byte subType, FVector pos, FVector velocity) {

			// Retrieve an available projectile from the pool.
			ProjectileEarth projectile = ProjectilePool.ProjectileEarth.GetObject();

			projectile.ResetProjectile(room, subType, pos, velocity);
			projectile.AssignSubType(subType);
			projectile.AssignBoundsByAtlas(2, 2, -2, -2);
			projectile.SetDamage(DamageStrength.Lethal);
			projectile.SetCollisionType(ProjectileCollisionType.Special);
			projectile.SetSafelyJumpOnTop(true);
			projectile.DeathSequence = 0;
			projectile.physics.SetGravity(FInt.Create(0.8));

			// Add the Projectile to Scene
			room.AddToScene(projectile, false);

			return projectile;
		}

		public override void RunTick() {

			// If the death sequence is active, test for when it ends.
			if(this.DeathSequence > 0 && Systems.timer.Frame > this.DeathSequence) {
				this.DeathSequence = 0;
				this.ReturnToPool();
				return;
			}

			base.RunTick();
		}

		public override void Destroy( DirCardinal dir = DirCardinal.None, GameObject obj = null ) {
			
			// Can't be destroyed if already being destroyed:
			if(this.DeathSequence > 0) { return; }

			// If no object was sent, it's colliding with an enemy or other object.
			if(obj == null) { return; }

			// Only collide with ground and blocks; not platforms.
			Arch objArch = obj.Meta.Archetype;

			if(objArch == Arch.Ground || objArch == Arch.Block || objArch == Arch.ToggleBlock) {

				// The projectile collided; bounce against the ground. Set it's death sequence values:
				this.DeathSequence = Systems.timer.Frame + 10;
				this.physics.velocity.Y = FInt.Create(-4);

				Systems.sounds.shellThud.Play();

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
