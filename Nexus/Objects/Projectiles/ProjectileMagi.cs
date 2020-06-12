using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System;

namespace Nexus.Objects {

	public enum ProjectileMagiSubType : byte {
		Magi = 1,			// Regenerate
		Magi2 = 2,			// Regenerate Slowly
		Fire = 3,			// Rotates Further (+10), Slightly Faster
		Frost = 4,          // Rotates Much Closer (-20) and Much Slower
		Electric = 5,       // Rotates Much Further (+20), and Much Faster 
		Poison = 6,			// Rotates Slower
		Water = 7,          // Rotates Closer (-10) and Slower
	}

	public class ProjectileMagi : Projectile {

		private GameObject actor;       // The game object that the magi ball is circling around.
		private MagiShield magiShield;	// The magi-shield that created this magi ball.
		private byte radius;			// The radius from the actor this rotates at.
		private short elapsedOffset;    // The elapsed offset / weight that this ball rotates with, comparative to others in the set.

		public bool isAlive = false;
		private short travelDuration;	// How fast (in frames) the ball travels around the diameter.
		private short regenFrames;      // If set above 0, this ball will regenerate after # of frames pass.
		private short regenEnergy;      // The amount of energy stored in the magiball. If less than regenFrames, it's deactivated and is regenerating.
		private float regenAlpha;		// The alpha percent of visibility of the ball, based on its current energy / regeneration status.

		public bool CanDamage { get { return this.regenEnergy >= this.regenFrames; } }

		public ProjectileMagi() : base(null, 0, FVector.Create(0, 0), FVector.Create(0, 0)) { }

		public static ProjectileMagi Create(MagiShield magiShield, GameObject actor, byte subType, byte numberOfBalls, byte ballNumber, byte radius, short regenFrames = 0) {

			// Retrieve an available projectile from the pool.
			ProjectileMagi projectile = ProjectilePool.ProjectileMagi.GetObject();

			projectile.ResetProjectile(actor.room, subType, FVector.Create(0, 0), FVector.Create(0, 0));
			projectile.ResetMagiBall(magiShield, actor, radius, regenFrames);
			projectile.SetCollisionType(ProjectileCollisionType.IgnoreWalls);
			projectile.SetSafelyJumpOnTop(false);
			projectile.SetDamage(DamageStrength.Standard);
			projectile.AssignSubType(subType);
			projectile.AssignBoundsByAtlas(2, 2, -2, -2);
			projectile.SetOffset(numberOfBalls, ballNumber);
			projectile.SetEndLife(Systems.timer.Frame + (60 * 60 * 10)); // Ten minute lifespan.

			// Add the Projectile to Scene
			actor.room.AddToScene(projectile, false);

			return projectile;
		}

		public void ResetMagiBall( MagiShield magiShield, GameObject actor, byte radius = 75, short regenFrames = 0 ) {
			this.magiShield = magiShield;
			this.actor = actor;
			this.ByActorID = actor.id;
			this.regenFrames = regenFrames;
			this.regenEnergy = regenFrames;
			this.regenAlpha = 1;
			this.radius = radius;
			this.isAlive = true;
		}

		public void SetOffset( byte numberOfBalls, byte ballNumber ) {
			this.elapsedOffset = (short) Math.Round((float)ballNumber / (float)numberOfBalls * this.travelDuration);
		}

		public override void RunTick() {

			// Magi-Rotation
			//FInt weight = FInt.Create(((Systems.timer.Frame - this.elapsedOffset) % this.travelDuration) / (float)this.travelDuration);
			//FInt radian = weight * FInt.PI * 2;

			float weight = (Systems.timer.Frame - this.elapsedOffset) % this.travelDuration / (float)this.travelDuration;
			float radian = weight * (float) Math.PI * 2;

			// Set Position of Projectile
			int getX = (int)(this.actor.posX + 8 + (this.radius * Math.Cos(radian)));
			int getY = (int)(this.actor.posY + 14 + (this.radius * Math.Sin(radian)));

			this.physics.MoveToPos(getX, getY);

			// Update Energy
			if(this.regenFrames > 0 && this.regenEnergy <= this.regenFrames) {
				this.regenEnergy++;

				if(this.regenEnergy >= this.regenFrames) {
					if(this.regenAlpha != 1) {
						this.regenAlpha = 1;
						Systems.sounds.pop.Play(0.25f, 0, 0);
					}
				} else {
					this.regenAlpha = (float)((float)regenEnergy / (float)regenFrames * 0.60);
				}
			}

			// Run Physics
			base.RunTick();
		}

		public override void Destroy( DirCardinal dir = DirCardinal.None, GameObject obj = null ) {
			
			// Only destroy the projectile if it's not able to regenerate:
			if(this.regenEnergy == 0) { this.DestroyFinal(); return; }

			// Otherwise, deplete its energy.
			this.regenEnergy = 1;
		}
		
		public void DestroyFinal() {
			this.isAlive = false;
			this.magiShield.CheckShieldEnd();
			base.Destroy();
		}

		private void AssignSubType(byte subType) {

			if(subType == (byte) ProjectileMagiSubType.Magi) {
				this.SetSpriteName("Projectiles/Magi");
				this.travelDuration = (short)(this.radius * 2f);
			}
			
			else if(subType == (byte) ProjectileMagiSubType.Magi2) {
				this.SetSpriteName("Projectiles/Magi2");
				this.travelDuration = (short)(this.radius * 2f);
			}

			// Rotates Further (+10), Slightly Faster
			else if(subType == (byte) ProjectileMagiSubType.Fire) {
				this.SetSpriteName("Projectiles/Fire");
				this.travelDuration = (short)(this.radius * 1.5f);
			}

			// Rotates Much Closer (-20) and Much Slower
			else if(subType == (byte) ProjectileMagiSubType.Frost) {
				this.SetSpriteName("Projectiles/Frost");
				this.travelDuration = (short)(this.radius * 3f);
			}

			// Rotates Much Further (+20), and Much Faster 
			else if(subType == (byte) ProjectileMagiSubType.Electric) {
				this.SetSpriteName("Projectiles/Electric");
				this.travelDuration = (short)(this.radius * 1.2f);
			}

			// Rotates Slower
			else if(subType == (byte) ProjectileMagiSubType.Poison) {
				this.SetSpriteName("Projectiles/Poison");
				this.travelDuration = (short)(this.radius * 2.6f);
			}

			// Rotates Closer (-10) and Slower
			else if(subType == (byte) ProjectileMagiSubType.Water) {
				this.SetSpriteName("Projectiles/Water");
				this.travelDuration = (short)(this.radius * 2.8f);
			}
		}

		public override void Draw(int camX, int camY) {
			this.Meta.Atlas.DrawAdvanced(this.SpriteName, this.posX - camX, this.posY - camY, Color.White * this.regenAlpha, this.rotation);
		}

		// Return Projectile to the Pool
		public override void ReturnToPool() {
			this.room.RemoveFromScene(this);
			ProjectilePool.ProjectileMagi.ReturnObject(this);
		}
	}
}
