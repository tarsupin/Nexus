using Microsoft.Xna.Framework;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Engine {

	public class UITheme {

		// Sub-Themes
		public readonly UIThemeNotifications notifs = new UIThemeNotifications();
		public readonly UIThemeToolTip tooltips = new UIThemeToolTip();
		public readonly UIThemeStatus status = new UIThemeStatus();

		// Background Colors
		public readonly Color NormBG = new Color(55, 55, 55, 220);
		public readonly Color ErrorBG = new Color(120, 55, 55, 220);
		public readonly Color WarningBG = new Color(120, 120, 55, 220);
		public readonly Color SuccessBG = new Color(55, 120, 55, 220);

		// Outline Colors
		public readonly Color NormOutline = Color.White;
		public readonly Color ErrorOutline = Color.White;
		public readonly Color WarningOutline = Color.White;
		public readonly Color SuccessOutline = Color.White;

		// Font Colors
		public readonly Color NormFG = Color.White;

		// Fonts
		public readonly FontClass headerFont = Systems.fonts.baseText;
		public readonly FontClass smallHeaderFont = Systems.fonts.baseText;
		public readonly FontClass bigFont = Systems.fonts.console;
		public readonly FontClass normalFont = Systems.fonts.console;
		public readonly FontClass smallFont = Systems.fonts.console;
		public readonly FontClass consoleFont = Systems.fonts.console;
	}

	public class UIThemeNotifications {

		// Sizes
		public readonly short ContainerHeight = 700;	// The height of the container.
		public readonly short ItemWidth = 320;			// The inner width of notifications; adds some extra width for boundaries.
		public readonly short NotifGap = 10;			// The gap size (margin) between notifications.

		// Positioning
		public readonly UIHorPosition xRel = UIHorPosition.Right;
		public readonly UIVertPosition yRel = UIVertPosition.Bottom;
		public readonly short xOffset = 50;
		public readonly short yOffset = 50;

		// Exit Behavior
		public readonly short exitDuration = 40;
	}

	public class UIThemeToolTip {
		public readonly short ItemWidth = 320;
		public readonly short CursorGap = 20;               // The gap (in pixels) between the cursor and tooltip.
		public readonly short EndDuration = 12;
	}

	public class UIThemeStatus {
		public readonly short EndDuration = 30;
		public readonly Color bg = Color.White;
		public readonly Color fg = Color.DarkSlateBlue;
	}
}
