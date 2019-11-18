
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nexus.Engine;
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
		public static bool DrawDebugFrames = false;
		public static LoadOrder[] DrawDebugLoadOrders = new LoadOrder[] {
			//LoadOrder.Character,
			LoadOrder.Projectile,
			LoadOrder.Enemy,
		};

		public static DebugTickSpeed TickSpeed = DebugTickSpeed.StandardSpeed;

		// Tracking Values
		public static uint trackTicks = 0;

		// Colors
		public static Texture2D highlightColor;

		public DebugConfig() {
			DebugConfig.highlightColor = new Texture2D(Systems.graphics.GraphicsDevice, 1, 1);
			DebugConfig.highlightColor.SetData(new[] { Color.DarkRed });
		}
		
		public static void ToggleDebugFrames() {

			// Ignore Toggling Debug Mode if there is no debugging options allowed.
			if(!Debug) { return; }

			DrawDebugFrames = !DrawDebugFrames;
		}

		public static void SetTickSpeed( DebugTickSpeed tickSpeed ) { DebugConfig.TickSpeed = tickSpeed; }

		public static void ToggleTickSpeed( bool YControl ) {

			// Ignore Toggling Debug Mode if there is no debugging options allowed.
			if(!Debug) { return; }

			if(YControl) {
				if(TickSpeed == DebugTickSpeed.WhenYPressed) { TickSpeed = DebugTickSpeed.WhileYHeldSlow; }
				else if(TickSpeed == DebugTickSpeed.WhileYHeldSlow) { TickSpeed = DebugTickSpeed.WhileYHeld; }
				else if(TickSpeed == DebugTickSpeed.WhileYHeld) { TickSpeed = DebugTickSpeed.StandardSpeed; }
				else { TickSpeed = DebugTickSpeed.WhenYPressed; }
			}

			else {
				if(TickSpeed == DebugTickSpeed.StandardSpeed) { TickSpeed = DebugTickSpeed.HalfSpeed; }
				else if(TickSpeed == DebugTickSpeed.HalfSpeed) { TickSpeed = DebugTickSpeed.QuarterSpeed; }
				else if(TickSpeed == DebugTickSpeed.QuarterSpeed) { TickSpeed = DebugTickSpeed.EighthSpeed; }
				else if(TickSpeed == DebugTickSpeed.EighthSpeed) { TickSpeed = DebugTickSpeed.StandardSpeed; }
				else { TickSpeed = DebugTickSpeed.StandardSpeed; }
			}
		}

		public static void ResetDebugValues() {
			DrawDebugFrames = false;
			TickSpeed = DebugTickSpeed.StandardSpeed;
		}
	}
}
