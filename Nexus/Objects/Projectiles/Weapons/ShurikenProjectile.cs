using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using static Nexus.ObjectComponents.Shuriken;

namespace Nexus.Objects {

	public class ShurikenProjectile : Projectile {
		
		private uint gravFrame;          // The frame that a movement style ends on.

		public ShurikenProjectile() : base(null, 0, FVector.Create(0, 0), FVector.Create(0, 0)) {
			this.Damage = DamageStrength.Standard;
			this.CollisionType = ProjectileCollisionType.DestroyOnCollide;
		}

		public static ShurikenProjectile Create(RoomScene room, byte subType, FVector pos, FVector velocity) {

			// Retrieve an available projectile from the pool.
			ShurikenProjectile projectile = ProjectilePool.ShurikenProjectile.GetObject();

			projectile.ResetProjectile(room, subType, pos, velocity);

			// Assign Projectile Appearance
			switch(subType) {
				case (byte)ShurikenSubType.Green: projectile.SetSpriteName("Weapon/ShurikenGreen"); break;
				case (byte)ShurikenSubType.Red: projectile.SetSpriteName("Weapon/ShurikenRed"); break;
				case (byte)ShurikenSubType.Blue: projectile.SetSpriteName("Weapon/ShurikenBlue"); break;
				case (byte)ShurikenSubType.Yellow: projectile.SetSpriteName("Weapon/ShurikenYellow"); break;
			}

			projectile.AssignBoundsByAtlas(2, 2, -2, -2);

			// Assign the beginning of the shuriken attack:
			projectile.physics.SetGravity(FInt.Create(0)); // Switches to 0.4 after MotionStart finished.
			projectile.SetState((byte) CommonState.MotionStart);
			projectile.gravFrame = Systems.timer.Frame + 10;
			projectile.spinRate = projectile.physics.velocity.X > 0 ? 0.14f : -0.14f;

			// Add the Projectile to Scene
			room.AddToScene(projectile, false);

			return projectile;
		}

		public override void RunTick() {

			// Adjust Motion for Shuriken after the gravity ignore phase.
			if(this.State == (byte)CommonState.MotionStart) {
				if(this.gravFrame == Systems.timer.Frame) {
					this.SetState((byte)CommonState.Motion);
					this.physics.SetGravity(FInt.Create(0.4));
				}
			}

			base.RunTick();
		}

		public override void Destroy(DirCardinal dir = DirCardinal.None, GameObject obj = null) {
			base.Destroy();

			EndBounceParticle particle = EndBounceParticle.SetParticle(this.room, Systems.mapper.atlas[(byte)AtlasGroup.Objects], this.SpriteName, new Vector2(this.posX, this.posY), Systems.timer.Frame + 10, 3, 0.5f, 0.12f);

			if(dir == DirCardinal.Right || dir == DirCardinal.Left) {
				particle.vel.Y -= (float)CalcRandom.FloatBetween(0, 3);
				particle.vel.X = dir == DirCardinal.Right ? CalcRandom.FloatBetween(-3, 0) : CalcRandom.FloatBetween(0, 3);
			}

			else if(dir == DirCardinal.Down || dir == DirCardinal.Up) {
				particle.vel.Y = dir == DirCardinal.Down ? CalcRandom.FloatBetween(-4, -2) : CalcRandom.FloatBetween(-1, 1);
				particle.vel.X = (float)this.physics.velocity.X.ToDouble() * 0.35f + CalcRandom.FloatBetween(-1, 1);
			}

			Systems.sounds.shellThud.Play(0.4f, 0, 0);
		}

		// Return Projectile to the Pool
		public override void ReturnToPool() {
			this.room.RemoveFromScene(this);
			ProjectilePool.ShurikenProjectile.ReturnObject(this);
		}
	}
}
