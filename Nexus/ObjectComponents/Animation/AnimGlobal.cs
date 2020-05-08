
// The AnimGlobal Class is mapped in GameMapper (.mapper) to AnimationMap.
// It can be attached as a component to the GameObject (e.g. chomper.animate).
// This class can be derived for specific entities (such as Chompers) to have them animate simulataneously.

using Nexus.Engine;

namespace Nexus.ObjectComponents {

	public class AnimGlobal {

		// Returns TRUE if we're on an animation tick (which is the same as a beat frame)
		public bool IsAnimationTick( TimerGlobal timer) {
			return timer.IsBeatFrame;
		}

		// Get 2-Per-Second Global Animation ID: Animations that cycle between two texture IDs per second.
		public byte Get2PSAnimId( TimerGlobal timer ) {
			return timer.beat4Modulus >= 2 ? (byte) 2 : (byte) 1;
		}

		// Get 3-Per-Second Global Animation ID: Animations that cycle between three texture IDs per second.
		public byte Get3PSAnimId( TimerGlobal timer ) {
			if(timer.frame60Modulus >= 40) { return 3; }
			if(timer.frame60Modulus >= 20) { return 2; }
			return 1;
		}

		// Get 4-Per-Second Global Animation ID: Animations that cycle between four texture IDs per second.
		public byte Get4PSAnimId( TimerGlobal timer ) {
			return (byte) (timer.beat4Modulus + 1);
		}

		// Get 5-Per-Second Global Animation ID: Animations that cycle between five texture IDs per second.
		public byte Get5PSAnimId( TimerGlobal timer ) {
			if(timer.frame60Modulus >= 36) { return timer.frame60Modulus >= 48 ? (byte) 5 : (byte) 4; }
			if(timer.frame60Modulus >= 12) { return timer.frame60Modulus >= 24 ? (byte) 3 : (byte) 2; }
			return 1;
		}
	}
}
