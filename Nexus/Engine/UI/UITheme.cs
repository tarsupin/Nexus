using Microsoft.Xna.Framework;
using Nexus.Gameplay;

namespace Nexus.Engine {

	public class UITheme {

		// Sub-Themes
		public readonly UIThemeNotifications notifs = new UIThemeNotifications();
		public readonly UIThemeToolTip tooltips = new UIThemeToolTip();
		public readonly UIThemeConfirmBox confirm = new UIThemeConfirmBox();
		public readonly UIThemeStatus status = new UIThemeStatus();
		public readonly UIThemeButton button = new UIThemeButton();

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
		public readonly FontClass bigFont = Systems.fonts.baseText;
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

	public class UIThemeConfirmBox {
		public readonly short Width = 400;
		public readonly short MinHeight = 250;
		public readonly short HeightGaps = 20;
		public readonly Color bg = new Color(55, 55, 55, 255);
		public readonly Color fg = Color.White;
	}

	public class UIThemeButton {
		public readonly short Height = 50;
		public readonly short Width = 140;

		// Background Colors
		public readonly Color NormalBG = new Color(55, 55, 55, 255);
		public readonly Color RejectBG = new Color(140, 55, 55, 255);
		public readonly Color AcceptBG = new Color(55, 140, 55, 255);

		// Outline Colors
		public readonly Color NormalHover = new Color(20, 20, 20, 255);
		public readonly Color RejectHover = new Color(145, 20, 20, 255);
		public readonly Color AcceptHover = new Color(20, 145, 20, 255);
	}
}
