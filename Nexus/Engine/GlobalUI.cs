using Microsoft.Xna.Framework;
using Nexus.GameEngine;
using System.Collections.Generic;

namespace Nexus.Engine {

	// GlobalUI Class provides a RunTick() that is designed to run over ALL scenes.
	// GlobalUI handles globally recognized UI features, including:
	//		- Notification Popups
	//		- Tool Tips
	//		- Alert + Confirmation Boxes (May Lock Screen + Require Interaction)
	//		- Right Click Menu
	// GlobalUI also has a .LocalSettings value that allows overwriting GlobalUI settings, positions, visibility, etc.
	public static class GlobalUI {

		// Notifications
		public static List<UINotifications> notifications = new List<UINotifications>();
		public static UINotificationSettings notificationSettings = new UINotificationSettings();

		public static void RunGlobalUITick() {

		}
	}
}
