using Microsoft.Xna.Framework;

namespace Nexus.Engine {

	public class UINotifications {
		public short height;            // May vary depending on amount of text in the notification.
		public string title;			// The title or main text for the notification.
		public string miniText;         // Less important text or minor details to support the notification title.

		// Action Parameters
		// If the notification is clicked, it can provide parameters for the resulting action called.
		public short[] paramNums;
	}

	public class UINotificationSettings {
		public readonly short InnerWidth = 200;			// The inner width of the notification; adds some extra width for boundaries.

		// Background Colors
		public readonly Color NormBG = Color.Black;
		public readonly Color ErrorBG = Color.DarkRed;
		public readonly Color WarningBG = Color.Yellow;
		public readonly Color SuccessBG = Color.DarkGreen;

		// Outline Colors
		public readonly Color NormOutline = Color.Black;
		public readonly Color ErrorOutline = Color.DarkRed;
		public readonly Color WarningOutline = Color.Yellow;
		public readonly Color SuccessOutline = Color.DarkGreen;

		// Font
		public readonly Color FontColor = Color.White;

		// Positioning
		public readonly UIHorPosition xRel;
		public readonly UIVertPosition yRel;
		public readonly short xOffset = 50;
		public readonly short yOffset = 50;
	}
}
