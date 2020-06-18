﻿using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class WallGrabAction : Action {

		public WallGrabAction() : base() {
			this.endsOnLanding = true;
		}

		// ActionMap.WallGrab.StartAction( character, dir );
		public void StartAction( Character character, DirCardinal dir ) {

			// If the Character is holding an item, there is no opportunity for wall jumping:
			if(character.heldItem.IsHeld) { return; }

			CharacterStatus status = character.status;

			// If the character is already grabbing the wall, end here.
			if(character.status.action is WallGrabAction) { return; }

			CharacterStats stats = character.stats;

			// If Able To Grab Walls
			if(stats.CanWallGrab) {
				this.EndLastActionIfActive(character);
				status.action = ActionMap.WallGrab;
				status.grabDir = dir; // Saves the direction of the grab for consistency in leap direction.
			}
			
			// If character can Slide on Walls
			else if(stats.CanWallSlide) {
				status.grabDir = dir; // Saves the direction of the grab for consistency in leap direction.

				// Only begin sliding if you're moving downward:
				if(character.physics.velocity.Y > 0) {
					this.EndLastActionIfActive(character);
					status.action = ActionMap.WallGrab;
				}
				
				// If you're moving upward against the wall, you can still kick off using wall delay:
				else {
					status.leaveWall = Systems.timer.Frame + 4;
				}
			}
		}

		public override void RunAction( Character character ) {
			CharacterStatus status = character.status;
			PlayerInput input = character.input;
			Physics physics = character.physics;
			bool grab = character.stats.CanWallGrab;

			status.jumpsUsed = 0;

			if(grab) {
				physics.StopY();
				physics.velocity.Y = physics.gravity.Inverse;
			} else {
				physics.velocity.Y = FInt.Create(0.4);
			}

			// If character jumped off the wall:
			if(input.isPressed(IKey.AButton)) {
				ActionMap.WallJump.StartAction(character, status.grabDir);
			}
			
			// If character is still holding wall:
			else {

				// Make sure character is still holding and not touching ground (important!)
				if(!input.isDown(IKey.XButton) || physics.touch.toBottom) {
					this.EndAction(character);
				}

				// Make sure the character is still facing the wall.
				else if((status.grabDir == DirCardinal.Left && !physics.touch.toLeft) || (status.grabDir == DirCardinal.Right && !physics.touch.toRight)) {
					this.EndAction(character);
				}
			}
		}

		public override void EndAction(Character character) {
			base.EndAction(character);
			character.status.leaveWall = Systems.timer.Frame + 4;
		}
	}
}
