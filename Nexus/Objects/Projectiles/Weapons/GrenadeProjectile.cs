using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class GrenadeProjectile : Projectile {

		private uint endFrame;          // The frame that a movement style ends on.

		private GrenadeProjectile(RoomScene room, byte subType, FVector pos, FVector velocity) : base(room, subType, pos, velocity) {
			this.Damage = DamageStrength.None;
			this.CollisionType = ProjectileCollisionType.DestroyOnCollide;
			this.physics.SetGravity(FInt.Create(0.4));
		}

		public static GrenadeProjectile Create(RoomScene room, byte subType, FVector pos, FVector velocity) {
			GrenadeProjectile projectile;

			// Retrieve a Projectile Ball from the ObjectPool, if one is available:
			if(ProjectilePool.WeaponGrenade.Count > 0) {
				projectile = ProjectilePool.WeaponGrenade.Pop();
				projectile.ResetProjectile(subType, pos, velocity);
			}

			// Create a New Projectile Ball
			else {
				projectile = new GrenadeProjectile(room, subType, pos, velocity);
			}

			projectile.SetSpriteName("Projectiles/Grenade");
			projectile.AssignBoundsByAtlas(2, 2, -2, -2);
			projectile.spinRate = projectile.physics.velocity.X > 0 ? 0.07f : -0.07f;

			// Add the Projectile to Scene
			room.AddToScene(projectile, false);

			return projectile;
		}

		public override void Draw(int camX, int camY) {
			this.Meta.Atlas.DrawRotation(this.SpriteName, this.posX + 16 - camX, this.posY + 16 - camY, this.rotation, new Vector2(16, 16));
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

			Systems.sounds.shellThud.Play(0.4f, 0, 0);
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
				if(this.endFrame == Systems.timer.Frame) {
					this.ReturnToPool();
				}
			}

			// Standard Physics
			this.physics.RunPhysicsTick();
		}
	}
}
