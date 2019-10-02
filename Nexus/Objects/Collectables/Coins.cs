using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public enum CoinsState {
		Collected = 1,
	}

	class Coins : Collectable {

		public Coins(Scene scene, byte subType, FVector pos, object[] paramList = null) : base(scene, subType, pos, paramList) {

		}

		public void SetState( byte state ) {
			//this._state = state;

			//if(state == (byte) CoinsState.Collected) {
			//	// TODO LOW PRIORITY: Destroy Coins on collection
			//}
		}

	}
}
