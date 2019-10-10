using Nexus.Engine;
using Nexus.ObjectComponents;

namespace Nexus.GameEngine {

	public enum Activity {
		Inactive = 0,               // The object is inactive, not visible, etc. No reason to run updates.
		Seen = 1,                   // The object is currently visible to a player (multiplayer-ready). Needs to run updates.
		ForceActive = 2,            // The object is forced to be active through the whole level. Always run updates.
	}

	public class DynamicGameObject : GameObject {

		public Activity activity;

		// TODO: ActionTrait, BehaviorTrait
		// TODO: Status (can have multiple status types); some don't need
		// TODO: -- last character touch direction; what relelvant for?
		// TODO: TrackInstructions (rules for dealing with tracks; not everything needs this, but... ???)

		//status?: any;
		//action: ActionTrait;			// Current Action
		//behavior: BehaviorTrait;

		// Physics Components
		public Physics physics;
		public Collision collision;

		public DynamicGameObject(LevelScene scene, byte subType, FVector pos, object[] paramList = null) : base(scene, subType, pos, paramList) {

		}

		public virtual void RunTick() {
			//if(this.action is Action) { this.action.RunTick(); } else if(this.behavior is Behavior) { this.behavior.RunTick(); }

			//if(this.RunTickCustom) { this.RunTickCustom(); } else { this.physics.RunTick() }; }
			this.physics.RunTick();
		}

		// Destroys the instance of this object.
		public void Destroy() {

		}

		public void RenderKnockoutRotation(int camX, int camY, TimerGlobal time) {
			//const rotateVal = Calc.lerpNumber(0, 6.283, (this.physics.velocity.x > 0 ? 1 : -1) * (time.elapsed % this.rotSpeed) / this.rotSpeed);
			//this.pixi.rotateTo( this.img, rotateVal );
			//this.img.position.set( this.pos.x - camX, this.pos.y - camY );
			//this.pixi.draw( this.img );
		}

		public void BounceUp(GameObject actor, byte strengthMod = 4, byte maxX = 4, byte relativeMult = 3) {
			
			//// Some dynamic archetypes shouldn't bounce.
			//if(this.archetype === Arch.Platform) { return; }
		
			//this.physics.velocity.y = -strengthMod;
			//const xDiff = actor.collision.getRelativeX(this);
		
			//if(xDiff< 0) { this.physics.velocity.x = Math.min(maxX, Math.abs(xDiff / relativeMult)); }
			//else { this.physics.velocity.x = -Math.min(maxX, xDiff / relativeMult); }
		}
	}
}
