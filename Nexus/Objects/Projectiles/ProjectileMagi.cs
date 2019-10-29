using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System;

namespace Nexus.Objects {

	public enum ProjectileMagiSubType : byte {
		Magi,
		Magi2,
	}

	public class ProjectileMagi : Projectile {

		private DynamicGameObject actor;    // The game object that the magi ball is circling around.
		private byte radius;                // The radius from the actor this rotates at.
		private FInt elapsedOffset;         // The elapsed offset / weight that this ball rotates with, comparative to others in the set.
		private FInt sustained;             // If set above 0, this ball doesn't get destroyed on contact (e.g. White Wizard). It recharges from 0 to 1 (also transparency).

		public ProjectileMagi(LevelScene scene, byte subType, DynamicGameObject actor, byte numberOfBalls, byte ballNumber) : base(scene, subType, FVector.Create(0, 0), FVector.Create(0, 0)) {
			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(2, 2, -2, -2);
			this.Reset(actor);

			// TODO COLLIDES VS TILES AND STATIC??
			// this.collision.ignores.static = true;
			this.CollisionType = ProjectileCollisionType.IgnoreWalls;
			this.SafelyJumpOnTop = true;
			this.Damage = DamageStrength.Trivial;

			this.SetOffset(numberOfBalls, ballNumber);
		}

		public void Reset( DynamicGameObject actor, byte radius = 75, bool isSustained = false ) {
			this.actor = actor;
			this.sustained = isSustained ? FInt.Create(1) : FInt.Create(0);
			this.radius = radius;
		}

		public void SetOffset( byte numberOfBalls, byte ballNumber ) {
			this.elapsedOffset = FInt.Create((ballNumber / numberOfBalls) * this.radius * 2); // this.radius * 2 = cycleDuration
		}

		public override void RunTick() {

			// Magi-Rotation
			int cycleDuration = this.radius * 2;
			FInt weight = FInt.Create(((Systems.timer.Frame - this.elapsedOffset.IntValue) % cycleDuration) / cycleDuration);
			FInt radian = weight * FInt.PI * 2;

			// Set Position of Projectile
			FInt getX = this.actor.posX + 8 + (radius * FInt.Cos(radian));
			FInt getY = this.actor.posY + 14 + (radius * FInt.Sin(radian));

			this.posX = getX.IntValue;
			this.posY = getY.IntValue;

			// Update Energy
			if(this.sustained > 0 && this.sustained < 1) {
				if(this.sustained > FInt.Create(0.6)) {
					this.sustained = FInt.Create(1);
				} else {
					this.sustained += FInt.Create(0.002);
				}
			}

			// Run Physics
			base.RunTick();
		}

		public void Destroy( bool force = false ) {
			
			// Only destroy the projectile if it's not being sustained (or if it's a forced destroy):
			if(this.sustained == FInt.Create(0) || force) { base.Destroy(); }

			// Otherwise, if it's a sustained projectile, set it just a sliver above 0 so that it regenerates.
			else {
				this.sustained = FInt.Create(0.02);
			}
		}

		private void AssignSubType(byte subType) {
			if(subType == (byte) ProjectileMagiSubType.Magi) {
				this.SetSpriteName("Projectiles/Magi");
			} else if(subType == (byte) ProjectileMagiSubType.Magi2) {
				this.SetSpriteName("Projectiles/Magi2");
			}
		}
	}
}
