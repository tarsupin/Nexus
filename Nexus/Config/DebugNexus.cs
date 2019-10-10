
namespace Nexus.Config {

	public enum DebugTickSpeed : byte {
		StandardSpeed = 0,
		HalfSpeed = 1,
		QuarterSpeed = 2,
		EighthSpeed = 3,
		WhileYHeld = 4,
		WhileYHeldSlow = 5,
		WhenYPressed = 6,
	}

	public class DebugConfig {

		public static bool Debug;
		public static DebugTickSpeed TickSpeed;

		// Tracking Values
		public static uint trackTicks = 0;

		// Set Debug Configurations Here
		public DebugConfig() {

			// Activate/Deactivate Debug to control other debugging values:
			Debug = true;

			if(Debug) {
				TickSpeed = DebugTickSpeed.StandardSpeed;
			}
		}
	}
}
