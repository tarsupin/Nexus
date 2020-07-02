using Nexus.Engine;
using System;
using static Nexus.GameEngine.Scene;

namespace Nexus.GameEngine {

	public class MenuUI {

		// References
		private readonly Scene scene;
		private readonly ICenterMenu mainMenu;
		private readonly ICenterMenu subMenu;

		public enum MenuUIOption : byte {
			Main,
			Level,
			World,
			PlanetSelect,
		}

		// Corner Menu
		private readonly UIIcon settings;
		private readonly UIIconVolume volume;
		private readonly UIIcon exit;

		private readonly UIIcon world;
		private readonly UIIcon patreon;

		// Social Buttons
		private readonly UIIcon discord;
		private readonly UIIcon reddit;
		private readonly UIIcon twitter;

		//private readonly UIButton youtube;
		//private readonly UIButton twitch;

		public MenuUI( Scene scene, MenuUIOption menuUIOpt ) {
			this.scene = scene;

			this.mainMenu = new MainMenuUI(scene);

			if(menuUIOpt == MenuUIOption.Level) {
				this.subMenu = new LevelMenuUI((LevelScene) scene);
			}

			short midX = (short)(Systems.screen.windowHalfWidth - 28);
			short rightX = (short)(Systems.screen.windowWidth - 56 - 10);
			//short bottomY = (short)(Systems.screen.windowHeight - 56 - 10);

			// Corner Menu
			this.settings = new UIIcon(null, "UI/Settings", 10, 10, delegate () {  } );
			this.volume = new UIIconVolume(null, "UI/Volume/On", 76, 10, delegate () { Systems.settings.audio.ToggleMute(); } );
			this.exit = new UIIcon(null, "UI/Quit", 142, 10, delegate () { Environment.Exit(0); } );

			this.world = new UIIcon(null, "UI/World", (short)(midX - 33), 10, delegate () { WebHandler.LaunchURL("https://nexus.games"); } );
			this.patreon = new UIIcon(null, "UI/Social/Patreon", (short)(midX + 33), 10, delegate () { WebHandler.LaunchURL("https://www.patreon.com/Nexus_Games"); } );

			// Create Social Buttons
			this.reddit = new UIIcon(null, "UI/Social/Reddit", (short)(rightX - 66), 10, delegate () { WebHandler.LaunchURL("https://www.reddit.com/r/NexusGames/"); } );
			this.discord = new UIIcon(null, "UI/Social/Discord", (short)(rightX - 132), 10, delegate () { WebHandler.LaunchURL("https://discord.gg/Wx5sGcr"); } );
			this.twitter = new UIIcon(null, "UI/Social/Twitter", rightX, 10, delegate () { WebHandler.LaunchURL("https://twitter.com/scionax"); } );
			//this.youtube = new UIButton(null, "UI/Social/YouTube", 580, 10, delegate () { WebHandler.LaunchURL("http://youtube.com"); } );
			//this.twitch = new UIButton(null, "UI/Social/Twitch", 280, 10, delegate () { WebHandler.LaunchURL("http://twitch.com"); } );
		}

		public void RunTick() {
			UIComponent.ComponentWithFocus = null;
			Cursor.UpdateMouseState();

			// Corner Menu
			this.settings.RunTick();
			this.volume.RunTick();
			this.exit.RunTick();

			this.world.RunTick();
			this.patreon.RunTick();

			// Center Menu
			if(this.scene.uiState == UIState.MainMenu) { this.mainMenu.RunTick(); }
			else if(this.subMenu is ICenterMenu) { this.subMenu.RunTick(); }

			// Create Social Buttons
			this.discord.RunTick();
			this.reddit.RunTick();
			this.twitter.RunTick();
			//this.youtube.RunTick();
			//this.twitch.RunTick();
		}

		public void Draw() {

			// Corner Menu
			this.settings.Draw();
			this.volume.Draw();
			this.exit.Draw();

			this.world.Draw();
			this.patreon.Draw();

			// Center Menu
			if(this.scene.uiState == UIState.MainMenu) { this.mainMenu.Draw(); }
			else if(this.subMenu is ICenterMenu) { this.subMenu.Draw(); }

			// Draw Social Buttons
			this.discord.Draw();
			this.reddit.Draw();
			this.twitter.Draw();
			//this.youtube.Draw();
			//this.twitch.Draw();
		}
	}
}
