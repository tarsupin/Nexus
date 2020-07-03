﻿using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class SlamAction : Action {

		public SlamAction() : base() {
			this.endsOnLanding = true;
		}

		public void StartAction( Character character ) {
			this.EndLastActionIfActive(character);
			CharacterStatus status = character.status;

			status.action = ActionMap.Slam;
			status.actionEnds = Systems.timer.Frame + 20;

			// Begin Slam Motion
			character.physics.velocity.X = FInt.Create(0);
			character.physics.velocity.Y = FInt.Create(12);
		}

		public override void RunAction( Character character ) {

			// End the action after the designated number of frames has elapsed:
			if(this.HasTimeElapsed(character)) {
				if(!character.input.isDown(IKey.BButton)) {
					this.EndAction(character);
				}
				return;
			}

			// Maintain Slam Motion
			character.physics.velocity.X = FInt.Create(0);
		}

		public static void CauseSlam(Character character) {
			Systems.camera.BeginCameraShake(8, 5);
			character.room.PlaySound(Systems.sounds.thudWhomp, 0.3f, character.posX + 16, character.posY + 16);
		}
	}
}
