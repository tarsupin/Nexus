using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// status.actionBool1 (horizontalOnly)		:: TRUE if only moving horizontally.

	public class HoverAction : ActionCharacter {

		public HoverAction() : base() {
			this.endsOnLanding = true;
			this.duration = 60;
		}

		public void StartAction( Character character, bool horizontalOnly = false ) {
			CharacterStatus status = character.status;

			// Don't start hovering if there is already a hover action in place.
			if(status.action is HoverAction && !this.HasTimeElapsed(character)) { return; }

			status.action = ActionMap.Hover;
			status.actionEnds = character.scene.timer.frame + this.duration;
			status.actionBool1 = horizontalOnly;

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
				character.faceRight = true;
				physics.velocity.X += (hoverSpeed * FInt.Create(0.15)); // HoverSpeed * 0.15
				if(physics.velocity.X > hoverSpeed) { physics.velocity.X = FInt.Create(hoverSpeed); }

			} else if(input.isDown(IKey.Left)) {
				character.faceRight = false;
				physics.velocity.X -= (hoverSpeed * FInt.Create(0.15)); // HoverSpeed * 0.15
				if(physics.velocity.X > hoverSpeed) { physics.velocity.X = FInt.Create(hoverSpeed); }
			}

			// Horizontal Deceleration
			else {
				physics.velocity.X = this.FlightDeceleration(physics.velocity.X, hoverSpeed * FInt.Create(0.1));
			}

			// Vertical Levitation
			if(input.isDown(IKey.Up)) {
				physics.velocity.Y += (hoverSpeed / FInt.Create(1.5) * FInt.Create(0.15)); // HoverSpeed / 1.5 * 0.15
				if(physics.velocity.Y > hoverSpeed) { physics.velocity.Y = FInt.Create(hoverSpeed / 1.5); }

			} else if(input.isDown(IKey.Down)) {
				physics.velocity.Y -= (hoverSpeed * FInt.Create(0.15)); // HoverSpeed * 0.15
				if(physics.velocity.Y > hoverSpeed) { physics.velocity.Y = FInt.Create(hoverSpeed); }
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
