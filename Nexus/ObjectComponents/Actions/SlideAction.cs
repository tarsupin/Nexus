using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// status.actionBool1 (slideRight)			:: Identifies the direction the slide started in (can only slide that direction).

	public class SlideAction : ActionCharacter {

		public SlideAction() : base() {
			this.endsOnLanding = false;
		}

		public void StartAction( Character character, bool slideRight ) {
			CharacterStatus status = character.status;

			status.action = character.ActionMap.Slide;
			status.actionEnds = character.scene.timer.frame + character.stats.SlideDuration;
			status.actionBool1 = slideRight;
			status.nextSlide = status.actionEnds + character.stats.SlideWaitDuration;

			// TODO SOUND: Slide Sound
		}

		public static bool IsAbleToSlide( Character character, bool slideRight ) {
			CharacterStatus status = character.status;
			sbyte velX = (sbyte) character.physics.velocity.X.IntValue;

			// Make sure the character's slide timer has cooled down.
			if(status.nextSlide > character.scene.timer.frame) { return false; }

			// Make sure you're facing the same direction you have momentum in.
			if(slideRight && velX < 0) { return false; }
			if(!slideRight && velX > 0) { return false; }

			return true;
		}

		public override void RunAction( Character character ) {

			// End the action after the designated number of frames has elapsed:
			if(this.HasTimeElapsed(character)) {
				this.EndAction(character);
				return;
			}

			CharacterStatus status = character.status;
			Physics physics = character.physics;

			bool slideRight = status.actionBool1;

			// If the character touches an obstruction, end the slide.
			if(slideRight) { if(physics.touch.toRight) { this.EndAction(character); return; } }
			else if(physics.touch.toLeft) { this.EndAction(character); return; }

			// Perform Slide
			physics.velocity.X = slideRight ? character.stats.SlideStrength : 0 - character.stats.SlideStrength;

			PlayerInput input = character.input;

			// Continue Sliding if Slide Buttons Held
			if(input.isDown(IKey.AButton) && input.isDown(IKey.Down)) { return; }

			this.EndAction(character);
			return;
		}
	}
}
