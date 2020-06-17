using Nexus.Engine;
using Nexus.Objects;
using System;
using static Nexus.ObjectComponents.Shuriken;

namespace Nexus.ObjectComponents {

	public enum PowerSubType : byte {

		None = 0,

		// Collectable Powers - Mobility
		SlowFall = 1,
		Hover = 2,
		Levitate = 3,
		Flight = 4,
		Athlete = 5,
		Leap = 6,
		Slam = 7,
		Burst = 8,
		Air = 9,
		Phase = 10,
		Teleport = 11,
		RandomPotion = 12,

		// Collectable Powers - Weapon
		RandomWeapon = 20,
		BoxingRed = 21,
		BoxingWhite = 22,
		Dagger = 23,
		DaggerGreen = 24,
		Spear = 25,
		Sword = 26,

		// Collectable Powers - Book / Projectiles
		RandomBook = 30,
		Electric = 31,
		Fire = 32,
		Frost = 33,
		Rock = 34,
		Water = 35,
		Poison = 36,

		// Collectable Powers - Thrown
		RandomThrown = 40,
		Axe = 41,
		Hammer = 42,
		ShurikenGreen = 43,
		ShurikenRed = 44,
		ShurikenBlue = 45,
		ShurikenYellow = 46,

		// Power Collectable - Bolts
		RandomBolt = 50,
		BoltBlue = 51,
		BoltGold = 52,
		BoltGreen = 53,
		BoltNecro = 54,
		Necro1 = 55,
		Necro2 = 56,

		// Collectable Powers - Stack
		Chakram = 60,
		ChakramPack = 61,
		Grenade = 62,
		GrenadePack = 63,
	}

	public class Power {

		// References
		protected readonly Character character;

		public string IconTexture { get; protected set; }	// The texture path for the Power Icon (e.g. "Power/" + this.pool)
		public string subStr { get; protected set; }

		protected byte cooldown;			// In Frames
		protected byte numberOfUses;
		protected byte delayBetweenUses;	// In Frames
		protected int lastActivation;
		protected int[] lastUseTracker;

		public Power( Character character ) {
			this.character = character;
		}

