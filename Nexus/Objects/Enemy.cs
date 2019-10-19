﻿using Nexus.Engine;
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

		public Enemy(LevelScene scene, byte subType, FVector pos, object[] paramList) : base(scene, subType, pos, paramList) {
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

		public bool CanResistDamage( DamageStrength damage ) {
			return this.ProjectileResist >= damage;
		}

		// TODO: Set to "Character" class, not DynamicGameObject
		public void WoundCharacter( DynamicGameObject character ) {
			// if action is Deathaction, return
			// character.ReceiveWound();
		}

		public bool GetJumpedOn( Character character, byte bounceStrength = 0 ) {
			if(this.status.action is DeathEnemyAction) { return false; }
			character.BounceUp( this, bounceStrength );
			return this.ReceiveWound();
		}

		public bool ReceiveWound() {
			// TODO: Play Sound (splat1)
			return this.Die(DeathResult.Knockout);
		}

		public bool Die( DeathResult deathType ) {
			ActionMap.DeathEnemy.StartAction(this, deathType);
			if(this.animate is Animate) { this.animate = null; }
			return true;
		}

		// Land Enemies typically die to TNT
		public bool DamageByTNT() {
			return this.Die(DeathResult.Knockout);
		}

	}
}
