using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;
using System.Collections.Generic;

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

	public static class DebugConfig {

		// Activate/Deactivate Debug to control other debugging values:
		public static bool Debug = true;

		// Debug Settings
		public static bool DrawDebugFrames = true;
		public static LoadOrder[] DrawDebugLoadOrders = new LoadOrder[] {
			//LoadOrder.Character,
			LoadOrder.Projectile,
			LoadOrder.Enemy,
		};

		public static DebugTickSpeed TickSpeed = DebugTickSpeed.StandardSpeed;

		// Tracking Values
		public static int trackTicks = 0;

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

		// Debug Notes (Display on Screen)
		public static List<string> debugNotesByFrame = new List<string>();

		public static void AddDebugNote(string note) {
			if(!DebugConfig.Debug) { return; }
			DebugConfig.debugNotesByFrame.Add(Systems.timer.frame60Modulus + ": " + note);
		}

		public static void ClearDebugNotes(byte count = 35) {
			for(byte i = 0; i < 100; i++) {
				if(DebugConfig.debugNotesByFrame.Count <= count) { return; }
				DebugConfig.debugNotesByFrame.RemoveAt(0);
			}
		}

		public static void DrawDebugNotes() {
			if(!DebugConfig.Debug) { return; }
			if(DebugConfig.debugNotesByFrame.Count > 35) { DebugConfig.ClearDebugNotes(); }

			byte count = (byte) DebugConfig.debugNotesByFrame.Count;

			for(byte i = 0; i < count;i++) {
				Systems.fonts.console.Draw(DebugConfig.debugNotesByFrame[i], 20, 20 + (i * 15), Color.Black);
			}
		}
	}
}
