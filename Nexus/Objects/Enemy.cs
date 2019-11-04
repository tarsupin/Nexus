﻿using Newtonsoft.Json.Linq;
using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class Enemy : DynamicGameObject {

		// Enemy Stats
		public DamageStrength ProjectileResist { get; protected set; }

		// Enemy Status
		public Behavior behavior;
		public EnemyStatus status;

		public Enemy(RoomScene room, byte subType, FVector pos, JObject paramList) : base(room, subType, pos, paramList) {
			this.status = new EnemyStatus();
		}

		public override void RunTick() {

			// Actions and Behaviors
			if(this.status.action is ActionEnemy) {
				this.status.action.RunAction(this);
			} else if(this.behavior is Behavior) {
				this.behavior.RunTick();
			}

			base.RunTick();
		}

		public virtual bool RunCharacterImpact( Character character ) {
			DirCardinal dir = CollideDetect.GetDirectionOfCollision(character, this);

			if(dir == DirCardinal.Down) {
				this.GetJumpedOn(character);
			} else {
				this.WoundCharacter(character);
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

		public virtual void WoundCharacter( Character character, DamageStrength damage = DamageStrength.Standard ) {
			if(this.status.action is DeathEnemyAction) { return; }
			character.wounds.ReceiveWoundDamage(damage);
		}

		public virtual bool GetJumpedOn( Character character, sbyte bounceStrength = 0 ) {
			if(this.status.action is DeathEnemyAction) { return false; }
			character.BounceUp( this, bounceStrength );
			return this.ReceiveWound();
		}

		public virtual bool ReceiveWound() {
			Systems.sounds.splat1.Play();
			return this.Die(DeathResult.Knockout);
		}

		public virtual bool Die( DeathResult deathType ) {
			ActionMap.DeathEnemy.StartAction(this, deathType);
			if(this.animate is Animate) { this.animate = null; }
			return true;
		}

		// Land Enemies typically die to TNT
		public virtual bool DamageByTNT() {
			return this.Die(DeathResult.Knockout);
		}

	}
}
