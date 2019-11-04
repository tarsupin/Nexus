
using Nexus.Gameplay;

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

		// Activate/Deactivate Debug to control other debugging values:
		public static bool Debug = true;

		// Debug Settings
		public static bool DrawDebugFrames;
		public static LoadOrder[] DrawDebugLoadOrders;
		public static DebugTickSpeed TickSpeed;

		// Tracking Values
		public static uint trackTicks = 0;

		// Initialize Debug Configurations Here
		public void InitializeDebugValues() {

			// Show Debug Frames around game objects, to help identify when collisions should occur.
			DrawDebugFrames = true;

			// Determine which game objects will have debug frames on them.
			DrawDebugLoadOrders = new LoadOrder[] {
				//LoadOrder.Character,
				LoadOrder.Projectile,
				LoadOrder.Enemy,
			};

			// Choose the Tick Speed; the rate at which the game runs.
			TickSpeed = DebugTickSpeed.StandardSpeed;
			//TickSpeed = DebugTickSpeed.HalfSpeed;
			//TickSpeed = DebugTickSpeed.QuarterSpeed;
			//TickSpeed = DebugTickSpeed.WhenYPressed;
		}

		public DebugConfig() {

			if(Debug) {
				this.InitializeDebugValues();
			}
		}
	}
}
