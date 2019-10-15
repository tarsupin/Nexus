using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class FlightAction : ActionCharacter {

		public FlightAction() : base() {
			this.endsOnLanding = true;
		}

		public void StartAction( Character character ) {
			character.status.action = ActionMap.Flight;
			character.physics.SetGravity((FInt) 0);
		}

		public override void RunAction( Character character ) {
			PlayerInput input = character.input;
			Physics physics = character.physics;

			byte hoverSpeed = input.isDown(IKey.XButton) ? (byte) 8 : (byte) 5;

			// Horizontal Levitation Movement/Speed
			if(input.isDown(IKey.Right)) {
				character.faceRight = true;
				physics.velocity.X += (hoverSpeed * FInt.Create(0.05)); // HoverSpeed * 0.05
				if(physics.velocity.X > hoverSpeed) { physics.velocity.X = FInt.Create(hoverSpeed); }

			} else if(input.isDown(IKey.Left)) {
				character.faceRight = false;
				physics.velocity.X -= (hoverSpeed * FInt.Create(0.05)); // HoverSpeed * 0.05
				if(physics.velocity.X > hoverSpeed) { physics.velocity.X = FInt.Create(hoverSpeed); }
			}

			// Horizontal Deceleration
			else {
				physics.velocity.X = this.FlightDeceleration(physics.velocity.X, hoverSpeed * FInt.Create(0.3));
			}

			// Vertical Levitation
			if(input.isDown(IKey.Up)) {
				physics.velocity.Y += (hoverSpeed * FInt.Create(0.05)); // HoverSpeed * 0.05
				if(physics.velocity.Y > hoverSpeed) { physics.velocity.Y = FInt.Create(hoverSpeed); }

			} else if(input.isDown(IKey.Down)) {
				physics.velocity.Y -= (hoverSpeed * FInt.Create(0.05)); // HoverSpeed * 0.05
				if(physics.velocity.Y > hoverSpeed) { physics.velocity.Y = FInt.Create(hoverSpeed); }
			}

			// Vertical Deceleration
			else {
				physics.velocity.Y = this.FlightDeceleration(physics.velocity.Y, hoverSpeed * FInt.Create(0.3));
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
