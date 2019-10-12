using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class ActionEnemy : Action {

		public bool HasTimeElapsed(Enemy enemy) {
			return enemy.scene.timer.frame > enemy.status.actionEnds;
		}

		public void EndAction(Enemy enemy) {
			enemy.status.action = null;
		}
	}
}
