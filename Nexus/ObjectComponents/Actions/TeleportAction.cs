using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// status.actionNum1 (distance)		:: the distance of the teleport from the character.
	// status.actionFloat1 (radian)		:: the radian of the teleport, relative to the character.
	// status.actionBool1 (faceRight)	:: tracks the facing position that the character *started* with (important for remembering input rotations)

	public class TeleportAction : Action {

		public const string teleSprite = "Particles/Fireball";
		public const float rotateSpeed = 0.04f;

		public TeleportAction() : base() {
			this.endsOnLanding = false;
		}

		public void StartAction( Character character ) {
			this.EndLastActionIfActive(character);

			CharacterStatus status = character.status;
			status.action = ActionMap.Teleport;
			status.actionEnds = Systems.timer.Frame + 180;
			status.actionNum1 = 0;
			status.actionBool1 = character.FaceRight;	// Tracks the starting facing position, to determine rotation movements.

			// Determine the Starting Radian based on Facing Position and Up/Down Inputs
			if(status.actionBool1) {
				if(character.input.isDown(IKey.Up)) {
					status.actionFloat1 = Radians.UpRight;
				} else if(character.input.isDown(IKey.Down)) {
					status.actionFloat1 = Radians.DownRight;
				} else {
					status.actionFloat1 = Radians.Right;
				}
			} else {
				if(character.input.isDown(IKey.Up)) {
					status.actionFloat1 = Radians.UpLeft;
				} else if(character.input.isDown(IKey.Down)) {
					status.actionFloat1 = Radians.DownLeft;
				} else {
					status.actionFloat1 = Radians.Left;
				}
			}
		}

		public override void RunAction( Character character ) {

			// End the action after the designated number of frames has elapsed:
			if(this.HasTimeElapsed(character)) {
				this.EndAction(character);
			}

			CharacterStatus status = character.status;

			// Extend the distance.
			status.actionNum1 += 7;

			float trueRotation = Radians.Normalize(status.actionFloat1);

			// Rotate the teleport radian based on character's directional inputs.
			if(character.input.isDown(IKey.Up)) {
				if(status.actionBool1) { trueRotation -= TeleportAction.rotateSpeed; }
				else { trueRotation += TeleportAction.rotateSpeed; }
			} else if(character.input.isDown(IKey.Down)) {
				if(status.actionBool1) { trueRotation += TeleportAction.rotateSpeed; }
				else { trueRotation -= TeleportAction.rotateSpeed; }
			}

			// Get X, Y Coordinates from Distance and Radian
			int xCoord = (int) Radians.GetXFromRotation(trueRotation, status.actionNum1) + character.posX + character.bounds.MidX;
			int yCoord = (int) Radians.GetYFromRotation(trueRotation, status.actionNum1) + character.posY + character.bounds.MidY;

			status.actionFloat1 = trueRotation;

			StationaryParticle.SetParticle(character.room, Systems.mapper.atlas[(byte)AtlasGroup.Objects], TeleportAction.teleSprite, new Vector2(xCoord, yCoord), Systems.timer.Frame + 1);
		}
	}
}
