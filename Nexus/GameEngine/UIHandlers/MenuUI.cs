using Nexus.Engine;
using System;
using static Nexus.GameEngine.Scene;

namespace Nexus.GameEngine {

	public class MenuUI {

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
		private readonly UIButton settings;
		private readonly UIButtonVolume volume;
		private readonly UIButton exit;

		private readonly UIButton world;
		private readonly UIButton patreon;

		// Social Buttons
		private readonly UIButton discord;
		private readonly UIButton reddit;
		private readonly UIButton twitter;

		//private readonly UIButton youtube;
		//private readonly UIButton twitch;

		public MenuUI( Scene scene, MenuUIOption menuUIOpt ) {
			this.scene = scene;

			this.mainMenu = new MainMenuUI(scene);

			if(menuUIOpt == MenuUIOption.Level) {
				this.subMenu = new LevelMenuUI((LevelScene) scene);
			}

			short rightX = (short)(Systems.screen.windowWidth - 56 - 10);
			short bottomY = (short)(Systems.screen.windowHeight - 56 - 10);

			// Corner Menu
			this.settings = new UIButton(null, "UI/Settings", 10, 10, delegate () {  } );
			this.volume = new UIButtonVolume(null, "UI/Volume/On", 76, 10, delegate () { Systems.settings.audio.ToggleMute(); } );
			this.exit = new UIButton(null, "UI/Quit", 142, 10, delegate () { Environment.Exit(0); } );

			this.world = new UIButton(null, "UI/World", 10, bottomY, delegate () { WebHandler.LaunchURL("https://nexus.games"); } );
			this.patreon = new UIButton(null, "UI/Social/Patreon", 76, bottomY, delegate () { WebHandler.LaunchURL("https://www.patreon.com/Nexus_Games"); } );

			// Create Social Buttons
			this.reddit = new UIButton(null, "UI/Social/Reddit", (short)(rightX - 66), bottomY, delegate () { WebHandler.LaunchURL("https://www.reddit.com/r/NexusGames/"); } );
			this.discord = new UIButton(null, "UI/Social/Discord", (short)(rightX - 132), bottomY, delegate () { WebHandler.LaunchURL("https://discord.gg/Wx5sGcr"); } );
			this.twitter = new UIButton(null, "UI/Social/Twitter", rightX, bottomY, delegate () { WebHandler.LaunchURL("https://twitter.com/scionax"); } );
			//this.youtube = new UIButton(null, "UI/Social/YouTube", 580, bottomY, delegate () { WebHandler.LaunchURL("http://youtube.com"); } );
			//this.twitch = new UIButton(null, "UI/Social/Twitch", 280, bottomY, delegate () { WebHandler.LaunchURL("http://twitch.com"); } );
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
