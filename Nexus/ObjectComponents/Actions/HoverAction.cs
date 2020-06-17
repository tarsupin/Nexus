using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// status.actionBool1 (allowVertical)		:: TRUE if you want to allow vertical movement.

	public class HoverAction : Action {

		public HoverAction() : base() {
			this.endsOnLanding = true;
			this.duration = 60;
		}

		public void StartAction( Character character, bool allowVertical = false ) {
			CharacterStatus status = character.status;

			// Don't start hovering if there is already a hover action in place.
			if(status.action is HoverAction && !this.HasTimeElapsed(character)) { return; }

			this.EndLastActionIfActive(character);

			status.action = ActionMap.Hover;
			status.actionEnds = Systems.timer.Frame + this.duration;
			status.actionBool1 = allowVertical;

			character.physics.SetGravity((FInt) 0);
		}

		public override void RunAction( Character character ) {

			// End the action after the designated number of frames has elapsed:
			if(this.HasTimeElapsed(character)) {
				this.EndAction(character);
				return;
			}

			PlayerInput input = character.input;
			Physics physics = character.physics;

			// Horizontal Movement
			byte hoverSpeed = input.isDown(IKey.XButton) ? (byte) 5 : (byte) 3;

			// Horizontal Levitation Movement/Speed
			if(input.isDown(IKey.Right)) {
				character.SetDirection(true);
				physics.velocity.X += hoverSpeed * FInt.Create(0.15); // HoverSpeed * 0.15
				if(physics.velocity.X > hoverSpeed) { physics.velocity.X = FInt.Create(hoverSpeed); }
			}
			
			else if(input.isDown(IKey.Left)) {
				character.SetDirection(false);
				physics.velocity.X -= hoverSpeed * FInt.Create(0.15); // HoverSpeed * 0.15
				if(physics.velocity.X < -hoverSpeed) { physics.velocity.X = FInt.Create(-hoverSpeed); }
			}

			// Horizontal Deceleration
			else {
				physics.velocity.X = this.FlightDeceleration(physics.velocity.X, hoverSpeed * FInt.Create(0.1));
			}

			// Vertical Levitation
			if(input.isDown(IKey.Down) && character.status.actionBool1) {
				physics.velocity.Y += hoverSpeed * FInt.Create(0.15); // HoverSpeed * 0.15
				if(physics.velocity.Y > hoverSpeed) { physics.velocity.Y = FInt.Create(hoverSpeed / 1.5); }
			}
				
			else if(input.isDown(IKey.Up) && character.status.actionBool1) {
				physics.velocity.Y -= hoverSpeed * FInt.Create(0.15); // HoverSpeed * 0.15
				if(physics.velocity.Y < -hoverSpeed) { physics.velocity.Y = FInt.Create(-hoverSpeed); }
			}

			// Vertical Deceleration
			else {
				// Vertical Deceleration is faster than Horizontal.
				physics.velocity.Y = this.FlightDeceleration(physics.velocity.Y, hoverSpeed * FInt.Create(0.2));
			}
		}

		public FInt FlightDeceleration( FInt vel, FInt deceleration ) {

			if(vel < 0) {
				if(vel + deceleration < 0) { return vel + deceleration; }
				return FInt.Create(0);
			}

			if(vel - deceleration > 0) { return vel - deceleration; }
			return FInt.Create(0);
		}

		public override void EndAction(Character character) {
			base.EndAction(character);
			character.physics.SetGravity(character.stats.BaseGravity);
		}
	}
}
