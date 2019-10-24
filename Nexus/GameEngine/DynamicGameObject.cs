using Newtonsoft.Json.Linq;
using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System;

namespace Nexus.GameEngine {

	public enum Activity : byte {
		Inactive = 0,               // The object is inactive, not visible, etc. No reason to run updates.
		NoCollide = 1,				// The object requires updates, but don't run collision.
		Active = 2,                 // The object is currently active; usually visible to a player (multiplayer-ready). Needs to run updates.
		ForceActive = 3,            // The object is forced to be active through the whole level. Always run updates.
	}

	public enum ActorState : byte {
		RestStandard,				// No special state for the actor. Just resting / inactive.
		RestSpecial,				// Special resting.
		RestStall,					// Waiting or stalling.
		RestStunned,				// Stunned (collided with wall, etc).
		MoveStandard,				// Standard movement for the actor.
		MoveSustain,				// Sustained movement, such as after a jump.
		MoveAir,					// Moving through the air.
		MoveLand,					// Landing, such as after a jump.
		MoveSpecial,				// Special movement.
		BehaviorStandard,			// Performing its standard behavior.
		BehaviorSpecial,			// Performing a variant of its behavior.
		ActionStandard,				// Performing a standard action.
		ActionSpecial,				// Performing a variant or special action.
		ReactionCharacter,			// Reacting to a Character.
		ReactionEnvironment,		// Reacting to the environment.
		ReactionStall,				// Stall that occurs after a reaction (e.g. brief pause, charging up).
		ReactingSpecial,			// Special Reaction.
	}

	public class DynamicGameObject : GameObject {

		public Activity Activity { get; protected set; }

		// TODO: ActionTrait, BehaviorTrait
		// TODO: Status (can have multiple status types); some don't need
		// TODO: -- last character touch direction; what relelvant for?
		// TODO: TrackInstructions (rules for dealing with tracks; not everything needs this, but... ???)

		//status?: any;
		//action: ActionTrait;			// Current Action
		//behavior: BehaviorTrait;

		// Components
		public Physics physics;
		public Animate animate;

		// State Changes (also see SetState() and OnStateChange() methods)
		public ActorState State { get; protected set; }    // Tracks the actor's current state.
		public bool FaceRight { get; protected set; }      // TRUE if the actor is facing right.

		public DynamicGameObject(LevelScene scene, byte subType, FVector pos, JObject paramList = null) : base(scene, subType, pos, paramList) {}

		public virtual void RunTick() {

			// Activity
			// TODO HIGH PRIORITY: End Tick if the activity isn't present.
			// if(this.activity == (byte) Activity.Inactive) { return; }

			// Actions and Behaviors
			//if(this.action is Action) {
			//	this.action.RunAction(this);
			//} else if(this.behavior is Behavior) {
			//	this.behavior.RunTick();
			//}

			// Standard Physics
			this.physics.RunTick();
			
			// Animations, if applicable.
			if(this.animate is Animate) {
				this.animate.RunAnimationTick(Systems.timer);
			}
		}

		// Run this method to change an actor's facing direction.
		public void SetActivity( Activity activity ) {
			if(this.Activity != Activity.ForceActive) {
				this.Activity = activity;
			}
		}
		
		// Run this method to change an actor's facing direction.
		public void SetDirection( bool faceRight ) {
			if(this.FaceRight != faceRight) {
				this.FaceRight = faceRight;
				this.OnDirectionChange();
			}
		}

		// Run this method to change an actor's state.
		public void SetState( ActorState state ) {
			if(this.State != state) {
				this.State = state;
				this.OnStateChange();
			}
		}

		// Run this method when the actor's direction has changed.
		public virtual void OnDirectionChange() {}

		// Run this method when the actor's .state has changed. This may affect sprites, animations, or other custom behaviors.
		public virtual void OnStateChange() {}

		public void SetSpriteName(string spriteName, bool isAnimation = false) {
			this.SpriteName = spriteName;
			if(!isAnimation && this.animate is Animate) { this.animate.DisableAnimation(); }
		}

		// Run Standard Impact
		// TODO HIGH PRIORITY: IMPLEMENT IMPACTS LIKE THIS
		// TODO HIGH PRIORITY: IMPLEMENT IMPACTS LIKE THIS
		// TODO HIGH PRIORITY: IMPLEMENT IMPACTS LIKE THIS
		// TODO HIGH PRIORITY: IMPLEMENT IMPACTS LIKE THIS
		public virtual bool RunImpact(DynamicGameObject actor, DirCardinal dir) {
			return true;
		}

		// Destroys the instance of this object.
		public virtual void Destroy() {
			this.scene.DestroyObject(this);
		}

		// Disables the instance of this object, returning it to a pool rather than destroying it altogether.
		public void Disable() {
			// TODO HIGH PRIORITY: How to disable? Better methods? Just return to pool somehow.
			// TODO HIGH PRIORITY: How to disable? Better methods? Just return to pool somehow.
			
		}

		public void RenderKnockoutRotation(int camX, int camY, TimerGlobal time) {
			//const rotateVal = Calc.lerpNumber(0, 6.283, (this.physics.velocity.x > 0 ? 1 : -1) * (time.elapsed % this.rotSpeed) / this.rotSpeed);
			//this.pixi.rotateTo( this.img, rotateVal );
			//this.img.position.set( this.pos.x - camX, this.pos.y - camY );
			//this.pixi.draw( this.img );
		}

		// TODO: Does this get used? Character.BounceUp() does.
		public virtual void BounceUp(GameObject obj, sbyte strengthMod = 4, byte maxX = 4, sbyte relativeMult = 3) {

			// Some dynamic archetypes shouldn't bounce.
			if(this.Meta.Archetype == Arch.Platform) { return; }

			this.physics.velocity.Y = FInt.Create(-strengthMod);
			short xDiff = CollideDetect.GetRelativeX(this, obj);

			if(xDiff < 0) {
				this.physics.velocity.X = FInt.Create(Math.Min(maxX, Math.Abs(xDiff / relativeMult)));
			}
			
			else {
				this.physics.velocity.X = FInt.Create(-Math.Min(maxX, xDiff / relativeMult));
			}
		}
	}
}
