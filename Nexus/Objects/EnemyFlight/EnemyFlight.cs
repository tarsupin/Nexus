using Newtonsoft.Json.Linq;
using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class EnemyFlight : Enemy {

		public EnemyFlight(LevelScene scene, byte subType, FVector pos, JObject paramList) : base(scene, subType, pos, paramList) {

		}

		// TODO CLEANUP: DELETE
		//public override void RunTick() {

		//	// Actions and Behaviors
		//	if(this.status.action is ActionEnemy) {
		//		this.status.action.RunAction(this);
		//	}

		//	// Standard Physics
		//	this.physics.RunTick();

		//	// Running behavior after physics, since that matters in Flight Physics.
		//	this.behavior.RunTick();

		//	// Animations, if applicable.
		//	if(this.animate is Animate) {
		//		this.animate.RunAnimationTick(Systems.timer);
		//	}
		//}
	}
}
