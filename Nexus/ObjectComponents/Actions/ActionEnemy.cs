﻿using Nexus.Engine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class ActionEnemy : Action {

		public bool HasTimeElapsed( Enemy enemy ) {
			return Systems.timer.frame > enemy.status.actionEnds;
		}

		public virtual void RunAction(Enemy enemy) { }

		public virtual void EndAction( Enemy enemy ) {
			enemy.status.action = null;
		}
	}
}