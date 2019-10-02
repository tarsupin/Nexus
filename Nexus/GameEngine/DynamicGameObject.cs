using Nexus.Engine;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public enum Activity {
		Inactive = 0,               // The object is inactive, not visible, etc. No reason to run updates.
		Seen = 1,                   // The object is currently visible to a player (multiplayer-ready). Needs to run updates.
		ForceActive = 2,            // The object is forced to be active through the whole level. Always run updates.
	}

	public class DynamicGameObject : GameObject {

		public Activity activity;

		// TODO: ActionTrait, BehaviorTrait
		// TODO: Status (can have multiple status types); some don't need
			// TODO: -- last character touch direction; what relelvant for?
		// TODO: TrackInstructions (rules for dealing with tracks; not everything needs this, but... ???)

		public DynamicGameObject(Scene scene, byte subType, FVector pos, object[] paramList = null) : base(scene, subType, pos, paramList) {

		}
	}
}
