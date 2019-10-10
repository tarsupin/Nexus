﻿using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class ActionCharacter : Action {

		public ActionCharacter() : base() {

		}
		
		public bool HasTimeElapsed( Character character ) {
			return character.scene.timer.frame > character.status.actionEnds;
		}

		public virtual void SetDuration( Character character ) {
			
		}
		
		public virtual void RunAction( Character character ) {}

		public virtual void LandsOnGround( Character character ) {
			if(this.endsOnLanding) { this.EndAction( character ); }
		}

		public virtual void EndAction( Character character ) {
			character.status.action = null;
		}

	}
}
