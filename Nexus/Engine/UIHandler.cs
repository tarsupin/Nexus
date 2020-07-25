using Microsoft.Xna.Framework;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Engine {

	// UIHandler processes global UI states, and provides RunTick() and Draw() methods that run over ALL scenes.
	// UIHandler also has a .theme value that allows overwriting themed positions, colors, sizes, visibility, etc.
	public static class UIHandler {

		// UI State Tracking
		private static bool mouseAlwaysVisible = false;
		private static bool cornerMenuAlwaysVisible = false;
		public static bool showCornerMenu { get; private set; }
		public static UIState uiState { get; private set; }
		public static IMenu menu { get; private set; }

		// Theme
		public static UITheme theme { get; private set; }

		// Global Component
		public static UIGlobal globalUI;

		// UI Atlas
		public static Atlas atlas;

		// Consoles
		public static readonly WorldConsole worldConsole = new WorldConsole();
		public static readonly WorldEditConsole worldEditConsole = new WorldEditConsole();
		public static readonly LevelConsole levelConsole = new LevelConsole();
		public static readonly EditorConsole editorConsole = new EditorConsole();

		// GUI, Menus
		public static readonly ControlMenu controlMenu = new ControlMenu(725, 400);
		public static readonly EditorGuideMenu guideMenu = new EditorGuideMenu(725, 400);
		public static readonly EmptyMenu emptyMenu = new EmptyMenu();
		public static readonly CornerMenuUI cornerMenu = new CornerMenuUI();
		public static readonly MainMenu mainMenu = new MainMenu();
		public static readonly LevelMenu levelMenu = new LevelMenu();
		public static readonly LoginMenu loginMenu = new LoginMenu(280, 386);
		public static readonly LoadWorldMenu loadWorldMenu = new LoadWorldMenu(280, 150);

		// Colors
		public static Color spaceBG = new Color(18, 24, 58);
		public static Color starColor = new Color(58, 63, 90);
		public static Color selector = Color.DarkRed;
		public static Color mouseSelect = Color.DarkCyan;

		public static void Setup() {
			UIHandler.atlas = Systems.mapper.atlas[(byte)AtlasGroup.UI];
			UIHandler.theme = new UITheme();
			UIHandler.globalUI = new UIGlobal();
			UIHandler.UpdateGlobalUITheme();
		}

		public static void SetUIOptions(bool mouseAlwaysVisible, bool cornerMenuAlwaysVisible) {
			UIHandler.mouseAlwaysVisible = mouseAlwaysVisible;
			UIHandler.cornerMenuAlwaysVisible = cornerMenuAlwaysVisible;
		}

		public static void SetMenu(IMenu menu, bool showCornerMenu) {

			if(menu is IMenu) {
				UIHandler.uiState = UIState.Menu;
				UIHandler.menu = menu;
			}

			else {
				UIHandler.uiState = UIState.Playing;
				UIHandler.menu = UIHandler.emptyMenu;
			}

			UIHandler.showCornerMenu = showCornerMenu || UIHandler.cornerMenuAlwaysVisible;

			// Determine If Mouse Should Appear
			if(uiState == UIState.Menu || UIHandler.mouseAlwaysVisible) { UIHandler.SetMouseVisible(true); }
			else { UIHandler.SetMouseVisible(false); }
		}

		public static void SetMouseVisible(bool visible) {
			Systems.game.IsMouseVisible = visible;
		}

		// Themes
		public static void UpdateGlobalUITheme() {
			UIHandler.globalUI.notifyBox.RunThemeUpdate();
			UIHandler.globalUI.toolTip.RunThemeUpdate();
		}

		// Notifications
		public static void AddNotification(UIAlertType type, string title, string text, int duration) {
			UIHandler.globalUI.notifyBox.AddIncomingNotification(type, title, text, duration);
		}

		// Tool Tips
		public static void RunToolTip(string id, string title, string text, UIPrimaryDirection dir) {
			if(!UIHandler.globalUI.toolTip._MaintainToolTip(id)) {
				UIHandler.globalUI.toolTip._CreateToolTip(id, title, text, dir);
				UIHandler.globalUI.toolTip._MaintainToolTip(id);
			}
		}

		public static void RunTick() {
			UIHandler.globalUI.notifyBox.RunTick();
			UIHandler.globalUI.toolTip.RunTick();
			//UIHandler.globalUI.confirmBox.RunTick();
		}

		public static void Draw() {
			UIHandler.globalUI.notifyBox.Draw();
			UIHandler.globalUI.toolTip.Draw();
			//UIHandler.globalUI.confirmBox.Draw();
		}
	}
}
