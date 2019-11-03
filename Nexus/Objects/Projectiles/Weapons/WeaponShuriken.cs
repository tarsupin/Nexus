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
			this.physics.SetGravity(FInt.Create(0)); // Switches to 0.4 after MotionStart finished.
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
			projectile.AssignBoundsByAtlas(2, 2, -2, -2);

			// Assign the beginning of the shuriken attack:
			projectile.SetState(ActorState.MotionStart);
			projectile.endFrame = Systems.timer.Frame + 6;

			// Add the Projectile to Scene
			room.AddToScene(projectile, false);

			return projectile;
		}

		public override void Destroy(DirCardinal dir = DirCardinal.Center, GameObject obj = null) {
			if(this.State == ActorState.Death) { return; }

			this.SetState(ActorState.Death);
			this.endFrame = Systems.timer.Frame + 9;

			Physics physics = this.physics;

			if(dir == DirCardinal.Right || dir == DirCardinal.Left) {
				physics.velocity.Y = physics.velocity.Y < 0 ? physics.velocity.Y * FInt.Create(0.25) : physics.velocity.Y;
				physics.velocity.X = dir == DirCardinal.Right ? FInt.Create(-1) : FInt.Create(1);
			}

			else if(dir == DirCardinal.Down || dir == DirCardinal.Up) {
				physics.velocity.X = physics.velocity.X * FInt.Create(0.25);
				physics.velocity.Y = dir == DirCardinal.Down ? FInt.Create(-1) : FInt.Create(1);
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

			// Standard Motion for Shuriken
			if(this.State == ActorState.MotionStart) {
				if(this.endFrame <= Systems.timer.Frame) {
					this.SetState(ActorState.Motion);
					this.physics.SetGravity(FInt.Create(0.4));
				}
			}
			
			// Death Motion for Shuriken
			else if(this.State == ActorState.Death) {
				if(this.endFrame <= Systems.timer.Frame) {
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
