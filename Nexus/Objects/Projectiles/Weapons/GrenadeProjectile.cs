using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class GrenadeProjectile : Projectile {

		private int endFrame;          // The frame that a movement style ends on.

		public GrenadeProjectile() : base(null, 0, FVector.Create(0, 0), FVector.Create(0, 0)) {
			this.Damage = DamageStrength.None;
			this.CollisionType = ProjectileCollisionType.DestroyOnCollide;
			this.physics.SetGravity(FInt.Create(0.4));
			this.SetSpriteName("Projectiles/Grenade");
		}

		public static GrenadeProjectile Create(RoomScene room, byte subType, FVector pos, FVector velocity) {

			// Retrieve an available projectile from the pool.
			GrenadeProjectile projectile = ProjectilePool.GrenadeProjectile.GetObject();

			projectile.ResetProjectile(room, subType, pos, velocity);
			projectile.SetState((byte)CommonState.Move);
			projectile.AssignBoundsByAtlas(2, 2, -2, -2);
			projectile.spinRate = projectile.physics.velocity.X > 0 ? 0.07f : -0.07f;

			// Add the Projectile to Scene
			room.AddToScene(projectile, false);

			return projectile;
		}

		public override void Destroy(DirCardinal dir = DirCardinal.None, GameObject obj = null) {
			if(this.State == (byte)CommonState.Death) { return; }

			this.SetState((byte)CommonState.Death);
			this.physics.SetGravity(FInt.Create(0.7));
			this.endFrame = Systems.timer.Frame + 12;

			Physics physics = this.physics;

			if(dir == DirCardinal.Right || dir == DirCardinal.Left) {
				physics.velocity.Y = physics.velocity.Y < 0 ? physics.velocity.Y * FInt.Create(0.25) : FInt.Create(-3);
				physics.velocity.X = dir == DirCardinal.Right ? FInt.Create(-1) : FInt.Create(1);
			} else if(dir == DirCardinal.Down || dir == DirCardinal.Up) {
				physics.velocity.X = physics.velocity.X * FInt.Create(0.25);
				physics.velocity.Y = dir == DirCardinal.Down ? FInt.Create(-4) : FInt.Create(1);
			} else {
				physics.velocity.X = FInt.Create(0);
				physics.velocity.Y = FInt.Create(-3);
			}

			this.room.PlaySound(Systems.sounds.shellThud, 0.4f, this.posX + 16, this.posY + 16);
		}

		public override void RunTick() {

			this.rotation += this.spinRate;

			// Standard Motion for Shuriken
			if(this.State == (byte)CommonState.MotionStart) {
				if(this.endFrame == Systems.timer.Frame) {
					this.SetState((byte)CommonState.Motion);
					this.physics.SetGravity(FInt.Create(0.4));
				}
			}

			// Death Motion for Shuriken
			else if(this.State == (byte)CommonState.Death) {
				this.rotation += this.spinRate > 0 ? -0.08f : 0.08f;

				// Explosion
				if(this.endFrame == Systems.timer.Frame) {
					Bomb.Detonate(this.room, this.posX + 16, this.posY + 16);
					this.ReturnToPool();
				}
			}

			// Standard Physics
			this.physics.RunPhysicsTick();
		}

		// Return Projectile to the Pool
		public override void ReturnToPool() {
			this.room.RemoveFromScene(this);
			ProjectilePool.GrenadeProjectile.ReturnObject(this);
		}
	}
}
