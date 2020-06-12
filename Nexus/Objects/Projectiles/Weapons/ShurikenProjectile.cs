﻿using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using static Nexus.ObjectComponents.Shuriken;

namespace Nexus.Objects {

	public class ShurikenProjectile : Projectile {

		private uint endFrame;          // The frame that a movement style ends on.

		public ShurikenProjectile() : base(null, 0, FVector.Create(0, 0), FVector.Create(0, 0)) { }

		public static ShurikenProjectile Create(RoomScene room, byte subType, FVector pos, FVector velocity) {

			// Retrieve an available projectile from the pool.
			ShurikenProjectile projectile = ProjectilePool.ShurikenProjectile.GetObject();

			projectile.ResetProjectile(room, subType, pos, velocity);
			projectile.Damage = DamageStrength.Standard;
			projectile.CollisionType = ProjectileCollisionType.DestroyOnCollide;

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
			projectile.endFrame = Systems.timer.Frame + 8;
			projectile.rotation = 2;
			projectile.spinRate = projectile.physics.velocity.X > 0 ? 0.14f : -0.14f;

			// Add the Projectile to Scene
			room.AddToScene(projectile, false);

			return projectile;
		}

		public override void Draw(int camX, int camY) {
			this.Meta.Atlas.DrawRotation(this.SpriteName, this.posX + 16 - camX, this.posY + 16 - camY, this.rotation, new Vector2(16, 16));
		}

		public override void Destroy(DirCardinal dir = DirCardinal.None, GameObject obj = null) {
			if(this.State == (byte) CommonState.Death) { return; }

			this.SetState((byte) CommonState.Death);
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

			this.rotation += this.spinRate;

			// Standard Motion for Shuriken
			if(this.State == (byte) CommonState.MotionStart) {
				if(this.endFrame == Systems.timer.Frame) {
					this.SetState((byte) CommonState.Motion);
					this.physics.SetGravity(FInt.Create(0.4));
				}
			}
			
			// Death Motion for Shuriken
			else if(this.State == (byte) CommonState.Death) {
				this.rotation += this.spinRate > 0 ? -0.08f : 0.08f;
				if(this.endFrame == Systems.timer.Frame) {
					this.ReturnToPool();
				}
			}

			// Standard Physics
			this.physics.RunPhysicsTick();
		}

		// Return Projectile to the Pool
		public override void ReturnToPool() {
			this.room.RemoveFromScene(this);
			ProjectilePool.ShurikenProjectile.ReturnObject(this);
		}
	}
}
