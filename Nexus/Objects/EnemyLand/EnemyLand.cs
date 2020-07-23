using Nexus.Config;
using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class EnemyLand : Enemy {

		protected FInt speed;

		public EnemyLand(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {

		}

		public override void RunTick() {
			if(this.behavior is Behavior) { this.behavior.RunTick(); }

			// Standard Physics
			this.physics.RunPhysicsTick();

			// Animations, if applicable.
			if(this.animate is Animate) {
				this.animate.RunAnimationTick(Systems.timer);
			}

			// Check if the enemy is about to run off of a cliff:
			if(Systems.timer.IsTickFrame && this.physics.touch.toFloor) {
				if(this.FaceRight) {
					if(!CollideTile.IsBlockingCoord(this.room.tilemap, this.posX + this.bounds.Right + 4, this.posY + this.bounds.Bottom + 10, DirCardinal.Down)) {
						this.SetDirection(false);
					}
				} else {
					if(!CollideTile.IsBlockingCoord(this.room.tilemap, this.posX + this.bounds.Left - 4, this.posY + this.bounds.Bottom + 10, DirCardinal.Down)) {
						this.SetDirection(true);
					}
				}
			}
		}

		public override bool CollideObjLeft(GameObject obj) {
			if(!base.CollideObjLeft(obj)) { return false; }
			this.WalkRight();
			return true;
		}

		public override bool CollideObjRight(GameObject obj) {
			if(!base.CollideObjRight(obj)) { return false; }
			this.WalkLeft();
			return true;
		}
		
		public override void CollidePosLeft(int posX) {
			base.CollidePosLeft(posX);
			this.WalkRight();
		}

		public override void CollidePosRight(int posX) {
			base.CollidePosRight(posX);
			this.WalkLeft();
		}

		public void WalkLeft() {
			if(!this.physics.touch.toFloor) { return; }
			this.SetDirection(false);
		}

		public void WalkRight() {
			if(!this.physics.touch.toFloor) { return; }
			this.SetDirection(true);
		}
	}
}
