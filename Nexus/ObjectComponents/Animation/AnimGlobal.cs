
// The AnimGlobal Class is mapped in GameMapper (.mapper) to AnimationMap.
// It can be attached as a component to the GameObject (e.g. chomper.animate).
// This class can be derived for specific entities (such as Chompers) to have them animate simulataneously.

using Nexus.Engine;

namespace Nexus.ObjectComponents {

	public class AnimGlobal {

		// Returns TRUE if we're on an update cycle.
		public bool IsAnimationTick( TimerGlobal timer) {
			return timer.frame % 15 == 0;
		}

		// Cycle Global Animation Tick. Cycles between two texture IDs:
		public byte GetAnimationId( TimerGlobal timer ) {
			return timer.frame % 30 == 0 ? (byte) 2 : (byte) 1;
		}
	}
}
