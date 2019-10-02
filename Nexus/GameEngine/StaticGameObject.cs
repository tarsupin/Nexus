
using Nexus.Engine;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class StaticGameObject : GameObject {

		// TODO: Sticky? Ungrippable, Damage, Friction, Conveyor
		// -- move all into a TouchEffect class that contains this; (Sticky, Ungrippable, Damage, Friction)
		// -- that would also allow it to be utilized in dynamic blocks

		public StaticGameObject(Scene scene, byte subType, FVector pos, object[] paramList = null) : base(scene, subType, pos, paramList) {

		}
	}
}
