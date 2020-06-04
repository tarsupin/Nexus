using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class Enemy : DynamicObject {

		public DamageStrength ProjectileResist { get; protected set; }
		public Behavior behavior;

		public Enemy(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			
		}

		public override void RunTick() {
			if(this.behavior is Behavior) { this.behavior.RunTick(); }
			base.RunTick();
		}

		public virtual bool RunCharacterImpact( Character character ) {
			DirCardinal dir = CollideDetect.GetDirectionOfCollision(character, this);

			if(dir == DirCardinal.Down) {
				this.GetJumpedOn(character);
			} else {
				character.wounds.ReceiveWoundDamage(DamageStrength.Standard);
			}

			return Impact.StandardImpact(character, this, dir);
		}

		public virtual bool RunProjectileImpact(Projectile projectile) {

			// TODO:
			// Must be a projectile created by a character.
			// if(!projectile.ignoreCharacter) { return false; }

			// TODO:
			// If the enemy ignores projectiles, they should pass through without being destroyed.
			// if(enemy.collision.ignores.projectiles) { return false; }

			// TODO: 
			// Special behavior for Magi-Balls. Projectiles faded out don't function.
			// if(projectile instanceof ProjectileMagi && projectile.sustained > 0) {
			// if(projectile.sustained < 1) { return false; }
			// }

			// If the enemy is resistant to the projectile, destroy the projectile.
			if(this.CanResistDamage(projectile.Damage)) {

				// If the projectile typicallly passes through walls, allow it to pass through indestructable enemies.
				return projectile.CollisionType != ProjectileCollisionType.IgnoreWalls;
			}

			DirCardinal dir = CollideDetect.GetDirectionOfCollision(projectile, this);

			// Destroy the Projectile
			projectile.Destroy(dir);

			// Wound the Enemy
			this.ReceiveWound();

			return true;
		}

		public bool CanResistDamage( DamageStrength damage ) {
			return this.ProjectileResist >= damage;
		}

		public virtual bool GetJumpedOn( Character character, sbyte bounceStrength = 0 ) {
			character.BounceUp( this, bounceStrength );
			return this.ReceiveWound();
		}

		public virtual bool ReceiveWound() {
			Systems.sounds.splat1.Play();
			return this.Die(DeathResult.Knockout);
		}

		public virtual bool Die( DeathResult deathResult ) {

			// Delete the Enemy
			if(this.animate is Animate) { this.animate = null; }
			this.Destroy();

			// Replace with Particle

			// Knockout
			if(deathResult == DeathResult.Knockout) {
				DeathEmitter.Knockout(this.room, this.SpriteName, this.posX, this.posY);
			}

			// Standard Squish
			else if(deathResult == DeathResult.Squish) {
				//status.actionEnds = Systems.timer.Frame + 25;

				// Lock the enemy in position. Since it's .activity is now set to NoCollide, it won't change its position.
				physics.SetGravity(FInt.Create(0));
				physics.velocity.X = FInt.Create(0);
				physics.velocity.Y = FInt.Create(0);
			}

			// Disappear - No Additional Behaviors Needed
			//else if(deathResult == DeathResult.Disappear) {}

			return true;
		}

		// Land Enemies typically die to TNT
		public virtual bool DamageByTNT() {
			return this.Die(DeathResult.Knockout);
		}
	}
}
