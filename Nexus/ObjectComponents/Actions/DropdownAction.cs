﻿using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class DropdownAction : Action {

		public DropdownAction() : base() {
			this.endsOnLanding = true;
		}

		public void StartAction( Character character, byte duration ) {
			this.EndLastActionIfActive(character);
			CharacterStatus status = character.status;

			status.action = ActionMap.Dropdown;
			status.actionEnds = Systems.timer.Frame + duration;
		}

		public override void RunAction( Character character ) {

			// End the action after the designated number of frames has elapsed:
			if(this.HasTimeElapsed(character)) {
				this.EndAction(character);
			}
		}
	}
}
