using Nexus.Engine;
using System;

namespace Nexus.GameEngine {

	public class CornerMenuUI {

		// Corner Menu
		private readonly UIIcon settings;
		private readonly ToggleMusic music;
		private readonly ToggleVolume volume;
		private readonly UIIcon exit;

		//private readonly UIIcon world;
		//private readonly UIIcon patreon;

		// Social Buttons
		private readonly UIIcon discord;
		private readonly UIIcon reddit;
		private readonly UIIcon twitter;

		//private readonly UIButton youtube;
		//private readonly UIButton twitch;

		public CornerMenuUI() {

			short midX = (short)(Systems.screen.windowHalfWidth - 28);
			short rightX = (short)(Systems.screen.windowWidth - 56 - 10);
			//short bottomY = (short)(Systems.screen.windowHeight - 56 - 10);

			// Corner Menu
			this.settings = new UIIcon(null, "Settings", 10, 10, delegate () { UIHandler.SetMenu(UIHandler.controlMenu, true); }, "Controls", "View the Keys and Gamepad Controls." );
			this.music = new ToggleMusic(null, 76, 10, delegate () { Systems.settings.audio.ToggleMusic(); });
			this.volume = new ToggleVolume(null, 142, 10, delegate () { Systems.settings.audio.ToggleMute(); } );
			this.exit = new UIIcon(null, "Quit", 208, 10, delegate () { Environment.Exit(0); }, "Quit Game", "Exit the Game." );

			//this.world = new UIIcon(null, "World", (short)(midX - 33), 10, delegate () { WebHandler.LaunchURL("https://nexus.games"); } );
			//this.patreon = new UIIcon(null, "Social/Patreon", (short)(midX + 33), 10, delegate () { WebHandler.LaunchURL("https://www.patreon.com/Nexus_Games"); } );

			// Create Social Buttons
			this.twitter = new UIIcon(null, "Social/Twitter", (short)(rightX - 132), 10, delegate () { WebHandler.LaunchURL("https://twitter.com/scionax"); }, "Scionax's Twitter", "Follow me on Twitter to help our community grow.");
			this.discord = new UIIcon(null, "Social/Discord", (short)(rightX - 66), 10, delegate () { WebHandler.LaunchURL("https://discord.gg/Wx5sGcr"); }, "Discord Community", "Opens a link to join our Discord Community.");
			this.reddit = new UIIcon(null, "Social/Reddit", rightX, 10, delegate () { WebHandler.LaunchURL("https://www.reddit.com/r/NexusGames/"); }, "Reddit Community", "Opens a link to the Reddit Community." );
			//this.youtube = new UIButton(null, "Social/YouTube", 580, 10, delegate () { WebHandler.LaunchURL("http://youtube.com"); } );
			//this.twitch = new UIButton(null, "Social/Twitch", 280, 10, delegate () { WebHandler.LaunchURL("http://twitch.com"); } );
		}

		public void RunTick() {
			if(!UIHandler.showCornerMenu) { return; }

			// Corner Menu
			this.settings.RunTick();
			this.music.RunTick();
			this.volume.RunTick();
			this.exit.RunTick();

			//this.world.RunTick();
			//this.patreon.RunTick();

			// Create Social Buttons
			this.twitter.RunTick();
			this.discord.RunTick();
			this.reddit.RunTick();
			//this.youtube.RunTick();
			//this.twitch.RunTick();
		}

		public void Draw() {
			if(!UIHandler.showCornerMenu) { return; }

			// Corner Menu
			this.settings.Draw();
			this.music.Draw();
			this.volume.Draw();
			this.exit.Draw();

			//this.world.Draw();
			//this.patreon.Draw();

			// Draw Social Buttons
			this.twitter.Draw();
			this.discord.Draw();
			this.reddit.Draw();
			//this.youtube.Draw();
			//this.twitch.Draw();
		}
	}
}
