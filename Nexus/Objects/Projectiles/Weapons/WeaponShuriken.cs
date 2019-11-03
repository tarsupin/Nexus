using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public enum WeaponShurikenSubType : byte {
		Shuriken
	}

	public class WeaponShuriken : Projectile {

		private uint endFrame;			// The frame that a movement style ends on.

		private WeaponShuriken(RoomScene room, byte subType, FVector pos, FVector velocity) : base(room, subType, pos, velocity) {
			this.Damage = DamageStrength.Standard;
			this.CollisionType = ProjectileCollisionType.DestroyOnCollide;
		}

		public static WeaponShuriken Create(RoomScene room, byte subType, FVector pos, FVector velocity) {
			WeaponShuriken projectile;

			// Retrieve a Projectile Ball from the ObjectPool, if one is available:
			if(ObjectPool.WeaponShuriken.Count > 0) {
				projectile = ObjectPool.WeaponShuriken.Pop();
				projectile.ResetProjectile(subType, pos, velocity);
				projectile.Damage = DamageStrength.Standard;    // Needs to be reset, because death action sets to trivial.
			}

			// Create a New Projectile Ball
			else {
				projectile = new WeaponShuriken(room, subType, pos, velocity);
			}

			projectile.AssignSubType(subType);
			projectile.AssignBoundsByAtlas(4, 4, -4, -4);

			// Assign the beginning of the shuriken attack:
			projectile.physics.SetGravity(FInt.Create(0)); // Switches to 0.4 after MotionStart finished.
			projectile.SetState(ActorState.MotionStart);
			projectile.endFrame = Systems.timer.Frame + 8;
			projectile.rotation = 2;

			// Add the Projectile to Scene
			room.AddToScene(projectile, false);

			return projectile;
		}

		public override void Draw(int camX, int camY) {
			this.Meta.Atlas.DrawRotation(this.SpriteName, this.posX + 16 - camX, this.posY + 16 - camY, this.rotation, new Vector2(16, 16));
		}

		public override void Destroy(DirCardinal dir = DirCardinal.Center, GameObject obj = null) {
			if(this.State == ActorState.Death) { return; }

			this.SetState(ActorState.Death);
			this.physics.SetGravity(FInt.Create(0.7));
			this.endFrame = Systems.timer.Frame + 12;

			Physics physics = this.physics;

			if(dir == DirCardinal.Right || dir == DirCardinal.Left) {
				physics.velocity.Y = physics.velocity.Y < 0 ? physics.velocity.Y * FInt.Create(0.25) : physics.velocity.Y;
				physics.velocity.X = dir == DirCardinal.Right ? FInt.Create(-1) : FInt.Create(1);
			}

			else if(dir == DirCardinal.Down || dir == DirCardinal.Up) {
				physics.velocity.X = physics.velocity.X * FInt.Create(0.25);
				physics.velocity.Y = dir == DirCardinal.Down ? FInt.Create(-4) : FInt.Create(1);
			}

			else {
				physics.velocity.X = FInt.Create(0);
				physics.velocity.Y = FInt.Create(0);
			}

			Systems.sounds.shellThud.Play(0.4f, 0, 0);
			this.Damage = DamageStrength.Trivial;
		}

		public override void RunTick() {

			// Activity
			// TODO HIGH PRIORITY: End Tick if the activity isn't present.
			// if(this.activity == (byte) Activity.Inactive) { return; }

			this.rotation += 0.14f;

			// Standard Motion for Shuriken
			if(this.State == ActorState.MotionStart) {
				if(this.endFrame == Systems.timer.Frame) {
					this.SetState(ActorState.Motion);
					this.physics.SetGravity(FInt.Create(0.4));
				}
			}
			
			// Death Motion for Shuriken
			else if(this.State == ActorState.Death) {
				this.rotation -= 0.08f;
				if(this.endFrame == Systems.timer.Frame) {
					this.Disable();
				}
			}

			// Standard Physics
			this.physics.RunTick();
		}

		private void AssignSubType(byte subType) {
			if(subType == (byte) WeaponShurikenSubType.Shuriken) {
				this.SetSpriteName("Weapon/Shuriken");
			}
		}
	}
}
