using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class Enemy : GameObject {

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
			// If the enemy ignores projectiles, they should pass through without being destroyed.
			// if(enemy.collision.ignores.projectiles) { return false; }

			// Can only be damaged if the projectile was cast by a Character.
			if(projectile.ByCharacterId == 0) { return false; }

			// Special behavior for Magi-Balls. Projectiles faded out don't function.
			if(projectile is ProjectileMagi) {
				ProjectileMagi magi = (ProjectileMagi)projectile;
				if(!magi.CanDamage) { return false; }
			}

			// Check if the enemy is resistant to the projectile. In most cases, destroy the projectile without harming the enemy.
			bool canResist = this.CanResistDamage(projectile.Damage);

			// If the projectile typicallly passes through walls, allow it to pass through indestructable enemies.
			if(canResist && projectile.CollisionType <= ProjectileCollisionType.IgnoreWallsDestroy) {
				return false;
			}

			DirCardinal dir = CollideDetect.GetDirectionOfCollision(projectile, this);

			// Destroy the Projectile (unless it ignores walls)
			if(projectile.CollisionType != ProjectileCollisionType.IgnoreWallsSurvive) { projectile.Destroy(dir); }

			// Wound the Enemy
			if(!canResist) { this.ReceiveWound(); }

			return true;
		}

		public bool CanResistDamage( DamageStrength damage ) {
			return this.ProjectileResist >= damage;
		}

		public virtual bool GetJumpedOn( Character character, sbyte bounceStrength = 0 ) {
			ActionMap.Jump.StartAction(character, bounceStrength, 0, 4, true);
			return this.ReceiveWound();
		}

		public virtual bool ReceiveWound() {
			Systems.sounds.splat1.Play();
			return this.Die(DeathResult.Knockout);
		}

		public virtual bool Die( DeathResult deathResult ) {

			// Delete the Enemy
			if(this.animate is Animate) { this.animate.DisableAnimation(); }
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
