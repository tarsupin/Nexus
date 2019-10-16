using Microsoft.Xna.Framework.Audio;

// https://www.gamefromscratch.com/post/2015/07/25/MonoGame-Tutorial-Audio.aspx

namespace Nexus.Gameplay {

	public class SoundAssets {

		// Character
		public readonly SoundEffect jump;
		public readonly SoundEffect woohoo;
		public readonly SoundEffect wound;

		// Items
		public readonly SoundEffect cannonFire;
		public readonly SoundEffect coin;
		public readonly SoundEffect collectable;
		public readonly SoundEffect collectBweep;
		public readonly SoundEffect collectDisable;
		public readonly SoundEffect collectKey;
		public readonly SoundEffect collectSubtle;
		public readonly SoundEffect disableCollectable;
		public readonly SoundEffect door;
		public readonly SoundEffect explosion;
		public readonly SoundEffect flag;
		public readonly SoundEffect food;
		public readonly SoundEffect potion;
		public readonly SoundEffect spring;
		public readonly SoundEffect unlock;

		// Clicks
		public readonly SoundEffect click1;
		public readonly SoundEffect click2;
		public readonly SoundEffect click3;

		// Impacts
		public readonly SoundEffect wood;
		public readonly SoundEffect brick;
		public readonly SoundEffect stone;

		// Thuds
		public readonly SoundEffect thudBonk;
		public readonly SoundEffect thudHit;
		public readonly SoundEffect thudTap;
		public readonly SoundEffect thudThump;
		public readonly SoundEffect thudWhomp;
		public readonly SoundEffect thudWhop;

		// Woosh
		public readonly SoundEffect wooshDeep;
		public readonly SoundEffect wooshLaser;
		public readonly SoundEffect wooshLong;
		public readonly SoundEffect wooshSubtle;

		// Enemy Sounds
		public readonly SoundEffect deathInsect;
		public readonly SoundEffect splat1;
		public readonly SoundEffect splat2;
		public readonly SoundEffect splatLong1;
		public readonly SoundEffect splatLong2;

		// Powers
		public readonly SoundEffect air;
		public readonly SoundEffect axe;
		public readonly SoundEffect bolt;
		public readonly SoundEffect dagger;
		public readonly SoundEffect flame;
		public readonly SoundEffect shield;
		public readonly SoundEffect slide;
		public readonly SoundEffect rock;
		public readonly SoundEffect sword;
		public readonly SoundEffect teleport;
		public readonly SoundEffect water;

		// Collisions
		public readonly SoundEffect brickBreak;
		public readonly SoundEffect bulletJump;
		public readonly SoundEffect crack;
		public readonly SoundEffect shellBoop;
		public readonly SoundEffect shellThud;
		public readonly SoundEffect toggle;

		// Misc
		public readonly SoundEffect timer1;
		public readonly SoundEffect timer2;
		public readonly SoundEffect pop;
		public readonly SoundEffect warp;