		public static void AssignPower(Character character, byte subType) {
			
			// Random Throwing Weapon
			if(subType == (byte) PowerSubType.RandomThrown) {
				Random rand = new Random((int) Systems.timer.Frame);
				subType = (byte)rand.Next(41, 43);

				// If it randomizes the Shuriken (Green), randomize between all of them.
				if(subType == (byte)PowerSubType.ShurikenGreen) {
					subType = (byte)rand.Next((byte)PowerSubType.ShurikenGreen, (byte)PowerSubType.ShurikenYellow);
				}
			}

			// Random Book (Ball Projectiles)
			else if(subType == (byte) PowerSubType.RandomBook) {
				Random rand = new Random((int)Systems.timer.Frame);
				subType = (byte)rand.Next(31, 37);
			}
			
			// Random Weapon
			else if(subType == (byte) PowerSubType.RandomWeapon) {
				Random rand = new Random((int)Systems.timer.Frame);
				subType = (byte)rand.Next(21, 26);
			}
			
			// Random Bolt
			else if(subType == (byte) PowerSubType.RandomBolt) {
				Random rand = new Random((int)Systems.timer.Frame);
				subType = (byte)rand.Next(51, 53);
			}

			// Random Potion (Mobility Powers)
			else if(subType == (byte)PowerSubType.RandomPotion) {
				Random rand = new Random((int)Systems.timer.Frame);
				subType = (byte)rand.Next(1, 11);
			}

			switch(subType) {

				// Collectable Powers - Mobility (Potion)
				case (byte)PowerSubType.SlowFall: character.mobilityPower = new SlowFallMobility(character); return;
				case (byte)PowerSubType.Hover: character.mobilityPower = new HoverMobility(character); return;
				case (byte)PowerSubType.Levitate: character.mobilityPower = new LevitateMobility(character); return;
				case (byte)PowerSubType.Flight: character.mobilityPower = new FlightMobility(character); return;
				case (byte)PowerSubType.Athlete: character.mobilityPower = new AthleteMobility(character); return;
				case (byte)PowerSubType.Leap: character.mobilityPower = new LeapMobility(character); return;
				case (byte)PowerSubType.Slam: character.mobilityPower = new SlamMobility(character); return;
				case (byte)PowerSubType.Burst: character.mobilityPower = new BurstMobility(character); return;
				case (byte)PowerSubType.Air: character.mobilityPower = new AirMobility(character); return;
				case (byte)PowerSubType.Phase: character.mobilityPower = new PhaseMobility(character); return;
				case (byte)PowerSubType.Teleport: character.mobilityPower = new TeleportMobility(character); return;

				// Collectable Powers - Weapon
				case (byte) PowerSubType.BoxingRed: character.attackPower = new BoxingGlove(character); return;
				case (byte) PowerSubType.BoxingWhite: character.attackPower = new BoxingGlove(character); return;
				case (byte) PowerSubType.Dagger: character.attackPower = new Dagger(character); return;
				case (byte) PowerSubType.DaggerGreen: character.attackPower = new Dagger(character); return;
				case (byte) PowerSubType.Spear: character.attackPower = new Spear(character); return;
				case (byte) PowerSubType.Sword: character.attackPower = new Sword(character); return;

				// Collectable Powers - Book
				case (byte) PowerSubType.Electric: character.attackPower = new ElectricBall(character); return;
				case (byte) PowerSubType.Fire: character.attackPower = new FireBall(character); return;
				case (byte) PowerSubType.Frost: character.attackPower = new FrostBall(character); return;
				case (byte) PowerSubType.Rock: character.attackPower = new RockBall(character); return;
				case (byte) PowerSubType.Water: character.attackPower = new WaterBall(character); return;
				case (byte) PowerSubType.Poison: character.attackPower = new PoisonBall(character); return;

				// Collectable Powers - Thrown
				case (byte) PowerSubType.Axe: character.attackPower = new Axe(character, WeaponAxeSubType.Axe); return;
				case (byte) PowerSubType.Hammer: character.attackPower = new Hammer(character, 0); return;
				case (byte) PowerSubType.ShurikenGreen: character.attackPower = new Shuriken(character, (byte)ShurikenSubType.Green); return;
				case (byte) PowerSubType.ShurikenRed: character.attackPower = new Shuriken(character, (byte)ShurikenSubType.Red); return;
				case (byte) PowerSubType.ShurikenBlue: character.attackPower = new Shuriken(character, (byte)ShurikenSubType.Blue); return;
				case (byte) PowerSubType.ShurikenYellow: character.attackPower = new Shuriken(character, (byte)ShurikenSubType.Yellow); return;

				// Power Collectable - Bolts
				case (byte) PowerSubType.BoltBlue: character.attackPower = new BoltBlue(character); return;
				case (byte) PowerSubType.BoltGold: character.attackPower = new BoltGold(character); return;
				case (byte) PowerSubType.BoltGreen: character.attackPower = new BoltGreen(character); return;
				case (byte) PowerSubType.BoltNecro: character.attackPower = new BoltNecro(character); return;
				//case (byte)  PowerSubType.Necro1: this.Something(); return;
				//case (byte)  PowerSubType.Necro2: this.Something(); return;
			}

			// Grenade Packs
			// TODO: ADD PACK COUNTS
			if(subType == (byte)PowerSubType.Grenade || subType == (byte)PowerSubType.GrenadePack) {
				byte count = subType == (byte)PowerSubType.Grenade ? (byte)1 : (byte)3;
				if(character.attackPower is Grenade == false) {
					character.attackPower = new Grenade(character);
				}
			}
		}

		public virtual bool Activate() { return false; }
		public virtual void UpdateAbilities() {}

		public void SetActivationSettings( byte cooldown, byte numberOfUses = 2, byte delayBetweenUses = 15 ) {
			this.cooldown = cooldown;
			this.numberOfUses = numberOfUses;
			this.delayBetweenUses = delayBetweenUses;
			this.lastActivation = 0;
			this.lastUseTracker = new int[this.numberOfUses];
		}

		public bool CanActivate() {

			// Cannot activate this power while holding an item:
			if(this.character.heldItem.IsHeld) { return false; }

			TimerGlobal timer = Systems.timer;

			// Delay if last activation was too recent.
			if(timer.Frame < this.lastActivation) { return false; }

			// If this character is a fast caster, run special activation behaviors:
			byte fastCastMult = this.character.stats.CanFastCast ? (byte) 2 : (byte) 1;

			// Loop through available power uses:
			for( byte i = 0; i < this.lastUseTracker.Length; i++ ) {

				if(timer.Frame > this.lastUseTracker[i]) {

					// Consume this activation for now:
					this.lastUseTracker[i] = timer.Frame + this.cooldown / fastCastMult;

					// Set most recent activation:
					this.lastActivation = timer.Frame + this.delayBetweenUses / fastCastMult;

					return true;
				}
			}

			return false;
		}
	}
}
