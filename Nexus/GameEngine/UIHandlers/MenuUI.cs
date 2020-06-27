
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class MenuUI {

		private readonly LevelUI levelUI;

		// Social Buttons
		private readonly UIButton discord;
		private readonly UIButton patreon;
		private readonly UIButton reddit;
		private readonly UIButton twitch;
		private readonly UIButton twitter;
		private readonly UIButton world;
		private readonly UIButton youtube;

		// Volume
		private readonly UIButton volume;

		public MenuUI( LevelUI levelUI ) {
			this.levelUI = levelUI;

			// Create Social Buttons
			this.discord = new UIButton(null, "UI/Social/Discord", 10, 10, delegate () { WebHandler.LaunchURL("http://discord.com"); } );
			this.patreon = new UIButton(null, "UI/Social/Patreon", 80, 10, delegate () { WebHandler.LaunchURL("http://patreon.com"); } );
			this.reddit = new UIButton(null, "UI/Social/Reddit", 180, 10, delegate () { WebHandler.LaunchURL("http://reddit.com"); } );
			this.twitch = new UIButton(null, "UI/Social/Twitch", 280, 10, delegate () { WebHandler.LaunchURL("http://twitch.com"); } );
			this.twitter = new UIButton(null, "UI/Social/Twitter", 380, 10, delegate () { WebHandler.LaunchURL("http://twitter.com"); } );
			this.world = new UIButton(null, "UI/Social/World", 480, 10, delegate () { WebHandler.LaunchURL("http://example.com"); } );
			this.youtube = new UIButton(null, "UI/Social/YouTube", 580, 10, delegate () { WebHandler.LaunchURL("http://youtube.com"); } );

			// Volume
			this.volume = new UIButton(null, "UI/Volume/On", 680, 10, delegate () { WebHandler.LaunchURL("http://example.com"); } );
		}

		public void RunTick() {

			UIComponent.ComponentWithFocus = null;
			Cursor.UpdateMouseState();

			// Create Social Buttons
			this.discord.RunTick();
			this.patreon.RunTick();
			this.reddit.RunTick();
			this.twitch.RunTick();
			this.twitter.RunTick();
			this.world.RunTick();
			this.youtube.RunTick();

			// Volume
			this.volume.RunTick();
		}

		public void Draw() {

			// Draw Social Buttons
			this.discord.Draw();
			this.patreon.Draw();
			this.reddit.Draw();
			this.twitch.Draw();
			this.twitter.Draw();
			this.world.Draw();
			this.youtube.Draw();

			// Draw Volume Icons
			this.volume.Draw();
		}
	}
}