		public SoundAssets(GameClient game) {
			var Content = game.Content;

			// Character
			this.jump = Content.Load<SoundEffect>("Sounds/character/jump");
			this.woohoo = Content.Load<SoundEffect>("Sounds/character/woohoo");
			this.wound = Content.Load<SoundEffect>("Sounds/character/wound");

			// Items
			this.cannonFire = Content.Load<SoundEffect>("Sounds/items/cannonFire");
			this.coin = Content.Load<SoundEffect>("Sounds/items/coin");
			this.collectable = Content.Load<SoundEffect>("Sounds/items/collectable");
			this.collectBweep = Content.Load<SoundEffect>("Sounds/items/collectBweep");
			this.collectDisable = Content.Load<SoundEffect>("Sounds/items/collectDisable");
			this.collectKey = Content.Load<SoundEffect>("Sounds/items/collectKey");
			this.collectSubtle = Content.Load<SoundEffect>("Sounds/items/collectSubtle");
			this.disableCollectable = Content.Load<SoundEffect>("Sounds/items/disableCollectable");
			this.door = Content.Load<SoundEffect>("Sounds/items/door");
			this.explosion = Content.Load<SoundEffect>("Sounds/items/explosion");
			this.flag = Content.Load<SoundEffect>("Sounds/items/flag");
			this.food = Content.Load<SoundEffect>("Sounds/items/food");
			this.potion = Content.Load<SoundEffect>("Sounds/items/potion");
			this.spring = Content.Load<SoundEffect>("Sounds/items/spring");
			this.unlock = Content.Load<SoundEffect>("Sounds/items/unlock");

			// Clicks
			this.click1 = Content.Load<SoundEffect>("Sounds/clicks/click1");
			this.click2 = Content.Load<SoundEffect>("Sounds/clicks/click2");
			this.click3 = Content.Load<SoundEffect>("Sounds/clicks/click3");
			this.pop = Content.Load<SoundEffect>("Sounds/clicks/pop");

			// Impacts
			this.wood = Content.Load<SoundEffect>("Sounds/impacts/impact-wood");
			this.brick = Content.Load<SoundEffect>("Sounds/impacts/impact-brick");
			this.stone = Content.Load<SoundEffect>("Sounds/impacts/impact-stone");
		
			// Thuds
			this.thudBonk = Content.Load<SoundEffect>("Sounds/impacts/thud-bonk");
			this.thudHit = Content.Load<SoundEffect>("Sounds/impacts/thud-hit");
			this.thudTap = Content.Load<SoundEffect>("Sounds/impacts/thud-tap");
			this.thudThump = Content.Load<SoundEffect>("Sounds/impacts/thud-thump");
			this.thudWhomp = Content.Load<SoundEffect>("Sounds/impacts/thud-whomp");
			this.thudWhop = Content.Load<SoundEffect>("Sounds/impacts/thud-whop");
		
			// Woosh
			this.wooshDeep = Content.Load<SoundEffect>("Sounds/woosh/woosh-deep");
			this.wooshLaser = Content.Load<SoundEffect>("Sounds/woosh/woosh-laser");
			this.wooshLong = Content.Load<SoundEffect>("Sounds/woosh/woosh-long");
			this.wooshSubtle = Content.Load<SoundEffect>("Sounds/woosh/woosh-subtle");
		
			// Enemy Sounds
			this.deathInsect = Content.Load<SoundEffect>("Sounds/enemies/deathInsect");
			this.splat1 = Content.Load<SoundEffect>("Sounds/enemies/splat1");
			this.splat2 = Content.Load<SoundEffect>("Sounds/enemies/splat2");
			this.splatLong1 = Content.Load<SoundEffect>("Sounds/enemies/splatLong1");
			this.splatLong2 = Content.Load<SoundEffect>("Sounds/enemies/splatLong2");
		
			// Powers
			this.air = Content.Load<SoundEffect>("Sounds/powers/air");
			this.axe = Content.Load<SoundEffect>("Sounds/powers/axe");
			this.bolt = Content.Load<SoundEffect>("Sounds/powers/bolt");
			this.dagger = Content.Load<SoundEffect>("Sounds/powers/dagger");
			this.flame = Content.Load<SoundEffect>("Sounds/powers/flame");
			this.shield = Content.Load<SoundEffect>("Sounds/powers/shield");
			this.slide = Content.Load<SoundEffect>("Sounds/powers/slide");
			this.rock = Content.Load<SoundEffect>("Sounds/powers/stone");
			this.sword = Content.Load<SoundEffect>("Sounds/powers/sword");
			this.teleport = Content.Load<SoundEffect>("Sounds/powers/teleport");
			this.water = Content.Load<SoundEffect>("Sounds/powers/water");
		
			// Collisions
			this.brickBreak = Content.Load<SoundEffect>("Sounds/collisions/brickBreak");
			this.bulletJump = Content.Load<SoundEffect>("Sounds/collisions/bulletJump");
			this.crack = Content.Load<SoundEffect>("Sounds/collisions/crack");
			this.shellBoop = Content.Load<SoundEffect>("Sounds/collisions/shellBoop");
			this.shellThud = Content.Load<SoundEffect>("Sounds/collisions/shellThud");
			this.toggle = Content.Load<SoundEffect>("Sounds/collisions/toggle");
			
			// Misc
			this.timer1 = Content.Load<SoundEffect>("Sounds/timer/timer1");
			this.timer2 = Content.Load<SoundEffect>("Sounds/timer/timer2");
			this.warp = Content.Load<SoundEffect>("Sounds/warp");
		}
	}
}
