using Nexus.Engine;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class ProjectileGameObject : GameObject {

		public ProjectileGameObject(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList = null) : base(room, subType, pos, paramList) {

		}

		public void RunTick() {
			//if(this.action is Action) { this.action.RunTick(); } else if(this.behavior is Behavior) { this.behavior.RunTick(); }
			//if(this.physics.RunTickCustom) { this.physics.RunTickCustom(); } else { this.physics.RunTick() }; }
		}

		public void ReturnToPool() {
			//this.pos.x = -5000;
			//this.pos.y = -5000;
			//delete this.room.activeObjects[this.loadOrder][this.id];
		}

		public void RenderBallRotation( int camX, int camY, TimerGlobal time ) {
			//const rotateVal = Calc.lerpNumber(0, 6.283, (this.physics.velocity.x > 0 ? 1 : -1) * (time.elapsed % 1000) / 1000);
			//this.pixi.rotateTo( this.img, rotateVal );
			//this.img.position.set( this.pos.x - camX, this.pos.y - camY );
			//this.pixi.draw( this.img );
		}
	}
}
