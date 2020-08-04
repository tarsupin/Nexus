using Nexus.Engine;
using System;

namespace Nexus.GameEngine {

	public class CornerMenuUI {

		// Corner Menu
		private readonly UICreoIcon mainMenu;
		private readonly ToggleMusic music;
		private readonly ToggleVolume volume;
		private readonly UICreoIcon exit;

		//private readonly UIIcon world;
		//private readonly UIIcon patreon;

		// Community
		private readonly UICreoIcon community;
		private readonly UICreoIcon credits;

		// Social Buttons
		private readonly UICreoIcon discord;
		private readonly UICreoIcon reddit;
		private readonly UICreoIcon twitter;

		//private readonly UIButton youtube;
		//private readonly UIButton twitch;

		public CornerMenuUI() {

			short midX = (short)(Systems.screen.viewHalfWidth - 28);
			short rightX = (short)(Systems.screen.viewWidth - 56 - 10);
			//short bottomY = (short)(Systems.screen.windowHeight - 56 - 10);

			// Corner Menu
			this.mainMenu = new UICreoIcon(null, "Menu", 10, 10, delegate () { UIHandler.SetMenu(UIHandler.mainMenu, true); }, "Main Menu", "Open the Main Menu." );
			this.music = new ToggleMusic(null, 76, 10, delegate () { Systems.settings.audio.ToggleMusic(); });
			this.volume = new ToggleVolume(null, 142, 10, delegate () { Systems.settings.audio.ToggleMute(); } );
			this.exit = new UICreoIcon(null, "Quit", 208, 10, delegate () { Environment.Exit(0); }, "Quit Game", "Exit the Game." );

			//this.world = new UIIcon(null, "World", (short)(midX - 33), 10, delegate () { WebHandler.LaunchURL("https://nexus.games"); } );
			//this.patreon = new UIIcon(null, "Social/Patreon", (short)(midX + 33), 10, delegate () { WebHandler.LaunchURL("https://www.patreon.com/Nexus_Games"); } );

			// Community Links
			this.community = new UICreoIcon(null, "Community", (short)(rightX - 265), 10, delegate () { WebHandler.LaunchURL("https://creo.nexus.games"); }, "Creo Website", "Visit the Creo website.");
			this.credits = new UICreoIcon(null, "About", (short)(rightX - 198), 10, delegate () { WebHandler.LaunchURL("https://creo.nexus.games/about.html#credits"); }, "Credits", "A list of credits for content in Creo.");

			// Social Buttons
			this.twitter = new UICreoIcon(null, "Social/Twitter", (short)(rightX - 132), 10, delegate () { WebHandler.LaunchURL("https://twitter.com/scionax"); }, "Scionax's Twitter", "Follow me on Twitter to help our community grow.");
			this.discord = new UICreoIcon(null, "Social/Discord", (short)(rightX - 66), 10, delegate () { WebHandler.LaunchURL("https://discord.gg/Wx5sGcr"); }, "Discord Community", "Opens a link to join our Discord Community.");
			this.reddit = new UICreoIcon(null, "Social/Reddit", rightX, 10, delegate () { WebHandler.LaunchURL("https://www.reddit.com/r/NexusGames/"); }, "Reddit Community", "Opens a link to the Reddit Community." );
			//this.youtube = new UIButton(null, "Social/YouTube", 580, 10, delegate () { WebHandler.LaunchURL("http://youtube.com"); } );
			//this.twitch = new UIButton(null, "Social/Twitch", 280, 10, delegate () { WebHandler.LaunchURL("http://twitch.com"); } );
		}

		public void RunTick() {
			if(!UIHandler.showCornerMenu) { return; }

			// Corner Menu
			this.mainMenu.RunTick();
			this.music.RunTick();
			this.volume.RunTick();
			this.exit.RunTick();

			//this.world.RunTick();
			//this.patreon.RunTick();
			
			// Community Links
			this.community.RunTick();
			this.credits.RunTick();

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
			this.mainMenu.Draw();
			this.music.Draw();
			this.volume.Draw();
			this.exit.Draw();

			//this.world.Draw();
			//this.patreon.Draw();

			// Community Links
			this.community.Draw();
			this.credits.Draw();

			// Draw Social Buttons
			this.twitter.Draw();
			this.discord.Draw();
			this.reddit.Draw();
			//this.youtube.Draw();
			//this.twitch.Draw();
		}
	}
}
