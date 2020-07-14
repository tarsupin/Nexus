using Microsoft.Xna.Framework;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Engine {

	public static class UIHandler {

		// UI Handler
		private static bool mouseAlwaysVisible = false;
		private static bool cornerMenuAlwaysVisible = false;
		public static bool showCornerMenu { get; private set; }
		public static UIState uiState { get; private set; }
		public static IMenu menu { get; private set; }

		public enum UIState : byte { Playing, Menu }

		// Theme
		public static UITheme theme { get; private set; }

		// Global Component
		public static GlobalUI globalUI;

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
		public static readonly LoginMenu loginMenu = new LoginMenu(280, 300);

		// Colors
		public static Color spaceBG = new Color(18, 24, 58);
		public static Color starColor = new Color(58, 63, 90);
		public static Color selector = Color.DarkRed;

		public static void Setup() {
			UIHandler.atlas = Systems.mapper.atlas[(byte)AtlasGroup.UI];
			UIHandler.theme = new UITheme();
			UIHandler.globalUI = new GlobalUI();
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
	}
}
